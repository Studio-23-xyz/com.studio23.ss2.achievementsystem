using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if STEAMWORKS_NET
using Steamworks;

#endif
public class SteamAchievementManager : MonoBehaviour
{
    public static SteamAchievementManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }

    public void UnlockSteamAchievement(string steamAchievementApi)
    {
#if STEAMWORKS_NET
        SteamUserStats.SetAchievement(steamAchievementApi);
#endif

    }

}
