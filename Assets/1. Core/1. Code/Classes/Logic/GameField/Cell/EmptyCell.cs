namespace CodeBase.Infrastructure.Logic.GameField {
    public class EmptyCell : Cell {
        public EmptyCell(State state = State.Hidden) : base(state) { }
        
        public override string ToString() => "0";
    }
}