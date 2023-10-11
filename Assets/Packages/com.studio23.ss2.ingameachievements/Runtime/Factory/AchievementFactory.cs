using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Studio23.SS2.InGameAchievementSystem.Core
{
	public static class AchievementFactory
	{
		private static AchievementManager m_achievementManager;

		private static void InitializeFactory()
		{
			if (m_achievementManager != null)
				return;

#if INGAMEACHIEVEMENT_USE_THIRDPARTY
			Assembly target = Assembly.Load("com.studio23.ss2.ingameachievementsystem.thirdparty");
			Debug.Log($"Using third party assembly");
#else
			Assembly target = Assembly.Load("com.studio23.ss2.ingameachievementsystem");
			Debug.Log($"Using default assembly");
#endif
			var managers = target.GetTypes().Where(mytype => !mytype.IsAbstract && mytype.IsSubclassOf(typeof(AchievementManager)));
			var targetType = managers.First();
			m_achievementManager = Activator.CreateInstance(targetType) as AchievementManager;
		}

		public static AchievementManager GetManager()
		{
			InitializeFactory();
			if (m_achievementManager != null)
			{
				Debug.Log($"AchievementManager found and is {m_achievementManager.GetType()}");
				return m_achievementManager;
			}
			Debug.LogWarning($"AchievementManager not initialized! Fatal!!!");
			return null;
		}
	}
}