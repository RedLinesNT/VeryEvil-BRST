using System;

namespace VEvil.Core.SceneManagement {

    /// <summary>
    /// Contain the target category of a <see cref="SceneData"/> file.
    /// </summary>
    public enum ESceneCategory : ushort {
        UNDEFINED = 0,
        DEBUG,
        GAMEPLAY,
        MENU,
    }

}