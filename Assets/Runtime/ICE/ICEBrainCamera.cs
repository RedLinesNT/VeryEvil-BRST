using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using VEvil.Core;
using Logger = VEvil.Core.Logger;

namespace VEvil.ICE {

    /// <summary>
    /// <see cref="ICEBrainCamera"/> is a single-instance main camera of the entire game used by the <see cref="ICESystem"/>.
    /// </summary>
    public class ICEBrainCamera : MonoBehaviour {

        #region Attributes

        /// <summary>
        /// The instance of this <see cref="ICEBrainCamera"/>.
        /// </summary>
        private static ICEBrainCamera instance = null;

        #endregion

        #region Properties

        /// <inheritdoc cref="instance"/>
        public static ICEBrainCamera Instance {
            get {
                if (instance != null) return instance; //If there's an instance running
                
                //Else, if there's no instance running
                GameObject _temp = new GameObject("ICE Brain Camera"); //Create a new Object
                _temp.AddComponent<ICEBrainCamera>(); //Add the new component

                return instance; //Return the new instance
            }
        }

        /// <summary>
        /// The <see cref="UnityEngine.Camera"/> component used by this <see cref="ICEBrainCamera"/>.
        /// </summary>
        [field: SerializeField, Header("ICE Brain Camera - References"), LabelOverride("Camera component"), Tooltip("The Unity Engine camera component.")] public Camera Camera { get; private set; } = null;
        /// <summary>
        /// The <see cref="CinemachineBrain"/> component used by this <see cref="ICEBrainCamera"/>.
        /// </summary>
        [field: SerializeField, LabelOverride("Cinemachine Brain component"), Tooltip("The Cinemachine Brain component.")] public CinemachineBrain Brain { get; private set; } = null;

        #endregion

        #region MonoBehaviour's Methods

        private void Awake() {
            if(instance != null) { //If there's already an instance running
                Logger.TraceWarning("ICE Brain Camera", $"Unable to create a new {nameof(ICEBrainCamera)} component. There's already an instance running!");
                DestroyImmediate(this.gameObject); //Destroy this object
                return; 
            }
            
            instance = this; //Set the new instance
            
            DontDestroyOnLoad(this);

            if(Camera == null) {
                Camera = gameObject.AddComponent<Camera>(); //If there's no Camera component assigned
                UniversalAdditionalCameraData _URPAD = gameObject.AddComponent<UniversalAdditionalCameraData>();
                _URPAD.renderPostProcessing = true;
            }

            if(Brain == null) Brain = gameObject.AddComponent<CinemachineBrain>(); //If there's no CinemachineBrain component assigned
            
            Brain.m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.Cut, 0f); //Set the default blend

            Logger.Trace("ICE Brain Camera", $"New instance created.");
        }

        #endregion
        
    }

}