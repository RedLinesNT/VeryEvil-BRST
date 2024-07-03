using System;
using UnityEngine;

namespace VEvil.GameLogic.Health {

    /// <summary>
    /// <see cref="HealthPreset"/> contain the most primitive and basic preset for a <see cref="AHealthModule"/>.
    /// </summary>
    [Serializable] public struct HealthPreset {

        #region Properties

        /// <summary>
        /// The maximum health this <see cref="AHealthModule"/> can have.
        /// </summary>
        [field: SerializeField, Header("Health Settings"), Tooltip("The maximum health this Health Module can have.")] public int MaxHealth { get; set; }
        /// <summary>
        /// The default health this <see cref="AHealthModule"/> will have when being created.
        /// </summary>
        /// <remarks>
        /// Shouldn't be greater than the <see cref="MaxHealth"/>!
        /// </remarks>
        [field: SerializeField, Tooltip("The default health this Health Module will have when being created.\nShouldn't be greater than the MaxHealth!")] public int DefaultHealth { get; set; }
        
        #endregion

    }

}