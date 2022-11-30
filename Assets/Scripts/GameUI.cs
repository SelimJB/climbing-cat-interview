using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
	public class GameUI : MonoBehaviour
	{
		[SerializeField] private TMP_Text score;
		[SerializeField] private TMP_Text time;
		[SerializeField] private GameManager gameManager;

		private void Start()
		{
			score.text = "0";
			gameManager.OnRightAnswer += OnRightAnswer;
		}

		void Update()
		{
			time.text = FormatTime(gameManager.Timer);
		}

		private void OnRightAnswer()
		{
			score.text = gameManager.Score.ToString();
		}

		private void OnDestroy()
		{
			gameManager.OnRightAnswer -= OnRightAnswer;
		}

		private string FormatTime(float time) => $"{Mathf.FloorToInt(time / 60):0}:{Mathf.FloorToInt(time % 60):00}";
	}
}