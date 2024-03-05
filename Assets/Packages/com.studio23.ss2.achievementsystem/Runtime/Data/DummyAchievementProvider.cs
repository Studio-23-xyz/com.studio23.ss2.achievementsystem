using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Studio23.SS2.AchievementSystem.Data
{
 
    public class DummyAchievementProvider : AbstractAchievementProvider
    {
        public bool failInit;
        public int errorCode;

        public override UniTask<int> Initialize()
        {
            if(!failInit)
            {
                Debug.Log($"Dummy Provider:\n Initialization<color=green>Success</color>");
                return new UniTask<int>(0);
            }
            else
            {
                Debug.Log($"Dummy Provider:\n Initialization <color=red>Failed</color> with tatus Code of <color=white>{errorCode}</color>");
                return new UniTask<int>(errorCode);
            }
        }

        public override UniTask<int> UpdateAchievementProgress(string id, float progression)
        {
            Debug.Log($"Dummy Provider:\n ID:{id}\n Mapped ID:{_achievementMapper.GetMappedID(id)}\nProgress:{progression}");
            return new UniTask<int>(0);
        }
    }
}