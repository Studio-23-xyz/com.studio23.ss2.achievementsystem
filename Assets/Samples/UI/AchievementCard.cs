using Studio23.SS2.InGameAchievementSystem.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Studio23.SS2.InGameAchievementSystem.UI
{
	public class AchievementCard : MonoBehaviour
	{
		public TextMeshProUGUI AchievementName;
		public TextMeshProUGUI AchievementDescription;
		public Image AchievementIcon;

		private AchievementData _data;

		public void Initialize(AchievementData achievement)
		{
			_data = achievement;
			AchievementName.text = _data.AchievementName;
			AchievementDescription.text = _data.AchievementDescription;
			if (!achievement.IsAchieved)
			{
				LockAchievementIcon();
			}
			else
			{
				UnlockAchievementIcon();
			}
		}

		private void UnlockAchievementIcon()
		{
			var unlockedTexture = _data.UnlockedIcon;
			AchievementIcon.sprite = Sprite.Create(unlockedTexture,
				new Rect(0, 0, unlockedTexture.width, unlockedTexture.height), new Vector2(0.5f, 0.5f));
		}

		private void LockAchievementIcon()
		{
			var lockedTexture = _data.LockedIcon;
			AchievementIcon.sprite = Sprite.Create(lockedTexture,
				new Rect(0, 0, lockedTexture.width, lockedTexture.height), new Vector2(0.5f, 0.5f));
		}
	}
}