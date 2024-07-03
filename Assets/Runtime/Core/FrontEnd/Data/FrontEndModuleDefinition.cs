using System;
using UnityEngine;
using VEvil.Core.SceneManagement;

namespace VEvil.Core.FE {

    /// <summary>
    /// <see cref="FrontEndModuleDefinition"/> contain the definition and settings of a <see cref="AFrontEndModule"/>
    /// stored in <see cref="FrontEndModuleDefinitionBank"/> <see cref="UnityEngine.ScriptableObject"/>s files.
    /// </summary>
    [Serializable] public class FrontEndModuleDefinition {

        #region Attributes

        [Header("References")]
        [SerializeField, Tooltip("Front End Module prefab.")] private AFrontEndModule modulePrefab = null;
        [SerializeField, Tooltip("The string identifier of this Front End Module.\nThis value can be used to instantiate the Front End Module specified above.")] private string identifier = string.Empty;

        [Header("Settings")]
        [SerializeField, Tooltip("If a scene with the category specified, this Front End Module will be automatically showed.")] private ESceneCategory createOnSceneCategoryLoaded = ESceneCategory.UNDEFINED;
        
        #endregion

        #region Properties

        /// <summary>
        /// The <see cref="AFrontEndModule"/> prefab.
        /// </summary>
        public AFrontEndModule ModulePrefab { get { return modulePrefab; } }
        /// <summary>
        /// The string identifier of this <see cref="AFrontEndModule"/>.
        /// </summary>
        /// <remarks>
        /// This value can be used to instantiate this <see cref="AFrontEndModule"/>.
        /// </remarks>
        public string Identifier { get { return identifier; } }
        /// <summary>
        /// If a <see cref="SceneData"/> with the <see cref="ESceneCategory"/> specified, the <see cref="ModulePrefab"/> will be automatically created.
        /// </summary>
        public ESceneCategory CreateOnSceneCategoryLoaded { get { return createOnSceneCategoryLoaded; } }

        #endregion
        
    }

}