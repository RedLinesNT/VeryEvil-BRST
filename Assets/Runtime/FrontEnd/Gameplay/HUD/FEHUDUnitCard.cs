using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VEvil.GameLogic.Units;
using Logger = VEvil.Core.Logger;

namespace VEvil.FE.Gameplay.HUD {

    /// <summary>
    /// <see cref="FEHUDUnitCard"/> (Front End Heads-Up Display Unit Card) is an element to show units as cards in the <see cref="HeadsUpDisplay"/>.
    /// </summary>
    public class FEHUDUnitCard : MonoBehaviour {

        #region Properties

        /// <summary>
        /// The <see cref="Image"/> displaying the <see cref="UnitDefinition"/>'s icon.
        /// </summary>
        [field: SerializeField, Tooltip("The Image displaying the UnitDefinition's icon.")] public Image UnitIconRenderer { get; private set; } = null;
        /// <summary>
        /// The <see cref="TextMeshProUGUI"/> displaying the <see cref="UnitDefinition"/>'s Mana cost.
        /// </summary>
        [field: SerializeField, Tooltip("The TextMeshPro UI text displaying the UnitDefinition's Mana cost.")] public TextMeshProUGUI UnitManaCost { get; private set; } = null;
        /// <summary>
        /// The <see cref="UnityEngine.UI.Button"/> component of this <see cref="FEHUDUnitCard"/>
        /// </summary>
        [field: SerializeField, Tooltip("The Button component of this FEHUD Unit Card.")] public Button Button { get; private set; } = null;

        public int LoadoutIndexRef { get; private set; } = 0;
        
        #endregion

        #region MonoBehaviour's Methods

        private void Awake() {
            //Check references
            if(UnitIconRenderer == null) { Logger.TraceError($"FEHUD Unit Card", "Missing Icon Renderer reference!"); }
            if(UnitManaCost == null) { Logger.TraceError("FEHUD Unit Card", "Missing Mana Cost Text reference!"); }
        }

        #endregion

        #region FEHUDUnitCard's External Methods

        /// <summary>
        /// Read a <see cref="UnitDefinition"/> file and associate its content on this <see cref="FEHUDUnitCard"/> to be displayed.
        /// </summary>
        public void Watch(UnitDefinition _unitDef, int _loadoutIndex) {
            UnitIconRenderer.sprite = _unitDef.Icon;
            UnitManaCost.text = $"{_unitDef.ManaCost}";

            LoadoutIndexRef = _loadoutIndex;
        }

        #endregion

    }

}