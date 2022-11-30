using ClimbingCat.CameraSystems;
using DG.Tweening;
using UnityEngine;

namespace ClimbingCat.Level
{
	public class Climber3D : Climber
	{
		[SerializeField] private Tower3D tower;
		[SerializeField] private Camera camera;
		[SerializeField] private CameraInterpolationSettings cameraInterpolationSettings;
		[SerializeField] private float cameraAnimationDuration = 1f;

		private Vector3 initialLocalPosition;
		private float pillarRotation;
		private float height;

		public override void Jump(int floorNumber)
		{
			var floor = tower.GetFloor(floorNumber);
			transform.parent = floor.transform;
			transform.localPosition = initialLocalPosition;
			CameraAnimation(floor);
		}

		private void Start()
		{
			initialLocalPosition = transform.localPosition;
			var floor = tower.GetFloor(0);
			transform.parent = floor.transform;
			transform.localPosition = initialLocalPosition;
			pillarRotation = floor.transform.localEulerAngles.y;
			height = floor.transform.position.y;
			AdjustCamera();
		}

		private void CameraAnimation(Floor3D floor)
		{
			DOTween.To(() => height, x => height = x, floor.Height, cameraAnimationDuration);
			DOTween.To(() => pillarRotation, x => pillarRotation = x, floor.Rotation, cameraAnimationDuration)
				.OnUpdate(AdjustCamera);
		}

		private void AdjustCamera()
		{
			var settings = cameraInterpolationSettings.GetCameraPlacementFromHeightAndAngle(height, pillarRotation, tower.TowerHeight);
			CameraPlacementUtility.AdjustCamera(camera, settings);
		}
	}
}