using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SocialPlatforms.Impl;

public class CSVDataManager : EditorWindow
{
    private TextAsset csvTextAsset;
    private List<string[]> csvData = new List<string[]>();
    private float progress = 0.0f;
    private bool isProcessing = false;
    private string debugLog = "";
    private Vector2 debugLogScrollPosition = Vector2.zero;

    [MenuItem("Window/CSV Editor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(CSVDataManager), false, "CSV Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("CSV Editor", EditorStyles.boldLabel);

        GUILayout.Space(10);

        GUILayout.Label("CSV File Path:");
        EditorGUILayout.BeginHorizontal();
        csvTextAsset = EditorGUILayout.ObjectField(csvTextAsset, typeof(TextAsset), false) as TextAsset;

        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10);

        if (GUILayout.Button("Process Data"))
        {
            LoadCsvData();
        }

        if (GUILayout.Button("Delete Data"))
            DeleteDirectory();
        GUILayout.Space(10);

        if (isProcessing)
        {
            Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
            EditorGUI.ProgressBar(rect, progress, $"{(progress * 100):F2}%");
        }
        GUILayout.Space(20);
        GUILayout.Label("Download Log:", EditorStyles.boldLabel);
        debugLogScrollPosition = GUILayout.BeginScrollView(debugLogScrollPosition, GUILayout.Height(100));
        GUILayout.TextArea(debugLog, GUILayout.ExpandHeight(true));
        GUILayout.EndScrollView();
    }

    public TextAsset CSVFile;

    private AchievementData _data;

    [MenuItem("CSV Reader/Debug Init")]
    public void Initialize()
    {
        if (!Directory.Exists("Assets/Resources/AchievementData/"))
            Directory.CreateDirectory("Assets/Resources/AchievementData/");

        if (!Directory.Exists("Assets/Resources/Icons/"))
        {
            Directory.CreateDirectory("Assets/Resources/Icons/UnlockedIcons");
            Directory.CreateDirectory("Assets/Resources/Icons/LockedIcons");
        }
    }

    [MenuItem("CSV Reader/LoadCSVData")]
    private async void LoadCsvData()
    {
        isProcessing = true;
        Initialize();

        string[] lines = csvTextAsset.text.Split('\n');

        for (int i = 1; i < lines.Length; i++) // Skip the header row (i = 0)
        {
            string[] values = lines[i].Split(',');


            // Parse data from CSV and assign it to the new AchievementData instance
            //Debug.Log("ID:" + values[0].Trim('"') + " Name:" + values[1] + " Description:" + values[3]);

            _data = ScriptableObject.CreateInstance<AchievementData>();

             EditorUtility.SetDirty(_data);
           // Undo.RecordObject(_data, "achievement_Recorded");
            _data.AchievementID = values[0].Trim('"');
            _data.AchievementName = values[1].Trim('"');
            _data.AchievementDescription = values[2].Trim('"');
            _data.AchievementID = Guid.NewGuid().ToString();
            _data.UnlockedIconResourceUrl = values[3].Trim('"');
            _data.LockedIconResourceUrl = values[4].Trim('"');

            //Store Data
            string path = $"Assets/Resources/AchievementData/{_data.AchievementName}.asset";
            AssetDatabase.CreateAsset(_data, path);
            await DownloadAndSaveSprites();
            debugLog += $"Achievement data created and saved at {path}";
            debugLog += "\n";
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
            progress = (float)i / lines.Length;
            Repaint();
        }

        await AssignTextures();
        progress = 0f;
        isProcessing = false;
    }

    private async UniTask DownloadAndSaveSprites()
    {
        Texture2D icon = null;
        icon = await LoadImageData(_data.UnlockedIconResourceUrl);
        await SaveTexture(icon, false);
        icon = await LoadImageData(_data.LockedIconResourceUrl);
        await SaveTexture(icon, true);
    }


    private async UniTask SaveTexture(Texture2D icon, bool isLocked)
    {
#if UNITY_EDITOR
        string savePath = isLocked ? $"Assets/Resources/Icons/LockedIcons" : $"Assets/Resources/Icons/UnlockedIcons";
        byte[] bytes = icon.EncodeToPNG();
        string fileName = $"{_data.AchievementName}.png";
        string filePath = Path.Combine(savePath, fileName);
        await File.WriteAllBytesAsync(filePath, bytes);
#endif
    }


    private async UniTask<Texture2D> LoadImageData(string imageUrl)
    {
        using UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error while loading image: " + request.error);
        }
        else
        {
            return DownloadHandlerTexture.GetContent(request);
        }

        return null;
    }

    [MenuItem("CSV Reader/AssignTextures")]
    public async UniTask AssignTextures()
    {
        AchievementData[] loadedAchievements = Resources.LoadAll<AchievementData>($"AchievementData");
        Debug.Log($"{loadedAchievements.Length} file downloaded completed");
        foreach (var achievement in loadedAchievements)
        {
            achievement.UnlockedIcon = Resources.Load<Texture2D>($"Icons/UnlockedIcons/{achievement.AchievementName}");
            achievement.LockedIcon = Resources.Load<Texture2D>($"Icons/LockedIcons/{achievement.AchievementName}");
            AssetDatabase.SaveAssetIfDirty(achievement);
            AssetDatabase.RefreshSettings();
            AssetDatabase.Refresh();
            
        }
    }

    public void DeleteDirectory()
    {
        Directory.Delete($"Assets/Resources/AchievementData", true);
        Directory.Delete($"Assets/Resources/Icons", true);
        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();
    }
}