using System;
using UnityEngine;

namespace ClimbingCat.CameraSystems
{
	[Serializable]
	public class CameraPlacementSettings
	{
		// Polar coordinate elements
		public float centerRotation;
		public float distance = 1;
		public Vector3 center = Vector3.zero;
		// Other characteristics
		public float height;
		public float horizontalOffset;
		// The camera only has one degree of rotation and can see more or less high in the direction of the center
		public float cameraRotation;
	}
}