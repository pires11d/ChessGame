using Lib.Entities;
using Lib.Enums.Pieces;

namespace Lib.Entities.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(PieceColorEnum color) : base(color)
        {
            Type = PieceTypeEnum.Pawn;
        }
        public Pawn(PieceColorEnum color, Board board) : this(color)
        {
            Board = board;
        }

        public override bool[,] PossibleMoves()
        {
            var matrix = new bool[Board.Rows, Board.Columns];
            var p = new Position();

            //Top
            p = Position.Top();
            if (CanMoveTo(p, true, false))
                matrix[p.Row, p.Column] = true;

            //TopLeft
            p = Position.TopLeft();
            if (CanMoveTo(p, false, true))
                matrix[p.Row, p.Column] = true;

            //TopRight
            p = Position.TopRight();
            if (CanMoveTo(p, false, true))
                matrix[p.Row, p.Column] = true;

            return matrix;
        }
    }
}
