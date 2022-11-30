using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClimbingCat.InputSystems
{
	// Alternative input controller
	public class KeyBoardInputController : MonoBehaviour
	{
		[SerializeField] private List<ButtonKeyAssociation> buttonKeyAssociations;

		public void Update()
		{
			foreach (var pair in buttonKeyAssociations)
				if (Input.GetKeyDown(pair.key))
					pair.button.SelectPattern();
		}

		[Serializable]
		private class ButtonKeyAssociation
		{
			public PatternButton button;
			public KeyCode key;
		}
	}
}