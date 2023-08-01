using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int balloonPoped = 0;
    string id;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        id = Resources.Load<AchievementData>("Achievements/Forsaken").AchievementID;

        if (Input.GetKeyDown(KeyCode.E))
        {
            // balloonPoped++;
           
            AchievementManager.instance.UnlockAchievement(id);

        }

        if (Input.GetKeyDown(KeyCode.S))
        {
           Debug.Log(AchievementManager.instance.IsAchievementUnlocked(id));
            // Debug.Log(AchievementManager.instance._unlockedAchievement[id]);
        }
    }
}
