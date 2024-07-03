using System;

namespace VEvil.GameLogic.Teams {

    /// <summary>
    /// Define the teams available in the game for an entity based on the Point-Of-View of the player.
    /// </summary>
    public enum ETeamType : byte {
        /// <summary>
        /// He's a good guy, in the player's team.
        /// </summary>
        FRIENDLY = 0x0,
        /// <summary>
        /// He's NOT a good guy!
        /// </summary>
        ENEMY,
        /// <summary>
        /// Placeholder dummy team.
        /// </summary>
        UNKNOWN,
    }

}