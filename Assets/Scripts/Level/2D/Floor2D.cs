using ClimbingCat.GameSettings;
using UnityEngine;

namespace ClimbingCat.Level
{
	public class Floor2D : MonoBehaviour
	{
		[SerializeField] private PatternColorSettings patternColorSettings;
		[SerializeField] private SpriteRenderer sprite;

		public void Initialize(Pattern pattern)
		{
			sprite.color = patternColorSettings.GetPatternColor(pattern);
		}
	}
}