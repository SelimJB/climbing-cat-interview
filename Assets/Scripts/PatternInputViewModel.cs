using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
	// TODO : change name
	public class PatternInputViewModel : MonoBehaviour
	{
		[SerializeField] private List<PatternButton> patternButtons;

		public event Action<Pattern> onPatternSelected;

		private void Start()
		{
			foreach (var b in patternButtons)
				b.onPatternSelected += OnPatternSelected;
		}

		private void OnPatternSelected(Pattern pattern)
		{
			onPatternSelected?.Invoke(pattern);
		}

		private void OnDestroy()
		{
			foreach (var b in patternButtons)
				b.onPatternSelected -= OnPatternSelected;
		}

		public void WrongAnswerFeedback(Pattern pattern, float timePenaltyDuration)
		{
			foreach (var b in patternButtons)
				b.WrongAnswerFeedback(pattern, timePenaltyDuration);
		}

		public void GoodAnswerFeedback(Pattern pattern)
		{
			foreach (var b in patternButtons)
				b.WriteAnswerFeedback(pattern);
		}

		public void SetEnable(bool value)
		{
			foreach (var b in patternButtons)
				b.SetEnable(value);
		}
	}
}