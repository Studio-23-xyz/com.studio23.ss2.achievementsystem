using NUnit.Framework;
using Studio23.SS2.AchievementSystem.Providers;
using System.Collections;

using UnityEngine.TestTools;

public class LocalManagerFunctionalityTests
{
	[UnityTest]
	public IEnumerator _Is_Local_Manager_Initialized_()
	{
		var localManager = AchievementFactory.GetManager();
		Assert.IsInstanceOf(typeof(LocalAchievements), localManager);
		yield return null;
	}
}
