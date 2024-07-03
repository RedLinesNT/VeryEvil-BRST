using System.IO;
using UnityEditor;
using UnityEngine;
using VEvil.Core.FE;
using VEvil.Core.SceneManagement;
using VEvil.GameLogic.Units;

namespace VEvil.Editor {

    /// <summary>
    /// Creates <see cref="ScriptableObject"/>s instances in the editor.
    /// </summary>
    public static class EditorScriptableObjectsCreator {
        
        #region SceneSystem's ScriptableObjects

        [MenuItem("VERY EVIL/Scenes/Scene Data file...")] public static void CreateSceneDataFile() {
            SceneData _asset = ScriptableObject.CreateInstance<SceneData>();
            
            //Check if the directory exists
            if (!Directory.Exists("Assets/Resources/Scenes/")) {
                Directory.CreateDirectory("Assets/Resources/Scenes/");
            }
            
            string _assetPath = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/Scenes/New SceneData file.asset");
            AssetDatabase.CreateAsset(_asset, _assetPath);
            AssetDatabase.SaveAssets();
            
            EditorUtility.FocusProjectWindow();
            
            Selection.activeObject = _asset;
        }

        #endregion
        
        #region FrontEnd's ScriptableObjects

        [MenuItem("VERY EVIL/FE/FE Bank Data file...")] public static void CreateFEBankFile() {
            FrontEndModuleDefinitionBank _asset = ScriptableObject.CreateInstance<FrontEndModuleDefinitionBank>();
            
            //Check if the directory exists
            if (!Directory.Exists("Assets/Resources/FE/")) {
                Directory.CreateDirectory("Assets/Resources/FE/");
            }
            
            string _assetPath = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/FE/New FE Bank file.asset");
            AssetDatabase.CreateAsset(_asset, _assetPath);
            AssetDatabase.SaveAssets();
            
            EditorUtility.FocusProjectWindow();
            
            Selection.activeObject = _asset;
        }

        #endregion
               
        #region GameLogic-Units' ScriptableObjects

        [MenuItem("VERY EVIL/Game Logic/Units/Unit Definition Data file...")] public static void CreateUnitDefinitionDataFile() {
            UnitDefinition _asset = ScriptableObject.CreateInstance<UnitDefinition>();
            
            //Check if the directory exists
            if (!Directory.Exists("Assets/Resources/Game Logic/Units")) {
                Directory.CreateDirectory("Assets/Resources/Game Logic/Units");
            }
            
            string _assetPath = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/Game Logic/Units/New Unit Definition Data file.asset");
            AssetDatabase.CreateAsset(_asset, _assetPath);
            AssetDatabase.SaveAssets();
            
            EditorUtility.FocusProjectWindow();
            
            Selection.activeObject = _asset;
        }

        #endregion
        
    }

}