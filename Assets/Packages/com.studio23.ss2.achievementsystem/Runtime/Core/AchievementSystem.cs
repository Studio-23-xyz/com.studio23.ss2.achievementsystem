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


        /// <summary>
        /// Initialize Provider
        /// </summary>
        public void Initialize()
        {
            _achievementProvider = GetComponent<AchievementProvider>();
            _achievementProvider?.Initialize();
        }

    
        public AchievementData GetAchievement(string id)
        {
            return _achievementProvider.GetAchievement(id);
        }
         

        /// <summary>
        /// Unlocks The achievement progression
        /// </summary>
        /// <param name="achievementName"></param>
        /// <param name="progression"> progression is the unlock percentage of a achievement, the value will be withing 0 to 100</param>
        
        public  void UpdateAchievementProgress(string achievementName, float progression)
        {
           _achievementProvider.UpdateAchievementProgress(achievementName, progression);
        }
        
        
        /// <summary>
        /// Unlocks The achievement
        /// </summary>
        /// <param name="achievementName"></param>

        
        public  void UnlockAchievement(string achievementName)
        {
            UpdateAchievementProgress(achievementName, 100);
        }

      


    }

}