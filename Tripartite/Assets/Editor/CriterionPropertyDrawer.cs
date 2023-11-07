using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tripartite.Dialogue;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Tripartite.EditorUI
{
    [CustomPropertyDrawer(typeof(Criterion))]
    public class CriterionPropertyDrawer : PropertyDrawer
    {
        private int index = 0;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Set the label
            label = EditorGUI.BeginProperty(position, label, property);

            // Get all the criteria
            Dictionary<string, Criterion> criteria = EditorHelp.GetCriteria();

            // Check the correct property types and set the index
            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {
                index = criteria.Values.ToList().IndexOf(property.objectReferenceValue as Criterion);
            } else if(property.propertyType == SerializedPropertyType.String)
            {
                index = criteria.Keys.ToList().IndexOf(property.stringValue);
            } else
            {
                EditorGUI.LabelField(position, label.text, "[Criterion]");
                return;
            }

            // Make sure index is always at least 0
            if (index < 0)
                index = 0;

            // Create the popup and assign
            index = EditorGUI.Popup(position, label.text, index, criteria.Keys.ToArray());

            // Check the correct property types and set the index
            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {
                property.objectReferenceValue = criteria[criteria.Keys.ToArray()[index]];
            }
            else if (property.propertyType == SerializedPropertyType.String)
            {
                property.stringValue = criteria.Keys.ToArray()[index];
            }
        }
    }
}
