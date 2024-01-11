using Studio23.SS2.AchievementSystem.Providers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Studio23.SS2.AchievementSystem.Core
{

    public class AchievementSystem : MonoBehaviour
    {
        public static AchievementSystem Instance;

        [Header("Config")]
        [SerializeField]
        private bool InitializeOnStart = true;
        public AchievementProvider _achievementProvider;


        private Queue<(string id, float progression)> _achievementProcessQueue;
        private bool _isProcessing;
        [SerializeField]
        private float _processingSpeed;

        [Header("Events")]
        public UnityEvent OnInitializeComplete;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            if (InitializeOnStart)
            {
                Initialize();
            }
        }


        /// <summary>
        /// Initialize Provider
        /// </summary>
        public void Initialize()
        {
            _achievementProcessQueue = new Queue<(string id, float progression)>();
            _achievementProvider = GetComponent<AchievementProvider>();
            _achievementProvider.OnInitializationComplete.AddListener(()=> OnInitializeComplete?.Invoke());
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
            _achievementProcessQueue.Enqueue((achievementName, progression));
            if (_isProcessing) return;
            StartCoroutine(ProcessAchievements());
        }
        
        
        /// <summary>
        /// Unlocks The achievement
        /// </summary>
        /// <param name="achievementName"></param>

        
        public  void UnlockAchievement(string achievementName)
        {
            UpdateAchievementProgress(achievementName, 100);
        }

        IEnumerator ProcessAchievements()
        {
            _isProcessing = true;
            while (_achievementProcessQueue.Count > 0)
            {
                var (id, progression) = _achievementProcessQueue.Dequeue();
                Debug.Log($"Processing Achievement Id {id} with progression: {progression}");
                _achievementProvider.UpdateAchievementProgress(id, progression);
                yield return new WaitForSeconds(_processingSpeed);
            }
            _isProcessing = false;
        }


    }

}
