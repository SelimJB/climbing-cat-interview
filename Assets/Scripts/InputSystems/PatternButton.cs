using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ClimbingCat.GameSettings;
using TMPro;

namespace ClimbingCat.InputSystems
{
	[RequireComponent(typeof(Button))]
	public class PatternButton : MonoBehaviour
	{
		[SerializeField] private Pattern pattern;
		[SerializeField] private Image image;
		[SerializeField] private PatternColorSettings patternColorSettings;
		// TODO : move
		[SerializeField] private TMP_Text text;
		[SerializeField] private Color sequenceFinishedColor;
		[SerializeField] private Color disabledColor;
		[SerializeField] private Color sequenceFinishedTextColor;

		private Button button;
		private State ButtonState = State.Enabled;

		private enum State
		{
			Enabled,
			SequenceFinished,
			Disabled
		}

		public event Action<Pattern> onPatternSelected;

		private void Start()
		{
			button = GetComponent<Button>();
			button.onClick.AddListener(SelectPattern);
			RefreshButtonColor();
		}

		public void SelectPattern()
		{
			if (ButtonState != State.Disabled)
				onPatternSelected?.Invoke(pattern);
		}

		public void WrongAnswerFeedback(Pattern correctColor, float timePenaltyDuration)
		{
			StartCoroutine(Disable(timePenaltyDuration));

			if (pattern == correctColor)
				return;
			else
				return;
		}

		public void GoodAnswerFeedback(Pattern correctColor)
		{
			if (pattern == correctColor)
				return;
			else
				return;
		}

		public void SequenceFinishedFeedback()
		{
			ButtonState = State.SequenceFinished;
			image.color = sequenceFinishedColor;
			text.text = "TOP!";
			text.color = sequenceFinishedTextColor;
		}

		public void Refresh()
		{
			if (ButtonState == State.SequenceFinished)
			{
				ButtonState = State.Enabled;
				text.text = string.Empty;
				RefreshButtonColor();
			}
		}

		private void RefreshButtonColor()
		{
			image.color = patternColorSettings.GetPatternColor(pattern);
		}

		private void OnDestroy()
		{
			button.onClick.RemoveAllListeners();
		}

		public void SetEnable(bool value)
		{
			ButtonState = value ? State.Enabled : State.Disabled;
		}

		private IEnumerator Disable(float duration)
		{
			ButtonState = State.Disabled;
			image.color = disabledColor;
			yield return new WaitForSeconds(duration);
			RefreshButtonColor();
			ButtonState = State.Enabled;
		}
	}
}