using Lib.Enums.Pieces;

namespace Lib.Entities.Pieces
{
    public class Rook : Piece
    {
        public Rook(PieceColorEnum color) : base(color)
        {
            Type = PieceTypeEnum.Rook;
        }

        public override bool[,] PossibleMoves(Player player)
        {
            var matrix = new bool[Board.Rows, Board.Columns];
            Position p;
            
            //Top
            p = this.Position;
            while (p.IsValid())
            {
                p = p.Top;
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

            //Bottom
            p = this.Position;
            while (p.IsValid())
            {
                p = p.Bottom;
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

            //Left
            p = this.Position;
            while (p.IsValid())
            {
                p = p.Left;
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

            //Right
            p = this.Position;
            while (p.IsValid())
            {
                p = p.Right;
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
