using Lib.Entities;
using Lib.Enums.Pieces;

namespace Lib.Entities.Pieces
{
    public class Queen : Piece
    {
        public Queen(PieceColorEnum color) : base(color)
        {
            Type = PieceTypeEnum.Queen;
        }
        public Queen(PieceColorEnum color, Board board) : this(color)
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
