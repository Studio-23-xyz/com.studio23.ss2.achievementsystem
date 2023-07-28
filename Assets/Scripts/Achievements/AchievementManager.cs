using System.Collections.Generic;
using UnityEngine;
using File = System.IO.File;
using System.IO;
using System.Linq;
using UnityEngine.SocialPlatforms.Impl;

public class AchievementManager : MonoBehaviour
{

    public static AchievementManager instance;

    public string SavePath;
    private string fileName = "achievementData.json";
    [SerializeField]private List<AchievementData> achievements;
    public List<UnlockableAchievements> _unlockedAchievement;
    public List<ProgressableAchievements> _progressAchievements;
    

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
            SavePath = Application.persistentDataPath + "/achievementData.json";
        }
      
    }

   
    void Start()
    {
        achievements = UIManager.Instance.AcheivementsData;
        LoadAchievements();
    }

    private void LoadAchievements()
    {
        string jsonData = File.ReadAllText(SavePath);
        //achievements = JsonUtility.FromJson<List<AchievementData>>(jsonData);
        Debug.Log(jsonData);
        //if (string.IsNullOrEmpty(jsonData))
        //    return;
        //achievements = JsonUtility.FromJson<List<AchievementData>>(jsonData);

        foreach (var achievement in achievements)
        {
            switch (achievement.Type)
            {
                case AchievementType.Locked:
                    _unlockedAchievement.Add(new UnlockableAchievements(achievement.AchievementID, false));
                    break;
                case AchievementType.Unlocked:
                    _unlockedAchievement.Add(new UnlockableAchievements(achievement.AchievementID, true));
                    break;
                case AchievementType.ProgressTracked:
                    _progressAchievements.Add(new ProgressableAchievements(achievement.AchievementID,0));
                    break;
            }
        }
    }

    public bool IsAchievementUnlocked(string achievementId)
    {
        var data = _unlockedAchievement.FirstOrDefault(x => x.AchievementID.Equals(achievementId));
        if (data != null)
        {
            return data.ISunlocked;
        }
        return false;
    }

    public void UnlockAchievement(string achievementId)
    {
        var data = _unlockedAchievement.FirstOrDefault(x => x.AchievementID.Equals(achievementId));
        if (data != null)
        {
            data.ISunlocked = true;
            SaveAchievements();
        }
    }

    public int GetAchievementProgress(string achievementId)
    {
        var data = _progressAchievements.FirstOrDefault(x=>x.AchievementID.Equals(achievementId));
        if (data != null) 
        {
            return data.ProgressGoal;
        }
        return 0;
    }

    public void UpdateAchievementProgress(string achievementId, int progress)
    {
        var data = _progressAchievements.FirstOrDefault(x => x.AchievementID.Equals(achievementId));
        if (data != null && achievements.Find(x => x.AchievementID == achievementId).Type == AchievementType.ProgressTracked)
        {
            data.ProgressGoal = (int)Mathf.Clamp(progress, 0, achievements.Find(x => x.AchievementID == achievementId).ProgressGoal);
            CheckProgressAchievementComplete(achievementId);
            SaveAchievements();
        }
    }

    private void CheckProgressAchievementComplete(string achievementId)
    {
        var data = _progressAchievements.FirstOrDefault(x => x.AchievementID.Equals(achievementId));
        var progress = data.ProgressGoal;
        var goal = achievements.Find(x => x.AchievementID == achievementId).ProgressGoal;
        if (progress >= goal)
        {
            UnlockAchievement(achievementId);
        }
    }

    private void OnApplicationQuit()
    {
        // Save achievement data when the game closes
         //SaveAchievements();
    }



    private void SaveAchievements()
    {
        if (File.Exists(SavePath))
            File.Delete(SavePath);
        foreach (var achievement in _unlockedAchievement)
        {
            string jsonData = JsonUtility.ToJson(achievement);
            Debug.Log(jsonData);
            File.AppendAllText(SavePath, jsonData);
        }

        foreach (var progress in _progressAchievements)
        {
            string jsonData = JsonUtility.ToJson(progress);
            Debug.Log(jsonData);
            File.AppendAllText(SavePath, jsonData);
        }



    }

    //public void LoadAchievementData()
    //{
    //    string jsonData = File.ReadAllText(SavePath);
    //    if (string.IsNullOrEmpty(jsonData))
    //        return;
    //    achievements = JsonUtility.FromJson<List<AchievementData>>(jsonData);
    //}

}

[System.Serializable]
public class UnlockableAchievements
{
    public string AchievementID;
    public bool ISunlocked;

    public UnlockableAchievements(string id, bool val)
    {
        AchievementID = id;
        ISunlocked = val;
    }
}

[System.Serializable]
public class ProgressableAchievements
{
    public string AchievementID;
    public int ProgressGoal;

    public ProgressableAchievements(string achievementID, int progressGoal)
    {
        AchievementID = achievementID;
        ProgressGoal = progressGoal;
    }
}
