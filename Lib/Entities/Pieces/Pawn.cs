using Lib.Enums.Pieces;

namespace Lib.Entities.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(PieceColorEnum color) : base(color)
        {
            Type = PieceTypeEnum.Pawn;
        }
        public Pawn(PieceColorEnum color, Board board) : this(color)
        {
            Board = board;
        }

        public override bool[,] PossibleMoves(Player player)
        {
            var matrix = new bool[Board.Rows, Board.Columns];
            var p = new Position();

            if (player.Number == 1)
            {
                //Bottom
                p = this.Position.Bottom();
                if (CanMoveTo(p, true, false))
                    matrix[p.Row, p.Column] = true;

                //BottomLeft
                p = this.Position.BottomLeft();
                if (CanMoveTo(p, false, true))
                    matrix[p.Row, p.Column] = true;

                //BottomRight
                p = this.Position.BottomRight();
                if (CanMoveTo(p, false, true))
                    matrix[p.Row, p.Column] = true;

                //Bottom(2x)
                if (Movements == 0)
                {
                    p = this.Position.Bottom().Bottom();
                    if (CanMoveTo(p, true, false))
                        matrix[p.Row, p.Column] = true;
                }
            }
            else
            {
                //Top
                p = this.Position.Top();
                if (CanMoveTo(p, true, false))
                    matrix[p.Row, p.Column] = true;

                //TopLeft
                p = this.Position.TopLeft();
                if (CanMoveTo(p, false, true))
                    matrix[p.Row, p.Column] = true;

                //TopRight
                p = this.Position.TopRight();
                if (CanMoveTo(p, false, true))
                    matrix[p.Row, p.Column] = true;

                //Top(2x)
                if (Movements == 0)
                {
                    p = this.Position.Top().Top();
                    if (CanMoveTo(p, true, false))
                        matrix[p.Row, p.Column] = true;
                }
            }

            return matrix;
        }
    }
}
