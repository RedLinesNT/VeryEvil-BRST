using System;
using System.Collections.Generic;
using UnityEngine;

namespace VEvil.Core {

    /// <summary>
    /// <see cref="LabelOverride"/> provide the possibility to change the name of a field displayed in the Editor's GUI Inspector only.<br/>
    /// The label defined is displayed in the editor using the <see cref="LabelOverridePropertyDrawer"/>.
    /// </summary>
    /// <remarks>
    /// <see cref="LabelOverride"/> won't work for <see cref="List{T}"/>, <see cref="Array"/>, and structs.
    /// </remarks>
    public class LabelOverride : PropertyAttribute {

        #region Properties

        /// <summary>
        /// The label used to be displayed in the editor.
        /// </summary>
        public string Label { get; private set; } = string.Empty;

        #endregion

        #region LabelOverride's Constructor Method

        /// <summary>
        /// Create a new <see cref="LabelOverride"/> instance for a field.
        /// </summary>
        /// <param name="_labelOverride">The name that'll be displayed in the editor for this field.</param>
        public LabelOverride(string _labelOverride) {
            Label = _labelOverride;
        }

        #endregion

    }

}