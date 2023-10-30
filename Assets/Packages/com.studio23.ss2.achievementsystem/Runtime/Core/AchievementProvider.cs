using UnityEngine;

namespace Studio23.SS2.AchievementSystem.Providers
{
	public  abstract class AchievementProvider : MonoBehaviour
	{
		public abstract void Initialize();
		public abstract void UnlockAchievement(string achievementIdentifier);

	}
}