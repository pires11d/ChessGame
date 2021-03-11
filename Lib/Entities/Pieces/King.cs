using Lib.Enums.Pieces;

namespace Lib.Entities.Pieces
{
    public class King : Piece
    {
        public King(PieceColorEnum color) : base(color)
        {
            Type = PieceTypeEnum.King;
        }
        public King(PieceColorEnum color, Board board) : this(color)
        {
            Board = board;
        }

        public override bool[,] PossibleMoves(Player player)
        {
            var matrix = new bool[Board.Rows, Board.Columns];
            var p = new Position();

            //Top
            p = this.Position.Top();
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            //Bottom
            p = this.Position.Bottom();
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            //Left
            p = this.Position.Left();
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            //Right
            p = this.Position.Right();
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            //TopLeft
            p = this.Position.TopLeft();
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            //TopRight
            p = this.Position.TopRight();
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            //BottomLeft
            p = this.Position.BottomLeft();
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            //BottomRight
            p = this.Position.BottomRight();
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            return matrix;
        }
    }
}
