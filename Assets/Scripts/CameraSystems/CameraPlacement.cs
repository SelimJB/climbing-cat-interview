using UnityEngine;

namespace ClimbingCat.CameraSystems
{
	public class CameraPlacement
	{
		public Vector3 Direction { get; }
		public Vector3 Position { get; }

		public CameraPlacement(Vector3 direction, Vector3 position)
		{
			Direction = direction;
			Position = position;
		}
	}
}