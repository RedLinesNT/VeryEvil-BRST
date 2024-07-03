using System;
using UnityEngine;
using VEvil.AI;
using VEvil.GameLogic.Attack;
using VEvil.GameLogic.Health;
using VEvil.GameLogic.Teams;
using Logger = VEvil.Core.Logger;

namespace VEvil.GameLogic.Units {

    /// <summary>
    /// The base of every unit of the game.
    /// </summary>
    public abstract class AUnitEntity : AAIAgentEntity, IAIPathCheckpointCallbackReceiver {

        #region Properties
        
        /// <summary>
        /// The <see cref="AAttackModule"/> component of this <see cref="AUnitEntity"/>.
        /// </summary>
        [field: SerializeField, Header("Unit Entity - References"), Tooltip("The attack module of this Unit Entity.")] public UnitAttackModule AttackModule { get; private set; } = null;
        /// <summary>
        /// The <see cref="AHealthModule"/> component of this <see cref="AUnitEntity"/>.
        /// </summary>
        [field: SerializeField, Tooltip("The health module of this Unit Entity.")] public AHealthModule HealthModule { get; private set; } = null;
        /// <summary>
        /// <see cref="VEvil.GameLogic.Teams.TeamIndicator"/> component of this <see cref="AUnitEntity"/>.
        /// </summary>
        [field: SerializeField, Tooltip("The Team Indicator component of this Unit Entity.")] public TeamIndicator TeamIndicator { get; private set; } = null;
        
        /// <summary>
        /// The <see cref="ETeamType"/> of this <see cref="AUnitEntity"/>.
        /// </summary>
        public ETeamType Team { get { return TeamIndicator.Team; } set { TeamIndicator.Team = value; } }
        /// <inheritdoc cref="VEvil.GameLogic.Units.UnitSettings"/>
        public UnitSettings UnitSettings {
            set {
                AttackModule.Settings = value.AttackSettings;
                HealthModule.HealthPreset = value.HealthPreset;
                Settings = value.AISettings;
            }
        }

        #endregion

        #region IAIPathCheckpointCallbackReceiver's External Methods

        public void OnNextPointGiven(GameObject _newPoint) {
            SetMainTarget(_newPoint);
        }

        #endregion

    }

}