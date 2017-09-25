using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Imparter.Internal;

namespace Imparter
{
    [CustomPropertyDrawer(typeof(GameVersion))]
    public class VersionPropertyDrawer : PropertyDrawer
    {
        private const int PADDING = 10;
        private const int TPADDING = 2;

        private const int COMPLETEHEIGHT = 20;

        private const int NOTESHEIGHT = 150;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var str = property.FindPropertyRelative("notes").stringValue;


            return EditorGUI.GetPropertyHeight(property.FindPropertyRelative("name")) +
                    EditorGUI.GetPropertyHeight(property.FindPropertyRelative("versionString")) +
                    NOTESHEIGHT + COMPLETEHEIGHT + PADDING * 2;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var name = property.FindPropertyRelative("name");
            float nameheight = EditorGUI.GetPropertyHeight(name);

            var version = property.FindPropertyRelative("versionString");
            float vheight = EditorGUI.GetPropertyHeight(version);

            var notes = property.FindPropertyRelative("notes");
            float notesheight = NOTESHEIGHT;

            var completed = property.FindPropertyRelative("completed");

            var completedTime = property.FindPropertyRelative("completedTime");

            position = position.withPadding(0, PADDING, TPADDING, PADDING);

            if (completed.boolValue)
            {
                GUI.enabled = false;
            }

            EditorGUI.PropertyField(position.withHeight(nameheight), name, new GUIContent("Name"));
            position = position.fromHeight(nameheight);

            EditorGUI.PropertyField(position.withHeight(vheight), version, new GUIContent("Number"));
            position = position.fromHeight(vheight);

            EditorGUI.PropertyField(position.withHeight(notesheight), notes);
            position = position.fromHeight(notesheight);

            GUI.enabled = true;

            Rect r = position.withHeight(COMPLETEHEIGHT);
            Color c = GUI.color;
            if (completed.boolValue)
            {
                var date = SerialisedLong.toDate(completedTime);
                GUI.Label(r.withWidth(r.width - 30),
                          "Published on " + date.ToShortDateString() + " at " + date.ToShortTimeString());
                GUI.color = Color.red;
                var span = new System.TimeSpan(System.DateTime.Now.Ticks - date.Ticks);
                if (span.Hours < 1 && GUI.Button(r.fromWidth(r.width - 30), "X"))
                {
                    completed.boolValue = false;
                    PlayerSettings.bundleVersion = GameInfo.Get().LatestVersion.versionString;
                }
            }
            else
            {
                GUI.color = Color.green;
                if (GUI.Button(position.withHeight(COMPLETEHEIGHT), "Publish!"))
                {
                    completed.boolValue = true;
                    SerialisedLong.FromDate(completedTime, System.DateTime.Now);
                    PlayerSettings.bundleVersion = version.stringValue;
                }
            }
            GUI.color = c;
        }
    }
}