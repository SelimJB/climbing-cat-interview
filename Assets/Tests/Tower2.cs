using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
	public class Tower2 : MonoBehaviour
	{
		[SerializeField] private Floor floorPrefab;
		[SerializeField] private GameObject tower;

		private List<Floor> floors;

		private float floorVerticalPadding = 0.5f;
		private float floorHorizontalPadding = 0.05f;
		private float maxDistanceBetweenFloor = 4f;
		private float towerThickness = 1.3f;

		private void Awake()
		{
			Create(new Sequence(10));
		}

		public void Create(Sequence sequence)
		{
			var height = 0f;
			var minimumDistanceBetweenFloor = floorPrefab.Platform.localScale.y + floorVerticalPadding;
			floors = new List<Floor>();

			for (var i = 0; i < sequence.Patterns.Count; i++)
			{
				var pattern = sequence.Patterns[i];
				var floor = Instantiate(floorPrefab, tower.transform.parent);

				AdjustFloorTransform(floor.transform, height);
				floor.Initialize(pattern);
				floors.Add(floor);

				if (i == sequence.Patterns.Count - 1)
					height += floor.Platform.localScale.y - 0.001f;
				else
					height += Random.Range(minimumDistanceBetweenFloor, maxDistanceBetweenFloor);
				// transform.parent = parent;
				// x++;
			}

			tower.transform.localScale = new Vector3(towerThickness, height, towerThickness);
			tower.transform.localPosition = new Vector3(0, height / 2, 0);
		}

		private void AdjustFloorTransform(Transform floor, float height)
		{
			var y = height + floorPrefab.Platform.localScale.y / 2;
			floor.transform.localPosition = new Vector3(0, y, 0);
			floor.transform.localRotation = Quaternion.Euler(0, 90 * Random.Range(0, 4), 0);
		}

		public Floor GetFloor(int floorNumber)
		{
			if (floors.ElementAtOrDefault(floorNumber) == null)
				throw new Exception($"There is no floor {floorNumber}");

			return floors[floorNumber];
		}
	}
}