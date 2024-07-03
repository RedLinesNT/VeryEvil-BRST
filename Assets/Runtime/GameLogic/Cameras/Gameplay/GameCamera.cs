using System;
using UnityEngine;
using UnityEngine.EventSystems;
using VEvil.ICE;
using VEvil.Inputs.Generated;
using Logger = VEvil.Core.Logger;

namespace VEvil.GameLogic.Cameras.Gameplay {

    /// <summary>
    /// <see cref="GameCamera"/> is supposed to be the main camera-controller used in the "gameplay" phase of VE.<br/>
    /// This camera is managed by the <see cref="ICESystem"/> and is heavily inspired from the camera used in <i>League Of Legends</i>.
    /// </summary>
    /// <remarks>
    /// You can use the <see cref="ICESystem"/> to get this instance from everywhere (if instantiated when asked).
    /// </remarks>
    public sealed class GameCamera : AICEVirtualCamera {

        #region Runtime Values

        /// <summary>
        /// Our next position to follow.
        /// </summary>
        /// <remarks>
        /// Used to lerp the current position with this one to smooth everything.
        /// </remarks>
        private Vector3 newPosition = Vector3.zero;
        
        #endregion
        
        #region Properties

        /// <summary>
        /// The <see cref="GameCamera"/>'s <see cref="GameplayInputProvider"/>.
        /// </summary>
        public GameplayInputProvider InputProvider { get; private set; } = null;

        /// <inheritdoc cref="GameCameraSettings"/>
        [field: SerializeField, Tooltip("The settings this GameCamera will use.")] public GameCameraSettings Settings { get; set; } = new GameCameraSettings(0);
        /// <summary>
        /// When true, the <see cref="GameCamera"/> won't move at all.
        /// </summary>
        /// <remarks>
        /// Use <see cref="ListenInputs"/> instead if you wish to simply cut the control of this <see cref="GameCamera"/> from the user.
        /// </remarks>
        public bool Freeze { get; set; } = false;
        /// <summary>
        /// When false, the <see cref="GameCamera"/> won't listen any inputs from the user.
        /// </summary>
        public bool ListenInputs { get; set; } = true;
        /// <summary>
        /// If you give a <see cref="Transform"/> value here, the <see cref="GameCamera"/> will follow this object.
        /// </summary>
        /// <remarks>
        /// Having something else than "null" will make <see cref="ListenInputs"/> having no effect.<br/>
        /// </remarks>
        public Transform FollowTransform { get; set; } = null;

        #endregion

        #region AICEVirtualCamera's Internal Virtual Methods (MonoBehaviour's replacement)

        private protected override void OnAwakeCamera() {
            InputProvider = new GameplayInputProvider(); //Create the GameplayInputProvider
            InputProvider.GameCamera.Enable();
            
            newPosition = transform.position; //Set the default position for now

            InputProvider.GameCamera.DetachTarget.started += (_context) => { //Ask to detach the target
                if(ListenInputs) FollowTransform = null;
            };
        }

        private protected override void OnUpdateCamera() {
            if(Freeze) return; //Don't execute anything while being frozen

            if(FollowTransform != null) { //Follow the target if we got one
                transform.position = FollowTransform.position;
                return;
            }

            if(Settings.AllowKBMovement && ListenInputs) HandleKeyboardMovement();
            if(Settings.AllowScreenEdgeMovement && ListenInputs) HandleScreenEdgeMovement();

            ExecuteCameraSmoothing();
        }

        public override void OnVirtualCameraShowed() {
            Cursor.lockState = CursorLockMode.Confined;
        }
        
        public override void OnVirtualCameraHidden() {
            Cursor.lockState = CursorLockMode.None;
        }

        #endregion

        #region GameCamera's Internal Methods

        /// <summary>
        /// Handle the movement of the <see cref="GameCamera"/> with the keyboard using the <see cref="InputProvider"/>.
        /// </summary>
        private void HandleKeyboardMovement() {
            if(InputProvider.GameCamera.MoveUp.IsPressed()) newPosition += transform.forward * Settings.MovementSpeed; //If pressing Up
            else if(InputProvider.GameCamera.MoveDown.IsPressed()) newPosition += transform.forward * -Settings.MovementSpeed; //If pressing Down
            else if(InputProvider.GameCamera.MoveRight.IsPressed()) newPosition += transform.right * Settings.MovementSpeed; //If pressing Right
            else if(InputProvider.GameCamera.MoveLeft.IsPressed()) newPosition += transform.right * -Settings.MovementSpeed; //If pressing Left
        }

        /// <summary>
        /// Handle the movement of the <see cref="GameCamera"/> with the Screen's Borders.
        /// </summary>
        private void HandleScreenEdgeMovement() {
            if (Input.mousePosition.x > Screen.width - Settings.ScreenEdgeSize) newPosition += transform.right * Settings.MovementSpeed; //If Right
            if (Input.mousePosition.x < Settings.ScreenEdgeSize) newPosition += transform.right * -Settings.MovementSpeed; //If Left
            if (Input.mousePosition.y > Screen.height - Settings.ScreenEdgeSize) newPosition += transform.forward * Settings.MovementSpeed; //If Up
            if (Input.mousePosition.y < Settings.ScreenEdgeSize) newPosition += transform.forward * -Settings.MovementSpeed; //If Down
        }

        /// <summary>
        /// Execute the <see cref="GameCamera"/>'s smoothing <i>logic</i>.
        /// </summary>
        /// <remarks>
        /// Nothing fancy, just lerp-ing the current position with the <see cref="newPosition"/>.
        /// </remarks>
        private void ExecuteCameraSmoothing() {
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * Settings.MovementSmoothing);
        }

        #endregion
        
    }

}