using System;
using UnityEngine;

namespace VEvil.GameLogic.Currencies {

    /// <summary>
    /// <see cref="ManaContainer"/> is the currency in-fight.<br/>
    /// This module is used track the mana of an entity willing to execute actions requiring mana-currency.
    /// </summary>
    public class ManaContainer : MonoBehaviour {

        #region Events

        /// <summary>
        /// Invoked when the <see cref="CurrentMana"/> value has been modified.
        /// </summary>
        /// <param name="01">The previous amount of mana.</param>
        /// <param name="02">The new amount of mana.</param>
        private Action<float, float> onManaValueChanged = null;

        #endregion
        
        #region Properties

        /// <inheritdoc cref="ManaContainerSettings"/>
        [field: SerializeField, Header("Mana Container - Settings"), Tooltip("The settings this ManaContainer will use.")] public ManaContainerSettings Settings { get; set; } = new ManaContainerSettings(0);
        
        /// <summary>
        /// The current amount of mana this <see cref="ManaContainer"/> have.
        /// </summary>
        public float CurrentMana { get; private set; } = 0.0f;
        /// <summary>
        /// The maximum amount of mana this <see cref="ManaContainer"/> can have.
        /// </summary>
        public float MaxMana { get { return Settings.MaxMana; } }
        /// <summary>
        /// If true, this <see cref="ManaContainer"/> will stop recovering mana.
        /// </summary>
        public bool Freeze { get; set; } = false;

        /// <inheritdoc cref="onManaValueChanged"/>
        public event Action<float, float> OnManaValueChanged { add { onManaValueChanged += value; } remove { onManaValueChanged -= value; } }

        #endregion

        #region MonoBehaviour's Methods

        private void Start() {
            CurrentMana = Settings.DefaultMana;
        }

        private void Update() {
            if(Freeze) return;

            if(CurrentMana <= Settings.MaxMana) {
                CurrentMana += Time.deltaTime * Settings.RecoverSpeed;
            }
        }

        #endregion

        #region ManaContainer's External Methods

        /// <summary>
        /// Consume and remove a defined amount of mana this <see cref="ManaContainer"/> have.
        /// </summary>
        /// <param name="_amount">The amount to remove.</param>
        public void ConsumeMana(float _amount) {
            if(_amount == 0) return;
            
            _amount = Mathf.Abs(_amount); //Absolute this!
            float _oldAmount = CurrentMana; //Keep this
            
            CurrentMana -= _amount;
            CurrentMana = Mathf.Clamp(CurrentMana, 0, Settings.MaxMana);
            
            onManaValueChanged?.Invoke(_oldAmount, CurrentMana);
        }

        /// <summary>
        /// Give a defined amount of mana to this <see cref="ManaContainer"/>.
        /// </summary>
        /// <param name="_amount">The amount to give.</param>
        /// <param name="_exceedMax">If set to true, the amount won't be clamped to be maximum mana defined in the <see cref="ManaContainerSettings"/> of this <see cref="ManaContainer"/>.</param>
        public void ProvideMana(float _amount, bool _exceedMax = false) {
            if(_amount == 0) return;
            
            _amount = Mathf.Abs(_amount); //Absolute this!
            float _oldAmount = CurrentMana; //Keep this
            
            CurrentMana += _amount;
            if(_exceedMax) CurrentMana = Mathf.Clamp(CurrentMana, 0, Settings.MaxMana);
            
            onManaValueChanged?.Invoke(_oldAmount, CurrentMana);
        }

        #endregion
        
    }

}