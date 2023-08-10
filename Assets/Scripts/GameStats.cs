using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Stats
{
    public bool IsLevelCompleted;
    public int LevelCompleted;
    public float TotalCash;
}


public class GameStats : MonoBehaviour
{
    public static GameStats Instance;
    public Stats CurrentGameStats;
    [SerializeField]
    private Stats _achievementStats;

   
}
