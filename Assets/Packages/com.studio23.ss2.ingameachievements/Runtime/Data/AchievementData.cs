using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Studio23.SS2.InGameAchievementSystem.Data
{
	[Serializable]
	[CreateAssetMenu(fileName = "New AchievementData", menuName = "Studio-23/Ingame Achievement Tool/Achievement Data")]
	public class AchievementData : ScriptableObject
	{
		public List<AchievementIdData> AchievementIDs;
		public string AchievementName;
		public string AchievementDescription;
		[SerializeField] public Texture2D LockedIcon;
		[SerializeField] public Texture2D UnlockedIcon;
		[SerializeField] public Texture2D ImageForXbox;
		[JsonIgnore] public string LockedIconResourceUrl;
		[JsonIgnore] public string UnlockedIconResourceUrl;

		public AchievementType Type;
		public float ProgressGoal; //for progress tracker achievement
		public bool IsAchieved;
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
}