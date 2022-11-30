using UnityEngine;

namespace ClimbingCat.CameraSystems
{
	public static class CameraPlacementUtility
	{
		public static void AdjustCamera(Camera camera, CameraPlacementSettings placementSettings)
		{
			AdjustCamera(camera, GetPlacement(placementSettings));
		}

		public static void AdjustCamera(Camera camera, CameraPlacement placement)
		{
			camera.transform.position = placement.Position;
			camera.transform.rotation = Quaternion.LookRotation(placement.Direction);
		}

		public static CameraPlacement GetPlacement(CameraPlacementSettings placementSettings)
		{
			var horizontalDirection = -Mathf.Atan(placementSettings.horizontalOffset / placementSettings.distance) * Mathf.Rad2Deg;
			var towerPivotQuaternion = Quaternion.Euler(0, placementSettings.centerRotation, 0);
			var position = placementSettings.center + towerPivotQuaternion * new Vector3(placementSettings.distance, placementSettings.height, placementSettings.horizontalOffset);
			var direction = towerPivotQuaternion * Quaternion.Euler(0, horizontalDirection, placementSettings.cameraRotation) * new Vector3(0, 1, 0);
			return new CameraPlacement(direction, position);
		}
	}
}