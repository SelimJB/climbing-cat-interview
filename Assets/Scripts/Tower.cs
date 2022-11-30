using UnityEngine;

namespace DefaultNamespace
{
	public abstract class Tower : MonoBehaviour
	{
		public abstract void Create(Sequence sequence);
		public abstract void Reset(Sequence sequence);
		public abstract Floor GetFloor(int floorNumber);
	}
}