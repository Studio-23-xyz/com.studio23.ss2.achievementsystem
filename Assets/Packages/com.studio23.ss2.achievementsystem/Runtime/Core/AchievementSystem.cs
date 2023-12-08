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

        
        /// <summary>
        /// Unlocks The achievement
        /// </summary>
        /// <param name="achievementName"></param>
        public  void UnlockAchievement(string achievementName)
        {
           _achievementProvider.UnlockAchievement(achievementName);
        }

        /// <summary>
        /// Sets the value of the stat
        /// </summary>
        /// <param name="statName"></param>
        /// <param name="value"></param>
        public void SetStat(string statName,float value)
        {
            _achievementProvider.SetStat(statName,value);
        }

        /// <summary>
        /// Gets the value of the stat
        /// </summary>
        /// <param name="statName"></param>
        /// <returns></returns>
        public float GetStat(string statName)
        {
            return _achievementProvider.GetStat();
        }


    }

}
