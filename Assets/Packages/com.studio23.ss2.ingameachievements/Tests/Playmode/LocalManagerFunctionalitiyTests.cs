using NUnit.Framework;
using Studio23.SS2.IngameAchievements.Core;
using Studio23.SS2.IngameAchievements.Local;
using System.Collections;
using UnityEngine.TestTools;

public class LocalManagerFunctionalityTests
{
	[UnityTest]
	public IEnumerator _Is_Local_Manager_Initialized_()
	{
		var localManager = AchievementFactory.GetManager("Local");
		Assert.IsInstanceOf(typeof(LocalAchievements), localManager);
		yield return null;
	}
}
