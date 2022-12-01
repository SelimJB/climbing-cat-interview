using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ClimbingCat.UI
{
	public class GameUI : MonoBehaviour
	{
		[SerializeField] private TMP_Text score;
		[SerializeField] private TMP_Text time;
		[SerializeField] private GameManager gameManager;
		[SerializeField] private Button menuButton;

		private void Start()
		{
			score.text = gameManager.Score.ToString();
			gameManager.OnRightAnswer += OnRightAnswer;
			menuButton.onClick.AddListener(OnMenuButtonClick);
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
			menuButton.onClick.RemoveListener(OnMenuButtonClick);
		}

		private void OnMenuButtonClick()
		{
			gameManager.EndGame();
		}

		private string FormatTime(float time) => $"{Mathf.FloorToInt(time / 60):0}:{Mathf.FloorToInt(time % 60):00}";
	}
}