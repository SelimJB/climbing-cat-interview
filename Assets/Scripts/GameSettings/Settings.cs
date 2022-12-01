using UnityEngine;

namespace ClimbingCat.GameSettings
{
	[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/Settings")]
	public class Settings : ScriptableObject
	{
		[Header("Classic mode")]
		[SerializeField] private float classicModeTimer;
		[SerializeField] private int scoreTarget;
		[Header("Arcade mode")]
		[SerializeField] private float arcadeModeTimer;
		[Header("Game parameters")]
		[SerializeField] private int sequenceLength;
		[SerializeField] private float timePenaltyDuration;
		[SerializeField] private int sequenceFinishedBonus;

		public int SequenceLength => sequenceLength;
		public float ClassicModeTimer => classicModeTimer;
		public float TimePenaltyDuration => timePenaltyDuration;
		public int SequenceFinishedBonus => sequenceFinishedBonus;
		public float ArcadeModeTimer => arcadeModeTimer;
		public int ScoreTarget => scoreTarget;
	}
}