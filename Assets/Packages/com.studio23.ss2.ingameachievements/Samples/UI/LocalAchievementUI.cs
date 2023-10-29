
using Studio23.SS2.AchievementSystem.Data;
using Studio23.SS2.AchievementSystem.Providers;
using UnityEngine;

namespace Studio23.SS2.AchievementSystem.UI
{
	public class LocalAchievementUI : MonoBehaviour
	{

		public AchievementCard AchievementCardPrefab;
		public Transform AchievementCardContainer;

		public LocalAchievements LocalAchievements;



        private async void Start()
        {
			Core.AchievementSystem.instance.Initialize();
            LocalAchievements = Core.AchievementSystem.instance._achievementProvider as LocalAchievements;
		    LocalAchievements.Initialize();
			await LocalAchievements.LoadAchievements();
			DrawInterface();
        }

        private void DrawInterface()
		{

			AchievementData[] achievements = LocalAchievements._achievements;

			if (achievements != null)
			{
				if (AchievementCardPrefab != null)
				{
					foreach (var achievement in achievements)
					{
						var card = Instantiate(AchievementCardPrefab, AchievementCardContainer);
						card.Initialize(achievement);
					}
				}
			}
			else
			{
				Debug.LogError($"Failed to load achievement data");
			}
		}
	}
}
