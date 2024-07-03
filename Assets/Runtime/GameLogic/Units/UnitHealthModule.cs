using System;
using VEvil.GameLogic.Health;

namespace VEvil.GameLogic.Units {

    /// <summary>
    /// IF YOU SEE THIS PLEASE INFORM L.G (RDNT) ABOUT THIS!!!
    /// </summary>
    public class UnitHealthModule : AHealthModule {

        protected override void OnHealthValueEmpty() {
            Destroy(this.gameObject);
        }

    }

}