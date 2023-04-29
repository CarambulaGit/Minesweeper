using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Logic.GameField {
    public class CellWithMine : Cell {
        public event Action OnExplode;
        public CellWithMine(State state = State.Hidden) : base(state) {
            if (CurrentState is State.Opened) {
                Debug.LogWarning("Creating opened mine-cell");
            }
        }

        public override void Open() {
            base.Open();
            OnExplode?.Invoke();
        }
        
        public override string ToString() => "*";
    }
}