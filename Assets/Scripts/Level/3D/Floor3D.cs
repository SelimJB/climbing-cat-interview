using ClimbingCat.GameSettings;
using UnityEngine;

namespace ClimbingCat.Level
{
	public class Floor3D : Floor
	{
		[SerializeField] private PatternColorSettings patternColorSettings;
		[SerializeField] private MeshRenderer mesh;
		[SerializeField] private Transform platform;

		public Transform Platform => platform;

		public override void Initialize(Pattern pattern)
		{
			mesh.material.color = patternColorSettings.GetPatternColor(pattern);
		}
	}
}