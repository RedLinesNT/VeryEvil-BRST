using System;
using UnityEngine;
using VEvil.GameLogic.Teams;

namespace VEvil.AI {

    /// <summary>
    /// Constraints for <see cref="AIPathPointsContainer"/> used by <see cref="AIPathCheckpoint"/>s.
    /// </summary>
    [Serializable] public struct AIPathPointsConstraints {

        #region Properties

        /// <inheritdoc cref="AIPathPointsContainer"/>
        [field: SerializeField] public AIPathPointsContainer Container { get; private set; }
        /// <summary>
        /// Is the <see cref="Container"/> enabled at its creation ?
        /// </summary>
        [field: SerializeField, Tooltip("Is the Container enabled at its creation ?")] public bool EnableAtLaunch { get; private set; }
        /// <summary>
        /// The <see cref="ETeamType"/>s allowed to use one of the <see cref="Container"/>'s point.
        /// </summary>
        /// <remarks>
        /// Quick question, why not using Flags for this Enum ?
        /// </remarks>
        [field: SerializeField, Tooltip("The Teams allowed to use on the Container's point.")] public ETeamType[] EnabledForTeams { get; private set; }
        
        /// <inheritdoc cref="AIPathPointsContainer.EnablePoints"/>
        public bool IsContainerEnabled { get { return Container.EnablePoints; } set { Container.EnablePoints = value; } }

        #endregion

        #region AIPathPointsConstraints' Constructor Method

        /// <summary>
        /// Create a new <see cref="AIPathPointsConstraints"/> with the basic/default settings.
        /// </summary>
        /// <param name="_dummy">Dummy integer, set it to whatever you want, it doesn't matter after all.</param>
        public AIPathPointsConstraints(int _dummy) {
            Container = null;
            EnableAtLaunch = true;
            EnabledForTeams = new[] { ETeamType.UNKNOWN };
        }

        #endregion

        #region AIPathPointsConstraints' External Methods

        /// <summary>
        /// Returns true if a specific <see cref="ETeamType"/> is allowed to use the <see cref="AIPathPointsContainer"/> referenced.
        /// </summary>
        public bool IsTeamAllowed(ETeamType _team) {
            if (EnabledForTeams == null) return false;

            for (int i=0; i<EnabledForTeams.Length; i++) {
                if (EnabledForTeams[i] == _team) return true;
            }

            return false;
        }

        #endregion
        
    }

}