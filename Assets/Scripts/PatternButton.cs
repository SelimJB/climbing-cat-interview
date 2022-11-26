using System;
using System.Collections;
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
		private bool isSelectable = true;

		public event Action<Pattern> onPatternSelected;

		private void Start()
		{
			button = GetComponent<Button>();
			button.onClick.AddListener(SelectPattern);
			RefreshButtonColor();
		}

		public void SelectPattern()
		{
			if (isSelectable)
				onPatternSelected?.Invoke(pattern);
		}

		public void WrongAnswerFeedback(Pattern correctColor, float timePenaltyDuration)
		{
			StartCoroutine(Disable(timePenaltyDuration));

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

		public void SetEnable(bool value)
		{
			isSelectable = value;
		}

		private IEnumerator Disable(float duration)
		{
			isSelectable = false;
			image.color = Color.gray;
			yield return new WaitForSeconds(duration);
			RefreshButtonColor();
			isSelectable = true;
		}
	}
}