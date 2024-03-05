using Studio23.SS2.AchievementSystem.Data;
using UnityEngine;

namespace Studio23.SS2.AchievementSystem.Core
{
    public class AchievementProviderFactory : MonoBehaviour
    {
        private  AbstractAchievementProvider _dummyProvider;
        private AbstractAchievementProvider _provider;
        internal void Initialize()
        {
            LoadProvidersFromResources();
        }

        private void LoadProvidersFromResources()
        {
            _dummyProvider = Resources.Load<AbstractAchievementProvider>("AchievementSystem/Providers/DummyAchievementProvider");
            _provider= Resources.Load<AbstractAchievementProvider>("AchievementSystem/Providers/AchievementProvider");

        }

        internal AbstractAchievementProvider GetProvider()
        {
            return _provider!= null ? _provider : _dummyProvider;
        }
    }
}