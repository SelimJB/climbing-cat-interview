using ClimbingCat.Level;
using UnityEngine;

namespace ClimbingCat.TestingHelpers
{
	public class TestClimbing : MonoBehaviour
	{
		[SerializeField] private Climber3D climber;
		[SerializeField] private Tower tower;

		private int level;

		private void Awake()
		{
			tower.Create(new Sequence(25));
		}

		public void OnGUI()
		{
			if (GUI.Button(new Rect(0, 0, 200, 200), "Climb"))
			{
				level++;

				if (level % 25 == 0)
				{
					climber.Reset();
					tower.Reset(new Sequence(25));
				}

				climber.Jump(level % 25);
			}
		}
	}
}