using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New AchievementData", menuName = "AchievementData")]
public class AchievementData : ScriptableObject
{
    public string AchievementID;
    public string SteamID;
    public string XBOXID;
    public string AchievementName;
    public string AchievementDescription;
    [JsonIgnore][SerializeField] public Texture2D LockedIcon;
    [JsonIgnore][SerializeField] public Texture2D UnlockedIcon;
    [JsonIgnore] public string LockedIconResourceUrl;
    [JsonIgnore] public string UnlockedIconResourceUrl;

    public AchievementType Type;
    public float ProgressGoal;   //for progress tracker achievement
    public bool isAchieved;

    //private void OnEnable()
    //{
    //    //AchievementID = Guid.NewGuid().ToString();
    //}
}
