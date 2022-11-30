using ClimbingCat.CameraSystems;
using ClimbingCat.GameSettings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ClimbingCat.Level
{
	public class Floor3D : Floor
	{
		[SerializeField] private PatternColorSettings patternColorSettings;
		[SerializeField] private MeshRenderer mesh;
		[SerializeField] private Transform platform;

		public Transform Platform => platform;
		public CameraPlacement CameraPlacement { get; private set; }

		public override void Initialize(Pattern pattern)
		{
			mesh.material.color = patternColorSettings.GetPatternColor(pattern);
			CalculatePointOfView();
		}

		private void CalculatePointOfView()
		{
			var cameraSettings = new CameraPlacementSettings
			{
				distance = Random.Range(5f, 10f),
				height = Random.Range(-2.5f, -0.2f) + transform.position.y,
				horizontalOffset = Random.Range(-10f, 10f),
				centerRotation = transform.eulerAngles.y,
				cameraRotation = Mathf.Lerp(60, 90, Random.Range(0f, 1f))
			};
			CameraPlacement = CameraPlacementUtility.GetPlacement(cameraSettings);
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawSphere(CameraPlacement.Position, 0.25f);
			Gizmos.DrawRay(CameraPlacement.Position, CameraPlacement.Direction * 2);
		}
	}
}