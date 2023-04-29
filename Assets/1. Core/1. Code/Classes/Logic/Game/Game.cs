using System;
using CodeBase.Infrastructure.Logic.GameField;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructure.Logic.Game {
    public class Game {
        public GameField.GameField GameField { get; private set; }
        public GameMode GameMode { get; }

        public event Action<bool> GameEndEvent;
        public event Action GameOverEvent;
        public event Action WinEvent;

        public Game(GameMode gameMode) {
            GameMode = gameMode;
            GameField = new GameField.GameField(GameMode);
            GameField.OnCompletelyInitialized += StartCheckForGameEnd;
        }

        public void OpenCell(Point point) => GameField.Open(point);
        public void SwapFlagState(Point point) => GameField.SwapFlagState(point);

        private void StartCheckForGameEnd() {
            // todo unsubscribe
            GameField.MinesPoints.ForEach(point => ((CellWithMine) GameField.Field.Get(point)).OnExplode += GameOver);
            GameField.Field.ForEach(cell => cell.StateChanged += CheckForWin);
        }

        private void CheckForWin() {
            if (GameField.Field.Any(cell => cell.Hidden)) return;
            Win();
        }

        private void GameOver() {
            Debug.Log("GAME OVER");
            GameEndEvent?.Invoke(false);
            GameOverEvent?.Invoke();
        }

        private void Win() {
            Debug.Log("WIN");
            GameEndEvent?.Invoke(true);
            WinEvent?.Invoke();
        }
    }
}