using UnityEngine;

namespace ClimbingCat.CameraSystems
{
	public class CameraPlacementTest : MonoBehaviour
	{
		[SerializeField] private CameraPlacementSettings settings;

		private void OnDrawGizmos()
		{
			var placement = CameraPlacementUtility.GetPlacement(settings);
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(placement.Position, 0.4f);
			Gizmos.DrawRay(placement.Position, placement.Direction);
		}
	}
}