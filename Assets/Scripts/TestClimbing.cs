using UnityEngine;

namespace DefaultNamespace
{
	public class TestClimbing : MonoBehaviour
	{
		[SerializeField] private Climber climber;

		private int currentLevel;

		public void OnGUI()
		{
			if (GUI.Button(new Rect(0, 0, 100, 100), "Test"))
			{
				climber.Jump(currentLevel + 1);
				currentLevel++;
			}
		}
	}
}