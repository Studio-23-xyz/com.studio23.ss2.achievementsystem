using Cysharp.Threading.Tasks;
using Studio23.SS2.AchievementSystem.Data;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Studio23.SS2.AchievementSystem.Core
{
    [RequireComponent(typeof(AchievementProviderFactory))]
    public class AchievementSystem : MonoBehaviour
    {
        public static AchievementSystem Instance;

  
        private AchievementProviderFactory _factory;
        [SerializeField]
        private AbstractAchievementProvider _achievementProvider;


        private Queue<(string id, float progression)> _achievementProcessQueue;
        private bool _isProcessing;
        [SerializeField]
        private float _processingDelay=2f;

        [Header("Events")]
        public UnityEvent OnInitializeComplete;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                _factory=GetComponent<AchievementProviderFactory>();
                _factory.Initialize();
            }
            else
            {
                Destroy(gameObject);
            }
        }


        private void Start()
        {

            _achievementProvider = _factory.GetProvider();
        }



        /// <summary>
        /// Initialize Provider & process queue
        /// </summary>
        public async UniTask Initialize()
        {

            _achievementProcessQueue = new Queue<(string id, float progression)>();
            _achievementProvider.OnInitializationComplete.AddListener(()=> OnInitializeComplete?.Invoke());
            await _achievementProvider.Initialize();
        }

    

        /// <summary>
        /// Unlocks The achievement progression
        /// </summary>
        /// <param name="achievementName"></param>
        /// <param name="progression"> progression is the unlock percentage of a achievement, the value will be withing 0 to 100</param>
        
        public async UniTask UpdateAchievementProgress(string achievementName, float progression)
        {
            _achievementProcessQueue.Enqueue((achievementName, progression));
            if (_isProcessing) return;

            _isProcessing = true;
            while (_achievementProcessQueue.Count > 0)
            {
                var (id, progress) = _achievementProcessQueue.Dequeue();
                Debug.Log($"Processing Achievement Id {id} with progression: {progress}");
                await _achievementProvider.UpdateAchievementProgress(id, progress);
                await UniTask.Delay((int)_processingDelay * 1000, true);
            }
            _isProcessing = false;

        }
        
        
        /// <summary>
        /// Unlocks The achievement to 100%
        /// </summary>
        /// <param name="achievementName"></param>

        
        public async UniTask UnlockAchievement(string achievementName)
        {
           await  UpdateAchievementProgress(achievementName, 100);
        }

   


    }

}
