using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Tripartite.Dialogue;
using PlasticPipe.PlasticProtocol.Messages;

namespace Tripartite.EditorUI
{
    [CustomEditor(typeof(Criterion))]
    public class CriterionEditor : Editor
    {
        #region FIELDS
        private SerializedProperty _name;
        private SerializedProperty _key;
        private SerializedProperty _operator;
        private SerializedProperty _value;
        #endregion

        private void OnEnable()
        {
            _name = serializedObject.FindProperty("_name");
            _key = serializedObject.FindProperty("key");
            _operator = serializedObject.FindProperty("criterionOperator");
            _value = serializedObject.FindProperty("value");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical();

            EditorGUILayout.PropertyField(_name);

            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                EditorGUILayout.PropertyField(_key, GUIContent.none);
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical();
                EditorGUILayout.PropertyField(_operator, GUIContent.none);
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical();
                EditorGUILayout.PropertyField(_value, GUIContent.none);
                EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
