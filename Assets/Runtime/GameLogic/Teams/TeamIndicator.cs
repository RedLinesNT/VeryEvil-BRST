using System;
using UnityEngine;

namespace VEvil.GameLogic.Teams {

    /// <summary>
    /// <see cref="TeamIndicator"/> helps define the team of an entity.
    /// </summary>
    public class TeamIndicator : MonoBehaviour {

        #region Events

        /// <summary>
        /// Invoked when this <see cref="TeamIndicator"/> has changed its <see cref="ETeamType"/> value.
        /// </summary>
        /// <param name="Arg 01">The new <see cref="ETeamType"/> of this <see cref="TeamIndicator"/>.</param>
        private Action<ETeamType> onTeamChanged = null;
 
        #endregion
        
        #region Properties

        /// <summary>
        /// The <see cref="ETeamType"/> of this entity.
        /// </summary>
        /// <remarks>
        /// Leave it to <see cref="ETeamType.UNKNOWN"/> you wish to directly modify it at this entity's initialization.<br/>
        /// Use <see cref="ChangeTeam"/> if you wish to modify this value at runtime.
        /// </remarks>
        [field: SerializeField, Tooltip("The team of this entity.\nLeave it to 'UNKNOWN' if you wish to directly modify it at this entity's initialization.")] public ETeamType Team { get; set; }

        /// <inheritdoc cref="onTeamChanged"/>
        public event Action<ETeamType> OnTeamChanged { add { onTeamChanged += value; } remove { onTeamChanged -= value; } }
        
        #endregion

        #region TeamIndicator's External Methods

        /// <summary>
        /// Change this <see cref="TeamIndicator"/>'s <see cref="ETeamType"/>.
        /// </summary>
        public void ChangeTeam(ETeamType _newTeam) {
            if (Team == _newTeam) return; //Don't execute anything if its the same type

            Team = _newTeam;
            onTeamChanged?.Invoke(Team);
        }

        #endregion
        
    }

}