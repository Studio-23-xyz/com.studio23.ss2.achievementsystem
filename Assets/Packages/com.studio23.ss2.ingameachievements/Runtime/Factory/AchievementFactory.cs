using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Studio23.SS2.IngameAchievements.Core
{
	public static class AchievementFactory
	{
		private static Dictionary<string, Type> m_achievementManagersByName;
		private static bool IsInitialized => m_achievementManagersByName != null;
		private static AchievementManager m_achievementManager;

		private static void InitializeFactory()
		{
			if (IsInitialized)
				return;

#if STUDIO23_INGAMEACHIEVEMENTS_THIRDPARTY_INSTALLED
			Assembly target = Assembly.Load("com.studio23.ss2.ingameachievements.thirdparty");
#else
			Assembly target = Assembly.Load("com.studio23.ss2.ingameachievements");
#endif
			var managers = target.GetTypes().Where(mytype => !mytype.IsAbstract && mytype.IsSubclassOf(typeof(AchievementManager)));
			m_achievementManagersByName = new Dictionary<string, Type>();

			foreach (var manager in managers)
			{
				var temp = Activator.CreateInstance(manager) as AchievementManager;
				m_achievementManagersByName.Add(temp.Name, manager);
			}
		}

		public static AchievementManager GetManager(string managerType)
		{
			InitializeFactory();
			if (m_achievementManager != null) return m_achievementManager;
			if (m_achievementManagersByName.TryGetValue(managerType, out var type))
			{
				var manager = Activator.CreateInstance(type) as AchievementManager;
				m_achievementManager = manager;
				return manager;
			}
			return null;
		}
	}
}