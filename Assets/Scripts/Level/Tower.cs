using UnityEngine;

namespace ClimbingCat.Level
{
	// TODO : rename Pillar 
	public abstract class Tower : MonoBehaviour
	{
		public abstract void Create(Sequence sequence);
		public abstract void Reset(Sequence sequence);
	}
}