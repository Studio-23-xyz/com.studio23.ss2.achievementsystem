using Studio23.SS2.IngameAchievements.Data;
using UnityEngine;

namespace Studio23.SS2.IngameAchievements.Core
{
	public class GameStats : MonoBehaviour
	{
		public static GameStats Instance;
		public Stats CurrentGameStats;
		[SerializeField] private Stats _achievementStats;

		[ContextMenu("unlock level complete")]
		public void LevelComplete()
		{
			AchivementsManager.instance.TestDummyUnlock("Cultist");
		}
	}
}
