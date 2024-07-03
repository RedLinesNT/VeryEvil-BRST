using System;
using UnityEngine;
using VEvil.GameLogic.Health;

namespace VEvil.FE.Gameplay.Health {

    /// <summary>
    /// The settings used by a <see cref="HealthModuleDisplayer"/>.
    /// </summary>
    [Serializable] public struct HealthModuleDisplayerSettings {

        #region Properties

        /// <summary>
        /// Should the <see cref="HealthModuleDisplayer"/> hide its containers over a certain amount of time if no damage has been detected on the target <see cref="AHealthModule"/> ?
        /// </summary>
        [field: SerializeField, Tooltip("Should the HealthModuleDisplayer hide its containers over a certain amount of time if no damage has been detected on the target HealthModule ?")] public bool HideOverTime { get; set; }
        /// <summary>
        /// The time to hide the containers of the <see cref="HealthModuleDisplayer"/>.
        /// </summary>
        /// <remarks>
        /// Time in seconds.<br/>
        /// <see cref="HideOverTime"/> must be true.
        /// </remarks>
        [field: SerializeField, Tooltip("The time to hide the containers of the HealthModuleDisplayer.\nTime in seconds!\nHideOverTime must be true.")] public float TimeToHide { get; set; }
        /// <summary>
        /// The smoothing value that'll be applied to the filler bar to make things look a bit more fancy.
        /// </summary>
        [field: SerializeField, Tooltip("The smoothing value that'll be applied to the filler bar to make things look a bit more fancy.")] public float FillerValueChangeSmoothing { get; set; }
        /// <summary>
        /// Should the <see cref="HealthModuleDisplayer"/> look at the camera ?
        /// </summary>
        [field: SerializeField, Tooltip("Should the HealthModuleDisplayer look at the camera ?")] public bool LookAtCamera { get; set; }

        #endregion
        
        #region HealthModuleDisplayerSettings' Constructor Method

        /// <summary>
        /// Create a new <see cref="HealthModuleDisplayerSettings"/> instance with default settings.
        /// </summary>
        /// <param name="_dummy">Dummy <see cref="byte"/>. There's no need to use it.</param>
        public HealthModuleDisplayerSettings(byte _dummy = 0x0) {
            HideOverTime = true;
            TimeToHide = 2.5f;
            FillerValueChangeSmoothing = 0.09f;
            LookAtCamera = true;
        }

        #endregion
        
    }

}