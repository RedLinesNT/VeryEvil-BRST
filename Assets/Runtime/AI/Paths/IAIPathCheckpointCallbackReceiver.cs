using System;
using UnityEngine;
using VEvil.GameLogic.Teams;

namespace VEvil.AI {

    public interface IAIPathCheckpointCallbackReceiver {

        #region Properties

        /// <summary>
        /// The <see cref="VEvil.GameLogic.Teams.TeamIndicator"/> component of this <see cref="IAIPathCheckpointCallbackReceiver"/>.
        /// </summary>
        public TeamIndicator TeamIndicator { get; }

        #endregion

        #region IAIPathCheckpointCallbackReceiver's External Methods

        /// <summary>
        /// Called when a <see cref="AIPathCheckpoint"/> give the next point to follow.
        /// </summary>
        /// <param name="_newPoint">The next point to follow.</param>
        public void OnNextPointGiven(GameObject _newPoint);

        #endregion

    }

}