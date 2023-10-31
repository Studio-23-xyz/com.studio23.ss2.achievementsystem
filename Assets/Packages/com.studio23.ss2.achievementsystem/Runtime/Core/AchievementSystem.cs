using Studio23.SS2.AchievementSystem.Providers;
using UnityEngine;

namespace Studio23.SS2.AchievementSystem.Core
{
    public class AchievementSystem : MonoBehaviour
    {
        public static AchievementSystem Instance;

        public AchievementProvider _achievementProvider;

        private void Awake()
        {
            Instance = this;
            Initialize();
        }


        public void Initialize()
        {
            _achievementProvider = GetComponent<AchievementProvider>();
            if (_achievementProvider == null )
            {
                Debug.LogError($"Must Have a provider");
            }
            _achievementProvider.Initialize();
        }

      
        public  void UnlockAchievement(string achievementName)
        {
           _achievementProvider.UnlockAchievement(achievementName);
        }


    }

}
