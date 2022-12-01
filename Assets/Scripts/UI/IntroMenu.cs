using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ClimbingCat.UI
{
	public class IntroMenu : MonoBehaviour
	{
		[SerializeField] private Button classicModeButton;
		[SerializeField] private Button arcadeModeButton;
		[SerializeField] private Button easyButton;
		[SerializeField] private Button mediumButton;
		[SerializeField] private Button hardButton;
		[SerializeField] private Button veryHardButton;
		[SerializeField] private Color uiColor1;
		[SerializeField] private Color uiColor2;
		
		private void Start()
		{
			classicModeButton.onClick.AddListener(() =>
			{
				GameModeSettings.Mode = GameMode.Classic;
				SceneManager.LoadScene(1);
			});
			arcadeModeButton.onClick.AddListener(() =>
			{
				GameModeSettings.Mode = GameMode.Arcade;
				SceneManager.LoadScene(1);
			});
			easyButton.onClick.AddListener(() => SelectDifficulty(GameDifficulty.Easy));
			mediumButton.onClick.AddListener(() => SelectDifficulty(GameDifficulty.Medium));
			hardButton.onClick.AddListener(() => SelectDifficulty(GameDifficulty.Hard));
			veryHardButton.onClick.AddListener(() => SelectDifficulty(GameDifficulty.VeryHard));
			SelectDifficulty(GameModeSettings.Difficulty);
		}

		private void OnDestroy()
		{
			classicModeButton.onClick.RemoveAllListeners();
			arcadeModeButton.onClick.RemoveAllListeners();
			easyButton.onClick.RemoveAllListeners();
			mediumButton.onClick.RemoveAllListeners();
			hardButton.onClick.RemoveAllListeners();
			veryHardButton.onClick.RemoveAllListeners();
		}

		private void SelectDifficulty(GameDifficulty difficulty)
		{
			GameModeSettings.Difficulty = difficulty;
			SetDifficultyButtonActive(easyButton, difficulty == GameDifficulty.Easy);
			SetDifficultyButtonActive(mediumButton, difficulty == GameDifficulty.Medium);
			SetDifficultyButtonActive(hardButton, difficulty == GameDifficulty.Hard);
			SetDifficultyButtonActive(veryHardButton, difficulty == GameDifficulty.VeryHard);
		}

		private void SetDifficultyButtonActive(Button button, bool value)
		{
			if (value)
			{
				button.Select();
				button.GetComponentInChildren<TMP_Text>().color = uiColor2;
				var backgroundColor = uiColor2;
				backgroundColor.a = 0;
				button.GetComponent<Image>().color = backgroundColor;
			}
			else
			{
				button.GetComponentInChildren<TMP_Text>().color = uiColor1;
				button.GetComponent<Image>().color = uiColor2;
			}
		}
	}
}