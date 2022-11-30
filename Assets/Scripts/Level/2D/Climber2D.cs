using UnityEngine;

namespace ClimbingCat.Level
{
	public class Climber2D : Climber
	{
		[SerializeField] private Tower2D tower;

		public override void Jump(int floorNumber)
		{
			var floor = tower.GetFloor(floorNumber);
			transform.position = floor.transform.position;
		}

		private void Start()
		{
			Jump(0);
		}
	}
}