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
       
    }

    void Start()
    {
        LoadAchievementData();
    }

    private void LoadAchievementData()
    {
        AchievementData[] achievements = Resources.LoadAll<AchievementData>("AchievementData");

        if (achievements != null)
        { 
             AcheivementsData.AddRange(achievements);
            if (AchievementCardPrefab != null)
            {
                foreach (var achievement in achievements)
                {
                    var card = Instantiate(AchievementCardPrefab, AchievementCardContainer);
                    card.Initialize(achievement);
                }

            }
        }
        else
        {
            Debug.LogError($"Failed to load achievement data");
        }
    }

}
