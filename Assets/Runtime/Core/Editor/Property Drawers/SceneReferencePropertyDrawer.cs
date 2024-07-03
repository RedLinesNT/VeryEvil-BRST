using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using VEvil.Core.SceneManagement;

namespace VEvil.Core.Editor.Properties {

    /// <summary>
    /// Display a <see cref="SceneReference"/> object inside the Editor.<br/>
    /// If the <see cref="SceneAsset"/> given is valid, provide basic buttons to interact with the Scene's role in the Build settings.
    /// </summary>
    [CustomPropertyDrawer(typeof(SceneReference))] public class SceneReferencePropertyDrawer : PropertyDrawer {
        
        #region Attributes

        /// <summary>
        /// The exact name of the asset's Object attribute in the <see cref="SceneReference"/>'s object.
        /// </summary>
        private const string SCENE_ASSET_PROPERTY_STRING = "sceneAsset";
        /// <summary>
        /// The exact name of the scene's path attribute in the <see cref="SceneReference"/>'s object.
        /// </summary>
        private const string SCENE_PATH_PROPERTY_STRING = "scenePath";

        /// <summary>
        /// The padding of the Box.
        /// </summary>
        private static readonly RectOffset boxPadding = EditorStyles.helpBox.padding;
        
        /// <summary>
        /// The size of the Box padding.
        /// </summary>
        private const float PADDING_SIZE = 2f;
        /// <summary>
        /// The height of the box's footer.
        /// </summary>
        private const float FOOTER_HEIGHT = 10f;
        /// <summary>
        /// The height of the lines.
        /// </summary>
        private static readonly float LINE_HEIGHT = EditorGUIUtility.singleLineHeight;
        /// <summary>
        /// The line with the padding.
        /// </summary>
        private static readonly float PADDED_LINE = LINE_HEIGHT + PADDING_SIZE;
        
        #endregion
        
        #region SceneReferencePropertyDrawer's Editor GUI Methods

        /// <summary>
        /// Drawing the <see cref="SceneReference"/> property.
        /// </summary>
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label) {
            EditorGUI.BeginProperty(_position, GUIContent.none, _property); {
                //Here we add the foldout using a single line height, the label and change the value of property.isExpanded
                _property.isExpanded = EditorGUI.Foldout(new Rect(_position.x, _position.y, _position.width, LINE_HEIGHT), _property.isExpanded, _label);

                //Now we want to draw the content only if you unfold this property
                if (_property.isExpanded) {
                    //Reduce the height by one line and move the content one line below
                    _position.height -= LINE_HEIGHT;
                    _position.y += LINE_HEIGHT;

                    SerializedProperty _sceneAssetProperty = GetSceneAssetProperty(_property);

                    //Draw the Box Background
                    _position.height -= FOOTER_HEIGHT;
                    GUI.Box(EditorGUI.IndentedRect(_position), GUIContent.none, EditorStyles.helpBox);
                    _position = boxPadding.Remove(_position);
                    _position.height = LINE_HEIGHT;

                    //Draw the main Object field
                    _label.tooltip = "The actual Scene Asset reference.\nOn serialize this is also stored as the asset's path.";

                    int _sceneControlID = GUIUtility.GetControlID(FocusType.Passive);
                    EditorGUI.BeginChangeCheck(); { //Removed the label here since we already have it in the foldout before
                        _sceneAssetProperty.objectReferenceValue = EditorGUI.ObjectField(_position, _sceneAssetProperty.objectReferenceValue, typeof(SceneAsset), false);
                    }
                    
                    BuildUtils.BuildScene _buildScene = BuildUtils.GetBuildScene(_sceneAssetProperty.objectReferenceValue);
                    if (EditorGUI.EndChangeCheck()) { //If no valid scene asset was selected, reset the stored path accordingly
                        if (_buildScene.scene == null) GetScenePathProperty(_property).stringValue = string.Empty;
                    }

                    _position.y += PADDED_LINE;

                    if (!_buildScene.assetGUID.Empty()) { //Draw the Build Settings Info of the selected Scene
                        DrawSceneInfoGUI(_position, _buildScene, _sceneControlID + 1);
                    }
                }
            }
            EditorGUI.EndProperty();
        }

        /// <summary>
        /// Ensure that what we draw in OnGUI always has the room it needs.
        /// </summary>
        public override float GetPropertyHeight(SerializedProperty _property, GUIContent _label) {
            SerializedProperty _sceneAssetProperty = GetSceneAssetProperty(_property);
            
            //Add an additional line and check if property.isExpanded
            int _lines = _property.isExpanded ? _sceneAssetProperty.objectReferenceValue != null ? 3 : 2 : 1;

            return boxPadding.vertical + LINE_HEIGHT * _lines + PADDING_SIZE * (_lines - 1) + FOOTER_HEIGHT;
        }

        /// <summary>
        /// Draws info box of the provided scene.
        /// </summary>
        private void DrawSceneInfoGUI(Rect _position, BuildUtils.BuildScene _buildScene, int _sceneControlID) {
            bool _readOnly = BuildUtils.IsReadOnly();
            string _readOnlyWarning = _readOnly ? "\n\nWARNING: Build Settings is not checked out and so cannot be modified." : "";

            //Label Prefix
            GUIContent _iconContent = new GUIContent();
            GUIContent _labelContent = new GUIContent();
            
            if (_buildScene.buildIndex == -1) { //Missing from build scenes
                _iconContent = EditorGUIUtility.IconContent("d_winbtn_mac_close");
                _labelContent.text = "NOT In Build";
                _labelContent.tooltip = "This scene is NOT in build settings.\nIt will be NOT included in builds.";
            } else if (_buildScene.scene.enabled) { //In build scenes and enabled
                _iconContent = EditorGUIUtility.IconContent("d_winbtn_mac_max");
                _labelContent.text = "BuildIndex: " + _buildScene.buildIndex;
                _labelContent.tooltip = "This scene is in build settings and ENABLED.\nIt will be included in builds." + _readOnlyWarning;
            } else { //In build scenes and disabled
                _iconContent = EditorGUIUtility.IconContent("d_winbtn_mac_min");
                _labelContent.text = "BuildIndex: " + _buildScene.buildIndex;
                _labelContent.tooltip = "This scene is in build settings and DISABLED.\nIt will be NOT included in builds.";
            }

            //Left status label
            using (new EditorGUI.DisabledScope(_readOnly)) {
                Rect _labelRect = DrawUtils.GetLabelRect(_position);
                Rect _iconRect = _labelRect;
                _iconRect.width = _iconContent.image.width + PADDING_SIZE;
                _labelRect.width -= _iconRect.width;
                _labelRect.x += _iconRect.width;
                EditorGUI.PrefixLabel(_iconRect, _sceneControlID, _iconContent);
                EditorGUI.PrefixLabel(_labelRect, _sceneControlID, _labelContent);
            }

            // Right context buttons
            Rect _buttonRect = DrawUtils.GetFieldRect(_position);
            _buttonRect.width = (_buttonRect.width) / 3;

            string _tooltipMsg = "";
            using (new EditorGUI.DisabledScope(_readOnly)) {
                if (_buildScene.buildIndex == -1) { //NOT in build settings
                    _buttonRect.width *= 2;
                    int _addIndex = EditorBuildSettings.scenes.Length;
                    _tooltipMsg = "Add this scene to build settings. It will be appended to the end of the build scenes as buildIndex: " + _addIndex + "." + _readOnlyWarning;
                    
                    if (DrawUtils.ButtonHelper(_buttonRect, "Add...", "Add (buildIndex " + _addIndex + ")", EditorStyles.miniButtonLeft, _tooltipMsg))
                        BuildUtils.AddBuildScene(_buildScene);
                    _buttonRect.width /= 2;
                    _buttonRect.x += _buttonRect.width;
                } else { //In build settings
                    bool _isEnabled = _buildScene.scene.enabled;
                    string _stateString = _isEnabled ? "Disable" : "Enable";
                    _tooltipMsg = _stateString + " this scene in build settings.\n" + (_isEnabled ? "It will no longer be included in builds" : "It will be included in builds") + "." + _readOnlyWarning;

                    if (DrawUtils.ButtonHelper(_buttonRect, _stateString, _stateString + " In Build", EditorStyles.miniButtonLeft, _tooltipMsg))
                        BuildUtils.SetBuildSceneState(_buildScene, !_isEnabled);
                    _buttonRect.x += _buttonRect.width;

                    _tooltipMsg = "Completely remove this scene from build settings.\nYou will need to add it again for it to be included in builds!" + _readOnlyWarning;
                    if (DrawUtils.ButtonHelper(_buttonRect, "Remove...", "Remove from Build", EditorStyles.miniButtonMid, _tooltipMsg))
                        BuildUtils.RemoveBuildScene(_buildScene);
                }
            }

            _buttonRect.x += _buttonRect.width;

            _tooltipMsg = "Open the 'Build Settings' Window for managing scenes." + _readOnlyWarning;
            if (DrawUtils.ButtonHelper(_buttonRect, "Settings", "Build Settings", EditorStyles.miniButtonRight, _tooltipMsg)) {
                BuildUtils.OpenBuildSettings();
            }

        }

        private static SerializedProperty GetSceneAssetProperty(SerializedProperty _property) {
            return _property.FindPropertyRelative(SCENE_ASSET_PROPERTY_STRING);
        }

        private static SerializedProperty GetScenePathProperty(SerializedProperty _property) {
            return _property.FindPropertyRelative(SCENE_PATH_PROPERTY_STRING);
        }

        private static class DrawUtils {
            
            /// <summary>
            /// Draw a GUI button, choosing between a short and a long button text based on if it fits.
            /// </summary>
            public static bool ButtonHelper(Rect _position, string _msgShort, string _msgLong, GUIStyle _style, string _tooltip = null) {
                GUIContent _content = new GUIContent(_msgLong) { tooltip = _tooltip };

                float _longWidth = _style.CalcSize(_content).x;
                if (_longWidth > _position.width) _content.text = _msgShort;

                return GUI.Button(_position, _content, _style);
            }

            /// <summary>
            /// Given a position rect, get its field portion.
            /// </summary>
            public static Rect GetFieldRect(Rect _position) {
                _position.width -= EditorGUIUtility.labelWidth;
                _position.x += EditorGUIUtility.labelWidth;
                return _position;
            }
            
            /// <summary>
            /// Given a position rect, get its label portion.
            /// </summary>
            public static Rect GetLabelRect(Rect _position) {
                _position.width = EditorGUIUtility.labelWidth - PADDING_SIZE;
                return _position;
            }
            
        }

        /// <summary>
        /// Various BuildSettings interactions.
        /// </summary>
        private static class BuildUtils {

            #region Attributes

            /// <summary>
            /// Time in seconds that we have to wait before we query again when <see cref="IsReadOnly"/> is called.
            /// </summary>
            private static readonly float minCheckWait = 3;

            private static float lastTimeChecked;
            private static bool cachedReadonlyVal = true;

            #endregion

            /// <summary>
            /// A small container for tracking scene data BuildSettings.
            /// </summary>
            public struct BuildScene {
                public int buildIndex;
                public GUID assetGUID;
                public string assetPath;
                public EditorBuildSettingsScene scene;
            }

            /// <summary>
            /// Check if the build settings asset is readonly.<br/>
            /// Caches value and only queries state a max of every <see cref="minCheckWait"/> seconds.
            /// </summary>
            public static bool IsReadOnly() {
                float _curTime = Time.realtimeSinceStartup;
                float _timeSinceLastCheck = _curTime - lastTimeChecked;

                if (!(_timeSinceLastCheck > minCheckWait)) return cachedReadonlyVal;

                lastTimeChecked = _curTime;
                cachedReadonlyVal = QueryBuildSettingsStatus();

                return cachedReadonlyVal;
            }

            /// <summary>
            /// A blocking call to the Version Control system to see if the build settings asset is readonly.<br/>
            /// Use BuildSettingsIsReadOnly for version that caches the value for better responsiveness.
            /// </summary>
            private static bool QueryBuildSettingsStatus() {
                // If no version control provider, assume not readonly
                if (!Provider.enabled) return false;

                // If we cannot checkout, then assume we are not readonly
                if (!Provider.hasCheckoutSupport) return false;

                // Try to get status for file
                Task _status = Provider.Status("ProjectSettings/EditorBuildSettings.asset", false);
                _status.Wait();

                // If no status listed we can edit
                if (_status.assetList is not { Count: 1 }) return true;

                // If is checked out, we can edit
                return !_status.assetList[0].IsState(Asset.States.CheckedOutLocal);
            }

            /// <summary>
            /// For a given <see cref="SceneAsset"/> object reference, extract its build settings data, including buildIndex.
            /// </summary>
            public static BuildScene GetBuildScene(Object _sceneObject) {
                BuildScene _entry = new BuildScene {
                    buildIndex = -1,
                    assetGUID = new GUID(string.Empty)
                };

                if (_sceneObject as SceneAsset == null) return _entry;

                _entry.assetPath = AssetDatabase.GetAssetPath(_sceneObject);
                _entry.assetGUID = new GUID(AssetDatabase.AssetPathToGUID(_entry.assetPath));

                EditorBuildSettingsScene[] _scenes = EditorBuildSettings.scenes;
                for (int i=0; i<_scenes.Length; ++i) {
                    if (!_entry.assetGUID.Equals(_scenes[i].guid)) continue;

                    _entry.scene = _scenes[i];
                    _entry.buildIndex = i;
                    return _entry;
                }

                return _entry;
            }

            /// <summary>
            /// Enable/Disable a given scene in the buildSettings.
            /// </summary>
            public static void SetBuildSceneState(BuildScene _buildScene, bool _enabled) {
                bool _modified = false;
                EditorBuildSettingsScene[] _scenesToModify = EditorBuildSettings.scenes;
                foreach (var _curScene in _scenesToModify.Where(curScene => curScene.guid.Equals(_buildScene.assetGUID))) {
                    _curScene.enabled = _enabled;
                    _modified = true;
                    break;
                }
                if (_modified) EditorBuildSettings.scenes = _scenesToModify;
            }

            /// <summary>
            /// Display Dialog to add a scene to build settings.
            /// </summary>
            public static void AddBuildScene(BuildScene _buildScene, bool _force = false, bool _enabled = true) {
                if (_force == false) {
                    int _selection = EditorUtility.DisplayDialogComplex(
                        "Add Scene To Build",
                        "You are about to add scene at " + _buildScene.assetPath + " To the Build Settings.",
                        "Add as Enabled",       // option 0
                        "Add as Disabled",      // option 1
                        "Cancel (do nothing)"); // option 2

                    switch (_selection) {
                        case 0: // enabled
                            _enabled = true;
                            break;
                        case 1: // disabled
                            _enabled = false;
                            break;
                        default:
                            //case 2: // cancel
                            return;
                    }
                }

                EditorBuildSettingsScene _newScene = new EditorBuildSettingsScene(_buildScene.assetGUID, _enabled);
                List<EditorBuildSettingsScene> _tempScenes = EditorBuildSettings.scenes.ToList();
                _tempScenes.Add(_newScene);
                EditorBuildSettings.scenes = _tempScenes.ToArray();
            }

            /// <summary>
            /// Display Dialog to remove a scene from build settings (or just disable it).
            /// </summary>
            public static void RemoveBuildScene(BuildScene _buildScene, bool _force = false) {
                bool _onlyDisable = false;
                if (_force == false) {
                    int _selection = -1;

                    string _title = "Remove Scene From Build";
                    string _details = $"You are about to remove the following scene from build settings:\n    {_buildScene.assetPath}\n    buildIndex: {_buildScene.buildIndex}\n\nThis will modify build settings, but the scene asset will remain untouched.";
                    string _confirm = "Remove From Build";
                    string _alt = "Just Disable";
                    string _cancel = "Cancel (do nothing)";

                    if (_buildScene.scene.enabled) {
                        _details += "\n\nIf you want, you can also just disable it instead.";
                        _selection = EditorUtility.DisplayDialogComplex(_title, _details, _confirm, _alt, _cancel);
                    } else {
                        _selection = EditorUtility.DisplayDialog(_title, _details, _confirm, _cancel) ? 0 : 2;
                    }

                    switch (_selection) {
                        case 0: // remove
                            break;
                        case 1: // disable
                            _onlyDisable = true;
                            break;
                        default:
                            //case 2: // cancel
                            return;
                    }
                }

                if (_onlyDisable) { //User chose to not remove, only disable the scene
                    SetBuildSceneState(_buildScene, false);
                } else { //User chose to fully remove the scene from build settings
                    List<EditorBuildSettingsScene> _tempScenes = EditorBuildSettings.scenes.ToList();
                    _tempScenes.RemoveAll(_scene => _scene.guid.Equals(_buildScene.assetGUID));
                    EditorBuildSettings.scenes = _tempScenes.ToArray();
                }
            }

            /// <summary>
            /// Open the default Unity Build Settings window.
            /// </summary>
            public static void OpenBuildSettings() {
                EditorWindow.GetWindow(typeof(BuildPlayerWindow));
            }

        }
    
        #endregion

    }

}