using Studio23.SS2.AchievementSystem.Data;
using UnityEngine;

namespace Studio23.SS2.AchievementSystem.Providers
{
	public  abstract class AchievementProvider : MonoBehaviour
	{
		[SerializeField] private IDTableMapper mapper;
		public abstract void Initialize();
		public abstract void UnlockAchievement(string achievementIdentifier);

	}
}