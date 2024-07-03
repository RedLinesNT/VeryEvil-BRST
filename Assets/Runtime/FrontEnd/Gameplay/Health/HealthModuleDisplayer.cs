using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using VEvil.Core;
using VEvil.GameLogic.Health;
using VEvil.ICE;
using Logger = VEvil.Core.Logger;

namespace VEvil.FE.Gameplay.Health {

    /// <summary>
    /// Give the ability to visually display and update a health bar on a <see cref="Canvas"/> based on the health of a <see cref="AHealthModule"/>.
    /// </summary>
    public class HealthModuleDisplayer : MonoBehaviour {

        #region Attributes

        [Header("Health Module Displayer - Containers")]
        [SerializeField, Tooltip("The GameObjects containing the HealthBar and other content.\nDon't include the Canvas and/or the GameObject containing this component as containers.")] private GameObject[] containers = null;

        [Header("Health Module Displayer - References")]
        [SerializeField, Tooltip("The Image on the canvas displaying the current health of the targeted HealthModule.")] private Image filler = null;
        [SerializeField, Tooltip("The HealthModule to target.")] private AHealthModule healthModule = null;

        #endregion

        #region Runtime Values

        private float fillerVelocity = 0;

        #endregion
        
        #region Properties

        /// <inheritdoc cref="HealthModuleDisplayer"/>
        [field: SerializeField, Header("Health Module Displayer - Settings"), Tooltip("The settings used by this HealthModuleDisplayer.")] public HealthModuleDisplayerSettings Settings { get; set; } = new HealthModuleDisplayerSettings(0);

        /// <summary>
        /// The <see cref="AHealthModule"/> this <see cref="HealthModuleDisplayer"/> is targeting.
        /// </summary>
        public AHealthModule HealthModule { 
            get { return healthModule; }
            set {
                if(value == null) return;
                if(healthModule != null) healthModule.OnHealthChanged -= UpdateDisplay; //Unbind events
                
                healthModule = value;
                healthModule.OnHealthChanged += UpdateDisplay;
            } 
        }
        /// <summary>
        /// The last time since the <see cref="HealthModule"/> targeted has received health changes.
        /// </summary>
        public float LastTimeSinceUpdate { get; private set; } = 0.0f;
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

        private void Start() {
            if(healthModule == null) { Logger.TraceWarning("Health Module Displayer", $"There's no HealthModule given!"); return; }

            healthModule.OnHealthChanged += UpdateDisplay;
            if(Settings.HideOverTime) containers.SetActive(false); //Auto-disable containers
        }

        private void OnDestroy() {
            if(healthModule != null) healthModule.OnHealthChanged -= UpdateDisplay;
        }

        private void Update() {
            if(Settings.LookAtCamera) { 
                transform.LookAt(ICESystem.BrainCamera.transform);
            }

            CurrentFillerValue = Mathf.SmoothDamp(CurrentFillerValue, TargetFillerValue, ref fillerVelocity, Settings.FillerValueChangeSmoothing); //Smooth things up
            
            if(!Settings.HideOverTime) return; //If allowed, update the hide condition
            if(LastTimeSinceUpdate >= Settings.TimeToHide) {
                containers.SetActive(false);
            } else {
                LastTimeSinceUpdate += Time.deltaTime;
            }
        }

        #endregion

        #region HealthModuleDisplayer's Internal Methods

        /// <inheritdoc cref="AHealthModule.OnHealthChanged"/>
        private void UpdateDisplay(int _previous, int _new) {
            if(_previous == _new) return;
            if(_new == healthModule.MaxHealth && CurrentFillerValue >= 1f) return;

            containers.SetActive(true);
            LastTimeSinceUpdate = 0.0f;

            TargetFillerValue = (float)_new / healthModule.MaxHealth;
        }

        #endregion

    }

}