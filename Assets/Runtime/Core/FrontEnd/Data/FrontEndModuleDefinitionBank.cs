using UnityEngine;

namespace VEvil.Core.FE {

    /// <summary>
    /// <see cref="FrontEndModuleDefinitionBank"/> contain multiple <see cref="FrontEndModuleDefinition"/>
    /// in a single <see cref="ScriptableObject"/>.
    /// </summary>
    public class FrontEndModuleDefinitionBank : ScriptableObject {

        #region Attributes

        [SerializeField, Tooltip("The internal name for this Bank file.")] private string bankInternalName = string.Empty;
        [SerializeField, Tooltip("List of FrontEndModuleDefinition.")] private FrontEndModuleDefinition[] modules = null;

        #endregion

        #region Properties

        /// <summary>
        /// The internal name of this <see cref="FrontEndModuleDefinitionBank"/> file.
        /// </summary>
        public string InternalName { get { return bankInternalName; } }
        /// <summary>
        /// Array of <see cref="FrontEndModuleDefinition"/> referenced under this <see cref="FrontEndModuleDefinitionBank"/> file.
        /// </summary>
        public FrontEndModuleDefinition[] Modules { get { return modules; } }

        #endregion

    }

}