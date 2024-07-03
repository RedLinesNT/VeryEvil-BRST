using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VEvil.Core.SceneManagement;
using Logger = VEvil.Core.Logger;

namespace VEvil.FE.Debug {

    /// <summary>
    /// <see cref="DebugLevelPicker"/> is managing the Debug Level Picker Screen.
    /// </summary>
    public class DebugLevelPicker : MonoBehaviour {

        #region Attributes

        [Header("Debug Level Picker - References")]
        [SerializeField, Tooltip("The text showing the current SceneData selection.")] private TextMeshProUGUI levelSelectionText = null;
        [Space(15)]
        [SerializeField, Tooltip("The text showing the Level's Name.")] private TextMeshProUGUI levelNameText = null;
        [SerializeField, Tooltip("The text showing the Level's Path.")] private TextMeshProUGUI levelPathText = null;
        [SerializeField, Tooltip("The text showing the Level's Category.")] private TextMeshProUGUI levelCategoryText = null;
        [SerializeField, Tooltip("The text showing the Level's GameMode Type.")] private TextMeshProUGUI levelGameModeText = null;
        [SerializeField, Tooltip("The text showing if the Level is Packaged in the build.")] private TextMeshProUGUI isPackagedText = null;
        [SerializeField, Tooltip("The texture showing the Level's Preview Image.")] private RawImage levelPreviewTexture = null;

        #endregion

        #region Runtime values

        /// <summary>
        /// <see cref="Array"/> of every <see cref="SceneData"/>s files available in the <see cref="SceneSystem"/>.
        /// </summary>
        private SceneData[] sceneDatas = null;
        private int currentSelectionIndex = 0;

        #endregion

        #region MonoBehaviour's Methods

        private void Awake() {
            sceneDatas = new List<SceneData>(SceneSystem.SceneDatas).ToArray(); //Get a copy of every SceneData files
            
            MoveSelection(0);
        }

        private void Update() {
            if(Input.GetKeyDown(KeyCode.Return)) LoadLevelSelection();
        }

        #endregion

        #region DebugLevelPicker's Internal Methods

        public void MoveSelection(int _newAmount) {
            currentSelectionIndex = Mathf.Clamp(currentSelectionIndex + _newAmount, 0, sceneDatas.Length- 1);

            levelSelectionText.text = $"{sceneDatas[currentSelectionIndex].InternalName}";
            levelNameText.text = $"{sceneDatas[currentSelectionIndex].InternalName}";
            levelPathText.text = $"{sceneDatas[currentSelectionIndex].SceneObject.ScenePath}";
            levelCategoryText.text = $"({(ushort)sceneDatas[currentSelectionIndex].Category}) {sceneDatas[currentSelectionIndex].Category}";
            levelGameModeText.text = "(-1) UNKNOWN"; //TODO: Modify this
            isPackagedText.text = $"{SceneSystem.IsDataValid(sceneDatas[currentSelectionIndex])}";
            levelPreviewTexture.texture = new Texture2D(64, 64); //TODO: Modify This
        }

        private async void LoadLevelSelection() {
            await SceneSystem.LoadSceneAsync(sceneDatas[currentSelectionIndex]);
        }

        #endregion

    }

}