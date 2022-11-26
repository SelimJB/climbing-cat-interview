using UnityEngine;

namespace DefaultNamespace
{
	public class Climber : MonoBehaviour
	{
		[SerializeField] private Tower tower;

		public void Jump(int floorNumber)
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