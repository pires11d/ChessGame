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

        public override bool[,] PossibleMoves(Player player)
        {
            var matrix = new bool[Board.Rows, Board.Columns];
            var p = new Position();

            //Top
            p = this.Position;
            while (p.IsValid())
            {
                p = p.Top();
                if (CanMoveTo(p))
                    matrix[p.Row, p.Column] = true;
                else
                    break;
            }

            //Bottom
            p = this.Position;
            while (p.IsValid())
            {
                p = p.Bottom();
                if (CanMoveTo(p))
                    matrix[p.Row, p.Column] = true;
                else
                    break;
            }

            //Left
            p = this.Position;
            while (p.IsValid())
            {
                p = p.Left();
                if (CanMoveTo(p))
                    matrix[p.Row, p.Column] = true;
                else
                    break;
            }

            //Right
            p = this.Position;
            while (p.IsValid())
            {
                p = p.Right();
                if (CanMoveTo(p))
                    matrix[p.Row, p.Column] = true;
                else
                    break;
            }

            //TopLeft
            p = this.Position;
            while (p.IsValid())
            {
                p = p.TopLeft();
                if (CanMoveTo(p))
                    matrix[p.Row, p.Column] = true;
                else
                    break;
            }

            //TopRight
            p = this.Position;
            while (p.IsValid())
            {
                p = p.TopRight();
                if (CanMoveTo(p))
                    matrix[p.Row, p.Column] = true;
                else
                    break;
            }

            //BottomLeft
            p = this.Position;
            while (p.IsValid())
            {
                p = p.BottomLeft();
                if (CanMoveTo(p))
                    matrix[p.Row, p.Column] = true;
                else
                    break;
            }

            //BottomRight
            p = this.Position;
            while (p.IsValid())
            {
                p = p.BottomRight();
                if (CanMoveTo(p))
                    matrix[p.Row, p.Column] = true;
                else
                    break;
            }

            return matrix;
        }
    }
}
