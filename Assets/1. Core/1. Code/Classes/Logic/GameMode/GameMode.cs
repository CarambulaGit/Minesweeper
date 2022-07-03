using System;
using UnityEngine;

namespace CodeBase.Logic {
    
    [Serializable]
    public class GameMode {
        [SerializeField] private int _numOfMines;
        [SerializeField] private int _fieldXSize;
        [SerializeField] private int _fieldYSize;

        public int NumOfMines => _numOfMines;

        public int FieldXSize => _fieldXSize;

        public int FieldYSize => _fieldYSize;

        public GameMode(int fieldXSize, int fieldYSize, int numOfMines) {
            _numOfMines = numOfMines;
            _fieldXSize = fieldXSize;
            _fieldYSize = fieldYSize;
        }

        public static GameMode EasyMode() => new GameMode(5, 5, 6);
        public static GameMode MediumMode() => new GameMode(10, 10, 25);
        public static GameMode HardMode() => new GameMode(20, 20, 100);

        public override string ToString()
        {
            return $"X Size = {FieldXSize}\nY Size = {FieldYSize}\nNum of mines = {NumOfMines}";
        }
    }
}