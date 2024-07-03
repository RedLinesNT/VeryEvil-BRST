using System;
using UnityEngine;
using UnityEngine.AI;

namespace VEvil.AI {

    /// <summary>
    /// <see cref="AIAgentSettings"/> contain the settings that can be set into a <see cref="AAIAgentEntity"/> instance to configure it in runtime/editor.
    /// </summary>
    [Serializable] public struct AIAgentSettings {

        #region Properties
        
        /// <summary>
        /// The relative vertical displacement of the owning GameObject.
        /// </summary>
        [field: SerializeField, Header("General Settings"), Tooltip("The relative vertical displacement of the owning GameObject.\nDefault value: 0")] public float BaseOffset { get; set; }
        
        /// <summary>
        /// The maximum movement speed when following a target.
        /// </summary>
        [field: SerializeField, Header("Movement Settings"), Tooltip("The maximum movement speed when following a target.\nDefault value: 3.5")] public float Speed { get; set; }
        /// <summary>
        /// The maximum turning speed in degrees/second while following a path.
        /// </summary>
        [field: SerializeField, Tooltip("The maximum turning speed in degrees/second while following a path.\nDefault value: 120")] public float AngularSpeed { get; set; }
        /// <summary>
        /// The maximum acceleration of the agent as it follow a path.
        /// </summary>
        /// <remarks>
        /// In units/sec^2.
        /// </remarks>
        [field: SerializeField, Tooltip("The maximum acceleration of the agent as it follow a path.\nIn units/sec^2.\nDefault value: 8")] public float Acceleration { get; set; }
        /// <summary>
        /// Stop withing this distance from the target position.
        /// </summary>
        [field: SerializeField, Tooltip("Stop withing this distance from the target position.\nDefault value: 0")] public float StoppingDistance { get; set; }
        /// <summary>
        /// Should the agent brake automatically to avoid overshooting the destination point ?
        /// </summary>
        [field: SerializeField, Tooltip("Should the agent brake automatically to avoid overshooting the destination point ?\nDefault value: True")] public bool AutoBraking { get; set; }

        /// <summary>
        /// The avoidance radius for the agent.
        /// </summary>
        [field: SerializeField, Header("Obstacles Avoidance Settings"), Tooltip("The avoidance radius for the agent.\nDefault value: 0.5")] public float Radius { get; set; }
        /// <summary>
        /// The height of the agent for purposes of passing under obstacles.
        /// </summary>
        [field: SerializeField, Tooltip("The height of the agent for purposes of passing under obstacles.\nDefault value: 2")] public float Height { get; set; }
        /// <summary>
        /// Higher quality avoidance reduces more the chances of agents overlapping but it is slower to compute than lower quality avoidance.
        /// </summary>
        [field: SerializeField, Tooltip("Higher quality avoidance reduces more the chances of agents overlapping but it is slower to compute than lower quality avoidance.\nDefault value: HighQualityObstacleAvoidance")] public ObstacleAvoidanceType Quality { get; set; }
        
        #endregion

        #region AIAgentSettings' Constructor Method

        /// <summary>
        /// Create a new <see cref="AIAgentSettings"/> with the basic/default settings.
        /// </summary>
        /// <param name="_dummy">Dummy integer, set it to whatever you want, it doesn't matter after all.</param>
        public AIAgentSettings(int _dummy) {
            BaseOffset = 0;
            
            Speed = 3.5f;
            AngularSpeed = 120;
            Acceleration = 8;
            StoppingDistance = 0;
            AutoBraking = true;

            Radius = 0.5f;
            Height = 2;
            Quality = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
        }

        #endregion
        
    }

}