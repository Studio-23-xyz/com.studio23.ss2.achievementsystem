using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using OpenCover.Framework.Model;
using UnityEngine;
using File = System.IO.File;

public class AchivementsManager : MonoBehaviour
{

    [SerializeField]private string SavePath = "";
    private string fileName = "/unlockedAchievementIDs.json";

    public static AchivementsManager instance;


    [SerializeField] private AchievementData[] _achievementData;

    public bool EnableToast;
    public RectTransform ToastContainer;
    public GameObject ToastPrefab;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (string.IsNullOrEmpty(SavePath))
        {
            SavePath = Application.persistentDataPath + fileName;
        }

        if (File.Exists(SavePath)) //available for locally only -> need to give savepath to steam cloud to work on cloud
        {
            LoadAchievements();
        }
        else
        {
            ResetAllUnlockAchievements();
        }


    }

    void Start()
    {
        _achievementData = Resources.LoadAll<AchievementData>($"AchievementData");
    }


    public void ResetAllUnlockAchievements()
    {
        AchievementData[] achievements = Resources.LoadAll<AchievementData>($"AchievementData");
        foreach (var achievement in achievements)
        {
            achievement.isAchieved = false;
        }
    }

    public void UnlockAchievement(string achievementName)
    {

        var achievement = _achievementData.First(t => t.AchievementName == achievementName);
        achievement.isAchieved = true;
        SaveAchievements();

        SteamAchievementManager.Instance.UnlockSteamAchievement(achievement.SteamID);
    }


    [ContextMenu("Save Achievement")]
    private void SaveAchievements()
    {
        AchievementData[] achievements = Resources.LoadAll<AchievementData>($"AchievementData");
        List<string> achievementID = new List<string>();
        foreach (var achievement in achievements)
        {
            if (achievement.isAchieved) achievementID.Add(achievement.AchievementID);
        }
        string jsonData=JsonConvert.SerializeObject(achievementID);

        File.WriteAllText(SavePath,jsonData);

        Debug.Log($"File saved to {SavePath}");
        
    }

    [ContextMenu("Load AchievementData")]
    public void LoadAchievements()
    {
        string jsonData = File.ReadAllText(SavePath);
        List<string> achievementID = JsonConvert.DeserializeObject<List<string>>(jsonData);

        AchievementData[] achievements = Resources.LoadAll<AchievementData>($"AchievementData");

        foreach (var id in achievementID)
        {
            var achievement = achievements.First(r => r.AchievementID == id);
            achievement.isAchieved = true;
          //  _achievementData.Add(achievement);
        }
        
    }

    [ContextMenu("Spawn Toast")]
    public void TestDummyUnlock(string name)
    {
        var achievement = _achievementData.First(t => t.AchievementName == name);
        var toastCard = Instantiate(ToastPrefab, ToastContainer);
        toastCard.GetComponent<AchievementPopupAnimation>().EnablePopUp(achievement.UnlockedIcon,achievement.AchievementName,achievement.AchievementDescription);
    }


}
