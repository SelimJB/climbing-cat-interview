using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
	[RequireComponent(typeof(Button))]
	public class PatternButton : MonoBehaviour
	{
		[SerializeField] private Pattern pattern;
		[SerializeField] private Image image;
		[SerializeField] private PatternColorSettings patternColorSettings;

		private Button button;

		public event Action<Pattern> onPatternSelected;

		private void Start()
		{
			button = GetComponent<Button>();
			button.onClick.AddListener(SelectPattern);
			RefreshButtonColor();
		}
		
		public void SelectPattern()
		{
			onPatternSelected?.Invoke(pattern);
		}

		public void WrongAnswerFeedback(Pattern correctColor)
		{
			if (pattern == correctColor)
				return;
			else
				return;
		}

		public void WriteAnswerFeedback(Pattern correctColor)
		{
			if (pattern == correctColor)
				return;
			else
				return;
		}
		
		private void RefreshButtonColor()
		{
			image.color = patternColorSettings.GetPatternColor(pattern);
		}

		private void OnDestroy()
		{
			button.onClick.RemoveAllListeners();
		}
	}
}