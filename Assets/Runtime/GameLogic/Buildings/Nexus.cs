using System;
using UnityEngine;
using UnityEngine.Events;
using VEvil.GameLogic.Currencies;
using VEvil.GameLogic.Health;
using VEvil.GameLogic.Player;
using VEvil.GameLogic.Teams;
using VEvil.GameLogic.Units;
using Logger = VEvil.Core.Logger;
using Random = UnityEngine.Random;

namespace VEvil.GameLogic.Buildings {

    /// <summary>
    /// <see cref="Nexus"/> is the main building the player should defend or the final and ultimate objective to destroy.
    /// </summary>
    public class Nexus : MonoBehaviour {

        #region Attributes

        [Header("Nexus - Events")]
        [SerializeField, Tooltip("Invoked when this Nexus has no more health (has been destroyed).")] private UnityEvent onNexusDestroyedEditor = null;

        #endregion

        #region Runtime Values

        /// <summary>
        /// The <see cref="TeamIndicator"/> component of this <see cref="Nexus"/>.
        /// </summary>
        private TeamIndicator teamIndicator = null;
        /// <summary>
        /// The next <see cref="UnitDefinition"/> this <see cref="Nexus"/> will spawn.
        /// </summary>
        /// <remarks>
        /// Only used if auto-spawn is allowed on this <see cref="Nexus"/>' <see cref="Settings"/>.
        /// </remarks>
        private UnitDefinition nextUnitToSpawn = null;
        /// <summary>
        /// The last <see cref="UnitDefinition"/> spawned bw this <see cref="Nexus"/>.
        /// </summary>
        private UnitDefinition lastUnitSpawned = null;

        #endregion
        
        #region Events

        /// <summary>
        /// Invoked when this <see cref="Nexus"/> has no more health (has been destroyed).
        /// </summary>
        private Action onNexusDestroyed = null;

        #endregion

        #region Properties

        /// <summary>
        /// The <see cref="AHealthModule"/> component used by this <see cref="Nexus"/>.
        /// </summary>
        [field: SerializeField, Header("Nexus - References"), Tooltip("The HealthModule component used by this Nexus.")] public AHealthModule HealthModule { get; private set; } = null;
        /// <summary>
        /// The <see cref="VEvil.GameLogic.Currencies.ManaContainer"/> component used by this <see cref="Nexus"/>.
        /// </summary>
        [field: SerializeField, Tooltip("The ManaContainer component used by this Nexus.")] public ManaContainer ManaContainer { get; private set; } = null;
        /// <summary>
        /// The spawn-points for the <see cref="AUnitEntity"/> this <see cref="Nexus"/> will summon.
        /// </summary>
        [field: SerializeField, Tooltip("The spawn-points for the Units this Nexus will summon.")] public Transform[] SpawnPoints { get; private set; } = null;

        /// <summary>
        /// The <see cref="NexusSettings"/> this <see cref="Nexus"/> will use.
        /// </summary>
        [field: SerializeField, Header("Nexus - Settings"), Tooltip("The settings this Nexus will use.")] public NexusSettings Settings { get; private set; } = new NexusSettings(0);

        /// <summary>
        /// The <see cref="UserLoadout"/> of the user owning this <see cref="Nexus"/>.
        /// </summary>
        /// <remarks>
        /// This is a placeholder and should be replaced by something better.
        /// </remarks>
        [field: SerializeField, Header("Nexus - PLACEHOLDER"), Tooltip("The loadout of the user owning this Nexus.")] public UserLoadout Loadout { get; set; } = default;
        
        /// <summary>
        /// The <see cref="ETeamType"/> of this <see cref="Nexus"/>.
        /// </summary>
        public ETeamType Team { get { return teamIndicator.Team; } }
        /// <summary>
        /// Is spawning <see cref="AUnitEntity"/> isn't allowed ?
        /// </summary>
        public bool LockSpawn { get; set; } = false;

        /// <inheritdoc cref="onNexusDestroyed"/>
        public event Action OnNexusDestroyed { add { onNexusDestroyed += value; } remove { onNexusDestroyed -= value; } }

        #endregion

        #region MonoBehaviour's Methods

        private void Start() {
            //Auto-assign references
            teamIndicator = GetComponent<TeamIndicator>();
            
            //Check references
            if(teamIndicator == null) { Logger.TraceError($"Nexus ({name})", "Unable to find the TeamIndicator component! This component should have a TeamIndicator one right next to it. Removing behaviour."); Destroy(this); }
            if(HealthModule == null) { Logger.TraceError($"Nexus ({teamIndicator.Team} - {name})", "Missing a HealthModule reference on this component! Removing behaviour."); Destroy(this); }
            if(ManaContainer == null) { Logger.TraceError($"Nexus ({teamIndicator.Team} - {name})", "Missing ManaContainer reference on this component! Removing behaviour."); Destroy(this); }
            
            //Setup settings
            HealthModule.HealthPreset = Settings.HealthPreset;
            ManaContainer.Settings = Settings.ManaSettings;
            
            //Bind events
            HealthModule.OnHealthEmpty += OnHealthModuleEmpty;
        }

        private void Update() {
            if (!Settings.AutoSpawn || LockSpawn) return;

            if (nextUnitToSpawn == null) {
                findnewunit:
                nextUnitToSpawn = Loadout.PickRandomUnit();
                if(nextUnitToSpawn == lastUnitSpawned || nextUnitToSpawn == null) goto findnewunit;
            } else {
                SpawnUnit(nextUnitToSpawn);
                nextUnitToSpawn = null;
            }
        }

        #endregion

        #region Nexus' External Methods

        /// <summary>
        /// Spawn a <see cref="AUnitEntity"/> prefab stored inside <see cref="UnitDefinition"/> files into a random spawn point.
        /// </summary>
        /// <param name="_unit">The <see cref="UnitDefinition"/> to spawn.</param>
        /// <param name="_useCustomPoint">Should this <see cref="Nexus"/> use a custom spawn point for this Unit ? If false, a random one among the <see cref="SpawnPoints"/> will be taken.</param>
        /// <param name="_customPoint">The custom spawn-point.</param>
        public void SpawnUnit(UnitDefinition _unit, bool _useCustomPoint = false, Vector3 _customPoint = new Vector3()) {
            if (LockSpawn) return; //We currently can't spawn
            if (_unit.ManaCost > ManaContainer.CurrentMana) return; //Cancel the operation, we don't have the required mana

            AUnitEntity _unitEntity = Instantiate(_unit.FindPrefabVariant(Team), _useCustomPoint ? _customPoint : SpawnPoints[Random.Range(0, SpawnPoints.Length)].position, Quaternion.identity);
            _unitEntity.Team = Team; //Set its team
            _unitEntity.UnitSettings = _unit.Settings; //Its settings
            
            ManaContainer.ConsumeMana(_unit.ManaCost);
            //TODO: Consume this Entity from the UserLoadout!

            lastUnitSpawned = _unit;
        }

        #endregion
        
        #region Nexus' Internal Methods

        /// <inheritdoc cref="AHealthModule.OnHealthEmpty"/>
        private void OnHealthModuleEmpty() {
            onNexusDestroyedEditor?.Invoke();
            onNexusDestroyed?.Invoke();
        }
        
        #endregion

        #region Nexus' Internal Editor Methods

        private void OnDrawGizmos() {
            if (SpawnPoints == null) return;
            
            for (int i=0; i<SpawnPoints.Length; i++) {
                if (SpawnPoints[i] == null) return;
                Debug.DrawLine(this.transform.position, SpawnPoints[i].position, Color.yellow, 0f);
                Gizmos.color = Color.yellow; {
                    Gizmos.DrawSphere(SpawnPoints[i].position, 0.1f);    
                };
            }
        }

        #endregion
        
    }

}
