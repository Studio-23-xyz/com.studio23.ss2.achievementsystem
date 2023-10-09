using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Studio23.SS2.IngameAchievements.Data
{
	[Serializable]
	[CreateAssetMenu(fileName = "New AchievementData", menuName = "Studio-23/Ingame Achievement Tool/Achievement Data")]
	public class AchievementData : ScriptableObject
	{
#if STUDIO23_INGAMEACHIEVEMENTS_THIRDPARTY_INSTALLED
		public List<AchievementIdData> AchievementIDs;
#endif
		public string AchievementName;
		public string AchievementDescription;
		[SerializeField] public Texture2D LockedIcon;
		[SerializeField] public Texture2D UnlockedIcon;
#if STUDIO23_INGAMEACHIEVEMENTS_THIRDPARTY_INSTALLED
		[JsonIgnore] public string LockedIconResourceUrl;
		[JsonIgnore] public string UnlockedIconResourceUrl;
#endif
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