using System;
using UnityEngine;
using VEvil.GameLogic.Health;

namespace VEvil.GameLogic.Interactable.TNT {

    /// <summary>
    /// Contain the settings a <see cref="TNTInteractable"/> will use.
    /// </summary>
    [Serializable] public struct TNTInteractableSettings {

        #region Properties

        /// <summary>
        /// The delay before the actual <see cref="TNTInteractable"/>'s explosion.
        /// </summary>
        [field: SerializeField, Tooltip("The delay before the actual TNT's explosion.")] public float ExplosionDelay { get; private set; }
        /// <summary>
        /// The range of the <see cref="TNTInteractable"/>'s explosion.
        /// </summary>
        [field: SerializeField, Tooltip("The range of the TNT's explosion.")] public float ExplosionRange { get; private set; }
        /// <summary>
        /// The range to damage <see cref="AHealthModule"/>s depending on their relative distance from the <see cref="TNTInteractable"/>'s explosion.
        /// </summary>
        /// <remarks>
        /// X => Far distance damage value.<br/>
        /// Y => Close distance damage value.
        /// </remarks>
        [field: SerializeField, Tooltip("The range to damage HealthModules depending on their relative distance from the TNT's explosion.\nX => Far distance damage value.\nY => Close distance damage value.")] public Vector2 DamageFactor { get; private set; }
        /// <summary>
        /// Can the <see cref="TNTInteractable"/>'s explosion deal damages to entities on the same team ?
        /// </summary>
        [field: SerializeField, Tooltip("Can the TNT's explosion deal damages to entities on the same team ?")] public bool FriendlyFire { get; private set; }

        #endregion
        
        #region TNTInteractableSettings' Constructor Method

        /// <summary>
        /// Create a new <see cref="TNTInteractableSettings"/> instance with default settings.
        /// </summary>
        /// <param name="_dummy">Dummy <see cref="byte"/>. There's no need to use it.</param>
        public TNTInteractableSettings(byte _dummy = 0x0) {
            ExplosionDelay = 0;
            ExplosionRange = 7.5f;
            DamageFactor = new Vector2(10, 20);
            FriendlyFire = true;
        }

        #endregion
        
    }

}