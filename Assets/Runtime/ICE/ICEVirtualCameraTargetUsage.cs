using System;

namespace VEvil.ICE {

    /// <summary>
    /// The target usage of a <see cref="AICEVirtualCamera"/>.
    /// </summary>
    public enum EICEVirtualCameraTargetUsage : byte {
        /// <summary>
        /// This type shouldn't be used.
        /// </summary>
        UNSPECIFIED = 0,
        /// <summary>
        /// The view of a player.
        /// </summary>
        PLAYER_VIEW,
    }

}