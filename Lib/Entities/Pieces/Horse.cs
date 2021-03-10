using Lib.Entities;
using Lib.Enums.Pieces;

namespace Lib.Entities.Pieces
{
    public class Horse : Piece
    {
        public Horse(PieceColorEnum color) : base(color)
        {
            Type = PieceTypeEnum.Horse;
        }
        public Horse(PieceColorEnum color, Board board) : this(color)
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
