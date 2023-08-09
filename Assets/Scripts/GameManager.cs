using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AchievementData[] achievements;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        foreach (var achievement in achievements)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                AchivementsManager.instance.UnlockAchievement(achievement);
            }
        }
    }
}
