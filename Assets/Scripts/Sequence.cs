using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace ClimbingCat
{
	public class Sequence
	{
		private readonly List<Pattern> patterns;
		private int currentIndex;

		public Pattern CurrentPattern => patterns[currentIndex];
		public Pattern NextPattern => patterns[currentIndex + 1];
		public int CurrentIndex => currentIndex;
		public List<Pattern> Patterns => patterns;

		public Sequence(int sequenceLength, int availablePatternNumber = 3)
		{
			patterns = GenerateRandomSequence(sequenceLength, availablePatternNumber);
		}

		public bool IsFinished() => currentIndex + 1 == patterns.Count;

		public void Next()
		{
			if (currentIndex < patterns.Count - 1)
				currentIndex++;
			else
				Debug.LogError("Sequence already completed");
		}

		private List<Pattern> GenerateRandomSequence(int sequenceLength, int availablePatternNumber = 3)
		{
			var sequence = new List<Pattern>();
			var patternValues = Enum.GetValues(typeof(Pattern));
			var random = new Random();

			for (var i = 0; i < sequenceLength; i++)
				sequence.Add((Pattern)patternValues.GetValue(random.Next(availablePatternNumber)));

			return sequence;
		}

		public override string ToString()
		{
			return $"{string.Join("->", patterns)} | Current:(pos={currentIndex}, pattern={CurrentPattern})";
		}
	}
}