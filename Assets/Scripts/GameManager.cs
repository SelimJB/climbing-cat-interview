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
		[SerializeField] private UIFeedbackController uiFeedbackController;

		public event Action OnRightAnswer;

		private Sequence sequence;
		private bool isTimerRunning;
		private GameMode gameMode;
		private string hightScoreSaveKey;

		public int Score { get; private set; }
		public float Timer { get; private set; }

		private void Awake()
		{
			gameMode = GameModeSettings.Mode;
			Initialize();
			patternInputViewModel.onPatternSelected += OnPatternSelected;
			hightScoreSaveKey = $"HighScore_{GameModeSettings.Mode}_{GameModeSettings.Difficulty}";
		}

		private void Update()
		{
			if (!isTimerRunning) return;

			if (IsGameFinished())
				EndGame();
			else
				Timer -= Time.deltaTime;
		}

		private bool IsGameFinished()
		{
			if (gameMode == GameMode.Classic)
				return Timer <= 0 || Score <= 0;

			return Timer <= 0;
		}

		private void Initialize()
		{
			sequence = new Sequence(settings.SequenceLength, GameModeSettings.AvailablePatternNumber);
			tower.Create(sequence);
			isTimerRunning = true;
			Score = gameMode == GameMode.Classic ? settings.ScoreTarget : 0;
			Timer = gameMode == GameMode.Classic ? settings.ClassicModeTimer : settings.ArcadeModeTimer;
		}

		public void EndGame()
		{
			uiFeedbackController.HideUIFeedbackElements();
			patternInputViewModel.SetEnable(false);
			Timer = 0;
			isTimerRunning = false;

			var highScore = PlayerPrefs.GetInt(hightScoreSaveKey);

			// TODO : better game mode / difficulty handling
			if (gameMode == GameMode.Classic && !PlayerPrefs.HasKey(hightScoreSaveKey))
				PlayerPrefs.SetInt(hightScoreSaveKey, settings.ScoreTarget);

			if (gameMode == GameMode.Classic && Score < highScore)
			{
				highScore = Score;
				PlayerPrefs.SetInt(hightScoreSaveKey, Score);
			}
			else if (gameMode == GameMode.Arcade && Score > highScore)
			{
				highScore = Score;
				PlayerPrefs.SetInt(hightScoreSaveKey, Score);
			}

			menu.Display(Score, highScore);
		}

		private void ResetSequence()
		{
			sequence = new Sequence(settings.SequenceLength, GameModeSettings.AvailablePatternNumber);
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
			patternInputViewModel.RightAnswer(pattern);
			sequence.Next();
			JumpToNextPattern();
			IncrementScore(1);
			OnRightAnswer?.Invoke();

			if (sequence.IsFinished())
			{
				patternInputViewModel.SequenceFinishedFeedback();
				uiFeedbackController.DisplaySequenceFinishedText(true);
			}
		}

		private void IncrementScore(int point)
		{
			if (gameMode == GameMode.Classic)
				Score -= point;
			else
				Score += point;
		}

		private void SequenceFinishedAnswer()
		{
			uiFeedbackController.DisplaySequenceFinishedText(false);
			uiFeedbackController.PlayBonusAnimation();
			IncrementScore(settings.SequenceFinishedBonus);
			ResetSequence();
			patternInputViewModel.RefreshButtonState();
			OnRightAnswer?.Invoke();
		}

		private void WrongAnswer(Pattern pattern)
		{
			patternInputViewModel.WrongAnswer(pattern, settings.TimePenaltyDuration);
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