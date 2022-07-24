using System;
using Unity.Plastic.Newtonsoft.Json;

namespace CodeBase.Logic
{
    [Serializable]
    public class GameMode
    {
        public enum Difficulty
        {
            Custom,
            Easy,
            Medium,
            Hard,
        }

        public int NumOfMines { get; }
        public int FieldXSize { get; }
        public int FieldYSize { get; }
        public Difficulty GameDifficulty { get; }

        [JsonConstructor]
        private GameMode(int fieldXSize, int fieldYSize, int numOfMines, Difficulty gameDifficulty)
        {
            NumOfMines = numOfMines;
            FieldXSize = fieldXSize;
            FieldYSize = fieldYSize;
            GameDifficulty = gameDifficulty;
        }

        public static GameMode GameModeWithDifficulty(Difficulty difficulty)
        {
            return difficulty switch
            {
                Difficulty.Easy => EasyMode(),
                Difficulty.Medium => MediumMode(),
                Difficulty.Hard => HardMode(),
                _ => throw new ArgumentOutOfRangeException(nameof(difficulty), difficulty, null)
            };
        }

        public static GameMode CustomMode(int fieldXSize, int fieldYSize, int numOfMines) =>
            new GameMode(fieldXSize, fieldYSize, numOfMines, Difficulty.Custom);

        private static GameMode EasyMode() => new GameMode(5, 5, 6, Difficulty.Easy);

        private static GameMode MediumMode() => new GameMode(10, 10, 25, Difficulty.Medium);

        private static GameMode HardMode() => new GameMode(20, 20, 100, Difficulty.Hard);

        public override bool Equals(object obj)
        {
            if (obj is not GameMode gameMode) return false;
            return GameDifficulty == gameMode.GameDifficulty && NumOfMines == gameMode.NumOfMines &&
                   FieldXSize == gameMode.FieldXSize && FieldYSize == gameMode.FieldYSize;
        }

        public override string ToString()
        {
            return $"X Size = {FieldXSize}\nY Size = {FieldYSize}\nNum of mines = {NumOfMines}";
        }
    }
}