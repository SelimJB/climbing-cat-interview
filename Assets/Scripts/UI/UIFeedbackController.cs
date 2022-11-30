using ClimbingCat.GameSettings;
using TMPro;
using UnityEngine;

namespace ClimbingCat.UI
{
	public class UIFeedbackController : MonoBehaviour
	{
		[SerializeField] private Settings settings;
		[SerializeField] private TMP_Text bonusText;
		[SerializeField] private Animator sequenceFinishedAnimator;
		[SerializeField] private Animation bonusAnimation;
		private static readonly int Launch = Animator.StringToHash("Launch");
		private static readonly int Hide = Animator.StringToHash("Hide");

		private void Start()
		{
			bonusText.text = $"+{settings.SequenceFinishedBonus.ToString()}";
		}

		public void PlayBonusAnimation()
		{
			bonusAnimation.gameObject.SetActive(true);
			bonusAnimation.Play();
		}

		public void DisplaySequenceFinishedText(bool value)
		{
			if (value)
			{
				sequenceFinishedAnimator.SetTrigger(Launch);
				sequenceFinishedAnimator.gameObject.SetActive(true);
			}
			else
			{
				sequenceFinishedAnimator.SetTrigger(Hide);
			}
		}

		public void HideUIFeedbackElements()
		{
			bonusAnimation.gameObject.SetActive(false);
			sequenceFinishedAnimator.gameObject.SetActive(false);
		}
	}
}