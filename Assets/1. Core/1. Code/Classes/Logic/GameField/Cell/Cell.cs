using System;

namespace CodeBase.Infrastructure.Logic.GameField {
    public abstract class Cell {
        public enum State {
            Hidden,
            Flagged,
            Opened
        }

        public State CurrentState { get; private set; }
        public bool Opened => CurrentState == State.Opened;
        public bool Hidden => CurrentState == State.Hidden;
        public bool Flagged => CurrentState == State.Flagged;
        public event Action StateChanged;

        public string VisibleMask => Opened ? ToString() : "?";

        protected Cell(State state = State.Hidden) {
            CurrentState = state;
        }

        public CellWithMine ConvertToCellWithMine() => new() {StateChanged = StateChanged};
        public CellNearMine ConvertToCellNearMine() => new() {StateChanged = StateChanged};
        
        public void SwapFlagState() {
            switch (CurrentState) {
                case State.Opened:
                    return;
                case State.Flagged:
                    RemoveFlag();
                    break;
                case State.Hidden:
                default:
                    SetFlag();
                    break;
            }
        }

        public void RemoveFlag() {
            if (CurrentState is State.Opened or State.Hidden) {
                return;
            }

            CurrentState = State.Hidden;
            StateChanged?.Invoke();
        }

        public void SetFlag() {
            if (CurrentState is State.Opened or State.Flagged) {
                return;
            }

            CurrentState = State.Flagged;
            StateChanged?.Invoke();
        }

        public virtual void Open() {
            if (CurrentState is State.Opened) {
                return;
            }

            CurrentState = State.Opened;
            StateChanged?.Invoke();
        }
    }
}