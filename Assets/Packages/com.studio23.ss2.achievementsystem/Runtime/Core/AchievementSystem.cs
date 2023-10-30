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
            Initialize();
        }


        public void Initialize()
        {
            _achievementProvider = GetComponent<AchievementProvider>();
            if (_achievementProvider != null )
            {
                Debug.LogError($"Must Have a provider");
            }
            _achievementProvider.Initialize();
        }

      
        public  void UnlockAchievement(AchievementData achievementData)
        {
           _achievementProvider.UnlockAchievement(achievementData.AchievementName);
        }


    }

}
