using Lib.Enums.Pieces;

namespace Lib.Entities.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(PieceColorEnum color) : base(color)
        {
            Type = PieceTypeEnum.Bishop;
        }

        public override bool[,] PossibleMoves(Player player)
        {
            var matrix = new bool[Board.Rows, Board.Columns];
            Position p;

            //TopLeft
            p = this.Position;
            while (p.IsValid())
            {
                p = p.TopLeft;
                if (CanMoveTo(p))
                {
                    matrix[p.Row, p.Column] = true;
                    if (HasCapture(p))
                        break;
                }
                else
                {
                    break;
                }
            }

            //TopRight
            p = this.Position;
            while (p.IsValid())
            {
                p = p.TopRight;
                if (CanMoveTo(p))
                {
                    matrix[p.Row, p.Column] = true;
                    if (HasCapture(p))
                        break;
                }
                else
                {
                    break;
                }
            }

            //BottomLeft
            p = this.Position;
            while (p.IsValid())
            {
                p = p.BottomLeft;
                if (CanMoveTo(p))
                {
                    matrix[p.Row, p.Column] = true;
                    if (HasCapture(p))
                        break;
                }
                else
                {
                    break;
                }
            }

            //BottomRight
            p = this.Position;
            while (p.IsValid())
            {
                p = p.BottomRight;
                if (CanMoveTo(p))
                {
                    matrix[p.Row, p.Column] = true;
                    if (HasCapture(p))
                        break;
                }
                else
                {
                    break;
                }
            }

            return matrix;
        }
    }
}
