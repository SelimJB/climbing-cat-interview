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
		[SerializeField] private ClimberView climberView;

		private float towerRotation;
		private float height;

		public override void Jump(int floorNumber)
		{
			var floor = tower.GetFloor(floorNumber);
			AdjustPosition(floor);
			CameraAnimation(floor);
		}

		private void Start()
		{
			var floor = tower.GetFloor(0);
			towerRotation = floor.transform.localEulerAngles.y + cameraInterpolationSettings.GetPillarRotationOffset;
			height = floor.transform.position.y;

			AdjustPosition(floor);
			AdjustCamera();
		}

		public void Reset()
		{
			transform.parent = tower.transform;
		}

		private void AdjustPosition(Floor3D floor)
		{
			transform.parent = floor.ClimberGrip.transform;
			transform.localPosition = Vector3.zero;
		}

		private void CameraAnimation(Floor3D floor)
		{
			var nextRotation = cameraInterpolationSettings.GetPillarRotationOffset + floor.Rotation;

			// TODO : improve
			if (Mathf.Abs(towerRotation - nextRotation) > 180)
			{
				if (towerRotation > 180)
					towerRotation -= 360;
				if (nextRotation > 180)
					nextRotation -= 360;
			}

			DOTween.To(() => height, x => height = x, floor.Height, cameraAnimationDuration);
			DOTween.To(() => towerRotation, x => towerRotation = x, nextRotation, cameraAnimationDuration)
				.OnUpdate(AdjustCamera);
		}

		private void AdjustCamera()
		{
			var settings = cameraInterpolationSettings.GetCameraPlacementFromHeightAndAngle(height, towerRotation, tower.TowerHeight);
			CameraPlacementUtility.AdjustCamera(camera, settings);
		}

		public override void WrongAnswerAnimation()
		{
			climberView.WrongAnswerAnimation();
		}
	}
}