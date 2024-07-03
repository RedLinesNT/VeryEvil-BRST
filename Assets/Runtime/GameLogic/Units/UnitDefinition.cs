using UnityEngine;
using VEvil.GameLogic.Teams;
using Logger = VEvil.Core.Logger;

namespace VEvil.GameLogic.Units {

    /// <summary>
    /// <see cref="UnitDefinition"/> files contain the global definitions and references of a Unit.
    /// </summary>
    public class UnitDefinition : ScriptableObject {

        #region Properties

        /// <summary>
        /// The internal name for this <see cref="UnitDefinition"/> file.
        /// </summary>
        [field: SerializeField, Tooltip("The internal name of this Unit Definition file.")] public string InternalName { get; private set; } = "UNIT DEF INT-NAME";
        /// <summary>
        /// The icon of this Unit.
        /// </summary>
        [field: SerializeField, Tooltip("The icon of this Unit.")] public Sprite Icon { get; private set; } = null;
        /// <summary>
        /// The <see cref="UnitSettings"/> configuration for this Unit.
        /// </summary>
        [field: SerializeField, Tooltip("The UnitSettings configuration for this Unit.")] public UnitSettings Settings { get; private set; } = default;
        /// <summary>
        /// The Mana cost in-game for this Unit.
        /// </summary>
        [field: SerializeField, Tooltip("The Mana cost in-game for this Unit.")] public int ManaCost { get; private set; } = 2;
        /// <summary>
        /// The <see cref="UnitPrefabVariant"/> of this Unit.
        /// </summary>
        [field: SerializeField, Tooltip("The prefab variants of this Unit.")] public UnitPrefabVariant[] PrefabVariants { get; private set; } = null;

        #endregion

        #region UnitDefinition's Methods

        /// <summary>
        /// Find a return a <see cref="AUnitEntity"/> prefab based on a defined <see cref="ETeamType"/>.
        /// </summary>
        /// <returns></returns>
        public AUnitEntity FindPrefabVariant(ETeamType _team) {
            for (int i=0; i<PrefabVariants.Length; i++) {
                if (PrefabVariants[i].TargetTeam == _team) return PrefabVariants[i].Prefab;
            }
            
            Logger.TraceWarning($"Unit Definition ({InternalName})", $"Unable to find the prefab variant ({_team}) for this Unit! Falling back to the first one indexed.");
            if (PrefabVariants[0].Prefab != null) return PrefabVariants[0].Prefab;

            return null; //Should ever be the final case
        }

        #endregion

    }

}