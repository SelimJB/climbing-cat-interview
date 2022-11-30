using UnityEngine;

namespace ClimbingCat.Level
{
	public class Climber3D : Climber
	{
		[SerializeField] private Tower3D tower;

		private Vector3 initialLocalPosition;

		public override void Jump(int floorNumber)
		{
			var floor = tower.GetFloor(floorNumber);
			transform.parent = floor.transform;
			transform.localPosition = initialLocalPosition;
		}

		private void Start()
		{
			initialLocalPosition = transform.localPosition;
			Jump(0);
		}
	}
}