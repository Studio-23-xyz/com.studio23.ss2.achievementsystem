using DG.Tweening;
using UnityEngine;

namespace Studio23.SS2.IngameAchievements.UI
{
	public class AchievementPopup : MonoBehaviour
	{
		public float slideInDuration = 0.5f;
		public float fadeDuration = 0.2f;
		public float displayDuration = 3f;

		public Sprite TestSprite;

		private RectTransform rectTransform;
		private CanvasGroup canvasGroup;


		private void Start()
		{
			rectTransform = GetComponent<RectTransform>();
			canvasGroup = GetComponent<CanvasGroup>();

			// Initialize the AchievementPopup
			rectTransform.anchoredPosition = new Vector2(0f, -rectTransform.rect.height);
			canvasGroup.alpha = 0f;

			ShowAchievement("Jabermunna", "Say jabermunna", TestSprite);

		}

		public void ShowAchievement(string title, string description, Sprite icon)
		{
			// Set the title, description, and icon in the UI using the passed parameters

			// Animate the AchievementPopup
			rectTransform.DOAnchorPosY(0f, slideInDuration)
				.SetEase(Ease.OutBounce)
				.OnComplete(() =>
				{
					canvasGroup.DOFade(1f, fadeDuration);
					HideAchievement();
				});
		}

		private void HideAchievement()
		{
			// Animate hiding the AchievementPopup
			DOVirtual.DelayedCall(displayDuration, () =>
			{
				canvasGroup.DOFade(0f, fadeDuration)
					.OnComplete(() => rectTransform.DOAnchorPosY(-rectTransform.rect.height, slideInDuration));
			});
		}
	}
}