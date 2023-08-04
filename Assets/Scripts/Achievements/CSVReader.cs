using Cysharp.Threading.Tasks;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class CSVReader : MonoBehaviour
{
    public TextAsset CSVFile;

    private AchievementData _data;

    [ContextMenu("Debug Init")]
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


    [ContextMenu("LoadCSVData")]
    private async void LoadCsvData()
    {
        Initialize();


        string[] lines = CSVFile.text.Split('\n');

        for (int i = 1; i < lines.Length; i++) // Skip the header row (i = 0)
        {
            string[] values = lines[i].Split(',');


            // Parse data from CSV and assign it to the new AchievementData instance
            Debug.Log("ID:" + values[0].Trim('"') + " Name:" + values[1] + " Description:" + values[3]);

            _data = ScriptableObject.CreateInstance<AchievementData>();

            EditorUtility.SetDirty(_data);
            _data.AchievementID = values[0].Trim('"');
            _data.AchievementName = values[1].Trim('"');
            _data.AchievementDescription = values[2].Trim('"');
            _data.AchievementID = Guid.NewGuid().ToString();
            _data.UnlockedIconResourceUrl = values[3].Trim('"');
            _data.LockedIconResourceUrl = values[4].Trim('"');


            //Store Data
            string path = $"Assets/Resources/AchievementData/{_data.AchievementName}.asset";
            AssetDatabase.CreateAsset(_data, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            await DownloadAndSaveSprites();
            Debug.Log($"Achievement data created and saved at {path}");
            AssetDatabase.Refresh();
        }

        await AssignTextures();
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

    [ContextMenu("AssignTextures")]
    public async UniTask AssignTextures()
    {
        AchievementData[] loadedAchievements = Resources.LoadAll<AchievementData>($"AchievementData");
        Debug.Log($"{loadedAchievements.Length} file downloaded completed");
        foreach (var achievement in loadedAchievements)
        {
            achievement.UnlockedIcon =
                Resources.Load<Texture2D>($"Icons/UnlockedIcons/{achievement.AchievementName}");
            achievement.LockedIcon =
                Resources.Load<Texture2D>($"Icons/LockedIcons/{achievement.AchievementName}");
        }
    }

}
