using Studio23.SS2.AchievementSystem.Data;
using Studio23.SS2.AchievementSystem.Providers;
using UnityEngine;

namespace Studio23.SS2.AchievementSystem.Core
{
    public class AchievementSystem : MonoBehaviour
    {
        public static AchievementSystem instance;

        public AchievementProvider _achievementProvider;


        void Awake()
        {
            instance = this;
        
        }


        public void Initialize()
        {
            _achievementProvider = AchievementFactory.GetManager();
            _achievementProvider.Initialize();
        }

      
        public async void UnlockAchievement(AchievementData achievementData)
        {
            await _achievementProvider.UnlockAchievementAsync(achievementData.AchievementName);
        }


    }

}
