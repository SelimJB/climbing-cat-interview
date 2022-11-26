using UnityEngine;

namespace DefaultNamespace
{
	// TODO : rename floor
	public class PatternHolder : MonoBehaviour
	{
		[SerializeField] private PatternColorSettings patternColorSettings;
		[SerializeField] private SpriteRenderer sprite;

		public void Initialize(Pattern pattern)
		{
			sprite.color = patternColorSettings.GetPatternColor(pattern);
		}
	}
}