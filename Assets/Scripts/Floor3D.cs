using UnityEngine;

namespace DefaultNamespace
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