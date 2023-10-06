using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Studio23.SS2.IngameAchievements.Data
{
	[Serializable]
	[CreateAssetMenu(fileName = "New AchievementData", menuName = "AchievementData")]
	public class AchievementData : ScriptableObject
	{

		public List<AchievementIdData> AchievementIDs;
		public string AchievementID;
		public string AchievementName;
		public string AchievementDescription;
		[SerializeField] public Texture2D LockedIcon;
		[SerializeField] public Texture2D UnlockedIcon;
		[JsonIgnore] public string LockedIconResourceUrl;
		[JsonIgnore] public string UnlockedIconResourceUrl;

		public AchievementType Type;
		public float ProgressGoal; //for progress tracker achievement
		public bool isAchieved;
	}
}

public enum AchievementProvider
{
	Xbox,
	Playstation,
	Steam
}

[Serializable]
public class AchievementIdData
{
	public AchievementProvider AchievementProvider;
	public string Id;
}