using Studio23.SS2.AchievementSystem.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Studio23.SS2.AchievementSystem.Providers
{
	public  abstract class AchievementProvider : MonoBehaviour
	{
		[SerializeField] protected IDTableMapper _achievementMapper;
        [SerializeField] protected IDTableMapper _statsMapper;

        public UnityEvent OnInitializationComplete;

        public AchievementData[] achievementDatas;
        public abstract void Initialize();
        public abstract AchievementData GetAchievement(string id);
		public abstract void UpdateAchievementProgress(string id, float progression);
 

    }
}