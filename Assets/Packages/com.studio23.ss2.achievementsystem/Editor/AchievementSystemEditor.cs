using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEditor;
using UnityEngine;

namespace Studio23.SS2.AchievementSystem.Editor
{
    [CustomEditor(typeof(Core.AchievementSystem))]
    public class AchievementSystemEditor : UnityEditor.Editor
    {
        private bool _showDebugTools = true;
        private int selectedAchievementIndex = 0; // Index of the selected achievement
        private float progressAmount = 100;

        private List<string> AchievementList;

        private void OnEnable()
        {
            AchievementList = LoadAchievementsTable();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Core.AchievementSystem achievementSystem = (Core.AchievementSystem)target;

            // Start Debug Tools foldout section
            _showDebugTools = EditorGUILayout.BeginFoldoutHeaderGroup(_showDebugTools, "Debug Tools");

            if (_showDebugTools)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                GUI.backgroundColor = Color.blue;
                if (GUILayout.Button("Initialize Achievement System"))
                {
                    achievementSystem.Initialize().Forget();
                }

                GUI.backgroundColor = Color.yellow;
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                GUI.backgroundColor = Color.white;

                // Use a dropdown to select the achievement ID
                selectedAchievementIndex = EditorGUILayout.Popup("Select Achievement", selectedAchievementIndex, AchievementList.ToArray());
                string selectedAchievementID = AchievementList[selectedAchievementIndex];

                progressAmount = EditorGUILayout.FloatField("Progress(%)", progressAmount);

                GUI.backgroundColor = Color.green;
                if (GUILayout.Button("Unlock Achievement"))
                {
                    achievementSystem.UnlockAchievement(selectedAchievementID).Forget();
                }

                EditorGUILayout.EndVertical();

                EditorGUILayout.EndVertical();
            }

            // End Debug Tools foldout section
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private List<string> LoadAchievementsTable()
        {
            List<string> list = new List<string>();
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                Type achievementsTableType = assembly.GetType("Studio23.SS2.AchievementSystem.Data.AchievementsTable");

                if (achievementsTableType != null)
                {
                    FieldInfo[] fields = achievementsTableType.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

                    foreach (FieldInfo field in fields)
                    {
                        if (field.FieldType == typeof(string))
                        {
                            list.Add(field.Name);
                        }
                    }

                    Debug.Log("AchievementsTable.cs properties loaded, default keys created.");
                    return list;
                }
            }
            Debug.LogError("AchievementsTable.cs class not found.");
            return list;
        }
    }
}
