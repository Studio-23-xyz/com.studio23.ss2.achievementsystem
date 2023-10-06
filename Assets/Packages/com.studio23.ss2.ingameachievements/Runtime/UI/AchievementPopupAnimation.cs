using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Studio23.SS2.IngameAchievements.UI
{
	public class AchievementPopupAnimation : MonoBehaviour
	{
		public RectTransform Achievement;
		public Image AchievementIcon;
		public Image BorderImage;
		public TextMeshProUGUI AchievementName;
		public TextMeshProUGUI AchievementDescription;
		public RectTransform DescriptionPanel;

		[Header("Value Modifier")] public float slideleftAmount;

		private Sequence animationSequence;

		void Start()
		{
			// EnablePopUp(true);
		}

		public void EnablePopUp(Texture2D achievementIcon, string name, string description)
		{
			AchievementIcon.sprite = Sprite.Create(achievementIcon,
				new Rect(0, 0, achievementIcon.width, achievementIcon.height), new Vector2(0.5f, 0.5f));
			;
			AchievementName.text = name;
			AchievementDescription.text = description;


			if (animationSequence != null && animationSequence.IsActive())
			{
				animationSequence.Kill(); // Stop any ongoing animation
			}

			animationSequence = DOTween.Sequence();


			Achievement.gameObject.SetActive(true);

			animationSequence.Append(AchievementIcon.DOFade(1f, 2f))
				.Join(BorderImage.rectTransform.DOScale(1.15f, 1.5f))
				.Append(Achievement.DOMoveX(Achievement.position.x - slideleftAmount, 1))
				.Append(DescriptionPanel.DOScaleX(5.26f, 1))
				.AppendCallback(() =>
				{
					AchievementName.DOFade(1f, 0.5f);
					AchievementDescription.DOFade(1f, 0.5f);
				})
				.AppendInterval(1f) // Wait for 1 second
				.Append(AchievementDescription.DOFade(0f, 0.5f))
				.AppendCallback(() => ReverseAnimation());
		}


		private void ReverseAnimation()
		{
			animationSequence = DOTween.Sequence();

			animationSequence.Append(AchievementDescription.DOFade(0f, 0.5f))
				.Join(AchievementName.DOFade(0f, 0.5f))
				.Append(DescriptionPanel.DOScaleX(1f, 1))
				.Append(Achievement.DOMoveX(Achievement.position.x, 0.3f))
				.Join(BorderImage.rectTransform.DOScale(1f, 0.1f))
				.Append(AchievementIcon.DOFade(0f, 0.1f)
					.OnComplete((() => { Achievement.gameObject.SetActive(false); }))
				);
		}

	}
}
