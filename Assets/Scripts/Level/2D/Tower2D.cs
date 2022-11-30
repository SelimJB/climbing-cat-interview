using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ClimbingCat.Level
{
	public class Tower2D : Tower
	{
		[SerializeField] private Floor floorPrefab;

		private List<Floor> floors;

		public Floor GetFloor(int floorNumber)
		{
			if (floors.ElementAtOrDefault(floorNumber) == null)
				throw new Exception($"There is no floor {floorNumber}");

			return floors[floorNumber];
		}

		public override void Create(Sequence sequence)
		{
			floors = new List<Floor>();
			var x = 0;

			foreach (var pattern in sequence.Patterns)
			{
				var floor = Instantiate(floorPrefab, new Vector3(x, 0, 0), Quaternion.identity);
				floor.Initialize(pattern);
				floors.Add(floor);
				floor.transform.parent = transform;
				x++;
			}
		}

		public override void Reset(Sequence sequence)
		{
			foreach (var floor in floors)
			{
				Destroy(floor.gameObject);
			}

			floors = new List<Floor>();

			Create(sequence);
		}
	}
}