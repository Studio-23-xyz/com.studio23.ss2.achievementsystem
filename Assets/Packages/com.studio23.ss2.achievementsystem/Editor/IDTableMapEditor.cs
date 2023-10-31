using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Studio23.SS2.AchievementSystem.Data;
using System.Reflection;
using System;
using System.IO;

public class IDTableMapEditor : EditorWindow
{
    private IDTableMapper idTableMap;
    private Dictionary<string, string> defaultValues=new Dictionary<string, string>();
    private string AssetSaveFolderPath = "Assets/Resources/AchievementSystem";

    [MenuItem("Studio-23/Achievement System/ID Table Map Generator")]
    public static void OpenWindow()
    {
        IDTableMapEditor window = GetWindow<IDTableMapEditor>("ID Table Map Generator");
        window.minSize = new Vector2(300, 300);
  
    }

    private void OnGUI()
    {
        GUILayout.Label("ID Table Map Generator", EditorStyles.boldLabel);

        if (GUILayout.Button("Create New ID Table Map"))
        {
            CreateIDTableMap();
        }

        if (idTableMap != null)
        {
            GUILayout.Label("ID Mappning Table", EditorStyles.boldLabel);

            foreach (var IDMap in idTableMap.IDMaps)
            {
                GUILayout.BeginHorizontal();
                IDMap.Value = EditorGUILayout.TextField(IDMap.Key,IDMap.Value);
                GUILayout.EndHorizontal();
            }
        }

       
    }

    private void CreateIDTableMap()
    {
        idTableMap = ScriptableObject.CreateInstance<IDTableMapper>();
        
        idTableMap.IDMaps = new List<IDMap>();
        LoadAchievementsTable();
        ApplyDefaultValues();

        if (!Directory.Exists(AssetSaveFolderPath))
        {
            Directory.CreateDirectory(AssetSaveFolderPath);
        }

        AssetDatabase.CreateAsset(idTableMap, $"{AssetSaveFolderPath}/IDMap.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

    }

    private void ApplyDefaultValues()
    {
        if (idTableMap == null)
        {
            Debug.LogWarning("ID Table Map is not created.");
            return;
        }

        idTableMap.IDMaps.Clear();

        foreach (var kvp in defaultValues)
        {
            idTableMap.IDMaps.Add(new IDMap { Key = kvp.Key, Value = kvp.Value });
        }

        EditorUtility.SetDirty(idTableMap);
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
