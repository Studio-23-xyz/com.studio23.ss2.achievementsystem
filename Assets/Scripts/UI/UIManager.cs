using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;


    public List<AchievementData> AcheivementsData = new List<AchievementData>();
    //public AchievementData[] AchievementData;
    public AchievementCard AchievementCardPrefab;
    public Transform AchievementCardContainer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadAchievementData();
    }
    private void LoadAchievementData()
    {
        AcheivementsData.Clear();
        AchievementData[] achievements = Resources.LoadAll<AchievementData>("AchievementData");

        if (achievements != null)
        { 
             AcheivementsData.AddRange(achievements);
            if (AchievementCardPrefab != null)
            {
                foreach (var achievement in achievements)
                {
                    AddAchievementDetails(achievement);
                }

                //for (int i = 0; i < achievements.Length; i++)
                //{
                //    // AddAchievementDetails(achievements[i]);

                //}
            }
        }
        else
        {
            Debug.LogError("Failed to load achievement data");
        }
    }

    private void AddAchievementDetails(AchievementData achievement)
    {
        var card = Instantiate(AchievementCardPrefab, AchievementCardContainer);
        card.AchievementName.text = achievement.AchievementName;
        card.AchievementDescription.text = achievement.AchievementDescription;
        card.LockedTexture.sprite = Sprite.Create(achievement.LockedIcon, new Rect(0, 0, achievement.LockedIcon.width, achievement.LockedIcon.height), new Vector2(0.5f, 0.5f));
    }

  
}
