using UnityEngine;

namespace DefaultNamespace
{
	public class Floor2D : Floor
	{
		[SerializeField] private PatternColorSettings patternColorSettings;
		[SerializeField] private SpriteRenderer sprite;

		public override void Initialize(Pattern pattern)
		{
			sprite.color = patternColorSettings.GetPatternColor(pattern);
		}
	}
}