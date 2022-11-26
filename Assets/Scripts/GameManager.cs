using System;
using UnityEngine;

namespace DefaultNamespace
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private Settings settings;
		[SerializeField] private PatternInputViewModel patternInputViewModel;
		[SerializeField] private Tower tower;
		[SerializeField] private Climber climber;

		public event Action OnRightAnswer;

		private Sequence sequence;
		private bool isTimerRunning;

		public int Score { get; private set; }
		public float Timer { get; private set; }

		private void Awake()
		{
			Initialize();
		}

		private void Update()
		{
			if (isTimerRunning && Timer > 0)
			{
				Timer -= Time.deltaTime;
			}
			else
			{
				Timer = 0;
				isTimerRunning = false;
			}
		}

		private void Initialize()
		{
			sequence = new Sequence(settings.SequenceLength);
			tower.Create(sequence);
			isTimerRunning = true;
			Score = 0;
			Timer = settings.ClassicModeTimer;
		}

		private void Reset() { }

		private void Start()
		{
			patternInputViewModel.onPatternSelected += OnPatternSelected;
		}

		private void ResetSequence()
		{
			sequence = new Sequence(settings.SequenceLength);
			tower.Reset(sequence);
			climber.Jump(0);
		}

		private void OnPatternSelected(Pattern pattern)
		{
			if (IsPatternCorrect(pattern))
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
				ResetSequence();
		}

		private void JumpToNextPattern()
		{
			climber.Jump(sequence.CurrentIndex);
		}

		private void WrongAnswer(Pattern pattern)
		{
			patternInputViewModel.WrongAnswerFeedback(pattern, settings.TimePenaltyDuration);
			Debug.LogWarning($"WRONG ANSWER : {pattern}");
		}

		private bool IsPatternCorrect(Pattern pattern)
		{
			return pattern == sequence.NextPattern;
		}
	}
}