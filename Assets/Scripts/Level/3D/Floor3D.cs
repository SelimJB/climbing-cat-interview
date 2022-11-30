using ClimbingCat.GameSettings;
using UnityEngine;

namespace ClimbingCat.Level
{
	public class Floor3D : MonoBehaviour
	{
		[SerializeField] private PatternColorSettings patternColorSettings;
		[SerializeField] private MeshRenderer mesh;
		[SerializeField] private Transform platform;

		public Transform Platform => platform;
		public float Height => transform.position.y;
		public float Rotation => transform.eulerAngles.y;

		public void Initialize(Pattern pattern)
		{
			mesh.material.color = patternColorSettings.GetPatternColor(pattern);
		}
	}
}