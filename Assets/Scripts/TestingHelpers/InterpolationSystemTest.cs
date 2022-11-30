using ClimbingCat.CameraSystems;
using UnityEngine;

namespace TestingHelpers
{
	public class InterpolationSystemTest : MonoBehaviour
	{
		[SerializeField] private CameraInterpolationSettings cameraInterpolationSettings;
		[Range(0f, 1f), SerializeField] private float cameraProgress;
		[SerializeField] private float maxHeight = 20f;
		[SerializeField] private bool controlCamera;
		[SerializeField] private Camera camera;

		private void OnDrawGizmos()
		{
			var centerRotation = cameraProgress * 360f;
			var height = cameraProgress * maxHeight;
			var placementSettings = cameraInterpolationSettings.GetCameraPlacementFromHeightAndAngle(height, centerRotation, maxHeight);
			var placement = CameraPlacementUtility.GetPlacement(placementSettings);
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(placement.Position, 0.4f);
			Gizmos.DrawRay(placement.Position, placement.Direction * 2);
			if (camera != null && controlCamera)
				CameraPlacementUtility.AdjustCamera(camera, placement);
		}
	}
}