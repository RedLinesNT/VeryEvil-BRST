using System;
using UnityEngine;
using UnityEngine.AI;
using Logger = VEvil.Core.Logger;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VEvil.AI {

    /// <summary>
    /// <see cref="AAIAgentEntity"/> is a "<i>wrapper</i>" for the <see cref="NavMeshAgent"/> and provide methods and events related.
    /// </summary>
    public abstract class AAIAgentEntity : MonoBehaviour {

        #region Attributes

        /// <summary>
        /// A temporary target that can be set in runtime, changing this value won't affect the <see cref="mainTarget"/>.
        /// </summary>
        /// <remarks>
        /// If no temporary target is assigned, the <see cref="mainTarget"/> will be used instead!
        /// </remarks>
        private GameObject temporaryTarget = null;
        /// <summary>
        /// The <see cref="GameObject"/> of the main target this <see cref="AAIAgentEntity"/> should follow.
        /// </summary>
        private GameObject mainTarget = null;
        /// <inheritdoc cref="AIAgentSettings"/>
        private AIAgentSettings settings = default;
        /// <summary>
        /// The time before the next internal update cycle of this <see cref="AAIAgentEntity"/> instance.
        /// </summary>
        private float nextUpdateIn = 0;
        private bool hasReachedDestination = false;

        #endregion

        #region Events

        /// <summary>
        /// Invoked when the <see cref="Target"/> of this <see cref="AAIAgentEntity"/> has been modified.
        /// </summary>
        /// <param name="Arg 01">The new <see cref="Target"/>.</param>
        /// <remarks>
        /// "<i>Arg 01</i>" might be null if the <see cref="Target"/> has been set like so.
        /// </remarks>
        private Action<GameObject> onTargetModified = null;
        /// <summary>
        /// Invoked when the destination to the <see cref="Target"/> has been completed.
        /// </summary>
        private Action onTargetReached = null;

        #endregion

        #region Properties
        
        /// <summary>
        /// The <see cref="NavMeshAgent"/> component used by this <see cref="AAIAgentEntity"/>.
        /// </summary>
        public NavMeshAgent BaseAgent { get; private set; } = null;
        /// <summary>
        /// The target this <see cref="AAIAgentEntity"/> is currently following.
        /// </summary>
        [field: SerializeField] public GameObject Target { get; private set; } = null;
        /// <summary>
        /// Should this <see cref="AAIAgentEntity"/> be able to move to its <see cref="Target"/>.
        /// </summary>
        /// <remarks>
        /// If <see cref="Target"/> is null, this <see cref="AAIAgentEntity"/> won't move. :)
        /// </remarks>
        public bool AllowMovement { get { return BaseAgent.enabled; } private protected set { BaseAgent.enabled = value; } }
        /// <inheritdoc cref="settings"/>
        public AIAgentSettings Settings {
            get { return settings; }
            private protected set {
                settings = value;

                if (BaseAgent == null) return; //Don't execute anything if there's no agent
                
                //Parse the AIAgentPrimitiveSettings into our NavMeshAgent
                BaseAgent.speed = settings.Speed;
                BaseAgent.angularSpeed = settings.AngularSpeed;
                BaseAgent.acceleration = settings.Acceleration;
                BaseAgent.stoppingDistance = settings.StoppingDistance;
                BaseAgent.autoBraking = settings.AutoBraking;
                BaseAgent.radius = settings.Radius;
                BaseAgent.height = settings.Height;
                BaseAgent.obstacleAvoidanceType = settings.Quality;
                BaseAgent.baseOffset = settings.BaseOffset;
            }
        }
        /// <summary>
        /// The time in seconds between each updates of this <see cref="AAIAgentEntity"/>.
        /// </summary>
        /// <remarks>
        /// This value won't impact to call-rate of the <see cref="OnUpdateAI"/> virtual method.<br/>
        /// You can tweak this value to make this <see cref="AAIAgentEntity"/> more/less reactive about targets modifications.
        /// </remarks>
        public float TimeBetweenUpdates { get; set; } = 2f;
        
        /// <inheritdoc cref="onTargetModified"/>
        public event Action<GameObject> OnTargetModified { add { onTargetModified += value; } remove { onTargetModified -= value; } }
        /// <inheritdoc cref="onTargetReached"/>
        public event Action OnTargetReached { add { onTargetReached += value; } remove { onTargetReached -= value; } }
        
        #endregion
        
        #region MonoBehaviour's Methods

        private void Awake() {
            //Assign required references
            BaseAgent = GetComponent<NavMeshAgent>();
            nextUpdateIn = TimeBetweenUpdates;
            
            //Check required references
            if (BaseAgent == null) { BaseAgent = gameObject.AddComponent<NavMeshAgent>(); }
            
            OnAwakeAI();
        }
        private void Start() => OnStartAI();
        private void OnEnable() => OnEnableAI();
        private void OnDisable() => OnDisableAI();
        private void OnDestroy() => OnDestroyAI();
        private void Update() {
            nextUpdateIn -= Time.deltaTime;
            
            if (nextUpdateIn <= 0f) { //Yay it's time to update!
                if (mainTarget != null && temporaryTarget == null && Target == null) { //If the current target is the temporary and became null
                    Target = mainTarget;
                    hasReachedDestination = false;
                }

                if (Target != null) {
                    if (Vector3.Distance(Target.transform.position, transform.position) <= 0.2f && !hasReachedDestination) {
                        hasReachedDestination = true;
                        
                        onTargetReached?.Invoke();
                        OnAITargetReached();
                    }
                    
                    BaseAgent.SetDestination(Target.transform.position); //If we have a target
                }

                nextUpdateIn = TimeBetweenUpdates; //Set back this value to wait again
            }
            
            OnUpdateAI();
        }
        private void FixedUpdate() => OnFixedUpdateAI();
        private void LateUpdate() => OnLateUpdateAI();

        #endregion
        
        #region AAIAgentEntity's Internal Virtual Methods

        /// <summary>
        /// <see cref="OnAITargetModified"/> is called when the <see cref="Target"/> of this <see cref="AAIAgentEntity"/> has been modified.
        /// </summary>
        /// <param name="_newTarget">The new <see cref="Target"/>.</param>
        protected virtual void OnAITargetModified(GameObject _newTarget){}
        /// <summary>
        /// <see cref="OnAITargetReached"/> is called when the <see cref="Target"/> of this <see cref="AAIAgentEntity"/> has been reached.
        /// </summary>
        protected virtual void OnAITargetReached() {}
        
        /// <summary>
        /// <see cref="OnAwakeAI"/> is called when an enabled script instance is being loaded.
        /// </summary>
        protected virtual void OnAwakeAI(){}
        /// <summary>
        /// <see cref="OnStartAI"/> is called on the frame when a script is enabled just before any of the Update methods are called for the first frame.
        /// </summary>
        protected virtual void OnStartAI(){}
        /// <summary>
        /// <see cref="OnEnableAI"/> is called when the object becomes enabled and active.
        /// </summary>
        protected virtual void OnEnableAI(){}
        /// <summary>
        /// <see cref="OnDisableAI"/> is called when the behaviour becomes disabled.
        /// </summary>
        protected virtual void OnDisableAI(){}
        /// <summary>
        /// Destroying the attached Behaviour will result in the game or Scene receiving OnDestroy.
        /// </summary>
        protected virtual void OnDestroyAI() {}
        /// <summary>
        /// <see cref="OnUpdateAI"/> is called every frames.
        /// </summary>
        protected virtual void OnUpdateAI(){}
        /// <summary>
        /// Frame-rate independent MonoBehaviour.FixedUpdate message for physics calculations.
        /// </summary>
        protected virtual void OnFixedUpdateAI(){}
        /// <summary>
        /// <see cref="OnLateUpdateAI"/> is called every frame, if the Behaviour is enabled.
        /// </summary>
        protected virtual void OnLateUpdateAI(){}
        
        #endregion

        #region AAIAgentEntity's External Methods

        /// <summary>
        /// Set a temporary target this <see cref="AAIAgentEntity"/> should follow.
        /// </summary>
        public void SetTemporaryTarget(GameObject _tempTarget) {
            if (mainTarget == _tempTarget) { //If we got the main target here
                return; //Don't execute anything
            }
            
            if (Target == null && mainTarget == null) { //If there's no current target AND no main target
                Logger.Trace($"AI Agent Entity ({name})", "Trying to set a temporary target while having no main one will result in the temporary target becoming the main one!");
                mainTarget = _tempTarget;
                Target = mainTarget;
                return;
            }
            
            temporaryTarget = _tempTarget;
            Target = temporaryTarget;
            
            if (Target != null) BaseAgent.SetDestination(Target.transform.position); //Force re-update the destination
            
            //Invoke these events
            onTargetModified?.Invoke(Target);
            OnAITargetModified(Target);

            hasReachedDestination = false;
        }
        
        /// <summary>
        /// Set the main target this <see cref="AAIAgentEntity"/> should follow.
        /// </summary>
        public void SetMainTarget(GameObject _target) {
            mainTarget = _target;

            if (Target == temporaryTarget && Target != null) { //If the temporary target is currently in use
                Logger.Trace($"AI Agent Entity ({name})", "A main target has been set while using the temporary one, you'll need to revert the temporary target to use the main one!");
                return;
            }
            
            Target = mainTarget;
            
            if (Target != null) BaseAgent.SetDestination(Target.transform.position); //Force re-update the destination
            
            //Invoke these events
            onTargetModified?.Invoke(Target);
            OnAITargetModified(Target);
            
            hasReachedDestination = false;
        }

        /// <summary>
        /// Revert and remove the temporary target.
        /// </summary>
        public void RevertTemporaryTarget() {
            Target = mainTarget;
            
            if (Target != null) BaseAgent.SetDestination(Target.transform.position); //Force re-update the destination
            
            //Invoke these events
            onTargetModified?.Invoke(Target);
            OnAITargetModified(Target);
            
            hasReachedDestination = false;
        }

        #endregion
        
        #region AAIAgentEntity's Editor Method

#if UNITY_EDITOR
        private void OnDrawGizmosSelected() {
            if (BaseAgent != null && Target != null) {
                Gizmos.color = Color.magenta; {
                    Vector3[] _pathCorners = BaseAgent.path.corners;
                    if (Target == mainTarget) {
                        Handles.Label(transform.position, $"FOLLOW MAIN TARGET ({Target.name}) - DIST {BaseAgent.remainingDistance}");
                    } else if (Target == temporaryTarget) {
                        Handles.Label(transform.position, $"FOLLOW TEMP TARGET ({Target.name}) - DIST {BaseAgent.remainingDistance}");
                    }
                
                    for (int i = 0; i < _pathCorners.Length - 1; i++) {
                        Gizmos.DrawLine(_pathCorners[i], _pathCorners[i + 1]);
                    }
                }
            }
        }
#endif

        #endregion
        
    }

}