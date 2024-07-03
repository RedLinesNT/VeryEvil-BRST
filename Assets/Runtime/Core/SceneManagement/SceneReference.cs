#if UNITY_EDITOR //Only include the UnityEditor's content in editor
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;

namespace VEvil.Core.SceneManagement {

    /// <summary>
    /// A wrapper that provides the means to safely serialize <see cref="SceneAsset"/> References.
    /// </summary>
    [System.Serializable] public class SceneReference : ISerializationCallbackReceiver {
        
        #region Attributes

#if UNITY_EDITOR //What is used inside the Editor to select a Scene Asset
        [SerializeField, LabelOverride("Scene file asset"), Tooltip("The Scene asset file.")] private Object sceneAsset = null;
#endif
        
        [SerializeField, Tooltip("The path of the Scene asset.")] private string scenePath = string.Empty; //This should only be set during serialization/deserialization.

        #endregion
        
        #region Properties

#if UNITY_EDITOR
        /// <summary>
        /// (Editor only) Returns if the <see cref="_sceneAsset"/> object given is a valid one.
        /// </summary>
        private bool IsSceneAssetValid { 
            get {
                if(!sceneAsset) return false;
                return sceneAsset is SceneAsset;
            } 
        }
#endif

        /// <summary>
        /// The path of this Scene.
        /// </summary>
        public string ScenePath {
            get {
#if UNITY_EDITOR
                return GetScenePathFromAsset(); //Always use the SceneAsset's path
#else 
                return scenePath; //At runtime, we rely on the stored path value, which we assume has been serialized correctly on Build time.
#endif
            }
            set {
#if UNITY_EDITOR
                sceneAsset = GetSceneAssetFromPath();
#endif
                scenePath = value;
            }
        }
            
        #endregion
        
        #region SceneReference's Implicit Operator Method

        public static implicit operator string(SceneReference _sceneReference) {
            return _sceneReference.ScenePath;
        }

        #endregion
        
        #region SceneReference's Serialize/Deserialize Methods

        /// <summary>
        /// Called to prepare this SceneReference's data for serialization.<br/>
        /// Removed from builds by Unity.
        /// </summary>
        public void OnBeforeSerialize() {
#if UNITY_EDITOR
            HandleBeforeSerialize();
#endif
        }

        /// <summary>
        /// Called to prepare this SceneReference's data for deserialization.<br/>
        /// Removed from builds by Unity.
        /// </summary>
        public void OnAfterDeserialize() {
#if UNITY_EDITOR
            //Impossible to touch the AssetDatabase at this time, deferring this operation.
            EditorApplication.update += HandleAfterDeserialize;
#endif
        }

#if UNITY_EDITOR
        
        public void HandleBeforeSerialize() {
            if(!IsSceneAssetValid && !string.IsNullOrEmpty(scenePath)) { //If the SceneAsset isn't valid but we have the SceneAsset's path, try to get the SceneAsset from its path
                sceneAsset = GetSceneAssetFromPath(); 
                
                if(sceneAsset == null) scenePath = string.Empty; //If no SceneAsset was found, remove the path
                
                EditorSceneManager.MarkAllScenesDirty();
            } else { //The SceneAsset is valid but there's no SceneAsset path set, overwrite it based on the SceneAsset's one.
                scenePath = GetScenePathFromAsset();
            }
        }
        
        public void HandleAfterDeserialize() {
            EditorApplication.update -= HandleAfterDeserialize; //Remove this method from being called
            
            //If the SceneAsset is valid, don't do anything.
            //The SceneAsset's path will always be set based on its path.
            if(IsSceneAssetValid) return;
            if(string.IsNullOrEmpty(scenePath)) return;
            
            sceneAsset = GetSceneAssetFromPath();
            
            //If no SceneAsset was found and its path is invalid, make sure we don't keep theses values with the invalid path.
            if(!sceneAsset) scenePath = string.Empty;
            
            //Mark all scenes dirty if not currently playing
            if(!Application.isPlaying) EditorSceneManager.MarkAllScenesDirty();
        }
        
#endif
        
        #endregion
        
        #region SceneReference's Editor Methods

#if UNITY_EDITOR
        
        /// <summary>
        /// Returns the SceneAsset from its path on this instance.
        /// </summary>
        private SceneAsset GetSceneAssetFromPath() {
            return string.IsNullOrEmpty(scenePath) ? null : AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
        }
        
        /// <summary>
        /// Returns the SceneAsset's path of this instance.
        /// </summary>
        private string GetScenePathFromAsset() {
            return sceneAsset == null ? string.Empty : AssetDatabase.GetAssetPath(sceneAsset);
        }
        
#endif

        #endregion
        
    }

}