using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Studio23.SS2.AchievementSystem.Data
{
	public abstract class AbstractAchievementProvider : ScriptableObject
	{

        [SerializeField] 
        protected IDTableMapper _achievementMapper;

        public UnityEvent OnInitializationComplete;

        protected virtual void OnEnable()
        {
            LoadIDTableMap();
        }
        private void LoadIDTableMap()
        {
            _achievementMapper = Resources.Load<IDTableMapper>("AchievementSystem/IDMap");

            if (_achievementMapper == null)
            {
                Debug.LogError("Achievement Table ID Map not found!, Achievement System will not work");
            }
        }

        public abstract UniTask<int> Initialize();
		public abstract UniTask<int> UpdateAchievementProgress(string id, float progression);
 
        
    }
}