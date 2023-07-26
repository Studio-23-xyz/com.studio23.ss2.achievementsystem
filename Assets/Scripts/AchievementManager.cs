using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class AchievementManager : MonoBehaviour
{

    public static AchievementManager instance;

    public List<AchievementData> achievements = new List<AchievementData>();
    private Dictionary<string,bool> _unlockedAchievement = new Dictionary<string,bool>();
    private Dictionary<string,int> _progressAchievements = new Dictionary<string,int>();

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
        LoadAchievements();
    }

    private void LoadAchievements()
    {
        foreach (var achievement in achievements)
        {
            switch (achievement.Type)
            {
                case AchievementType.Locked:
                    _unlockedAchievement.Add(achievement.AchievementID, false);
                    break;
                case AchievementType.Unlocked:
                    _unlockedAchievement.Add(achievement.AchievementID, true);
                    break;
                case AchievementType.ProgressTracked:
                    _progressAchievements.Add(achievement.AchievementID, 0);
                    break;
            }
        }
    }

    public bool IsAchievementUnlocked(string achievementId)
    {
        if (_unlockedAchievement.ContainsKey(achievementId))
        {
            return _unlockedAchievement[achievementId];
        }
        return false;
    }

    public void UnlockAchievement(string achievementId)
    {
        if (_unlockedAchievement.ContainsKey(achievementId))
        {
            _unlockedAchievement[achievementId] = true;
            SaveAchievements();
        }
    }

    public int GetAchievementProgress(string achievementId)
    {
        if (_progressAchievements.ContainsKey(achievementId))
        {
            return _progressAchievements[achievementId];
        }
        return 0;
    }

    public void UpdateAchievementProgress(string achievementId, int progress)
    {
        if (_progressAchievements.ContainsKey(achievementId) && achievements.Find(x => x.AchievementID == achievementId).Type == AchievementType.ProgressTracked)
        {
            _progressAchievements[achievementId] = (int)Mathf.Clamp(progress, 0, achievements.Find(x => x.AchievementID == achievementId).ProgressGoal);
            CheckProgressAchievementComplete(achievementId);
            SaveAchievements();
        }
    }

    private void CheckProgressAchievementComplete(string achievementId)
    {
        var progress = _progressAchievements[achievementId];
        var goal = achievements.Find(x => x.AchievementID == achievementId).ProgressGoal;
        if (progress >= goal)
        {
            UnlockAchievement(achievementId);
        }
    }

    private void SaveAchievements()
    {
        
    }


}
