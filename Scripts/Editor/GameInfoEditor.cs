using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Imparter.Internal;

namespace Imparter
{
    [CustomEditor(typeof(GameInfo))]
    public class GameInfoEditor : Editor
    {

        private ReorderableList versionList;

        private void OnEnable()
        {
            versionList = new ReorderableList(serializedObject,
                                              serializedObject.FindProperty("versions"),
                                              false, true, true, true);
            versionList.drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                var element = versionList.serializedProperty.GetArrayElementAtIndex(index);
                EditorGUI.PropertyField(rect, element);
            };
            versionList.elementHeightCallback = (index) =>
            {
                var element = versionList.serializedProperty.GetArrayElementAtIndex(index);
                return EditorGUI.GetPropertyHeight(element, true);
            };
            versionList.drawHeaderCallback = (rect) =>
            {
                EditorGUI.LabelField(rect, "Versions");
            };
            versionList.onAddCallback = (list) =>
            {
                var index = list.serializedProperty.arraySize;
                list.serializedProperty.arraySize++;
                var element = versionList.serializedProperty.GetArrayElementAtIndex(index);
                element.FindPropertyRelative("name").stringValue = "";
                element.FindPropertyRelative("notes").stringValue = "";
                element.FindPropertyRelative("completed").boolValue = false;
                element.FindPropertyRelative("completedTime").FindPropertyRelative("high").intValue = 0;
                element.FindPropertyRelative("completedTime").FindPropertyRelative("low").intValue = 0;
            };
            versionList.onCanRemoveCallback = (list) =>
            {
                var element = versionList.serializedProperty.GetArrayElementAtIndex(list.index);
                return !element.FindPropertyRelative("completed").boolValue;
            };
        }

        public GameInfo gameInfo
        {
            get
            {
                return target as GameInfo;
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            PlayerSettings.productName = EditorGUILayout.TextField("Game Name", PlayerSettings.productName);
            gameInfo.gameName = PlayerSettings.productName;

            serializedObject.Update();

            versionList.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }
    }
}