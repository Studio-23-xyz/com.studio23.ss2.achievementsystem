using NUnit.Framework;
using System.Collections;
using Studio23.SS2.InGameAchievementSystem.Core;
using Studio23.SS2.InGameAchievementSystem.Local;
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
