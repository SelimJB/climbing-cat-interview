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
			tower.Create(sequence);
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
			isTimerRunning = true;
			Score = 0;
			Timer = settings.ClassicModeTimer;
			tower.Create(sequence);
		}

		private void Reset() { }

		private void Start()
		{
			patternInputViewModel.onPatternSelected += OnPatternSelected;
		}

		private void OnPatternSelected(Pattern pattern)
		{
			if (sequence.IsFinished())
			{
				Debug.LogError("GAME COMPLETED");
				return;
			}

			if (IsPatternCorrect(pattern))
				RightAnswer(sequence.NextPattern);
			else
				WrongAnswer(pattern);
			Debug.Log(sequence);
		}

		private void RightAnswer(Pattern pattern)
		{
			patternInputViewModel.GoodAnswerFeedback(pattern);
			sequence.Next();
			JumpToNextPattern();
			Score++;
			OnRightAnswer?.Invoke();
		}

		private void JumpToNextPattern()
		{
			climber.Jump(sequence.CurrentIndex);
		}

		private void WrongAnswer(Pattern pattern)
		{
			patternInputViewModel.WrongAnswerFeedback(pattern);
			Debug.LogWarning($"WRONG ANSWER : {pattern}");
		}

		private bool IsPatternCorrect(Pattern pattern)
		{
			return pattern == sequence.NextPattern;
		}
	}
}