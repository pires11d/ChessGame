using Lib.Enums.Pieces;

namespace Lib.Entities.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(PieceColorEnum color) : base(color)
        {
            Type = PieceTypeEnum.Pawn;
        }

        public override bool[,] PossibleMoves(Player player)
        {
            var matrix = new bool[Board.Rows, Board.Columns];
            Position p;

            //Black
            if (player.Number == 1)
            {
                //Bottom
                p = this.Position.Bottom;
                if (CanMoveTo(p, true, false))
                    matrix[p.Row, p.Column] = true;

                //BottomLeft
                p = this.Position.BottomLeft;
                if (CanMoveTo(p, false, true))
                    matrix[p.Row, p.Column] = true;

                //BottomRight
                p = this.Position.BottomRight;
                if (CanMoveTo(p, false, true))
                    matrix[p.Row, p.Column] = true;

                //Bottom(2x)
                if (Movements == 0)
                {
                    p = this.Position.Bottom.Bottom;
                    if (CanMoveTo(p, true, false))
                        matrix[p.Row, p.Column] = true;
                }

                //EnPassant
                if (this.Position.Row == Board.Rows - 4)
                {
                    p = this.Position.Left;
                    if (HasCapture(p) && Board.Piece(p) == Board.EnPassant)
                    {
                        p = p.Bottom;
                        matrix[p.Row, p.Column] = true;
                    }
                    p = this.Position.Right;
                    if (HasCapture(p) && Board.Piece(p) == Board.EnPassant)
                    {
                        p = p.Bottom;
                        matrix[p.Row, p.Column] = true;
                    }
                }
            }
            //White
            else
            {
                //Top
                p = this.Position.Top;
                if (CanMoveTo(p, true, false))
                    matrix[p.Row, p.Column] = true;

                //TopLeft
                p = this.Position.TopLeft;
                if (CanMoveTo(p, false, true))
                    matrix[p.Row, p.Column] = true;

                //TopRight
                p = this.Position.TopRight;
                if (CanMoveTo(p, false, true))
                    matrix[p.Row, p.Column] = true;

                //Top(2x)
                if (Movements == 0)
                {
                    p = this.Position.Top.Top;
                    if (CanMoveTo(p, true, false))
                        matrix[p.Row, p.Column] = true;
                }

                //EnPassant
                if (this.Position.Row == 3)
                {
                    p = this.Position.Left;
                    if (HasCapture(p) && Board.Piece(p) == Board.EnPassant)
                    {
                        p = p.Top;
                        matrix[p.Row, p.Column] = true;
                    }
                    p = this.Position.Right;
                    if (HasCapture(p) && Board.Piece(p) == Board.EnPassant)
                    {
                        p = p.Top;
                        matrix[p.Row, p.Column] = true;
                    }
                }
            }

            return matrix;
        }
    }
}
