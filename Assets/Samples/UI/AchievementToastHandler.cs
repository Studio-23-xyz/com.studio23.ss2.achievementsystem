using DG.Tweening;
using Studio23.SS2.InGameAchievementSystem.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Studio23.SS2.InGameAchievementSystem.UI
{
	public class AchievementToastHandler : MonoBehaviour
	{

		public Image AchievementIcon;
		public Image RadialIcon;

		public Image DescriptionPanel;

		public RectTransform PopUPPanel;

		public TextMeshProUGUI AchievementName;
		public TextMeshProUGUI AchivementDescription;

		public ToastType toastType = ToastType.Top;
		// Start is called before the first frame update

		void Start()
		{
			//if (AchievementManager.instance.ShowToast)
			//{
			PopUp();
			//}


		}

		public void OnEnableAchievementToast(string name, string description)
		{
			//AchievementIcon = icon;
			AchievementName.text = name;
			AchivementDescription.text = description;
		}

		public void PopUp()
		{
			#region ChangeToastPosition

			if (toastType == ToastType.Top)
			{
				PopUPPanel.anchorMin = new Vector2(0.5f, 1);
				PopUPPanel.anchorMax = new Vector2(0.5f, 1);
				PopUPPanel.anchoredPosition = new Vector2(0, -50f);
			}
			else if (toastType == ToastType.Bottom)
			{
				PopUPPanel.anchorMin = new Vector2(0.5f, 0);
				PopUPPanel.anchorMax = new Vector2(0.5f, 0);
				PopUPPanel.anchoredPosition = new Vector2(0, 50f);
			}
			else if (toastType == ToastType.Left)
			{
				PopUPPanel.anchorMin = new Vector2(0, 0.5f);
				PopUPPanel.anchorMax = new Vector2(0, 0.5f);
				PopUPPanel.anchoredPosition = new Vector2(600, -438f);
			}
			else if (toastType == ToastType.Right)
			{
				PopUPPanel.anchorMin = new Vector2(1, 0.5f);
				PopUPPanel.anchorMax = new Vector2(1, 0.5f);
				PopUPPanel.anchoredPosition = new Vector2(0, -438f);
			}


			#endregion


			#region ToastAnimation

			AchievementIcon.DOFade(0.8f, 1f)
				.OnComplete((() =>
				{
					RadialIcon.transform.DOScale(RadialIcon.transform.localScale * 120f, 0.5f)
						.OnComplete((() =>
						{
							AchievementIcon.rectTransform.DOMove(
								AchievementIcon.rectTransform.position - new Vector3(100, 0, 0), 0.5f);
							RadialIcon.rectTransform.DOMove(RadialIcon.rectTransform.position - new Vector3(100, 0, 0),
								0.5f);
							DescriptionPanel.rectTransform
								.DOMove(RadialIcon.rectTransform.position - new Vector3(100, 0, 0), 0.5f)
								.OnComplete((() =>
								{
									DescriptionPanel.DOFade(1, 0.5f);
									DescriptionPanel.rectTransform.DOScale(new Vector3(5, 1, 1), 1f)
										.OnComplete((() =>
										{
											AchievementName.rectTransform.DOMove(
												AchievementName.rectTransform.position - new Vector3(100, 0, 0), 0.5f);


											AchivementDescription.rectTransform.DOMove(
												AchievementName.rectTransform.position - new Vector3(100, 25, 0), 0.5f);

											AchievementName.DOFade(1, 1f);
											AchivementDescription.DOFade(1, 1f).SetDelay(0.5f).OnComplete(() =>
											{
												AchievementName.DOFade(0, 1f);
												AchivementDescription.DOFade(0, 1f);
											});

											DescriptionPanel.rectTransform.DOScale(new Vector3(0, 1, 1), 1f)
												.SetDelay(3f)
												.OnComplete((() =>
												{
													RadialIcon.DOFade(0f, 0.1f);
													AchievementIcon.DOFade(0f, 0.1f);
												}));

										}));
								}));
						}));
				}));

			#endregion

		}
	}
}