using System.Collections;
using System.Collections.Generic;
using Studio23.SS2.IngameAchievements.Core;
using UnityEngine;

public class DummyScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        AchievementFactory.GetManager("Local").SetupAchievements();
    }

    [ContextMenu("Debug Unlock Yellow")]
    public void DebugFunction()
    {
	    if (AchievementFactory.GetManager("Local").UnlockAchievement("Yellow"))
		    Debug.Log($"Success!");
	    else
		    Debug.Log($"Die UwU");
    }
}
