using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ClimbingCat.UI
{
	public class GameMenu : MonoBehaviour
	{
		[SerializeField] private Button newGameButton;
		[SerializeField] private Button introMenuButton;
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
			newGameButton.onClick.AddListener(() => SceneManager.LoadScene(1));
			introMenuButton.onClick.AddListener(() => SceneManager.LoadScene(0));
		}

		private void OnDestroy()
		{
			newGameButton.onClick.RemoveAllListeners();
			introMenuButton.onClick.RemoveAllListeners();
		}
	}
}