using Studio23.SS2.IngameAchievements.Local;
using System;
using System.Collections.Generic;

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

			//var managerTypes = Assembly.GetAssembly(typeof(AchievementManager)).GetTypes()
			//	.Where(myType => myType.IsClass && !myType.IsAbstract);
			m_achievementManagersByName = new Dictionary<string, Type>();
			m_achievementManagersByName.Add("Local", typeof(LocalAchievements));

			//foreach (var manager in managerTypes)
			//{
			//	var temp = Activator.CreateInstance(manager) as AchievementManager;
			//	m_achievementManagersByName.Add(temp.Name, manager);
			//}
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