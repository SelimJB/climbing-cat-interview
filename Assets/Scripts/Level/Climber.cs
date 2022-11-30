using UnityEngine;

namespace ClimbingCat.Level
{
	public abstract class Climber : MonoBehaviour
	{
		public abstract void Jump(int floorNumber);
		public abstract void WrongAnswerAnimation();
	}
}