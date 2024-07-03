using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using VEvil.GameLogic.Health;
using VEvil.GameLogic.Teams;
using Logger = VEvil.Core.Logger;

namespace VEvil.GameLogic.Interactable.TNT {

    /// <summary>
    /// The main logic for the TNT Interactable entity available throughout the levels.
    /// </summary>
    /// <remarks>
    /// This component require a <see cref="TeamIndicator"/> component in order to work.
    /// </remarks>
    public sealed class TNTInteractable : APointerClickableEntity {

        #region Attributes
        
        [Header("TNT Interactable - References")]
        [SerializeField, Tooltip("The GameObject supposed to contain and play the TNT explosion's VFX (Optional).")] private GameObject explosionVFX = null;

        [Header("TNT Interactable - Events")] 
        [SerializeField, Tooltip("Invoked when the timer for this TNT to explode is about to begin.\nArg 01: The time before the explosion.\nWon't be invoked if no delay has been set!")] private UnityEvent<float> onExplosionTimerStartedEditor = null;
        [SerializeField, Tooltip("Invoked when this TNT has exploded.\nArg 01: The number of entities that took damages.")] private UnityEvent<int> onExplosionEditor = null;
        
        #endregion

        #region Runtime Values

        /// <summary>
        /// The <see cref="TeamIndicator"/> component indicating the target <see cref="ETeamType"/> fo this <see cref="TNTInteractable"/>.
        /// </summary>
        private TeamIndicator teamIndicator = null;

        #endregion
        
        #region Events

        /// <summary>
        /// Invoked when the timer for this <see cref="TNTInteractable"/> to explode is about to begin.
        /// </summary>
        /// <remarks>
        /// Won't be invoked if no delay in the <see cref="Settings"/> has been set!
        /// </remarks>
        /// <param name="Arg 01">The time before the explosion</param>
        private Action<float> onExplosionTimerStarted = null;
        /// <summary>
        /// Invoked when this <see cref="TNTInteractable"/> has exploded.
        /// </summary>
        /// <param name="Arg 01">The number of <see cref="AHealthModule"/>s that took damages.</param>
        private Action<int> onExplosion = null;

        #endregion
        
        #region Properties

        /// <inheritdoc cref="TNTInteractableSettings"/>
        [field: SerializeField, Header("TNT Interactable - Settings"), Tooltip("The settings this TNT Interactable will use.")] public TNTInteractableSettings Settings { get; private set; } = new TNTInteractableSettings(0);

        /// <inheritdoc cref="onExplosionTimerStarted"/>
        public event Action<float> OnExplosionTimerStarted { add { onExplosionTimerStarted += value; } remove { onExplosionTimerStarted -= value; } }
        /// <inheritdoc cref="onExplosion"/>
        public event Action<int> OnExplosion { add { onExplosion += value; } remove { onExplosion -= value; } }

        #endregion

        #region MonoBehaviour's Methods

        private void Awake() {
            //Auto-assign references
            teamIndicator = GetComponent<TeamIndicator>();
            
            //Check references
            if(teamIndicator == null) { Logger.TraceError($"TNT Interactable ({name})", $"Unable to find the TeamIndicator component! Read more about this in the source documentation. This object will be destroyed!"); Destroy(this.gameObject); }
        }

        #endregion
        
        #region APointerClickableEntity's Internal Virtual Methods

        protected override void OnPointerClick() {
            if(Settings.ExplosionDelay > 0) { //If a delay is set
                //Invoked these events
                onExplosionTimerStarted?.Invoke(Settings.ExplosionDelay);
                onExplosionTimerStartedEditor?.Invoke(Settings.ExplosionDelay);
                
                Invoke(nameof(AttackModules), Settings.ExplosionDelay);
            } else {
                AttackModules();
            }
        }

        #endregion

        #region TNTInteractable's Internal Methods

        /// <summary>
        /// Find <see cref="AHealthModule"/>s in the <see cref="TNTInteractableSettings.ExplosionRange"/> while respecting the <see cref="TNTInteractableSettings.FriendlyFire"/> flag and return the results as a <see cref="List{AHealthModule}"/>.
        /// </summary>
        private List<AHealthModule> FindAttackableEntities() {
            List<AHealthModule> _attackableModules = new List<AHealthModule>();
            Collider[] _targets = Physics.OverlapSphere(this.transform.position, Settings.ExplosionRange).Where(_hit => (_hit.GetComponentInParent<AHealthModule>() != null)).ToArray();

            for (int i=0; i<_targets.Length; i++) {
                AHealthModule _currentModule = _targets[i].GetComponentInParent<AHealthModule>(); //Keep a reference to this component
                
                if(Settings.FriendlyFire) { //If we can damage anyone, 
                    _attackableModules.Add(_currentModule); //Add it directly into the list
                } else if(_currentModule.Team != teamIndicator.Team) { //Else, friendly-fire isn't allowed, only take modules not in the same team as this entity
                    _attackableModules.Add(_currentModule); //And then add it, simple!
                }
            }

            return _attackableModules; //Return our magnificent list :)
        }

        /// <summary>
        /// Attack the <see cref="AHealthModule"/> detected and sorted in the <see cref="FindAttackableEntities"/> method.
        /// </summary>
        private void AttackModules() {
            List<AHealthModule> _modulesToAttack = FindAttackableEntities();

            for (int i=0; i<_modulesToAttack.Count; i++) { //Calculate the distance to apply correctly the damages on every modules
                float _distance = Vector3.Distance(transform.position, _modulesToAttack[i].gameObject.transform.position); //Calculate the distance
                float _percent = (_distance / (Settings.ExplosionRange)); //Divide the distance with the explosion radius

                int _damage = (int)((1.0f - _percent) * (Settings.DamageFactor.y - Settings.DamageFactor.x) + Settings.DamageFactor.x); //We get the damage to inflict on this module!
                _modulesToAttack[i].Damage(_damage);
            }
            
            //Invoke these events
            onExplosion?.Invoke(_modulesToAttack.Count);
            onExplosionEditor?.Invoke(_modulesToAttack.Count);

            if(explosionVFX != null) { //If we have a VFX do instantiate
                Instantiate(explosionVFX, transform.position, transform.rotation);
            }
            
            Destroy(gameObject);
        }

        #endregion
        
        #region TNTInteractable's Internal Editor Methods

        [Conditional("UNITY_EDITOR")] private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue; {
                Gizmos.DrawWireSphere(transform.position, Settings.ExplosionRange);
            }
        }

        #endregion
        
    }

}