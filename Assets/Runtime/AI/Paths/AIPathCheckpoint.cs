using System;
using System.Linq;
using UnityEngine;
using VEvil.GameLogic.Teams;
using Logger = VEvil.Core.Logger;
using Random = UnityEngine.Random;

namespace VEvil.AI {

    /// <summary>
    /// <see cref="AIPathCheckpoint"/> is a trigger waiting for objects with <see cref="IAIPathCheckpointCallbackReceiver"/> to enter it to give it a new point to follow.
    /// </summary>
    public class AIPathCheckpoint : MonoBehaviour {

        #region Runtime Values

        /// <summary>
        /// The <see cref="Collider"/> component of this <see cref="AIPathCheckpoint"/>.
        /// </summary>
        private Collider collider = null;

        #endregion

        #region Properties

        /// <inheritdoc cref="AIPathPointsConstraints"/>
        [field: SerializeField] public AIPathPointsConstraints[] Paths { get; private set; }

        #endregion

        #region MonoBehaviour's Methods

        private void OnEnable() {
            //Auto-assign references
            collider = GetComponent<Collider>();
            
            //Check references
            if(collider == null) { Logger.TraceError($"AI Path Checkpoint ({name})", "Unable to find a Collider component! Please be sure this AIPathCheckpoint component is placed right next to a Collider one! Removing behaviour."); Destroy(this); }
            if(!collider.isTrigger) { Logger.TraceWarning($"AI Path Checkpoint ({name})", "The Collider component doesn't have 'isTrigger' enabled! Enabling it..."); collider.isTrigger = true; }
            if(Paths.Equals(default(AIPathPointsConstraints))) { Logger.TraceWarning($"AI Path Checkpoint ({name})", $"Could find any PathContainerConstraints, this AIPath behaviour is useless! Removing behaviour!"); Destroy(this); }

            //Setup default content
            for (int i=0; i<Paths.Length; i++) {
                Paths[i].IsContainerEnabled = Paths[i].EnableAtLaunch;
            }
        }

        private void OnTriggerEnter(Collider _other) {
            IAIPathCheckpointCallbackReceiver _receiver = _other.GetComponentInParent<IAIPathCheckpointCallbackReceiver>(); //Try getting the component
            if (_receiver == null) return; //Couldn't find any Receiver

            ETeamType _team = _receiver.TeamIndicator.Team; //Keep a reference to the team of the receiver
            AIPathPointsConstraints[] _containers = Paths.Where(_path => _path.IsContainerEnabled && _path.IsTeamAllowed(_team)).ToArray(); //Find Paths that can suit the receiver
            
            if(_containers.Length <= 0) { Logger.Trace($"AI Path Checkpoint ({name})", $"Couldn't find any path for the AIPathCheckpointCallbackReceiver '{_other.transform.parent.name}'!"); return; }

            _receiver.OnNextPointGiven(_containers[Random.Range(0, _containers.Length)].Container.PickRandomPoint().gameObject); //Find a random point and give it to our receiver
        }

        #endregion

        #region AIPathCheckpoint's Internal Editor Methods

        private void OnDrawGizmosSelected() {
            Collider _collider = GetComponent<Collider>();
            if (_collider == null) return;
            
            Gizmos.color = Color.white; {
                Gizmos.DrawCube(_collider.bounds.center, _collider.bounds.size);
            }
        }

        #endregion

    }

}