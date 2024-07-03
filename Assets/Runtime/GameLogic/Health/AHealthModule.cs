using System;
using UnityEngine;
using UnityEngine.Events;
using VEvil.GameLogic.Teams;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VEvil.GameLogic.Health {

    /// <summary>
    /// <see cref="AHealthModule"/> bring a pretty "primitive" way to manage health on a <see cref="GameObject"/>.
    /// </summary>
    /// <remarks>
    /// You can place this component right next to a <see cref="TeamIndicator"/> one, this component will keep its <see cref="ETeamType"/>.
    /// </remarks>
    public abstract class AHealthModule : MonoBehaviour {

        #region Attributes

        [Header("Health Module - Settings")]
        [SerializeField, Tooltip("The HealthPreset of this Health Module.\nThis field is optional if you're planning to modify this field in runtime at the initialization of this entity.")] private HealthPreset healthPreset = default;

        [Header("Health Module - Events")]
        [SerializeField, Tooltip("Invoked when the health of this Health Module has been changed.\nArg 01: The previous amount of health.\nArg 02: The new amount of health.")] private UnityEvent<int, int> onHealthChangedEditor = null;
        [SerializeField, Tooltip("Invoked when this Health Module has no more health.")] private UnityEvent onHealthEmptyEditor = null;

        #endregion

        #region Events

        /// <summary>
        /// Invoked when the <see cref="Health"/> of this <see cref="AHealthModule"/> has been changed.
        /// </summary>
        /// <param name="01">The previous amount of health.</param>
        /// <param name="02">The new amount of health.</param>
        private Action<int, int> onHealthChanged;
        /// <summary>
        /// Invoked when this <see cref="AHealthModule"/> has no more health.
        /// </summary>
        private Action onHealthEmpty;

        #endregion

        #region Properties

        /// <summary>
        /// The maximum amount of health this <see cref="AHealthModule"/> can have.
        /// </summary>
        public int MaxHealth { get; private set; } = 0;
        /// <summary>
        /// The current amount of health this <see cref="AHealthModule"/> have.
        /// </summary>
        public int Health { get; private set; } = 0;
        /// <summary>
        /// The default <see cref="HealthPreset"/> given.
        /// </summary>
        /// <remarks>
        /// If this value is modified in runtime, the health values will be reset to the one given in the new value.
        /// </remarks>
        public HealthPreset HealthPreset {
            get { return healthPreset; }
            set {
                healthPreset = value; 
                MaxHealth = healthPreset.MaxHealth;
                Health = healthPreset.DefaultHealth; //Set the default health
                
                Refill();
            }
        }

        /// <inheritdoc cref="ETeamType"/>
        public ETeamType Team { get; private set; } = ETeamType.UNKNOWN;
        
        /// <inheritdoc cref="onHealthChanged"/>
        public event Action<int, int> OnHealthChanged { add { onHealthChanged += value; } remove { onHealthChanged -= value; } }
        /// <inheritdoc cref="onHealthEmpty"/>
        public event Action OnHealthEmpty { add { onHealthEmpty += value; } remove { onHealthEmpty -= value; } }
        
        #endregion

        #region MonoBehaviour's Methods

        private void Awake() {
            TeamIndicator _teamIndicator = GetComponent<TeamIndicator>(); //Try to get the TeamIndicator component
            if (_teamIndicator != null) { //If a component exists
                Team = _teamIndicator.Team; //Set the team
                _teamIndicator.OnTeamChanged += OnTeamIndicatorTeamChanged; //Bind this event
            }
        }

        private void Start() { //Use Start instead of Awake to let the HealthPreset being modified before Mono's initialization
            MaxHealth = healthPreset.MaxHealth;
            Health = healthPreset.DefaultHealth; //Set the default health
            
            Refill(); //Call this method to invoke every events
        }

        #endregion

        #region AHealthModule's Internal Methods

        /// <inheritdoc cref="TeamIndicator.OnTeamChanged"/>
        private void OnTeamIndicatorTeamChanged(ETeamType _newTeam) {
            Team = _newTeam;
        }

        #endregion
        
        #region AHealthModule's Internal Virtual Methods

        /// <summary>
        /// <see cref="OnHealthValueChanged"/> is called when the <see cref="Health"/> value of this <see cref="AHealthModule"/> has changed.
        /// </summary>
        /// <param name="_previousValue"></param>
        /// <param name="_newValue"></param>
        protected virtual void OnHealthValueChanged(int _previousValue, int _newValue) {}
        /// <summary>
        /// <see cref="OnHealthValueEmpty"/> is called when the <see cref="Health"/> value of this <see cref="AHealthModule"/> has reached 0.
        /// </summary>
        protected virtual void OnHealthValueEmpty() {}

        #endregion
        
        #region AHealthModule's External Methods

        /// <summary>
        /// Refill this entire <see cref="AHealthModule"/>'s <see cref="Health"/>.
        /// </summary>
        public void Refill() {
            int _oldHealth = Health;
            Health = MaxHealth;
            
            OnHealthValueChanged(_oldHealth, Health);
            onHealthChanged?.Invoke(_oldHealth, Health);
            onHealthChangedEditor?.Invoke(_oldHealth, Health);
        }
        
        /// <summary>
        /// Refill a specific amount of <see cref="Health"/>.
        /// </summary>
        /// <param name="_amount">The amount of <see cref="Health"/> to add.</param>
        public void Refill(int _amount) {
            if(_amount == 0) return; //Don't execute anything if there's nothing to do

            _amount = Mathf.Abs(_amount); //Make this value always positive
            int _oldHealth = Health; //Keep track of this old health
            
            Health += _amount;
            Health = Mathf.Clamp(Health, 0, MaxHealth); //Clamp the value
            
            OnHealthValueChanged(_oldHealth, Health);
            onHealthChanged?.Invoke(_oldHealth, Health);
            onHealthChangedEditor?.Invoke(_oldHealth, Health);
        }
        
        /// <summary>
        /// Set damage and reduce the amount of <see cref="Health"/>.
        /// </summary>
        /// <param name="_amount">The amount of <see cref="Health"/> to remove.</param>
        public void Damage(int _amount) {
            if(_amount == 0) return; //Don't execute anything if there's nothing to do

            _amount = Mathf.Abs(_amount); //Make this value always positive
            int _oldHealth = Health; //Keep track of this old health

            Health -= _amount;
            Health = Mathf.Clamp(Health, 0, MaxHealth); //Clamp the value

            OnHealthValueChanged(_oldHealth, Health);
            onHealthChanged?.Invoke(_oldHealth, Health);
            onHealthChangedEditor?.Invoke(_oldHealth, Health);
            
            if(Health <= 0) { //Check if empty
                OnHealthValueEmpty();
                onHealthEmpty?.Invoke();
                onHealthEmptyEditor?.Invoke();
                return;
            }
        }

        #endregion
        
        #region AHealthModule's Editor Methods

#if UNITY_EDITOR
        private void OnDrawGizmosSelected() {
            GUIStyle _textStyle = new GUIStyle();
            _textStyle.normal.textColor = Color.black;
            _textStyle.fontSize = 20;
            
            Handles.Label(transform.position + Vector3.up, $"Health: {Health}/{MaxHealth}", _textStyle);
        }
#endif

        #endregion
        
    }

}