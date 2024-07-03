using UnityEditor;
using UnityEngine;

namespace VEvil.Core.Editor.Properties {

    [CustomPropertyDrawer(typeof(LabelOverride))] public class LabelOverridePropertyDrawer : PropertyDrawer {

        #region LabelOverridePropertyDrawer's Internal Methods

        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label) {
            try {
                LabelOverride _propertyAttribute = attribute as LabelOverride;
                
                if (!IsFieldArray(_property)) { //If the field isn't an array
                    _label.text = _propertyAttribute.Label; //Get the label asked for this field and set it
                } else { //Else, it's an array
                    Logger.TraceWarning("LabelOverride PropertyDrawer", $"{nameof(LabelOverride)} ({_propertyAttribute.Label}) doesn't support arrays!");
                }

                EditorGUI.PropertyField(_position, _property, _label);

            } catch (System.Exception _exception) {
                Logger.TraceException(_exception);
            }
        }
		
        /// <summary>
        /// Return true if the <see cref="SerializedProperty"/> given is an array.
        /// </summary>
        private bool IsFieldArray(SerializedProperty _property) {
            string _path =  _property.propertyPath;
            if(_path.IndexOf('.') == -1 ) return false;
            
            string propName = _path.Substring(0, _path.IndexOf('.'));
            SerializedProperty prop = _property.serializedObject.FindProperty(propName);
            return prop.isArray;
        }

        #endregion
        
    }

}