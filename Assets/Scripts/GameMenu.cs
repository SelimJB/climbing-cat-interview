using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
	public class GameMenu : MonoBehaviour
	{
		[SerializeField] private Button newGameButton;
		[SerializeField] private TMP_Text highscore;
		[SerializeField] private TMP_Text score;

		public void Display(int score, int highscore)
		{
			gameObject.SetActive(true);
			this.highscore.text = highscore.ToString();
			this.score.text = score.ToString();
		}

		private void Start()
		{
			newGameButton.onClick.AddListener(() => SceneManager.LoadScene("Game"));
		}

		private void OnDestroy()
		{
			newGameButton.onClick.RemoveAllListeners();
		}
	}
}