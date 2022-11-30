using System.Collections.Generic;
using ClimbingCat.CameraSystems;
using UnityEngine;

namespace ClimbingCat.Level
{
	public class FloorCameraPlacementVisualiser : MonoBehaviour
	{
		[SerializeField] private Tower3D tower;
		[SerializeField] private CameraInterpolationSettings settings;

		private readonly List<CameraPlacement> placements = new List<CameraPlacement>();

		private void Start()
		{
			foreach (var f in tower.Floors)
				placements.Add(CameraPlacementUtility.GetPlacement(settings.GetCameraPlacementFromHeightAndAngle(f.Height, f.Rotation, tower.TowerHeight)));
		}

		private void OnDrawGizmos()
		{
			if (!Application.isPlaying) return;

			foreach (var placement in placements)
			{
				Gizmos.color = Color.cyan;
				Gizmos.DrawSphere(placement.Position, 0.25f);
				Gizmos.DrawRay(placement.Position, placement.Direction * 2f);
			}
		}
	}
}