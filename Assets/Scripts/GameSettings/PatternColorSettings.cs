using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ClimbingCat.GameSettings
{
	[CreateAssetMenu(fileName = "PatternColorSettings", menuName = "ScriptableObjects/PatternColorSettings")]
	public class PatternColorSettings : ScriptableObject
	{
		[SerializeField] private List<PatternColorAssociation> patternColorSettings;

		[Serializable]
		private class PatternColorAssociation
		{
			public Pattern pattern;
			public Color color;
		}

		public Color GetPatternColor(Pattern pattern)
		{
			var res = patternColorSettings.FirstOrDefault(i => i.pattern == pattern)?.color;
			if (res != null) return res.Value;

			Debug.LogError($"No color associated to this pattern : ${pattern}");
			return Color.magenta;
		}
	}
}