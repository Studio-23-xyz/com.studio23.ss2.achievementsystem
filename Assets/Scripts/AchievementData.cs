using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New AchievementData", menuName = "AchievementData")]
public class AchievementData : ScriptableObject
{
    public string SteamID;
    public string XBOXID;
    public string AchievementName;
    public string AchievementDescription;
    public Sprite Icon;
    public AchievementType Type;
    public float ProgressGoal;   //for progress tracker achievement
}
