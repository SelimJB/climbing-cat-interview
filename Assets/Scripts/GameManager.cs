using UnityEngine;

namespace DefaultNamespace
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private Settings settings;
		[SerializeField] private PatternInputViewModel patternInputViewModel;
		[SerializeField] private Tower tower;
		[SerializeField] private Climber climber;

		private Sequence sequence;

		private void Awake()
		{
			sequence = new Sequence(settings.SequenceLength);
			Debug.Log(sequence.CurrentPattern);
			Debug.Log(sequence.ToString());
			tower.Create(sequence);
		}

		private void OnGUI()
		{
			if (GUI.Button(new Rect(0, 0, 100, 100), "Test sequence generation"))
			{
				sequence = new Sequence(settings.SequenceLength);
				Debug.Log(sequence.ToString());
			}
		}

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