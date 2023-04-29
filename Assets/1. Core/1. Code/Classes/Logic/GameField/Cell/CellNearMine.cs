namespace CodeBase.Infrastructure.Logic.GameField {
    public class CellNearMine : Cell {
        public int NumOfMinesNear { get; set; }

        public CellNearMine(int numOfMinesNear, State state = State.Hidden) : base(state) {
            NumOfMinesNear = numOfMinesNear;
        }

        public CellNearMine(State state = State.Hidden) : this (0, state) { }
        
        public override string ToString() => NumOfMinesNear.ToString();
    }
}