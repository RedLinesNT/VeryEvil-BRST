using System;
using VEvil.Core;
using VEvil.GameLogic.Attack;

namespace VEvil.GameLogic.Units {

    /// <summary>
    /// IF YOU SEE THIS PLEASE INFORM L.G (RDNT) ABOUT THIS!!!
    /// </summary>
    public class UnitAttackModule : AAttackModule {

        #region Attributes

        /// <summary>
        /// The <see cref="AUnitEntity"/> owning this <see cref="UnitAttackModule"/> instance.
        /// </summary>
        private AUnitEntity parentUnit = null;

        #endregion

        #region AAttackModule's Internal Virtual Methods

        protected override void OnAwakeModule() {
            //Auto-assign references
            parentUnit = GetComponent<AUnitEntity>();
            
            //Check references
            if(parentUnit == null) { Logger.TraceError($"Unit Attack Module ({name})", $"Unable to find a component deriving from AUnitEntity! This component should a component deriving from AUnitEntity placed right next to this one. Removing behaviour."); Destroy(this); }
        }

        protected override void OnTargetFound() {
            parentUnit.SetTemporaryTarget(Target.gameObject); //Dummy
        }

        #endregion

    }

}