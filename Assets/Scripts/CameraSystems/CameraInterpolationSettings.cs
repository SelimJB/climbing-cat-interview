using UnityEngine;

namespace ClimbingCat.CameraSystems
{
	[CreateAssetMenu(fileName = "CameraInterpolationSettings", menuName = "ScriptableObjects/CameraInterpolationSettings")]
	public class CameraInterpolationSettings : ScriptableObject
	{
		[SerializeField] private AnimationCurve distanceCurve;
		[SerializeField] private AnimationCurve rotationCurve;
		[SerializeField] private AnimationCurve horizontalOffsetCurve;
		[SerializeField] private AnimationCurve verticalOffsetCurve;
		[SerializeField] private float distanceFactor = 1;
		[SerializeField] private float horizontalOffsetFactor = 1;
		[SerializeField] private float verticalOffsetFactor = 1;

		private float GetDistance(float height, float maxHeight) => distanceFactor * distanceCurve.Evaluate(height / maxHeight);
		private float GetCameraRotation(float height, float maxHeight) => 180 * rotationCurve.Evaluate(height / maxHeight);
		private float GetHorizontalOffset(float height, float maxHeight) => horizontalOffsetFactor * horizontalOffsetCurve.Evaluate(height / maxHeight);
		private float GetVerticalOffset(float height, float maxHeight) => verticalOffsetFactor * verticalOffsetCurve.Evaluate(height / maxHeight);

		public CameraPlacementSettings GetCameraPlacementFromHeightAndAngle(float height, float pillarRotation, float maxHeight)
		{
			return new CameraPlacementSettings
			{
				centerRotation = pillarRotation,
				height = height,
				distance = GetDistance(height, maxHeight),
				cameraRotation = GetCameraRotation(height, maxHeight),
				horizontalOffset = GetHorizontalOffset(height, maxHeight),
				verticalOffset = GetVerticalOffset(height, maxHeight)
			};
		}
	}
}