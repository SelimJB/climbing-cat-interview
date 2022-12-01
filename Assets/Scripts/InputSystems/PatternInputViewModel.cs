using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace ClimbingCat.InputSystems
{
	public class PatternInputViewModel : MonoBehaviour
	{
		[SerializeField] private List<PatternButton> patternButtons;

		private List<PatternButton> activePatternButtons;

		public event Action<Pattern> onPatternSelected;

		private void Start()
		{
			if (GameModeSettings.AvailablePatternNumber < patternButtons.Count)
				for (var i = GameModeSettings.AvailablePatternNumber; i < patternButtons.Count; i++)
					patternButtons[i].gameObject.SetActive(false);
			activePatternButtons = patternButtons.Where(b => b.gameObject.activeSelf).ToList();

			foreach (var b in activePatternButtons)
				b.onPatternSelected += OnPatternSelected;
		}

		private void OnPatternSelected(Pattern pattern)
		{
			onPatternSelected?.Invoke(pattern);
		}

		private void OnDestroy()
		{
			foreach (var b in activePatternButtons)
				b.onPatternSelected -= OnPatternSelected;
		}

		public void WrongAnswer(Pattern pattern, float timePenaltyDuration)
		{
			foreach (var b in activePatternButtons)
				b.WrongAnswerFeedback(pattern, timePenaltyDuration);
		}

		public void RightAnswer(Pattern pattern)
		{
			// TODO : Improve difficulty logic & game mode settings injection
			if ((int)GameModeSettings.Difficulty > 1)
			{
				var patterns = new List<Pattern>();
				for (var i = 0; i < activePatternButtons.Count; i++)
					patterns.Add((Pattern)i);
				var rand = new Random();
				patterns = patterns.OrderBy(_ => rand.Next()).ToList();
				for (var i = 0; i < activePatternButtons.Count; i++)
					activePatternButtons[i].ChangePattern(patterns[i]);
			}

			foreach (var b in activePatternButtons)
				b.RightAnswerFeedback(pattern);
		}

		public void SetEnable(bool value)
		{
			foreach (var b in activePatternButtons)
				b.SetEnable(value);
		}

		public void SequenceFinishedFeedback()
		{
			foreach (var b in activePatternButtons)
				b.SequenceFinishedFeedback();
		}

		public void RefreshButtonState()
		{
			foreach (var b in activePatternButtons)
				b.Refresh();
		}
	}
}