using System;
using UnityEngine;
using VEvil.GameLogic.Buildings;
using VEvil.ICE;

namespace VEvil.Dummies.Levels {

    /// <summary>
    /// Dummy class to run tests on the scene "DT_GameElements" (Debug-Test)
    /// </summary>
    public class DT_GameElements : MonoBehaviour {

        #region Attributes

        [Header("Nexus Enablers")]
        [SerializeField, Tooltip("The Nexus to enable. (Unlock spawning)")] private Nexus[] nexus = null;

        #endregion

        #region MonoBehaviour's Methods

        private void OnEnable() {
            if (nexus == null) return;
            
            for (int i=0; i<nexus.Length; i++) {
                nexus[i].LockSpawn = false;
            }
        }

        #endregion

    }

}