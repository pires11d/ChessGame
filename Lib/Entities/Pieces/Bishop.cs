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

        public override bool[,] PossibleMoves(Player player)
        {
            var matrix = new bool[Board.Rows, Board.Columns];
            var p = new Position();

            //TopLeft
            p = this.Position;
            while (p.IsValid())
            {
                p = p.TopLeft();
                if (CanMoveTo(p))
                    matrix[p.Row, p.Column] = true;
            }

            //TopRight
            p = this.Position;
            while (p.IsValid())
            {
                p = p.TopRight();
                if (CanMoveTo(p))
                    matrix[p.Row, p.Column] = true;
            }

            //BottomLeft
            p = this.Position;
            while (p.IsValid())
            {
                p = p.BottomLeft();
                if (CanMoveTo(p))
                    matrix[p.Row, p.Column] = true;
            }

            //BottomRight
            p = this.Position;
            while (p.IsValid())
            {
                p = p.BottomRight();
                if (CanMoveTo(p))
                    matrix[p.Row, p.Column] = true;
            }

            return matrix;
        }
    }
}
