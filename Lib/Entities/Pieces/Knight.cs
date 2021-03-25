using Lib.Enums.Pieces;

namespace Lib.Entities.Pieces
{
    public class Knight : Piece
    {
        public Knight(PieceColorEnum color) : base(color)
        {
            Type = PieceTypeEnum.Night;
        }

        public override bool[,] PossibleMoves(Player player)
        {
            var matrix = new bool[Board.Rows, Board.Columns];
            Position p;

            //L-TopTopLeft
            p = this.Position;
            p = p.Top.Top.Left;
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;
            //L-TopTopRight
            p = this.Position;
            p = p.Top.Top.Right;
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            //L-BottomBottomLeft
            p = this.Position;
            p = p.Bottom.Bottom.Left;
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;
            //L-BottomBottomRight
            p = this.Position;
            p = p.Bottom.Bottom.Right;
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            //L-LeftLeftTop
            p = this.Position;
            p = p.Left.Left.Top;
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;
            //L-LeftLeftBottom
            p = this.Position;
            p = p.Left.Left.Bottom;
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            //L-RightRightTop
            p = this.Position;
            p = p.Right.Right.Top;
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;
            //L-RightRightBottom
            p = this.Position;
            p = p.Right.Right.Bottom;
            if (CanMoveTo(p))
                matrix[p.Row, p.Column] = true;

            return matrix;
        }
    }
}
