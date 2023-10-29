using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly:InternalsVisibleTo("com.studio23.ss2.achievementsystem.tests.playmode")]
namespace Studio23.SS2.AchievementSystem.Providers
{
	internal static class AchievementFactory
	{
		private static AchievementProvider m_achievementManager;

		private static void InitializeFactory()
		{
			if (m_achievementManager != null)
				return;


			Assembly target = Assembly.Load("com.studio23.ss2.achievementsystem");


			var managers = target.GetTypes().Where(mytype => !mytype.IsAbstract && mytype.IsSubclassOf(typeof(AchievementProvider)));
			var targetType = managers.First();//TODO Order by priority later
			m_achievementManager = Activator.CreateInstance(targetType) as AchievementProvider;
		}

		public static AchievementProvider GetManager()
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