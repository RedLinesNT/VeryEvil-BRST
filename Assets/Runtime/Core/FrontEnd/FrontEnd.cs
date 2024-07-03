using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using VEvil.Core.SceneManagement;
using Object = UnityEngine.Object;

namespace VEvil.Core.FE {

    /// <summary>
    /// <see cref="FrontEnd"/> manage <see cref="AFrontEndModule"/>s throughout the game.
    /// </summary>
    public static class FrontEnd {
        
        #region Attributes

        /// <summary>
        /// List of <see cref="FrontEndModuleDefinition"/>s prefabs found in <see cref="FrontEndModuleDefinitionBank"/> files.
        /// </summary>
        private static List<FrontEndModuleDefinition> modulesDefinitions = new List<FrontEndModuleDefinition>();
        /// <summary>
        /// List of <see cref="AFrontEndModule"/>s currently active.
        /// </summary>
        private static List<AFrontEndModule> activeModules = new List<AFrontEndModule>();
        /// <summary>
        /// <see cref="Dictionary{TKey,TValue}"/> containing the <see cref="Action{T}"/> that have been bind to later dispose these links.
        /// </summary>
        private static Dictionary<AFrontEndModule, Action<InputAction.CallbackContext>> inputEvents = new Dictionary<AFrontEndModule, Action<InputAction.CallbackContext>>();

        #endregion

        #region FrontEnd's Initialization Method

        /// <summary>
        /// Initialize the <see cref="FrontEnd"/> system.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)] private static void Initialize() {
            FrontEndModuleDefinitionBank[] _modulesDefinitionBanks = ResourceFetcher.GetResourceFilesFromType<FrontEndModuleDefinitionBank>(); //Find and get every bank files

            for (int i=0; i<_modulesDefinitionBanks.Length; i++) { //Loop through every bank files
                modulesDefinitions.AddRange(_modulesDefinitionBanks[i].Modules);
            }

            SceneSystem.OnSceneLoaded += OnSceneChanged;
            
            Logger.Trace("Front End", $"Found and registered '{modulesDefinitions.Count}' FrontEndModuleDefinitions.");
        }

        #endregion
        
        #region FrontEnd's Registration Methods

        /// <summary>
        /// Register a <see cref="AFrontEndModule"/> into the <see cref="FrontEnd"/> system.
        /// </summary>
        /// <param name="_module">The <see cref="AFrontEndModule"/> to register.</param>
        public static void RegisterModule(AFrontEndModule _module) {
            bool _isRegistered = activeModules.Exists(_mod => _mod.ModuleInstanceID == _module.ModuleInstanceID); //Find if this module is already registered

            if (_isRegistered) {
                Logger.TraceWarning("Front End", $"Unable to register and keep alive the Module '{_module.ModuleInstanceID} ('{_module.name}')'. (Already registered!)");
                Object.Destroy(_module); //Destroy this Module
                return;
            }

            if(_module.InputSettings.InputAction != null) { //If a InputAction has been bind
                Action<InputAction.CallbackContext> _event = delegate(InputAction.CallbackContext _context) { OnModuleInputStateChanged(_module, _context); };

                _module.InputSettings.InputAction.started += _event;
                _module.InputSettings.InputAction.performed += _event;
                _module.InputSettings.InputAction.canceled += _event;
                
                inputEvents.Add(_module, _event); //Add it to later remove the event
                DisableModule(_module);
            }

            activeModules.Add(_module); //Add this Module to the list
            Logger.Trace("Front End", $"Module '{_module.ModuleInstanceID} ('{_module.gameObject.name}')' has been registered!");
        }

        /// <summary>
        /// Unregister a <see cref="AFrontEndModule"/> from the <see cref="FrontEnd"/> system.
        /// </summary>
        /// <param name="_module">The <see cref="AFrontEndModule"/> to unregister.</param>
        public static void UnregisterModule(AFrontEndModule _module) {
            bool _isRegistered = activeModules.Exists(_mod => _mod.ModuleInstanceID == _module.ModuleInstanceID); //Find if this module is registered
            if (!_isRegistered) return;

            if(_module.InputSettings.InputAction != null) { //If a InputAction has been bind
                Action<InputAction.CallbackContext> _event = null; //The event to unbind

                foreach (KeyValuePair<AFrontEndModule, Action<InputAction.CallbackContext>> _inputEvent in inputEvents) {
                    if(_inputEvent.Key == _module) {
                        _event = _inputEvent.Value;
                    }
                }

                if(_event == null) {
                    Logger.TraceWarning("Front End", $"Couldn't found the Input Events bind previously on the Module '{_module.ModuleInstanceID} ('{_module.name}')'.");
                    return;
                }

                _module.InputSettings.InputAction.started -= _event;
                _module.InputSettings.InputAction.performed -= _event;
                _module.InputSettings.InputAction.canceled -= _event;

                inputEvents.Remove(_module); //Remove it from our list of event
            }
            
            activeModules.Remove(_module);
            Logger.Trace("Front End", $"Module '{_module.ModuleInstanceID} ('{_module.name}')' has been unregistered! ({activeModules.Count} Modules still running)");
        }

        #endregion
        
        #region FrontEnd's Internal Methods

        /// <summary>
        /// Called when <see cref="SceneSystem.OnSceneLoaded"/> has been invoked.
        /// Used to load the front end modules that should be instantiated on a specific <see cref="ESceneCategory"/>.
        /// </summary>
        private static void OnSceneChanged(SceneData _newScene) {
            for (int i=0; i<modulesDefinitions.Count; i++) {
                if(modulesDefinitions[i].CreateOnSceneCategoryLoaded.HasFlag(_newScene.Category)) Create(modulesDefinitions[i].Identifier);
            }
        }

        /// <summary>
        /// Called when the <see cref="InputAction"/> referenced in the <see cref="FrontEndModuleInputSettings"/> of a <see cref="AFrontEndModule"/> has been pressed/started/canceled.
        /// </summary>
        private static void OnModuleInputStateChanged(AFrontEndModule _module, InputAction.CallbackContext _context) {
            if(_module.InputSettings.EnableCondition == _module.InputSettings.DisableCondition) { //Explicit check, if the Enable/Disable Phases are the same
                if(_module.gameObject.activeSelf) {
                    DisableModule(_module);
                } else {
                    EnableModule(_module);
                }

                return; //Don't execute anything else
            }

            if(_module.InputSettings.DisableCondition == _context.phase) { //The Disable condition is met
                DisableModule(_module);
            } else { //The Enable condition is met
                EnableModule(_module);
            }
        }

        /// <summary>
        /// Enable a <see cref="AFrontEndModule"/>'s root <see cref="GameObject"/>.
        /// </summary>
        /// <param name="_module">The <see cref="AFrontEndModule"/> to enable.</param>
        private static void EnableModule(AFrontEndModule _module) {
            _module.gameObject.SetActive(true);
            _module.OnModuleShowed();
        }
        
        /// <summary>
        /// Disable a <see cref="AFrontEndModule"/>'s root <see cref="GameObject"/>.
        /// </summary>
        /// <param name="_module">The <see cref="AFrontEndModule"/> to disable.</param>
        private static void DisableModule(AFrontEndModule _module) {
            _module.gameObject.SetActive(false);
            _module.OnModuleHidden();
        }

        #endregion
        
        #region FrontEnd's External Methods
        
        /// <summary>
        /// Try to find and create a <see cref="AFrontEndModule"/> referenced in a <see cref="FrontEndModuleDefinitionBank"/> file based on it's string identifier.
        /// </summary>
        /// <param name="_identifier">The target <see cref="AFrontEndModule"/>'s string identifier.</param>
        public static AFrontEndModule Create(string _identifier) {
            FrontEndModuleDefinition _module = modulesDefinitions.Find(_mod => _mod.Identifier == _identifier); //Find the correct Module

            if (_module == null) { //If no Module was found
                Logger.TraceWarning("Front End", $"Couldn't found any module with '{_identifier}' as its identifier!");
                return null;
            }

            AFrontEndModule _moduleInstance = Object.Instantiate(_module.ModulePrefab); //Instantiate the Module's prefab
            _moduleInstance.gameObject.name = $"{_module.ModulePrefab.name}";
            return _moduleInstance;
        }

        /// <summary>
        /// Try to find and destroy a <see cref="AFrontEndModule"/> currently active based on its string identifier.
        /// </summary>
        /// <param name="_identifier">The target <see cref="AFrontEndModule"/>'s string identifier.</param>
        public static void Destroy(string _identifier) {
            FrontEndModuleDefinition _module = modulesDefinitions.Find(_mod => _mod.Identifier == _identifier); //Find the correct Module
            if (_module == null) return; //If no Module was found

            AFrontEndModule _moduleInstance = activeModules.Find(_mod => _mod == _module.ModulePrefab); //Find the instantiated module
            if (_moduleInstance == null) { //If no Module Instance could be found
                Logger.TraceWarning("Front End", $"Unable to destroy to module '{_identifier}'! (Not instance running found)");
                return;
            }
            
            Object.Destroy(_moduleInstance); //Destroy the module
        }

        /// <summary>
        /// Enable a previously created <see cref="AFrontEndModule"/>.
        /// </summary>
        public static void Show(string _identifier) {
            FrontEndModuleDefinition _module = modulesDefinitions.Find(_mod => _mod.Identifier == _identifier); //Find the correct Module
            if(_module == null) return; //If no Module was found

            AFrontEndModule _moduleInstance = activeModules.Find(_mod => _mod == _module.ModulePrefab); //Find the instantiated module
            if(_moduleInstance == null) return;
            
            EnableModule(_moduleInstance);
        }

        /// <summary>
        /// Disable a previously created <see cref="AFrontEndModule"/>.
        /// </summary>
        public static void Hide(string _identifier) {
            FrontEndModuleDefinition _module = modulesDefinitions.Find(_mod => _mod.Identifier == _identifier); //Find the correct Module
            if(_module == null) return; //If no Module was found

            AFrontEndModule _moduleInstance = activeModules.Find(_mod => _mod == _module.ModulePrefab); //Find the instantiated module
            if(_moduleInstance == null) return;
            
            DisableModule(_moduleInstance);
        }

        #endregion
        
    }

}