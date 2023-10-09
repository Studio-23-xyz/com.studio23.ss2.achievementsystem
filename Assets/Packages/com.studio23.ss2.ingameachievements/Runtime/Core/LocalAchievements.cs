using Newtonsoft.Json;
using Studio23.SS2.IngameAchievements.Core;
using Studio23.SS2.IngameAchievements.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Studio23.SS2.IngameAchievements.Local
{
	public class LocalAchievements : AchievementManager
	{
		private AchievementData[] _achievementData;
		private string _localSavePath = Application.persistentDataPath + "/unlockedAchievementIDs.json";

		public AchievementData[] AchievementData
		{
			get { return _achievementData; }
		}

		public override string Name => "Local";

		public override void SetupAchievements()
		{
			_achievementData = Resources.LoadAll<AchievementData>($"AchievementData");
			Debug.Log($"Achievement data: {_achievementData.Length}");
		}

		public override bool UnlockAchievement(string achievementName)
		{
			var achievement = _achievementData.First(t => t.AchievementName == achievementName);
			achievement.IsAchieved = true;
			SaveAchievements();
			return true;
		}

		public override void SaveAchievements()
		{
			AchievementData[] achievements = Resources.LoadAll<AchievementData>($"AchievementData");
			List<string> achievementID = new List<string>();
			foreach (var achievement in achievements)
			{
				if (achievement.IsAchieved)
				{
					achievementID.Add(achievement.AchievementName);
				}
			}
			string jsonData = JsonConvert.SerializeObject(achievementID);
			File.WriteAllText(_localSavePath, jsonData);

			Debug.Log($"File saved to {_localSavePath}");
		}

		public override void LoadAchievements()
		{
			string jsonData = File.ReadAllText(_localSavePath);
			List<string> achievementNames = JsonConvert.DeserializeObject<List<string>>(jsonData);
			AchievementData[] achievements = Resources.LoadAll<AchievementData>($"AchievementData");

			foreach (var name in achievementNames)
			{
				var achievement = achievements.First(r => r.AchievementName == name);
				achievement.IsAchieved = true;
			}
		}
	}
}