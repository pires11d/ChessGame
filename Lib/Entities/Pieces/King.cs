using Lib.Enums.Pieces;
using System;

namespace Lib.Entities.Pieces
{
    public class King : Piece
    {
        public King(PieceColorEnum color) : base(color)
        {
            Type = PieceTypeEnum.King;
        }

        public bool IsOnCheck { get; set; }

        public override bool[,] PossibleMoves(Player player)
        {
            var matrix = new bool[Board.Rows, Board.Columns];
            Position p;

            //Top
            p = this.Position.Top;
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            //Bottom
            p = this.Position.Bottom;
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            //Left
            p = this.Position.Left;
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            //Right
            p = this.Position.Right;
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            //TopLeft
            p = this.Position.TopLeft;
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            //TopRight
            p = this.Position.TopRight;
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            //BottomLeft
            p = this.Position.BottomLeft;
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            //BottomRight
            p = this.Position.BottomRight;
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            //Castling
            if (Movements == 0 && !IsOnCheck)
            {
                //short
                if (HasRook(this.Position.Right.Right.Right))
                {
                    var p1 = this.Position.Right;
                    var p2 = this.Position.Right.Right;
                    if (Board.Piece(p1) == null && Board.Piece(p2) == null)
                        matrix[p2.Row, p2.Column] = true;
                }
                //long
                if (HasRook(this.Position.Left.Left.Left.Left))
                {
                    var p1 = this.Position.Left;
                    var p2 = this.Position.Left.Left;
                    var p3 = this.Position.Left.Left.Left;
                    if (Board.Piece(p1) == null && Board.Piece(p2) == null && Board.Piece(p3) == null)
                        matrix[p2.Row, p2.Column] = true;
                }
            }

            return matrix;
        }

        private bool HasRook(Position position)
        {
            Piece rook = Board.Piece(position);
            return (rook != null && rook is Rook && rook.Color == Color && rook.Movements == 0);
        }
    }
}
