using System;
using UnityEngine;
using VEvil.ICE;

namespace VEvil.Dummies.Levels {

    /// <summary>
    /// Dummy class to run tests on the scene "DT_Cameras" (Debug-Test)
    /// </summary>
    public class DT_Cameras : MonoBehaviour {

        #region Attributes

        [Header("Quick Camera Selector")]
        [SerializeField, Tooltip("Specify the ICE Virtual Camera to set as the main one.")] private AICEVirtualCamera mainCameraToBind = null;

        #endregion

        #region MonoBehaviour's Methods

        private void Start() {
            ICESystem.SetMainVirtualCamera(mainCameraToBind); //Set our main target virtual camera
        }

        #endregion

    }

}