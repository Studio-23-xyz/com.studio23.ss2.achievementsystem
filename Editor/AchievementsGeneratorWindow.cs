using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AchievementsGeneratorWindow : EditorWindow
{
    private class Property
    {
        public string name;
        public bool isEditing;
    }

    private List<Property> properties = new List<Property>();
    private static string _className = "AchievementsTable";
    private static string _nameSpace = "Studio23.SS2.AchievementSystem.Data";
    private Vector2 scrollPosition;


    [MenuItem("Studio-23/Achievement System/ID Table Generator")]
    public static void OpenWindow()
    {
        AchievementsGeneratorWindow window = GetWindow<AchievementsGeneratorWindow>("ID Table Generator");
        window.minSize = new Vector2(250, 150);
    }

    private void OnGUI()
    {
        GUILayout.Label("ID Table Generator", EditorStyles.boldLabel);


        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        for (int i = 0; i < properties.Count; i++)
        {
            GUILayout.BeginHorizontal();

            if (properties[i].isEditing)
            {
                properties[i].name = EditorGUILayout.TextField(properties[i].name);
            }
            else
            {
                GUILayout.Label(properties[i].name, EditorStyles.label);
            }

            if (GUILayout.Button("Edit"))
            {
                properties[i].isEditing = !properties[i].isEditing;
            }

            if (GUILayout.Button("Remove"))
            {
                properties.RemoveAt(i);
            }

            GUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();

        if (GUILayout.Button("Add Property"))
        {
            properties.Add(new Property() { name = "ACHIEVEMENT_NAME" });
        }

        if (GUILayout.Button("Generate"))
        {
            GenerateStringProperties();
        }
    }

    private void GenerateStringProperties()
    {
        string scriptContent = $"namespace {_nameSpace}\n{{\n";

        scriptContent += $"\tpublic static class {_className}\n\t{{\n";

        foreach (var property in properties)
        {
            if (!string.IsNullOrEmpty(property.name))
            {
                scriptContent += $"\t\tpublic static readonly string {property.name} = \"{property.name}\";\n";
            }
        }

        scriptContent += "\t}\n";
        scriptContent += "}";

        string scriptPath = Path.Combine("Assets", $"{_className}.cs");
        File.WriteAllText(scriptPath, scriptContent);
        AssetDatabase.Refresh();
    }
}


