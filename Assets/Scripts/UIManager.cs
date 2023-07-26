using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;


    public List<AchievementData> AcheivementsData = new List<AchievementData>();
    //public AchievementData[] AchievementData;
    public GameObject AchievementCardPrefab;
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
        AchievementData[] achievements = Resources.LoadAll<AchievementData>("");

        if (achievements != null)
        { 
            AcheivementsData.AddRange(achievements);
            if (AchievementCardPrefab != null)
            {
                foreach (var achievement in achievements)
                {
                   
                    Debug.Log("achi " + achievement.AchievementName);
                    AddAchievementDetails(achievement);
                }
            }

        }
        else
        {
            Debug.LogError("Failed to load achievement data");
        }
    }

    private void AddAchievementDetails(AchievementData achievement)
    {
        _ = Instantiate(AchievementCardPrefab, AchievementCardContainer);
        AchievementCardPrefab.GetComponent<AchievementCard>().AchievementName.text = achievement.AchievementName;
        AchievementCardPrefab.GetComponent<AchievementCard>().AchievementDescription.text = achievement.AchievementDescription;
        AchievementCardPrefab.GetComponent<AchievementCard>().Icon.sprite = achievement.Icon;
    }

  
}
