using System;
using UnityEngine;
using VEvil.AI;
using Logger = VEvil.Core.Logger;

namespace VEvil.GameLogic.Interactable.Panel {

    public class DirectionalPanelInteractable : APointerClickableEntity {

        #region Attributes

        [Header("Directional Panel - Settings")]
        [SerializeField, Tooltip("The cooldown to change again the direction.\nTime in seconds!")] private float changeDirectionCooldown = 0.5f;
        [SerializeField, Tooltip("The default index of the Paths to enable first (The others will be automatically disabled).")] private int defaultActiveIndex = 0;

        #endregion

        #region Properties

        /// <summary>
        /// The <see cref="AIPathPointsContainer"/> this <see cref="DirectionalPanelInteractable"/> can enable/disable.
        /// </summary>
        [field: SerializeField, Header("Directional Panel - References"), Tooltip("The AIPathPointsContainer this DirectionalPanelInteractable can enable/disable.")] public AIPathPointsContainer[] Paths { get; private set; } = null;

        /// <summary>
        /// The current <see cref="Paths"/> active's index.
        /// </summary>
        public int CurrentActiveIndex { get; private set; } = 0;

        #endregion

        #region MonoBehaviour's Methods

        private void Start() {
            IsInteractable = true;
            ChangeIndex(defaultActiveIndex);
        }

        #endregion

        #region APointerClickableEntity's Internal Virtual Methods

        protected override void OnPointerClick() {
            int _newIndexTarget = CurrentActiveIndex + 1;
            if (_newIndexTarget > Paths.Length-1) { //If we already reached the maximum
                _newIndexTarget = 0; //Fallback to the start position
            }
            
            ChangeIndex(_newIndexTarget); //Change direction
        }

        #endregion
        
        #region DirectionalPanelInteractable's Internal Methods

        /// <summary>
        /// Change the <see cref="CurrentActiveIndex"/> thus the active <see cref="AIPathPointsContainer"/> among the <see cref="Paths"/> referenced.
        /// </summary>
        private void ChangeIndex(int _newIndex) {
            CurrentActiveIndex = Mathf.Clamp(_newIndex, 0, Paths.Length); //Clamp it
            IsInteractable = false; //Will be true after the cooldown

            for (int i=0; i<Paths.Length; i++) {
                Paths[i].EnablePoints = false;
            }

            Paths[CurrentActiveIndex].EnablePoints = true;
            Invoke(nameof(ResetInteractableState), changeDirectionCooldown); //Reset IsInteractable after the cooldown
        }
        
        /// <summary>
        /// Reset the <see cref="APointerClickableEntity.IsInteractable"/> value after the <see cref="changeDirectionCooldown"/> has passed.
        /// </summary>
        private void ResetInteractableState() {
            IsInteractable = true;
        }

        #endregion

    }

}