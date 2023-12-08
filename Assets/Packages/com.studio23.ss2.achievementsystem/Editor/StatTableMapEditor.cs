using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Studio23.SS2.AchievementSystem.Data;
using System.Reflection;
using System;
using System.IO;

public class StatTableMapEditor : EditorWindow
{
    private IDTableMapper AchievementIdTableMap;
    private Dictionary<string, string> defaultValues=new Dictionary<string, string>();
    private string AssetSaveFolderPath = "Assets/Resources/AchievementSystem";

    string scriptPath => Path.Combine("Assets", "Resources", $"StatsTable.cs");


    [MenuItem("Studio-23/Achievement System/Stats Table Map Generator")]
    public static void OpenWindow()
    {
        AchievementTableMapEditor window = GetWindow<AchievementTableMapEditor>("Stats Table Map Generator");
        window.minSize = new Vector2(300, 300);
  
    }

    private void OnGUI()
    {
        GUILayout.Label("Stats Table Map Generator", EditorStyles.boldLabel);

        if(!File.Exists(scriptPath))
        {
            EditorGUILayout.HelpBox("Create Stats Table First", MessageType.Error);
            return;
        }


        if (GUILayout.Button("Create New Stats Table Map"))
        {
            CreateIDTableMap();
        }

        if (AchievementIdTableMap != null)
        {
            GUILayout.Label("Stats Mapping Table", EditorStyles.boldLabel);

            foreach (var IDMap in AchievementIdTableMap.IDMaps)
            {
                GUILayout.BeginHorizontal();
                IDMap.Value = EditorGUILayout.TextField(IDMap.Key,IDMap.Value);
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

        AssetDatabase.CreateAsset(AchievementIdTableMap, $"{AssetSaveFolderPath}/StatIDMap.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

    }

    private void ApplyDefaultValues()
    {
        if (AchievementIdTableMap == null)
        {
            Debug.LogWarning("Stats Table Map is not created.");
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
            Type achievementsTableType = assembly.GetType("Studio23.SS2.AchievementSystem.Data.StatsTable");

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

                Debug.Log("StatsTable.cs properties loaded, default keys created.");
                return; // Exit the loop if AchievementsTable is found
            }
        }

        Debug.LogError("StatsTable.cs class not found.");
    }
}
