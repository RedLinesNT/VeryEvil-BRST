using System;
using UnityEngine;
using UnityEngine.UI;
using VEvil.GameLogic.Currencies;

namespace VEvil.FE.Gameplay.HUD {

    /// <summary>
    /// <see cref="FEHUDManaBar"/> (Front End Heads-Up Display Mana Bar) is a component part of the <see cref="HeadsUpDisplay"/>, displaying the current mana of a <see cref="ManaContainer"/>
    /// </summary>
    public class FEHUDManaBar : MonoBehaviour {

        #region Attributes

        [Header("FEHUD Mana Bar - References")]
        [SerializeField, Tooltip("The Image on the canvas displaying the current mana of the targeted ManaContainer.")] private Image filler = null;

        [Header("FEHUD Mana Bar - Settings")]
        [SerializeField, Tooltip("Amount of smoothing applied to the mana bar.")] private float smoothing = 0.05f;
        
        #endregion
        
        #region Runtime Values

        private float fillerVelocity = 0;
        
        #endregion
        
        #region Properties

        /// <summary>
        /// The <see cref="ManaContainer"/> this <see cref="FEHUDManaBar"/> is watching.
        /// </summary>
        public ManaContainer ManaContainer { get; private set; } = null;
        /// <summary>
        /// The target/desired filler amount the <see cref="filler"/> should have.
        /// </summary>
        public float TargetFillerValue { get; private set; } = 1.0f;
        /// <summary>
        /// The current filler amount the <see cref="filler"/> have.
        /// </summary>
        public float CurrentFillerValue { get { return filler.fillAmount; } private set { filler.fillAmount = value; } }

        #endregion
        
        #region MonoBehaviour's Methods

        private void Update() {
            if (ManaContainer == null) return;

            TargetFillerValue = (float)ManaContainer.CurrentMana / ManaContainer.MaxMana;
            CurrentFillerValue = Mathf.SmoothDamp(CurrentFillerValue, TargetFillerValue, ref fillerVelocity, smoothing); //Smooth things up
        }

        #endregion

        public void Watch(ManaContainer _container) => ManaContainer = _container;


    }

}