using UnityEngine;

namespace DefaultNamespace
{
	public class Climber2 : MonoBehaviour
	{
		[SerializeField] private Tower2 tower;

		private int currentLevel;
		private Vector3 initialLocalPosition;

		public void OnGUI()
		{
			if (GUI.Button(new Rect(0, 0, 100, 100), "Test"))
			{
				Jump(currentLevel + 1);
			}
		}

		public void Jump(int floorNumber)
		{
			var floor = tower.GetFloor(floorNumber);
			transform.parent = floor.transform;
			transform.localPosition = initialLocalPosition;
			currentLevel = floorNumber;
		}

		private void Start()
		{
			initialLocalPosition = transform.localPosition;
			Jump(0);
		}
	}
}