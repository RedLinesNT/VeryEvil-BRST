using System;
using UnityEngine;

namespace VEvil.GameLogic.Cameras.Gameplay {

    /// <summary>
    /// Contain the settings used by the <see cref="GameCamera"/>.
    /// </summary>
    [Serializable] public struct GameCameraSettings {

        #region Properties

        /// <summary>
        /// Is the <see cref="GameCamera"/> allowed to move based on the keyboard's inputs ?
        /// </summary>
        [field: SerializeField, Header("Movement Rules"), Tooltip("Is the GameCamera allowed to move based on the Keyboard's inputs ?")] public bool AllowKBMovement { get; set; }
        /// <summary>
        /// Is the <see cref="GameCamera"/> allowed to move by putting the pointer near the edges of the main viewport ?
        /// </summary>
        [field: SerializeField, Tooltip("Is the GameCamera allowed to move by putting the pointer near the edges of the main viewport ?")] public bool AllowScreenEdgeMovement { get; set; }
        /// <summary>
        /// The smoothing applied to the <see cref="GameCamera"/>'s movement.
        /// </summary>
        [field: SerializeField, Tooltip("The smoothing applied to the GameCamera's movement.")] public float MovementSmoothing { get; set; }
        /// <summary>
        /// The movement speed of the <see cref="GameCamera"/> when using the keyboard's inputs.
        /// </summary>
        [field: SerializeField, Tooltip("The movement speed of the GameCamera in general.")] public float MovementSpeed { get; set; }
        
        /// <summary>
        /// The size of the screen's edge to move the <see cref="GameCamera"/> with the pointer's position.
        /// </summary>
        /// <remarks>
        /// Size in pixels!
        /// </remarks>
        [field: SerializeField, Header("Edge Screen Movement Settings"), Tooltip("The size of the screen's edge to move the GameCamera with the pointer's position.")] public float ScreenEdgeSize { get; set; }

        #endregion

        #region GameCameraSettings' Constructor Method

        /// <summary>
        /// Create a new <see cref="GameCameraSettings"/> instance with default settings.
        /// </summary>
        /// <param name="_dummy">Dummy <see cref="byte"/>. There's no need to use it.</param>
        public GameCameraSettings(byte _dummy = 0x0) {
            AllowKBMovement = true;
            AllowScreenEdgeMovement = true;
            MovementSmoothing = 10f;

            MovementSpeed = 0.05f;
            ScreenEdgeSize = 50f;
        }

        #endregion

    }

}