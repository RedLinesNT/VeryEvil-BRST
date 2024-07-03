using System;
using UnityEngine;
using VEvil.Core.FE;
using VEvil.FE.Gameplay.HUD;
using VEvil.GameLogic.Buildings;
using VEvil.GameLogic.Player;
using Logger = VEvil.Core.Logger;

namespace VEvil {

    public class GameLoop : MonoBehaviour {

        [SerializeField] private Nexus enemyNexus = null;
        [SerializeField] private Nexus allyNexus = null;

        [SerializeField] private UserLoadout playerLoadout = default;

        private void Awake() {
            allyNexus.Loadout = playerLoadout;
            enemyNexus.OnNexusDestroyed += OnGameEnd;
            
            HeadsUpDisplay _hud = FrontEnd.Create("Game-HUD") as HeadsUpDisplay;
            _hud.WatchLoadout(allyNexus.Loadout);
            _hud.WatchManaContainer(allyNexus.ManaContainer);
            _hud.OnHUDUnitButtonPressedFromLoadout += SpawnUnitFromHUD;
        }

        
        private void OnGameEnd() {
            Logger.Trace("GameLoop PLACEHOLDER", "Game ended, showing reward!");

            FrontEnd.Create("Game-End");
            Time.timeScale = 0f;
        }

        private void SpawnUnitFromHUD(int _loadoutIndex) {
            allyNexus.SpawnUnit(playerLoadout.Loadout[_loadoutIndex].Unit);
        }

    }

}