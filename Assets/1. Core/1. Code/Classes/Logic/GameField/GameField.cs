using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeBase.Logic;
using MyBox;
using RelationsInspector.Extensions;

namespace CodeBase.Infrastructure.Logic.GameField {
    public class GameField {
        public GameMode GameMode { get; private set; }
        public Cell[,] Field { get; private set; }
        public List<Point> MinesPoints { get; private set; } = new();
        public bool CompletelyInitialized { get; private set; }

        public event Action OnCompletelyInitialized;

        public GameField(GameMode gameMode) {
            GameMode = gameMode;
            Field = new Cell[gameMode.FieldYSize, gameMode.FieldXSize];
            InitializeWithEmptyCells();
        }

        public void Open(Point point) {
            if (!CompletelyInitialized) {
                InitializeWithMines(point);
            }

            var cell = Field.Get(point);
            if (cell.Opened) return;
            cell.Open();
            if (cell is EmptyCell) SmartOpenEmptyCell(point);
        }

        public void SwapFlagState(Point point) => Field.Get(point).SwapFlagState();

        public override string ToString() {
            var result = new StringBuilder();
            for (var y = 0; y < GameMode.FieldYSize; y++) {
                for (var x = 0; x < GameMode.FieldXSize; x++) {
                    result.Append($"{Field[y, x]} ");
                }

                result.Append("\n");
            }

            return result.ToString();
        }

        public string VisibleMask() {
            var result = new StringBuilder();
            for (var y = 0; y < GameMode.FieldYSize; y++) {
                for (var x = 0; x < GameMode.FieldXSize; x++) {
                    result.Append($"{Field[y, x].VisibleMask} ");
                }

                result.Append("\n");
            }

            return result.ToString();
        }

        private void SmartOpenEmptyCell(Point point) {
            // if (Field.Get(point).Opened) return;
            var neighborsPoints = FindPointNeighbors(point);
            var emptyCellsPoints = neighborsPoints.Where(neighbor => Field.Get(neighbor) is EmptyCell {Opened: false, Hidden: true}).ToList();
            neighborsPoints.ForEach(neighbor => Field.Get(neighbor).Open()); // todo don't if flagged
            emptyCellsPoints.ForEach(SmartOpenEmptyCell);
        }

        private void InitializeWithMines(Point emptyCellPoint) {
            FillWithMines(emptyCellPoint);
            InitializeCellsNearMines();
            CompletelyInitialized = true;
            OnCompletelyInitialized?.Invoke();
        }

        private void InitializeWithEmptyCells() => Field.FillWith(() => new EmptyCell());

        private void FillWithMines(Point emptyCellPoint) {
            MinesPoints.Clear();
            var indexes = Enumerable.Range(0, Field.Length).ToList();
            indexes.Remove(Field.GetListIndexOf(emptyCellPoint));
            var mineIndexes = indexes.Shuffle().Take(GameMode.NumOfMines).ToList();
            foreach (var index in mineIndexes) {
                var point = Field.PointByListIndex(index);
                Field.GetRef(point) = Field.Get(point).ConvertToCellWithMine();
                MinesPoints.Add(point);
            }
        }

        private void InitializeCellsNearMines() {
            foreach (var point in MinesPoints) {
                var neighbors = FindPointNeighbors(point);
                neighbors.ForEach(AddInfoAboutMine);
            }
        }

        private void AddInfoAboutMine(Point point) {
            if (Field.Get(point) is CellWithMine) return;
            if (Field.Get(point) is EmptyCell emptyCell) {
                Field.GetRef(point) = emptyCell.ConvertToCellNearMine();
            }

            ((CellNearMine) Field.GetRef(point)).NumOfMinesNear++;
        }

        private List<Point> FindPointNeighbors(Point point) {
            var result = point.FindNeighbors();
            result.RemoveWhere(neighbor =>
                neighbor.X < 0 || neighbor.Y < 0 ||
                neighbor.X >= GameMode.FieldXSize || neighbor.Y >= GameMode.FieldYSize);
            return result;
        }

        private List<Cell> SelectNeighborsByPoint(Point point) =>
            FindPointNeighbors(point).Select(neighbor => Field.Get(neighbor)).ToList();
    }
}