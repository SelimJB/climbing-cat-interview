using UnityEngine;

namespace ClimbingCat.Level
{
	public abstract class Floor : MonoBehaviour
	{
		public abstract void Initialize(Pattern pattern);
	}
}