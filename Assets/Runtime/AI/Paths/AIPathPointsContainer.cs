using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace VEvil.AI {

    /// <summary>
    /// <see cref="AIPathPointsContainer"/> is an object containing multiple <see cref="Transform"/> inside of itself.<br/>
    /// These children are points <see cref="AIPathCheckpoint"/> can give to a <see cref="IAIPathCheckpointCallbackReceiver"/>.
    /// </summary>
    public class AIPathPointsContainer : MonoBehaviour {

        #region Attributes

        /// <summary>
        /// <see cref="List{T}"/> of points found inside this <see cref="AIPathPointsContainer"/>.
        /// </summary>
        private List<Transform> points = new List<Transform>();
        /// <summary>
        /// Should this path be enabled and selected by the <see cref="AIPathCheckpoint"/>.
        /// </summary>
        private bool enablePoints = true;

        #endregion

        #region Events

        [Header("AI Path Points Container - Events")]
        [SerializeField] private UnityEvent onPathEnabled = null;
        [SerializeField] private UnityEvent onPathDisabled = null;

        #endregion

        #region Properties

        /// <inheritdoc cref="enablePoints"/>
        public bool EnablePoints {
            get { return enablePoints; }
            set {
                enablePoints = value;
                if(value) onPathEnabled?.Invoke(); else onPathDisabled?.Invoke();
            }
        }

        #endregion
        
        #region MonoBehaviour's Methods

        private void Awake() {
            Transform[] _points = GetComponentsInChildren<Transform>(); //Get every points

            for (int i=0; i<_points.Length; i++) { //Remove the Transform point of this object //TODO: Find a better way to do this, I want to stop thinking IL2CPP will do the job for me
                if (_points[i].parent != null && _points[i].GetComponent<AIPathPointsContainer>() == null) {
                    _points[i].name = $"AI Path Point - {i}";
                    points.Add(_points[i]);
                }
            }
        }

        #endregion

        #region AIPathPointsContainer's External Methods

        /// <summary>
        /// Find and return a random point.
        /// </summary>
        public Transform PickRandomPoint() {
            return points[Random.Range(0, points.Count)];
        }

        #endregion

        #region AIPathPointsContainer's Internal Editor Methods

        private void OnDrawGizmos() {
            Transform[] _points = GetComponentsInChildren<Transform>(); //Get every points
            if (_points == null) return;
            
            if (EnablePoints) Gizmos.color = Color.green; else Gizmos.color = Color.red;
            
            {
                for (int i=0; i<_points.Length; i++) {
                    Gizmos.DrawLine(this.transform.position, _points[i].position);
                }
            }
        }

        #endregion
        
    }

}