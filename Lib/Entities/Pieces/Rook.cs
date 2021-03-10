using Lib.Entities;
using Lib.Enums.Pieces;

namespace Lib.Entities.Pieces
{
    public class Rook : Piece
    {
        public Rook(PieceColorEnum color) : base(color)
        {
            Type = PieceTypeEnum.Rook;
        }
        public Rook(PieceColorEnum color, Board board) : this(color)
        {
            Board = board;
        }

        public override bool[,] PossibleMoves()
        {
            var matrix = new bool[Board.Rows, Board.Columns];

            return matrix;
        }
    }
}
