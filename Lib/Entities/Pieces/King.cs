using Lib.Entities;
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

        public override bool[,] PossibleMoves()
        {
            var matrix = new bool[Board.Rows, Board.Columns];
            var p = new Position();

            //Top
            p = Position.Top();
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            //Bottom
            p = Position.Bottom();
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            //Left
            p = Position.Left();
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            //Right
            p = Position.Right();
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            //TopLeft
            p = Position.TopLeft();
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            //TopRight
            p = Position.TopRight();
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            //BottomLeft
            p = Position.BottomLeft();
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            //BottomRight
            p = Position.BottomRight();
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            return matrix;
        }
    }
}
