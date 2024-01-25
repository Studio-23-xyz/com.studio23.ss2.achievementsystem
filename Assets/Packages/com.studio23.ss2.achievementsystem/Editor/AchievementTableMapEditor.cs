using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Studio23.SS2.AchievementSystem.Data;
using System.Reflection;
using System;
using System.IO;

namespace Studio23.SS2.AchievementSystem.Editor
{
    public class AchievementTableMapEditor : EditorWindow
    {
        private IDTableMapper AchievementIdTableMap;
        private Dictionary<string, string> defaultValues = new Dictionary<string, string>();
        private string AssetSaveFolderPath = "Assets/Resources/AchievementSystem";

        string scriptPath => Path.Combine(AssetSaveFolderPath, "AchievementsTable.cs");


        [MenuItem("Studio-23/AchievementSystem/Generators/Table Mapper")]
        public static void OpenWindow()
        {
            AchievementTableMapEditor window = GetWindow<AchievementTableMapEditor>("Achievement Table Map Generator");
            window.minSize = new Vector2(300, 300);

        }

        private void OnGUI()
        {
            GUILayout.Label("Achievement Table Map Generator", EditorStyles.boldLabel);

            if (!File.Exists(scriptPath))
            {
                EditorGUILayout.HelpBox("Create Achievement Table First", MessageType.Error);
                return;
            }


            if (GUILayout.Button("Create New AchievementID Table Map"))
            {
                CreateIDTableMap();
            }

            if (AchievementIdTableMap != null)
            {
                GUILayout.Label("AchievementID Mappning Table", EditorStyles.boldLabel);

                foreach (var IDMap in AchievementIdTableMap.IDMaps)
                {
                    GUILayout.BeginHorizontal();
                    IDMap.Value = EditorGUILayout.TextField(IDMap.Key, IDMap.Value);
                    GUILayout.EndHorizontal();
                }
            }


        }

        private void CreateIDTableMap()
        {
            AchievementIdTableMap = ScriptableObject.CreateInstance<IDTableMapper>();

            AchievementIdTableMap.IDMaps = new List<IDMap>();
            LoadAchievementsTable();
            ApplyDefaultValues();

            if (!Directory.Exists(AssetSaveFolderPath))
            {
                Directory.CreateDirectory(AssetSaveFolderPath);
            }

            AssetDatabase.CreateAsset(AchievementIdTableMap, $"{AssetSaveFolderPath}/AchievementIDMap.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

        }

        private void ApplyDefaultValues()
        {
            if (AchievementIdTableMap == null)
            {
                Debug.LogWarning("Achievement Table Map is not created.");
                return;
            }

            AchievementIdTableMap.IDMaps.Clear();

            foreach (var kvp in defaultValues)
            {
                AchievementIdTableMap.IDMaps.Add(new IDMap { Key = kvp.Key, Value = kvp.Value });
            }

            EditorUtility.SetDirty(AchievementIdTableMap);
            AssetDatabase.SaveAssets();
            Debug.Log("Default values applied and IDTableMap saved.");
        }

        private void LoadAchievementsTable()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                Type achievementsTableType = assembly.GetType("Studio23.SS2.AchievementSystem.Data.AchievementsTable");

                if (achievementsTableType != null)
                {
                    FieldInfo[] fields = achievementsTableType.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

                    defaultValues.Clear();

                    foreach (FieldInfo field in fields)
                    {
                        if (field.FieldType == typeof(string))
                        {
                            defaultValues[field.Name] = "";
                        }
                    }

                    Debug.Log("AchievementsTable.cs properties loaded, default keys created.");
                    return; // Exit the loop if AchievementsTable is found
                }
            }

            Debug.LogError("AchievementsTable.cs class not found.");
        }
    }
}


