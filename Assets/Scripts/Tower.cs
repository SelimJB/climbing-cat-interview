using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
	public class Tower : MonoBehaviour
	{
		[SerializeField] private PatternHolder patternHolderPrefab;

		private List<PatternHolder> floors;

		public PatternHolder GetFloor(int floorNumber)
		{
			if (floors.ElementAtOrDefault(floorNumber) == null)
				throw new Exception($"There is no floor {floorNumber}");

			return floors[floorNumber];
		}

		public void Create(Sequence sequence)
		{
			floors = new List<PatternHolder>();
			var x = 0;

			foreach (var pattern in sequence.Patterns)
			{
				var patternHolder = Instantiate(patternHolderPrefab, new Vector3(x, 0, 0), Quaternion.identity);
				patternHolder.Initialize(pattern);
				floors.Add(patternHolder);
				patternHolder.transform.parent = transform;
				x++;
			}
		}

		public void Reset(Sequence sequence)
		{
			foreach (var floor in floors)
			{
				Destroy(floor.gameObject);
			}

			floors = new List<PatternHolder>();

			Create(sequence);
		}
	}
}