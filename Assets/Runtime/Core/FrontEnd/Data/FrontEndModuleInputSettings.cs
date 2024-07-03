using System;
using UnityEngine.InputSystem;

namespace VEvil.Core.FE {

    /// <summary>
    /// Contain at which a <see cref="AFrontEndModule"/> should be enabled/disabled.
    /// </summary>
    /// <remarks>
    /// Set <see cref="InputAction"/> to null, the <see cref="FrontEnd"/> system will ignore Inputs.
    /// </remarks>
    public struct FrontEndModuleInputSettings {

        #region Properties

        /// <summary>
        /// The <see cref="InputAction"/> to watch in order to enable/disable the <see cref="AFrontEndModule"/>.
        /// </summary>
        /// <remarks>
        /// <inheritdoc cref="FrontEndModuleInputSettings"/>
        /// </remarks>
        public InputAction InputAction { get; set; }
        /// <summary>
        /// The condition to enable the <see cref="AFrontEndModule"/>.
        /// </summary>
        /// <remarks>
        /// This value is ignored if <see cref="InputAction"/> is null!
        /// </remarks>
        public InputActionPhase EnableCondition { get; set; }
        /// <summary>
        /// The condition to disable the <see cref="AFrontEndModule"/>.
        /// </summary>
        /// <remarks>
        /// This value is ignored if <see cref="InputAction"/> is null!
        /// </remarks>
        public InputActionPhase DisableCondition { get; set; }

        #endregion
        
    }

}