using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using VEvil.GameLogic.Health;
using VEvil.GameLogic.Teams;
using Logger = VEvil.Core.Logger;

namespace VEvil.GameLogic.Attack {

    /// <summary>
    /// <see cref="AAttackModule"/> bring the ability to attack an entity who have a <see cref="AHealthModule"/> component.
    /// </summary>
    /// <remarks>
    /// If there's a <see cref="TeamIndicator"/> placed right next to a <see cref="AAttackModule"/> component, the <see cref="Team"/> field will be automatically bind and follow its events.
    /// </remarks>
    public abstract class AAttackModule : MonoBehaviour {

        #region Attributes

        /// <summary>
        /// The maximum failed distance errors this <see cref="AAttackModule"/> can have.
        /// </summary>
        private int maxFailedDistance = 5;
        /// <summary>
        /// The actual number of distance checks error encountered on the current <see cref="Target"/>.
        /// </summary>
        private int currentFailedDistance = 0;

        /// <summary>
        /// Is the attack cooldown currently active ?
        /// </summary>
        private bool isInAttackCooldown = false;
        /// <summary>
        /// Is the detection cooldown currently active ?
        /// </summary>
        private bool isInDetectionCooldown = false;

        #endregion
        
        #region Properties

        /// <summary>
        /// The <see cref="AttackSettings"/> used by this <see cref="AAttackModule"/>.
        /// </summary>
        [field: SerializeField, Tooltip("The AttackSettings used by this AttackModule.\nThis field can be untouched if you plan to modify it at this entity's initialization.")] public AttackSettings Settings { get; set; } = default;

        /// <summary>
        /// Set it to false if you want this <see cref="AAttackModule"/> to stop from looking at new targets.
        /// </summary>
        public bool LookForTarget { get; private protected set; } = true;
        /// <summary>
        /// The <see cref="AHealthModule"/> component of the target.
        /// </summary>
        public AHealthModule Target { get; private set; } = null;
        /// <summary>
        /// The <see cref="ETeamType"/> of the entity owning this <see cref="AAttackModule"/>.
        /// </summary>
        public ETeamType Team { get; private set; } = ETeamType.UNKNOWN;

        #endregion
        
        #region MonoBehaviour's Methods

        private void Awake() {
            TeamIndicator _teamIndicator = GetComponent<TeamIndicator>(); //Try to get the TeamIndicator component
            if (_teamIndicator != null) { //If a component exists
                Team = _teamIndicator.Team; //Set the team
                _teamIndicator.OnTeamChanged += OnTeamIndicatorTeamChanged; //Bind this event
            } else {
                Logger.TraceWarning($"Attack Module ({name})", "Unable to find the TeamIndicator component of this AttackModule! You can ignore this warning if this behaviour is intended.");
            }

            OnAwakeModule(); //Call this method on every sub-instances
        }
        
        private void Start() {
            if(Settings.Equals(default(AttackSettings))) { Logger.TraceError($"Attack Module ({name})", $"The AttackSettings data given is invalid! You must give a valid one before 'Start'-Mono execution! This component will be destroyed."); Destroy(this); }
            
            OnStartModule(); //Call this method on every sub-instances
        }

        private void Update() {
            if(Target != null) { //If we have a target
                if(!IsTargetInSeeRange()) { //If the target isn't in the see range anymore
                    if(currentFailedDistance >= maxFailedDistance) { //If we reached the maximum attempts
                        OnTargetEscaped();
                        Target = null;
                        currentFailedDistance = 0;
                    }

                    currentFailedDistance++;
                    return;
                }

                if(IsTargetInAttackRange()) { //If the target is in the attack range
                    AttackTarget();
                }
            } else { //If there's no target
                CheckForTarget();
            }

            OnUpdateModule(); //Call this method on every sub-instances
        }

        private void OnDestroy() => OnDestroyModule();
        private void OnEnable() => OnEnableModule();
        private void OnDisable() => OnDisableModule();
        private void FixedUpdate() => OnFixedUpdateModule();
        private void LateUpdate() => OnLateUpdateModule();

        #endregion
        
        #region AAttackModule's Internal Virtual Methods

        /// <summary>
        /// <see cref="OnAwakeModule"/> is called when an enabled script instance is being loaded.
        /// </summary>
        protected virtual void OnAwakeModule(){}
        /// <summary>
        /// <see cref="OnStartModule"/> is called on the frame when a script is enabled just before any of the Update methods are called for the first frame.
        /// </summary>
        protected virtual void OnStartModule(){}
        /// <summary>
        /// <see cref="OnEnableModule"/> is called when the object becomes enabled and active.
        /// </summary>
        protected virtual void OnEnableModule(){}
        /// <summary>
        /// <see cref="OnDisableModule"/> is called when the behaviour becomes disabled.
        /// </summary>
        protected virtual void OnDisableModule(){}
        /// <summary>
        /// Destroying the attached Behaviour will result in the game or Scene receiving OnDestroy.
        /// </summary>
        protected virtual void OnDestroyModule() {}
        /// <summary>
        /// <see cref="OnUpdateModule"/> is called every frames.
        /// </summary>
        protected virtual void OnUpdateModule(){}
        /// <summary>
        /// Frame-rate independent MonoBehaviour.FixedUpdate message for physics calculations.
        /// </summary>
        protected virtual void OnFixedUpdateModule(){}
        /// <summary>
        /// <see cref="OnLateUpdateModule"/> is called every frame, if the Behaviour is enabled.
        /// </summary>
        protected virtual void OnLateUpdateModule(){}

        /// <summary>
        /// Called when the target this <see cref="AAttackModule"/> got escaped for too long.
        /// </summary>
        protected virtual void OnTargetEscaped(){}
        /// <summary>
        /// Called when this <see cref="AAttackModule"/> attacks the target.
        /// </summary>
        protected virtual void OnAttack(){}
        /// <summary>
        /// Called when this <see cref="AAttackModule"/> has killed the target.
        /// </summary>
        protected virtual void OnTargetDead(){}
        /// <summary>
        /// Called when this <see cref="AAttackModule"/> found a target.
        /// </summary>
        protected virtual void OnTargetFound(){}

        #endregion

        #region AAttackModule's Internal Methods

        /// <summary>
        /// Returns true if the <see cref="Target"/> is still inside the <see cref="Settings"/>'s attack range.
        /// </summary>
        private bool IsTargetInAttackRange() {
            if (Target == null) return false;

            return Vector3.Distance(this.transform.position, Target.transform.position) <= (Settings.AttackRange);
        }

        /// <summary>
        /// Returns true if the <see cref="Target"/> is still inside the <see cref="Settings"/>'s see range.
        /// </summary>
        private bool IsTargetInSeeRange() {
            if (Target == null) return false;

            return Vector3.Distance(this.transform.position, Target.transform.position) <= (Settings.SeeRange);
        }

        /// <summary>
        /// Tries to attack the <see cref="Target"/> previouly set.
        /// </summary>
        private void AttackTarget() {
            if(isInAttackCooldown || Target == null) return;

            OnAttack(); //Invoke this
            Target.Damage(Settings.AttackDamage); //Damage the target

            if(Target == null || Target?.Health <= 0) { //If the target has no more health
                OnTargetDead();
            }

            isInAttackCooldown = true;
            
            Task.Run(async () => { //Wait a little bit here to reset the cooldown state
                await Task.Delay(Settings.AttackCooldownMS).ConfigureAwait(false); 
                isInAttackCooldown = false;
            });
        }

        /// <summary>
        /// Project a <see cref="Physics.OverlapSphere(Vector3, float)"/> to check for potential enemies with a <see cref="AHealthModule"/> component.
        /// </summary>
        private void CheckForTarget() {
            if(isInDetectionCooldown || !LookForTarget) return;

            isInDetectionCooldown = true;
            Collider[] _targets = Physics.OverlapSphere(this.transform.position, Settings.SeeRange).Where(_hit => (_hit.GetComponentInParent<AHealthModule>() != null) && _hit.GetComponentInParent<AHealthModule>().Team != Team).ToArray();

            AHealthModule _lowestTarget = null;
            
            //TODO: Optimize this process!
            for (int i=0; i<_targets.Length; i++) { //Find the enemy with the lowest health
                if(_lowestTarget == null) {
                    _lowestTarget = _targets[i].GetComponentInParent<AHealthModule>();
                } else if(_lowestTarget?.Health >= _targets[i].GetComponentInParent<AHealthModule>().Health) { //If this entity has a lower health
                    _lowestTarget = _targets[i].GetComponentInParent<AHealthModule>();
                }
            }

            if(_lowestTarget != null) { //If a target was found
                Target = _lowestTarget;
                
                OnTargetFound(); //Invoke this
            }
            
            Task.Run(async () => { //Wait a little bit here to reset the cooldown state
                await Task.Delay(Settings.DetectionCooldownMS).ConfigureAwait(false); 
                isInDetectionCooldown = false;
            }); 
        }

        /// <inheritdoc cref="TeamIndicator.OnTeamChanged"/>
        private void OnTeamIndicatorTeamChanged(ETeamType _newTeam) {
            Team = _newTeam;
        }
        
        #endregion
        
        #region AAttackModule's Internal Editor Methods

        [Conditional("UNITY_EDITOR")] private void OnDrawGizmosSelected() {
            Gizmos.color = Color.cyan; {
                Gizmos.DrawWireSphere(transform.position, Settings.SeeRange);
            }

            if (Target != null) {
                if (IsTargetInAttackRange()) {
                    Gizmos.color = Color.green; {
                        Gizmos.DrawWireSphere(transform.position, Settings.AttackRange);
                    }
                } else {
                    Gizmos.color = Color.red; {
                        Gizmos.DrawWireSphere(transform.position, Settings.AttackRange);
                    }
                }
            }
        }

        #endregion
        
    }

}