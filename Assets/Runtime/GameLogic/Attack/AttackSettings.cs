using System;
using UnityEngine;

namespace VEvil.GameLogic.Attack {

    [Serializable] public struct AttackSettings {
        
        /// <summary>
        /// The range to see an enemy.
        /// </summary>
        [field: SerializeField, Header("Attack Settings"), Tooltip("The range to see an enemy.")] public float SeeRange { get; private set; }
        /// <summary>
        /// The cooldown to perform detection checks to find a target.
        /// </summary>
        /// <remarks>
        /// Time in milliseconds!
        /// </remarks>
        [field: SerializeField, Tooltip("The cooldown to perform detection checks to find a target.\nTime in milliseconds!")] public int DetectionCooldownMS { get; private set; }
        /// <summary>
        /// The range to attack an enemy if in the <see cref="SeeRange"/>.
        /// </summary>
        [field: SerializeField, Tooltip("The range to attack an enemy if in the 'See Range'.")] public float AttackRange { get; private set; }
        /// <summary>
        /// That damage that'll be done to the enemy in range.
        /// </summary>
        [field: SerializeField, Tooltip("The damage that'll be done to the enemy in range.")] public int AttackDamage { get; private set; }
        /// <summary>
        /// The time between each attacks.
        /// </summary>
        /// <remarks>
        /// Time in milliseconds!
        /// </remarks>
        [field: SerializeField, Tooltip("The time between each attacks.\nTime in milliseconds!")] public int AttackCooldownMS { get; private set; }
        
    }

}