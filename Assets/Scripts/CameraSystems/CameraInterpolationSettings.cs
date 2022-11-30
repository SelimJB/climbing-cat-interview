using UnityEngine;
using UnityEngine.Serialization;

namespace ClimbingCat.CameraSystems
{
	[CreateAssetMenu(fileName = "CameraInterpolationSettings", menuName = "ScriptableObjects/CameraInterpolationSettings")]
	public class CameraInterpolationSettings : ScriptableObject
	{
		[SerializeField] private AnimationCurve distanceCurve;
		[FormerlySerializedAs("rotationCurve")]
		[SerializeField] private AnimationCurve cameraRotationCurve;
		[SerializeField] private AnimationCurve verticalOffsetCurve;
		[SerializeField] private float distanceFactor = 1;
		[SerializeField] private float verticalOffsetFactor = 1;
		[SerializeField] private float maxPillarRotationOffset = 1;
		[SerializeField] private float minPillarRotationOffset = 1;

		private float GetDistance(float height, float maxHeight) => distanceFactor * distanceCurve.Evaluate(height / maxHeight);
		private float GetCameraRotation(float height, float maxHeight) => 180 - 180 * cameraRotationCurve.Evaluate(height / maxHeight);
		private float GetVerticalOffset(float height, float maxHeight) => verticalOffsetFactor * verticalOffsetCurve.Evaluate(height / maxHeight);
		public float GetPillarRotationOffset => (Random.Range(0, 2) * 2 - 1) * Random.Range(minPillarRotationOffset, maxPillarRotationOffset);

		public CameraPlacementSettings GetCameraPlacementFromHeightAndAngle(float height, float pillarRotation, float maxHeight)
		{
			return new CameraPlacementSettings
			{
				centerRotation = pillarRotation,
				height = height,
				distance = GetDistance(height, maxHeight),
				cameraRotation = GetCameraRotation(height, maxHeight),
				verticalOffset = GetVerticalOffset(height, maxHeight)
			};
		}
	}
}