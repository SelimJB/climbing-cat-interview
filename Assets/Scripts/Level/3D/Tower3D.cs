using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ClimbingCat.Level
{
	public class Tower3D : Tower
	{
		[SerializeField] private Floor3D floor3DPrefab;
		[SerializeField] private GameObject tower;

		private List<Floor3D> floors;

		private float floorVerticalPadding = 0.5f;
		private float floorHorizontalPadding = 0.05f;
		private float maxDistanceBetweenFloor = 3f;
		private float towerThickness = 1.3f;
		private float towerHeight;

		public float TowerHeight => towerHeight;
		public List<Floor3D> Floors => floors;

		public override void Create(Sequence sequence)
		{
			var height = 0f;
			var minimumDistanceBetweenFloor = floor3DPrefab.Platform.localScale.y + floorVerticalPadding;
			floors = new List<Floor3D>();

			for (var i = 0; i < sequence.Patterns.Count; i++)
			{
				var floor = Instantiate(floor3DPrefab, tower.transform.parent);
				AdjustFloorTransform(floor.transform, height);
				floor.Initialize(sequence.Patterns[i]);
				floors.Add(floor);

				if (i == sequence.Patterns.Count - 1)
					height += floor.Platform.localScale.y - 0.001f;
				else
					height += Random.Range(minimumDistanceBetweenFloor, maxDistanceBetweenFloor);
			}

			towerHeight = height;
			tower.transform.localScale = new Vector3(towerThickness, towerHeight, towerThickness);
			tower.transform.localPosition = new Vector3(0, towerHeight / 2, 0);
		}

		public override void Reset(Sequence sequence)
		{
			foreach (var floor in floors)
				Destroy(floor.gameObject);

			floors = new List<Floor3D>();

			Create(sequence);
		}

		private void AdjustFloorTransform(Transform floor, float height)
		{
			var y = height + floor3DPrefab.Platform.localScale.y / 2;
			floor.transform.localPosition = new Vector3(0, y, 0);
			floor.transform.localRotation = Quaternion.Euler(0, 90 * Random.Range(0, 4), 0);
		}

		public Floor3D GetFloor(int floorNumber)
		{
			if (floors.ElementAtOrDefault(floorNumber) == null)
				throw new Exception($"There is no floor {floorNumber}");

			return floors[floorNumber];
		}
	}
}