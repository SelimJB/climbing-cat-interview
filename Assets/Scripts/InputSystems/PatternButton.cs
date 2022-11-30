using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ClimbingCat.GameSettings;

namespace ClimbingCat.InputSystems
{
	[RequireComponent(typeof(Button))]
	public class PatternButton : MonoBehaviour
	{
		[SerializeField] private Pattern pattern;
		[SerializeField] private Image image;
		[SerializeField] private PatternColorSettings patternColorSettings;

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

		public void WriteAnswerFeedback(Pattern correctColor)
		{
			if (pattern == correctColor)
				return;
			else
				return;
		}

		public void SequenceFinishedFeedback()
		{
			ButtonState = State.SequenceFinished;
			image.color = Color.green;
		}

		public void Refresh()
		{
			if (ButtonState == State.SequenceFinished)
			{
				ButtonState = State.Enabled;
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
			image.color = Color.gray;
			yield return new WaitForSeconds(duration);
			RefreshButtonColor();
			ButtonState = State.Enabled;
		}
	}
}