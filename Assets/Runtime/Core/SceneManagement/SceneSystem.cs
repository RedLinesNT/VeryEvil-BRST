using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VEvil.Core.SceneManagement {

    /// <summary>
    /// Provide the ability to load/unload scenes via <see cref="SceneData"/> files.
    /// </summary>
    public static class SceneSystem {

        #region Attributes

        /// <summary>
        /// <see cref="List{T}"/> of every <see cref="SceneData"/>s files found upon initialization.
        /// </summary>
        private static List<SceneData> sceneDatas = null;
        /// <summary>
        /// <see cref="Dictionary{TKey,TValue}"/> of current loaded <see cref="Scene"/>s.
        /// </summary>
        private static Dictionary<SceneData, Scene> loadedScenes = new Dictionary<SceneData, Scene>();

        #endregion

        #region Events

        /// <summary>
        /// Invoked when a <see cref="SceneData"/> is about to be loaded.
        /// </summary>
        /// <param name="Arg 01">The <see cref="SceneData"/> that'll be loaded.</param>
        private static Action<SceneData> onSceneBeginLoading = null;
        /// <summary>
        /// Invoked when a <see cref="SceneData"/> has been loaded.
        /// </summary>
        private static Action<SceneData> onSceneLoaded = null;
        /// <summary>
        /// Invoked before <see cref="onSceneLoaded"/>.
        /// </summary>
        private static Action<SceneData> onSceneLoadedEarly = null;

        /// <summary>
        /// Invoked when the active scene has been changed.
        /// </summary>
        /// <param name="Arg 01">The <see cref="SceneData"/> file of the new scene currently active.</param
        private static Action<SceneData> onActiveSceneChanged = null;

        #endregion

        #region Properties

        /// <summary>
        /// <inheritdoc cref="sceneDatas"/>
        /// </summary>
        public static IReadOnlyList<SceneData> SceneDatas { get { return sceneDatas.AsReadOnly(); } }
        /// <summary>
        /// <inheritdoc cref="loadedScenes"/>
        /// </summary>
        public static IReadOnlyDictionary<SceneData, Scene> LoadedScenes { get { return loadedScenes; } }
        /// <summary>
        /// <see cref="KeyValuePair{TKey,TValue}"/> of the current scene active.
        /// </summary>
        public static KeyValuePair<SceneData, Scene> ActiveScene { get; private set; } = new KeyValuePair<SceneData, Scene>();

        /// <summary>
        /// <inheritdoc cref="onSceneBeginLoading"/>
        /// </summary>
        /// <param name="Arg 01"><inheritdoc cref="onSceneBeginLoading"/></param>
        public static event Action<SceneData> OnSceneBeginLoading { add { onSceneBeginLoading += value; } remove { onSceneBeginLoading -= value; } }
        /// <summary>
        /// <inheritdoc cref="onSceneLoaded"/>
        /// </summary>
        public static event Action<SceneData> OnSceneLoaded { add { onSceneLoaded += value; } remove { onSceneLoaded -= value; } }
        /// <summary>
        /// <inheritdoc cref="onSceneLoadedEarly"/>
        /// </summary>
        public static event Action<SceneData> OnSceneLoadedEarly { add { onSceneLoadedEarly += value; } remove { onSceneLoadedEarly -= value; } }
        /// <summary>
        /// <inheritdoc cref="onActiveSceneChanged"/>
        /// </summary>
        /// <param name="Arg 01"><inheritdoc cref="onActiveSceneChanged"/></param>
        public static event Action<SceneData> OnActiveSceneChanged { add { onActiveSceneChanged += value; } remove { onActiveSceneChanged -= value; } }

        #endregion

        #region SceneSystem's Initialization Methods

        /// <summary>
        /// Initialize the <see cref="SceneSystem"/>'s required content.
        /// </summary>
        /// <remarks>
        /// First Initialization Pass.<br/>
        /// Called automatically at the <see cref="RuntimeInitializeLoadType.SubsystemRegistration"/>.
        /// </remarks>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)] private static void FirstInitializationPass() {
            sceneDatas = ResourceFetcher.GetResourceFilesFromType<SceneData>().ToList(); //Find every scene data files available

            if(sceneDatas == null || sceneDatas.Count <= 0) { //If no SceneData files has been found
                Logger.TraceWarning("Scene System", "Unable to found any SceneData files!");
            }
        }

        /// <summary>
        /// Second Initialization Pass.
        /// </summary>
        /// <remarks>
        /// Called automatically at the <see cref="RuntimeInitializeLoadType.BeforeSceneLoad"/>.<br/>
        /// Register this first loaded <see cref="Scene"/> into the <see cref="SceneSystem"/>.
        /// </remarks>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] private static void SecondInitializationPass() {
            Scene _firstScene = SceneManager.GetActiveScene();
            SceneData _firstSceneData = FindDataFromScenePath(_firstScene.path);

            if (_firstSceneData == null) { //If the first scene executed couldn't be found
                Logger.TraceWarning("Scene System", $"The first scene launched '{_firstScene.name}' isn't registered in any SceneData file. You can ignore this warning if this is intended.");
                loadedScenes.Add(FindDataFromName("GENERIC-PLACEHOLDER"), _firstScene);

                return;
            }
            
            //Set the default values of the SceneSystem
            loadedScenes.Add(_firstSceneData, _firstScene);
            SetActiveScene(_firstScene, false);
        }

        #endregion

        #region SceneSystem's External Load Methods

        /// <summary>
        /// Load a <see cref="Scene"/> referenced in a <see cref="SceneData"/> file asynchronously in the background.
        /// </summary>
        /// <remarks>
        /// The new <see cref="Scene"/> loaded will automatically be the new active scene by default.
        /// </remarks>
        /// <param name="_data">The <see cref="SceneData"/> containing the <see cref="Scene"/> to load.</param>
        /// <param name="_loadMode">The <see cref="LoadSceneMode"/>.</param>
        public static async Task LoadSceneAsync(SceneData _data, LoadSceneMode _loadMode = LoadSceneMode.Single) {
            onSceneBeginLoading?.Invoke(_data);
            await Task.Run(async () => { await Task.Delay(1000).ConfigureAwait(false); }); //Wait a little bit here

            Stopwatch _stopwatch = new Stopwatch(); //Create a stopwatch to trace the loading time
            _stopwatch.Start();

            await SceneManager.LoadSceneAsync(_data.SceneObject, LoadSceneMode.Additive); //Load the scene referenced

            if(_loadMode == LoadSceneMode.Single) { //If the LoadMode is set to single
                foreach (KeyValuePair<SceneData, Scene> _loadedScene in loadedScenes) {
                    await SceneManager.UnloadSceneAsync(_loadedScene.Value);
                }
            }
            
            //Clear everything
            loadedScenes.Clear();
            ActiveScene = new KeyValuePair<SceneData, Scene>();

            for (int i=0; i<SceneManager.sceneCount; i++) {
                loadedScenes.Add(FindDataFromScenePath(SceneManager.GetSceneAt(i).path), SceneManager.GetSceneAt(i));
            }

            Scene _newSceneAdded = FindLoadedSceneFromData(_data);
            SetActiveScene(_newSceneAdded);
            
            _stopwatch.Stop();
            Logger.Trace("Scene System", $"Loaded scene '{_data.InternalName}' ({_stopwatch.Elapsed.Milliseconds} milliseconds elapsed).");
            onSceneLoadedEarly?.Invoke(_data); //Invoke this event

            await Task.Run(async () => { await Task.Delay(2500).ConfigureAwait(false); }); //Wait a little bit here
            onSceneLoaded?.Invoke(_data);
        }

        #endregion

        #region ScenSystem's External Methods

        /// <summary>
        /// Define the active <see cref="Scene"/> running.
        /// </summary>
        /// <param name="_scene">To scene to set active.</param>
        /// <param name="_declareOnSceneManager">Should this action be also taken in account into the <see cref="SceneManager"/> Internal API.</param>
        public static void SetActiveScene(Scene _scene, bool _declareOnSceneManager = true) {
            SceneData _sceneData = FindDataFromScenePath(_scene.path);
            ActiveScene = new KeyValuePair<SceneData, Scene>(_sceneData, _scene);
            if(_declareOnSceneManager) SceneManager.SetActiveScene(_scene);
            
            onActiveSceneChanged?.Invoke(_sceneData);
        }

        #endregion

        #region SceneSystem's External Getters

        /// <summary>
        /// Find and return a <see cref="SceneData"/> file.
        /// </summary>
        /// <param name="_internalName">The internal name of the <see cref="SceneData"/> file to find.</param>
        public static SceneData FindDataFromName(string _internalName) {
            return sceneDatas.Find(_sceneData => _sceneData.InternalName == _internalName);
        }

        /// <summary>
        /// Find and return a <see cref="SceneData"/> file.
        /// </summary>
        /// <param name="_scenePath">The <see cref="Scene"/>'s path of the <see cref="SceneReference"/> given in a <see cref="SceneData"/> file.</param>
        public static SceneData FindDataFromScenePath(string _scenePath) {
            return sceneDatas.Find(_sceneData => _sceneData.SceneObject.ScenePath == _scenePath);
        }

        /// <summary>
        /// Find and return a <see cref="IReadOnlyList{SceneData}"/> files.
        /// </summary>
        /// <param name="_sceneCategory">The <see cref="ESceneCategory"/> of the <see cref="SceneData"/>s files to find.</param>
        public static IReadOnlyList<SceneData> FindDatasFromCategory(ESceneCategory _sceneCategory) {
            return sceneDatas.Where(_sceneData => _sceneData.Category == _sceneCategory).ToList();
        }

        public static Scene FindLoadedSceneFromData(SceneData _sceneData) {
            foreach (KeyValuePair<SceneData, Scene> _loadedScene in loadedScenes) {
                if(_loadedScene.Value.path == _sceneData.SceneObject.ScenePath) {
                    return _loadedScene.Value;
                }
            }

            return default;
        }

        /// <summary>
        /// Is the <see cref="Scene"/> referenced in a <see cref="SceneData"/> file valid.
        /// </summary>
        public static bool IsDataValid(SceneData _sceneData) {
            return SceneUtility.GetBuildIndexByScenePath(_sceneData.SceneObject.ScenePath) != -1;
        }

        #endregion

    }

}