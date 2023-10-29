using Cysharp.Threading.Tasks;
using Studio23.SS2.AchievementSystem.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Studio23.SS2.AchievementSystem.Providers
{
	public class LocalAchievements : AchievementProvider
	{

        public override string Name => "Local";
		public override int Priority => 0;

        private string _localSavePath => Path.Combine( Application.persistentDataPath,"Achievement System");
		private string _localFileName => "UnlockedAchievements";

		public AchievementData[] _achievements;



        public override void Initialize()
        {
			_achievements = Resources.LoadAll<AchievementData>("Achievement System/AchievementData");
        }

        public override async UniTask<bool> UnlockAchievementAsync(string achievementName)
		{
			var achievement = _achievements.First(t => t.AchievementName == achievementName);
			achievement.IsAchieved = true;
			await SaveAchievementsAsync();
			return true;
		}

		public async UniTask SaveAchievementsAsync()
		{
			
			List<string> achievementNames = new List<string>();
			foreach (var achievement in _achievements)
			{
				if (achievement.IsAchieved)
				{
					achievementNames.Add(achievement.AchievementName);
				}
			}

			await SaveSystem.Core.SaveSystem.Instance.SaveData(achievementNames, _localFileName, _localSavePath,".pty",true);
			

			Debug.Log($"File saved to {_localSavePath}");
		}

		public async UniTask LoadAchievements()
		{

			List<string> achievementNames = await SaveSystem.Core.SaveSystem.Instance.LoadData< List<string>>(_localFileName,_localSavePath,".pty",true);
			foreach (var name in achievementNames)
			{
				var achievement = _achievements.First(r => r.AchievementName == name);
				achievement.IsAchieved = true;
			}
		}

        
    }
}