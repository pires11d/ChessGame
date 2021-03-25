using Lib.Enums.Pieces;

namespace Lib.Entities.Pieces
{
    public abstract class Piece
    {
        public Position Position { get; set; }
        public PieceColorEnum CurrentColor { get; set; }
        public PieceColorEnum Color { get; set; }
        public PieceTypeEnum Type { get; protected set; }
        public int Movements { get; protected set; }
        public static Board Board { get; set; }

        public Piece(PieceColorEnum color)
        {
            Position = null;
            CurrentColor = color;
            Color = color;
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

        public void RemoveMovement()
        {
            Movements--;
        }

        public override string ToString()
        {
            return Code;
        }

        public void Select()
        {
            Board.CurrentPiece = this;
            CurrentColor = PieceColorEnum.Green;
        }

        public void Deselect()
        {
            Board.CurrentPiece = null;
            CurrentColor = Color;
        }

        internal bool CanMoveTo(Position position, bool canMoveFreely = true, bool canCapture = true)
        {
            if (!position.IsValid())
                return false;

            var piece = Board.Piece(position);

            return (canMoveFreely && piece == null) || (canCapture && piece != null && piece?.Color != Color);
        }

        internal bool HasCapture(Position position)
        {
            if (!position.IsValid())
                return false;

            var piece = Board.Piece(position);

            if (piece == null)
                return false;

            return (piece.Color != Color);
        }

        public abstract bool[,] PossibleMoves(Player player);

        public bool IsPossibleMove(Player player, Position position)
        {
            return PossibleMoves(player)[position.Row, position.Column];
        }
    }
}
