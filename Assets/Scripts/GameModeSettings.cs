namespace ClimbingCat
{
	// TODO : better injection
	public static class GameModeSettings
	{
		public static GameMode Mode = GameMode.Classic;
		public static GameDifficulty Difficulty = GameDifficulty.Easy;
		public static int AvailablePatternNumber => Difficulty == GameDifficulty.Medium || Difficulty == GameDifficulty.VeryHard ? 6 : 3;
	}

	public enum GameMode
	{
		Arcade,
		Classic
	}

	public enum GameDifficulty
	{
		Easy,
		Medium,
		Hard,
		VeryHard
	}
}