using System;
using UnityEngine;

namespace VEvil.GameLogic.Currencies {

    /// <summary>
    /// Contain the settings of a <see cref="ManaContainer"/>.
    /// </summary>
    [Serializable] public struct ManaContainerSettings {

        #region Properties

        /// <summary>
        /// The maximum mana the <see cref="ManaContainer"/> can have.
        /// </summary>
        [field: SerializeField, Tooltip("The maximum mana the ManaContainer can have.")] public float MaxMana { get; set; }
        /// <summary>
        /// The default amount of mana the <see cref="ManaContainer"/> will have at creation.
        /// </summary>
        [field: SerializeField, Tooltip("The default amount of mana the ManaContainer will have at creation.")] public float DefaultMana { get; set; }
        /// <summary>
        /// How much mana the <see cref="ManaContainer"/> is getting every second.
        /// </summary>
        [field: SerializeField, Tooltip("How much mana the ManaContainer is getting every second.")] public float RecoverSpeed { get; set; }

        #endregion
        
        #region ManaContainerSettings' Constructor Method

        /// <summary>
        /// Create a new <see cref="ManaContainerSettings"/> instance with default settings.
        /// </summary>
        /// <param name="_dummy">Dummy <see cref="byte"/>. There's no need to use it.</param>
        public ManaContainerSettings(byte _dummy = 0x0) {
            MaxMana = 10f;
            DefaultMana = 10f;
            RecoverSpeed = 1f;
        }

        #endregion
        
    }

}