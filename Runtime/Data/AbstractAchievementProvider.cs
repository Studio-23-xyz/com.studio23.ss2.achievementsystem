using Cysharp.Threading.Tasks;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Studio23.SS2.AchievementSystem.Data
{
	public abstract class AbstractAchievementProvider : ScriptableObject
	{
        [SerializeField]
        public PlatformProvider PlatformProvider;
        [SerializeField] 
        protected IDTableMapper _achievementMapper;

        public UnityEvent OnInitializationComplete;

        protected virtual void OnEnable()
        {
            LoadIDTableMap();
        }
        private void LoadIDTableMap()
        {
            IDTableMapper[] idmaps = Resources.LoadAll<IDTableMapper>("AchievementSystem");
            _achievementMapper = idmaps.Where(r => r.PlatformProvider == PlatformProvider).FirstOrDefault();
            if (_achievementMapper == null)
            {
                Debug.LogError("Achievement Table ID Map not found!, Achievement System will not work");
            }
        }

        public abstract UniTask<int> Initialize();
        public abstract UniTask<AchievementData> GetAchievement(string id);
        public abstract UniTask<AchievementData[]> GetAllAchievements();
		public abstract UniTask<int> UpdateAchievementProgress(string id, float progression);
 
        
    }
}