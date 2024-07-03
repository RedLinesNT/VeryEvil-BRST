using System;
using UnityEngine;
using VEvil.AI;
using VEvil.GameLogic.Attack;
using VEvil.GameLogic.Health;

namespace VEvil.GameLogic.Units {

    /// <summary>
    /// <see cref="UnitSettings"/> contain the settings of a unit, such as it's movement/health/... settings.
    /// </summary>
    [Serializable] public struct UnitSettings {

        #region Properties

        /// <summary>
        /// The <see cref="AIAgentSettings"/> of this Unit's <see cref="AAIAgentEntity"/>.
        /// </summary>
        [field: SerializeField, Tooltip("The movement settings of this Unit.")] public AIAgentSettings AISettings { get; private set; }
        /// <summary>
        /// The <see cref="VEvil.GameLogic.Attack.AttackSettings"/> of this Unit.
        /// </summary>
        [field: SerializeField, Tooltip("The AttackSettings of this Unit.")] public AttackSettings AttackSettings { get; private set; } 
        /// <summary>
        /// The <see cref="VEvil.GameLogic.Health.HealthPreset"/> of this Unit's <see cref="AHealthModule"/>.
        /// </summary>
        [field: SerializeField, Tooltip("The HealthPreset of this Unit.")] public HealthPreset HealthPreset { get; private set; } 
        
        #endregion

    }

}