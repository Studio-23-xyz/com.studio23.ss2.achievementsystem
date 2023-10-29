using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Studio23.SS2.AchievementSystem.Data
{
	[Serializable]
	[CreateAssetMenu(fileName = "New AchievementData", menuName = "Studio-23/Achievement System/New Achievement")]
	public class AchievementData : ScriptableObject
	{
		public string AchievementName;
		public string AchievementDescription;
        public Texture2D LockedIcon;
		public Texture2D UnlockedIcon;


		public AchievementType Type;
		public float ProgressGoal;
		public bool IsAchieved;
	}


	[Serializable]
	public class AchievementIdData
	{
		public string AchievementProvider;
		public string Id;
	}
}