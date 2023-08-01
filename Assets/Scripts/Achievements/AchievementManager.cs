using System;
using System.Collections.Generic;
using UnityEngine;
using File = System.IO.File;
using System.IO;
using UnityEngine.SocialPlatforms.Impl;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;

    public AchievementData[] data;
    public Dictionary<string, AchievementData> achievements = new Dictionary<string, AchievementData>();
    public Dictionary<string, int> dummy = new Dictionary<string, int>();
    public string saveFileName = "achievements.json";

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
    }

    void Start()
    {
        dummy.Add("a", 1);
        dummy.Add("bv", 1);
        dummy.Add("e", 1);
        dummy.Add("w", 1);
        LoadAchievementDataFromResources();
       // SaveAchievementData();
        // LoadAchievementDataFromResources();
       // LoadAchievementData();

    }

    public void LoadAchievementDataFromResources()
    {
        AchievementData[] loadedAchievements = Resources.LoadAll<AchievementData>("Achievements");
        data = loadedAchievements;

        foreach (var achievement in loadedAchievements)
        {
            if (!achievements.ContainsKey(achievement.AchievementID))
            {
                achievements.Add(achievement.AchievementID, achievement);
            }
        }

        Debug.Log("Achievement data loaded from Resources folder.");
    }




    public void UnlockAchievement(string achievementID)
    {
        if (achievements.TryGetValue(achievementID, out AchievementData achievement) && !achievement.isAchieved)
        {
            achievement.isAchieved = true;
            SaveAchievementData();
            Debug.Log("Unlocked Achievement: " + achievement.AchievementName);
        }
    }

    public bool IsAchievementUnlocked(string achievementID)
    {
        if (achievements.TryGetValue(achievementID, out AchievementData achievement))
        {
            return achievement.isAchieved;
        }

        return false;
    }

    public void SetAchievementProgress(string achievementID, int progress)
    {
        if (achievements.TryGetValue(achievementID, out AchievementData achievement) && achievement.Type == AchievementType.ProgressTracked)
        {
            achievement.ProgressGoal = progress;
            achievement.isAchieved = true;
            SaveAchievementData();
            Debug.Log("Unlocked Achievement: " + achievement.AchievementName);
        }
    }

    public AchievementData GetAchievement(string achievementID)
    {
        if (achievements.TryGetValue(achievementID, out AchievementData achievement))
        {
            return achievement;
        }

        return null;
    }


    //public void SaveAchievementData()
    //{
    //    string jsonData = JsonUtility.ToJson(this);
    //    string savePath = Path.Combine(Application.persistentDataPath, saveFileName);
    //    File.WriteAllText(savePath, jsonData);
    //    Debug.Log("Achievement data saved to: " + savePath);
    //}

    [ContextMenu("Save")]
    public void SaveAchievementData()
    {
        string jsonData = JsonUtility.ToJson(dummy);
        string savePath = Path.Combine(Application.persistentDataPath, saveFileName);
        File.WriteAllText(savePath, jsonData);
        Debug.Log("Achievement data saved to: " + savePath);
    }

    public void LoadAchievementData()
    {
        string loadPath = Path.Combine(Application.persistentDataPath, saveFileName);

        if (File.Exists(loadPath))
        {
            string jsonData = File.ReadAllText(loadPath);
            achievements = JsonUtility.FromJson<Dictionary<string, AchievementData>>(jsonData);
            Debug.Log("Achievement data loaded from: " + loadPath);
        }
        else
        {
            Debug.LogWarning("No achievement data JSON file found at: " + loadPath);
        }
    }
}
