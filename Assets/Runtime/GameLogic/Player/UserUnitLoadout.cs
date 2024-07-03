using System;
using UnityEngine;
using VEvil.GameLogic.Units;
using Random = UnityEngine.Random;

namespace VEvil.GameLogic.Player {

    /// <summary>
    /// Contain the loadout of a player.
    /// </summary>
    [Serializable] public struct UserLoadout {

        #region Properties

        /// <inheritdoc cref="UnitLoadoutInfo"/>
        [field: SerializeField, Tooltip("Contain the unit data of a User Loadout.")] public UnitLoadoutInfo[] Loadout { get; private set; }

        #endregion

        #region UserUnitLoadout's External Methods

        /// <summary>
        /// Returns true if a specified Unit is present on this <see cref="UserLoadout"/>.
        /// </summary>
        public bool HasUnit(UnitDefinition _unit) {
            for (int i=0; i<Loadout.Length; i++) {
                if (Loadout[i].Unit == _unit) return true;
            }

            return false; 
        }

        /// <summary>
        /// Find and return a random <see cref="UnitDefinition"/> on this <see cref="UserLoadout"/>.
        /// </summary>
        /// <remarks>
        /// might return null.
        /// </remarks>
        public UnitDefinition PickRandomUnit() {
            if (Loadout == null) return null;
            
            checkexpress:
            UnitLoadoutInfo _result = Loadout[Random.Range(0, Loadout.Length)];

            if (_result.Amount == 0) goto checkexpress;

            return _result.Unit;
        }
        
        #endregion

    }

    /// <summary>
    /// Contain the unit data of a <see cref="UserLoadout"/>.
    /// </summary>
    [Serializable] public struct UnitLoadoutInfo {

        #region Properties

        [field: SerializeField] public UnitDefinition Unit { get; private set; }
        /// <summary>
        /// The amount of the <see cref="Unit"/> on this <see cref="UnitLoadoutInfo"/>.
        /// </summary>
        /// <remarks>
        /// Set it to -1 to have an infinite amount.
        /// </remarks>
        [field: SerializeField, Tooltip("The amount of this Unit on this loadout.\nSet it to -1 to have an infinite amount.")] public int Amount { get; set; }

        #endregion
        
    }

}