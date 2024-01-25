using Studio23.SS2.AchievementSystem.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Studio23.SS2.AchievementSystem.Core
{
    public class AchievementProviderFactory : MonoBehaviour
    {
        private Dictionary<PlatformProvider, AbstractAchievementProvider> _providers;

        internal void Initialize()
        {
            _providers = new Dictionary<PlatformProvider, AbstractAchievementProvider>();
            LoadProvidersFromResources();
        }

        internal void LoadProvidersFromResources()
        {
            AbstractAchievementProvider[] providers = Resources.LoadAll<AbstractAchievementProvider>("AchievementSystem/Providers");
            foreach (AbstractAchievementProvider provider in providers)
            {
                _providers[provider.PlatformProvider] = provider;
            }

        }

        internal AbstractAchievementProvider GetProvider(PlatformProvider provider)
        {
            return _providers[provider];
        }
    }
}