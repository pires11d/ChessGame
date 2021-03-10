using Lib.Enums.Pieces;
using System;

namespace Lib.Entities.Pieces
{
    public abstract class Piece
    {
        public Position Position { get; set; }
        public PieceColorEnum Color { get; set; }
        public PieceColorEnum DefaultColor { get; set; }
        public PieceTypeEnum Type { get; protected set; }
        public int Movements { get; protected set; }
        public static Board Board { get; set; }

        public Piece(PieceColorEnum color)
        {
            Position = null;
            Color = color;
            DefaultColor = color;
            Movements = 0;
        }

        public Piece(PieceColorEnum color, Board board) : this(color)
        {
            Board = board;
        }

        public string Code
        {
            get
            {
                return Type.ToString().Substring(0,1);
            }
        }

        public void AddMovement()
        {
            Movements++;
        }

        public override string ToString()
        {
            return Code;
        }

        public void Select()
        {
            Color = PieceColorEnum.Green;
        }

        public void Deselect()
        {
            Color = DefaultColor;
        }

        public bool CanMoveTo(Position position, bool canMoveFreely = true, bool canCapture = true)
        {
            if (!position.IsValid())
                return false;

            var piece = Board.Piece(position);

            return (canMoveFreely && piece == null) || (canCapture && piece != null && piece.DefaultColor != DefaultColor);
        }

        public abstract bool[,] PossibleMoves();
    }
}
