using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using VEvil.Core.FE;
using VEvil.GameLogic.Currencies;
using VEvil.GameLogic.Player;

namespace VEvil.FE.Gameplay.HUD {

    public class HeadsUpDisplay : AFrontEndModule {

        #region Attributes

        [Header("Heads Up Display - References")]
        [SerializeField, Tooltip("The Horizontal Layout Group component containing the units of the player's loadout.")] private HorizontalLayoutGroup unitsGroup = null;
        [SerializeField, Tooltip("The FEHUDUnitCard prefab used to display the Units of the player's loadout.")] private FEHUDUnitCard unitCardPrefab = null;
        
        
        #endregion

        #region Runtime Values

        /// <summary>
        /// List of instantiated <see cref="FEHUDUnitCard"/> prefabs into the <see cref="HorizontalLayoutGroup"/> (<see cref="unitsGroup"/>).
        /// </summary>
        private List<FEHUDUnitCard> loadoutUnitCards = new List<FEHUDUnitCard>();

        #endregion

        #region Events

        private Action<int> onHUDUnitButtonPressedFromLoadout = null;

        #endregion

        #region Properties

        /// <summary>
        /// The <see cref="FEHUDManaBar"/> component used bw this <see cref="HeadsUpDisplay"/>.
        /// </summary>
        [field: SerializeField, Tooltip("The FEHUDManaBar component used by this Heads-Up Display.")] public FEHUDManaBar ManaBar { get; private set; } = null;

        public event Action<int> OnHUDUnitButtonPressedFromLoadout { add { onHUDUnitButtonPressedFromLoadout += value; } remove { onHUDUnitButtonPressedFromLoadout -= value; } }
        
        #endregion
        
        #region AFrontEndModule's Internal Virtual Methods

        protected override private FrontEndModuleInputSettings DefineInputSettings() {
            return new FrontEndModuleInputSettings() {
                InputAction = null,
                DisableCondition = InputActionPhase.Disabled,
                EnableCondition = InputActionPhase.Disabled,
            };
        }

        #endregion

        #region HeadsUpDisplay's Internal Methods

        /// <summary>
        /// Clear every elements this <see cref="HeadsUpDisplay"/> has created.
        /// </summary>
        private void ClearLoadout() {
            for (int i=0; i<loadoutUnitCards.Count; i++) { //Destroy Unit Cards
                Destroy(loadoutUnitCards[i].gameObject);
            }
        }

        #endregion
        
        #region HeadsUpDisplay's External Methods

        /// <summary>
        /// Watch and setup the content of a <see cref="UserLoadout"/> into this <see cref="HeadsUpDisplay"/>.
        /// </summary>
        public void WatchLoadout(UserLoadout _loadout) {
            ClearLoadout();
            
            for (int i=0; i<_loadout.Loadout.Length; i++) { //Create Unit Cards
                FEHUDUnitCard _unitCard = Instantiate(unitCardPrefab, unitsGroup.transform);
                _unitCard.Watch(_loadout.Loadout[i].Unit, i);
                _unitCard.Button.onClick.AddListener(() => { onHUDUnitButtonPressedFromLoadout?.Invoke(_unitCard.LoadoutIndexRef); });
                loadoutUnitCards.Add(_unitCard); //Add it into our list
            }
        }

        public void WatchManaContainer(ManaContainer _container) {
            ManaBar.Watch(_container);
        }

        #endregion
        
    }

}