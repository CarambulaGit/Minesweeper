using System.Threading.Tasks;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Logic;
using CodeBase.Infrastructure.Logic.Game;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure {
    public class GameView : MonoBehaviour {
        [SerializeField] private Grid grid;
        private GameFactory _gameFactory;
        public Game Game { get; private set; }

        public async UniTask Initialize(Game game, GameFactory gameFactory) {
            _gameFactory = gameFactory;
            Game = game;
            grid.columns = Game.GameField.GameMode.FieldXSize;
            grid.rows = Game.GameField.GameMode.FieldYSize;
            await FillFieldWithCellsView();
            grid.UpdateGrid();
        }

        private async UniTask FillFieldWithCellsView() {
            for (var y = 0; y < Game.GameField.GameMode.FieldYSize; y++) {
                for (var x = 0; x < Game.GameField.GameMode.FieldXSize; x++) {
                    await CreateCell(new Point(y, x));
                }
            }
        }

        private async UniTask CreateCell(Point point) {
            var cell = await _gameFactory.CreateCell(grid.transform);
            cell.Initialize(Game, Game.GameField, point);
        }
    }
}