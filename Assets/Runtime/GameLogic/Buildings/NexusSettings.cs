using System;
using UnityEngine;
using VEvil.GameLogic.Currencies;
using VEvil.GameLogic.Health;

namespace VEvil.GameLogic.Buildings {

    /// <summary>
    /// Contain the settings used by a <see cref="Nexus"/>.
    /// </summary>
    [Serializable] public struct NexusSettings {

        #region Properties

        /// <summary>
        /// The <see cref="VEvil.GameLogic.Health.HealthPreset"/> configuration used by the <see cref="Nexus"/>.
        /// </summary>
        [field: SerializeField, Tooltip("The HealthPreset configuration used by the Nexus.")] public HealthPreset HealthPreset { get; set; }
        /// <summary>
        /// The <see cref="ManaContainerSettings"/> the <see cref="Nexus"/> will have by default.
        /// </summary>
        /// <remarks>
        /// If this <see cref="Nexus"/> is owned by the player, the amount of mana will be overwritten by the <see cref="GameModeSystem"/>.
        /// </remarks>
        [field: SerializeField, Tooltip("The ManaContainerSettings the Nexus will have by default.\nIf this Nexus is owned by the player, the amount of mana will be overwritten by the GameModeSystem.")] public ManaContainerSettings ManaSettings { get; set; }
        /// <summary>
        /// Is the <see cref="Nexus"/> allowed to auto-spawn units ?
        /// </summary>
        [field: SerializeField, Tooltip("Is the Nexus allowed to auto-spawn units ?")] public bool AutoSpawn { get; set; }
        
        //TODO: Include a list of Units spawnable

        #endregion
        
        #region NexusSettings' Constructor Method

        /// <summary>
        /// Create a new <see cref="NexusSettings"/> instance with default settings.
        /// </summary>
        /// <param name="_dummy">Dummy <see cref="byte"/>. There's no need to use it.</param>
        public NexusSettings(byte _dummy = 0x0) {
            HealthPreset = new HealthPreset() {
                MaxHealth = 250,
                DefaultHealth = 250,
            };
            ManaSettings = new ManaContainerSettings(0);
            AutoSpawn = false;
        }

        #endregion

        
    }

}