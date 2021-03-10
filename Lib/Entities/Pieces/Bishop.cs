using Lib.Entities;
using Lib.Enums.Pieces;

namespace Lib.Entities.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(PieceColorEnum color) : base(color)
        {
            Type = PieceTypeEnum.Bishop;
        }
        public Bishop(PieceColorEnum color, Board board) : this(color)
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
