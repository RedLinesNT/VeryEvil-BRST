using UnityEngine;

namespace VEvil.Core.FE {

    /// <summary>
    /// <see cref="AFrontEndModule"/> is designed to be placed on <see cref="GameObject"/>s next to a <see cref="UnityEngine.Canvas"/> component.
    /// </summary>
    public abstract class AFrontEndModule : MonoBehaviour {

        #region Properties

        /// <inheritdoc cref="MonoBehaviour.GetInstanceID"/>
        public int ModuleInstanceID { get; private set; }
        /// <summary>
        /// The <see cref="UnityEngine.Canvas"/> component of this <see cref="AFrontEndModule"/>.
        /// </summary>
        public Canvas Canvas { get; private set; } = null;
        /// <inheritdoc cref="FrontEndModuleInputSettings"/>
        public FrontEndModuleInputSettings InputSettings { get; private set; }

        #endregion
        
        #region MonoBehaviour's Methods

        private void Awake() => OnAwakeModule();
        private void OnDestroy() {
            FrontEnd.UnregisterModule(this);
            
            OnDestroyModule();
        }
        private void Start() {
            Canvas = GetComponent<Canvas>(); //Get the Canvas component of this Front End Module
            if (Canvas == null) { //Check if no Canvas was found
                Logger.TraceError($"Front End Module ({name})", $"Unable to find any Canvas component! Please be sure this Module component is next to a Canvas component.");
                DestroyImmediate(this.gameObject); //Immediately destroy this object
                return;
            }

            InputSettings = DefineInputSettings();
            
            FrontEnd.RegisterModule(this);
            
            OnStartModule();
        }
        private void OnEnable() => OnEnableModule();
        private void OnDisable() => OnDisableModule();
        private void Update() => OnUpdateModule();
        private void FixedUpdate() => OnFixedUpdateModule();
        private void LateUpdate() => OnLateUpdateModule();

        #endregion

        #region AFrontEndModule's Internal Virtual Methods
        
        /// <summary>
        /// Define the <see cref="FrontEndModuleInputSettings"/> this <see cref="AFrontEndModule"/> will use.
        /// </summary>
        private protected abstract FrontEndModuleInputSettings DefineInputSettings();
        /// <summary>
        /// <see cref="OnAwakeModule"/> is called when an enabled script instance is being loaded.
        /// </summary>
        private protected virtual void OnAwakeModule(){}
        /// <summary>
        /// <see cref="OnStartModule"/> is called on the frame when a script is enabled just before any of the Update methods are called for the first frame.
        /// </summary>
        private protected virtual void OnStartModule(){}
        /// <summary>
        /// <see cref="OnEnableModule"/> is called when the object becomes enabled and active.
        /// </summary>
        private protected virtual void OnEnableModule(){}
        /// <summary>
        /// <see cref="OnDisableModule"/> is called when the behaviour becomes disabled.
        /// </summary>
        private protected virtual void OnDisableModule(){}
        /// <summary>
        /// Destroying the attached Behaviour will result in the game or Scene receiving OnDestroy.
        /// </summary>
        private protected virtual void OnDestroyModule() {}
        /// <summary>
        /// <see cref="OnUpdateModule"/> is called every frames.
        /// </summary>
        private protected virtual void OnUpdateModule(){}
        /// <summary>
        /// Frame-rate independent MonoBehaviour.FixedUpdate message for physics calculations.
        /// </summary>
        private protected virtual void OnFixedUpdateModule(){}
        /// <summary>
        /// <see cref="OnLateUpdateModule"/> is called every frame, if the Behaviour is enabled.
        /// </summary>
        private protected virtual void OnLateUpdateModule(){}

        #endregion
        
        #region AFrontModule's External Virtual Methods

        /// <summary>
        /// <see cref="OnModuleShowed"/> is called when this <see cref="AFrontEndModule"/> is showed.
        /// </summary>
        public virtual void OnModuleShowed(){}
        /// <summary>
        /// <see cref="OnModuleShowed"/> is called when this <see cref="AFrontEndModule"/> is no longer showed.
        /// </summary>
        public virtual void OnModuleHidden(){}

        #endregion
        
    }

}