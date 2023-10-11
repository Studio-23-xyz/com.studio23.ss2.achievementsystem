using Studio23.SS2.InGameAchievementSystem.Core;
using System.Collections.Generic;
using Studio23.SS2.InGameAchievementSystem.Data;
using UnityEngine;

namespace Studio23.SS2.InGameAchievementSystem.UI
{
	public class UIManager : MonoBehaviour
	{
		public static UIManager Instance;
		public List<AchievementData> AcheivementsData = new List<AchievementData>();
		public AchievementCard AchievementCardPrefab;
		public Transform AchievementCardContainer;

		void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
				DontDestroyOnLoad(gameObject);
			}
			else
			{
				Destroy(gameObject);
			}
		}

		void Start()
		{
			LoadAchievementData();
		}

		private void LoadAchievementData()
		{
			AchievementData[] achievements = Resources.LoadAll<AchievementData>("AchievementData");

			if (achievements != null)
			{
				AcheivementsData.AddRange(achievements);
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
