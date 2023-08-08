using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementCard : MonoBehaviour
{
    public TextMeshProUGUI AchievementName;
    public TextMeshProUGUI AchievementDescription;
    public Texture2D locked;
    public Texture2D unlock;
    public Image AchievementIcon;

    public void OnEnableAchievementCard(AchievementData achievement)
    {
       AchievementName.text = achievement.AchievementName;
       AchievementDescription.text = achievement.AchievementDescription;
       ChangeAchievementIcon(achievement.isAchieved,achievement.LockedIcon,achievement.UnlockedIcon);
    }


    private void ChangeAchievementIcon(bool isunlocked, Texture2D lockedTexture, Texture2D unlockedTexture)
    {
        AchievementIcon.sprite = isunlocked
            ? Sprite.Create(unlockedTexture, new Rect(0, 0, unlockedTexture.width, unlockedTexture.height), new Vector2(0.5f, 0.5f))
            : Sprite.Create(lockedTexture, new Rect(0, 0, lockedTexture.width, lockedTexture.height), new Vector2(0.5f, 0.5f));
    }

}
