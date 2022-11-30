using System;
using UnityEngine;
using ClimbingCat.GameSettings;
using ClimbingCat.InputSystems;
using ClimbingCat.Level;
using ClimbingCat.UI;

namespace ClimbingCat
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private Settings settings;
		[SerializeField] private PatternInputViewModel patternInputViewModel;
		[SerializeField] private Tower tower;
		[SerializeField] private Climber climber;
		[SerializeField] private GameMenu menu;

		public event Action OnRightAnswer;

		private Sequence sequence;
		private bool isTimerRunning;

		public int Score { get; private set; }
		public float Timer { get; private set; }

		private void Awake()
		{
			Initialize();
			patternInputViewModel.onPatternSelected += OnPatternSelected;
		}

		private void Update()
		{
			if (!isTimerRunning) return;

			if (Timer > 0)
				Timer -= Time.deltaTime;
			else
				EndGame();
		}

		private void Initialize()
		{
			sequence = new Sequence(settings.SequenceLength);
			tower.Create(sequence);
			isTimerRunning = true;
			Score = 0;
			Timer = settings.ClassicModeTimer;
		}


		private void EndGame()
		{
			var highScore = PlayerPrefs.GetInt("HighScore");

			if (Score > highScore)
			{
				highScore = Score;
				PlayerPrefs.SetInt("HighScore", Score);
			}

			patternInputViewModel.SetEnable(false);
			Timer = 0;
			isTimerRunning = false;
			menu.Display(Score, highScore);
		}

		private void ResetSequence()
		{
			sequence = new Sequence(settings.SequenceLength);
			tower.Reset(sequence);
			climber.Jump(0);
		}

		private void OnPatternSelected(Pattern pattern)
		{
			if (sequence.IsFinished())
				SequenceFinishedAnswer();
			else if (IsPatternCorrect(pattern))
				RightAnswer(sequence.NextPattern);
			else
				WrongAnswer(pattern);
		}

		private void RightAnswer(Pattern pattern)
		{
			patternInputViewModel.GoodAnswerFeedback(pattern);
			sequence.Next();
			JumpToNextPattern();
			Score++;
			OnRightAnswer?.Invoke();

			if (sequence.IsFinished())
			{
				patternInputViewModel.SequenceFinishedFeedback();
			}
		}

		private void SequenceFinishedAnswer()
		{
			Score += settings.SequenceFinishedBonus;
			ResetSequence();
			patternInputViewModel.RefreshButtonState();
			OnRightAnswer?.Invoke();
		}

		private void WrongAnswer(Pattern pattern)
		{
			patternInputViewModel.WrongAnswerFeedback(pattern, settings.TimePenaltyDuration);
			climber.WrongAnswerAnimation();
		}

		private void JumpToNextPattern()
		{
			climber.Jump(sequence.CurrentIndex);
		}

		private bool IsPatternCorrect(Pattern pattern)
		{
			return pattern == sequence.NextPattern;
		}

		private void OnDestroy()
		{
			patternInputViewModel.onPatternSelected -= OnPatternSelected;
		}
	}
}