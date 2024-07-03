using System;
using UnityEngine;
using VEvil.GameLogic.Teams;

namespace VEvil.GameLogic.Units {

    /// <summary>
    /// Contain team variant prefabs for a <see cref="AUnitEntity"/>.
    /// </summary>
    [Serializable] public struct UnitPrefabVariant {

        #region Properties

        /// <summary>
        /// The <see cref="ETeamType"/> for this prefab variant.
        /// </summary>
        [field: SerializeField, Tooltip("The team for this prefab variant.")] public ETeamType TargetTeam { get; private set; }
        /// <summary>
        /// The <see cref="AUnitEntity"/> prefab for this team variant.
        /// </summary>
        [field: SerializeField, Tooltip("The Unit Entity prefab for this team variant.")] public AUnitEntity Prefab { get; private set; }

        #endregion

    }

}