using Studio23.SS2.AchievementSystem.Data;
using UnityEngine;

namespace Studio23.SS2.AchievementSystem.Providers
{
	public  abstract class AchievementProvider : MonoBehaviour
	{
		[SerializeField] protected IDTableMapper _achievementMapper;
        [SerializeField] protected IDTableMapper _statsMapper;
        public abstract void Initialize();
		public abstract void UnlockAchievement(string achievementIdentifier);
        public abstract float GetStat(string statName);
        public abstract void SetStat(string statName, float value);
    }
}