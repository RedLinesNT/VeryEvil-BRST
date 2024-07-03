using UnityEngine;

namespace VEvil.Core.SceneManagement {

    /// <summary>
    /// Contain the data related to a playable scene.
    /// </summary>
    public class SceneData : ScriptableObject {

        #region Attributes

        [Header("Scene Object References")] 
        [SerializeField, Tooltip("The Scene object reference.")] private SceneReference sceneObject = null;
        [Space(15)] 
        [SerializeField, LabelOverride("Internal Name"), Tooltip("The internal name of this SceneData file.\nThis name can be used to load this scene.")] private string internalName = string.Empty;
        [SerializeField, Tooltip("The category of this SceneData file.")] private ESceneCategory category = ESceneCategory.UNDEFINED;
        
        #endregion

        #region Properties

        /// <summary>
        /// The <see cref="SceneReference"/> referenced on this <see cref="SceneData"/> file.
        /// </summary>
        public SceneReference SceneObject { get { return sceneObject; } }
        /// <summary>
        /// The internal name of this <see cref="SceneData"/> file.
        /// </summary>
        /// <remarks>
        /// This name can be used to load the <see cref="SceneReference"/> given.
        /// </remarks>
        public string InternalName { get { return internalName; } }
        /// <summary>
        /// The <see cref="ESceneCategory"/> of this <see cref="SceneData"/> file.
        /// </summary>
        public ESceneCategory Category { get { return category; } }

        #endregion

    }

}