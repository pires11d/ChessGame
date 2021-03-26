using Lib.Entities.Pieces;
using Lib.Enums.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib.Entities
{
    public class Match
    {
        public Match()
        {
            Board = new Board(8, 8);
            StartNewGame(PieceColorEnum.Red, PieceColorEnum.Blue);
        }

        public Match(PieceColorEnum color1, PieceColorEnum color2)
        {
            Board = new Board(8, 8);
            StartNewGame(color1, color2);
        }

        public int Turn { get; set; }

        public Board Board { get; set; }

        public Position Source { get; set; }

        public Position Destination { get; set; }

        public Dictionary<int, Player> Players { get; set; } = new Dictionary<int, Player>();

        public Player CurrentPlayer { get; set; }

        public Player OtherPlayer { get; set; }

        public bool IsOver { get; set; }

        public (Position, Position) Play(Position source, Position destination)
        {
            Piece pieceToMove = Board.Piece(source);

            if (pieceToMove.IsPossibleMove(CurrentPlayer, destination))
            {
                Piece pieceCaptured = MovePiece(source, destination);

                if (IsCheck())
                {
                    UndoMovePiece(source, destination, pieceCaptured);
                    if (IsCheckMate())
                    {
                        this.IsOver = true;
                        throw new ApplicationException("Xeque mate!!!");
                    }
                    else
                    {
                        throw new ApplicationException("Você não pode se colocar em xeque!");
                    }
                }

                var (x,y) = SpecialMove(pieceToMove, source, destination);

                ChangeTurn(pieceToMove.Color);
                IsCheck();

                return (x, y);
            }
            else 
            {
                throw new ApplicationException("Posição de destino inválida!");
            }
        }

        public Piece MovePiece(Position source, Position destination)
        {
            Piece pieceToMove = Board.TakePiece(source);
            Piece pieceToCapture = Board.TakePiece(destination);

            if (pieceToCapture != null)
                CurrentPlayer.Capture(pieceToCapture);

            Board.AddPiece(destination, pieceToMove);
            pieceToMove.AddMovement();

            return pieceToCapture;
        }

        private (Position, Position) SpecialMove(Piece currentPiece, Position source, Position destination)
        {
            //EnPassant (flagging)
            if (currentPiece is Pawn && Math.Abs(destination.Row - source.Row) == 2)
                Board.EnPassant = currentPiece;
            else
                Board.EnPassant = null;

            //Castling
            if (currentPiece is King && Math.Abs(destination.Column - source.Column) > 1)
            {
                Position source2, destination2;
                switch (destination.Column - source.Column)
                {
                    //short
                    case 2:
                        source2 = source.Right.Right.Right;
                        destination2 = source.Right;
                        MovePiece(source2, destination2);
                        return (source2, destination2);
                    //long
                    case -2:
                        source2 = source.Left.Left.Left.Left;
                        destination2 = source.Left;
                        MovePiece(source2, destination2);
                        return (source2, destination2);
                }
            }
            //Promotion
            else if (currentPiece is Pawn && (destination.Row == 0 || destination.Row == Board.Rows))
            {
                Board.TakePiece(currentPiece.Position);
                Board.AddPiece(destination, new Queen(currentPiece.CurrentColor));
                return (null, destination);
            }
            //EnPassant (capturing)
            else if (currentPiece is Pawn && destination.Column != source.Column)
            {
                Position shift;
                if (CurrentPlayer.Number == 1)
                    shift = destination.Top;
                else
                    shift = destination.Bottom;
                CurrentPlayer.Capture(Board.TakePiece(shift));
                return (shift, null);
            }

            return (null, null);
        }

        public void UndoMovePiece(Position origin, Position destination, Piece pieceCaptured)
        {
            Piece pieceToUndo = Board.TakePiece(destination);
            Board.AddPiece(origin, pieceToUndo);
            pieceToUndo.RemoveMovement();

            if (pieceCaptured != null)
            {
                CurrentPlayer.UnCapture(pieceCaptured);
                Board.AddPiece(destination, pieceCaptured);
            }
        }

        public bool IsCheck()
        {
            bool playerIsOnCheck = false;
            King king = CurrentPlayer.King;
            List<Piece> piecesByColor = OtherPlayer.Pieces;

            foreach (Piece piece in piecesByColor)
            {
                if (piece.IsPossibleMove(OtherPlayer, king.Position))
                    playerIsOnCheck = true;
            }

            Players[CurrentPlayer.Number].IsOnCheck = playerIsOnCheck;
            king.IsOnCheck = playerIsOnCheck;

            return playerIsOnCheck;
        }

        public bool IsCheckMate()
        {
            foreach (Piece piece in CurrentPlayer.Pieces)
            {
                for (int i = 0; i < Board.Rows; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        Position originalPosition = piece.Position;
                        Position availableDestination = new Position(i, j);

                        if (piece.IsPossibleMove(CurrentPlayer, availableDestination))
                        {
                            Piece pieceCaptured = MovePiece(originalPosition, availableDestination);
                            bool isCheck = IsCheck();
                            UndoMovePiece(originalPosition, availableDestination, pieceCaptured);

                            if (!isCheck)
                                return false;
                        }
                    }
                }
            }

            return true;
        }

        private void ChangeTurn(PieceColorEnum? currentColor)
        {
            if (currentColor != null)
            {
                Turn++;
                CurrentPlayer = Players.Values.FirstOrDefault(x => x.Color != currentColor);
                OtherPlayer = Players.Values.FirstOrDefault(x => x.Color == currentColor);
                Board.CurrentPiece = null;
            }
        }

        private void StartNewGame(PieceColorEnum color1, PieceColorEnum color2)
        {
            Piece.Board = Board;
            Player.Board = Board;

            Players.Add(1, new Player(1, color1));
            Players.Add(2, new Player(2, color2));

            AddPieces(color1, color2);

            ChangeTurn(color1);
        }

        private void AddPieces(PieceColorEnum color1, PieceColorEnum color2)
        {
            //Player 1
            Board.AddPiece(new Position(0, 0), new Rook(color1));
            Board.AddPiece(new Position(0, 1), new Knight(color1));
            Board.AddPiece(new Position(0, 2), new Bishop(color1));
            Board.AddPiece(new Position(0, 3), new Queen(color1));
            Board.AddPiece(new Position(0, 4), new King(color1));
            Board.AddPiece(new Position(0, 5), new Bishop(color1));
            Board.AddPiece(new Position(0, 6), new Knight(color1));
            Board.AddPiece(new Position(0, 7), new Rook(color1));
            Board.AddPiece(new Position(1, 0), new Pawn(color1));
            Board.AddPiece(new Position(1, 1), new Pawn(color1));
            Board.AddPiece(new Position(1, 2), new Pawn(color1));
            Board.AddPiece(new Position(1, 3), new Pawn(color1));
            Board.AddPiece(new Position(1, 4), new Pawn(color1));
            Board.AddPiece(new Position(1, 5), new Pawn(color1));
            Board.AddPiece(new Position(1, 6), new Pawn(color1));
            Board.AddPiece(new Position(1, 7), new Pawn(color1));

            //Player 2
            Board.AddPiece(new Position(6, 0), new Pawn(color2));
            Board.AddPiece(new Position(6, 1), new Pawn(color2));
            Board.AddPiece(new Position(6, 2), new Pawn(color2));
            Board.AddPiece(new Position(6, 3), new Pawn(color2));
            Board.AddPiece(new Position(6, 4), new Pawn(color2));
            Board.AddPiece(new Position(6, 5), new Pawn(color2));
            Board.AddPiece(new Position(6, 6), new Pawn(color2));
            Board.AddPiece(new Position(6, 7), new Pawn(color2));
            Board.AddPiece(new Position(7, 0), new Rook(color2));
            Board.AddPiece(new Position(7, 1), new Knight(color2));
            Board.AddPiece(new Position(7, 2), new Bishop(color2));
            Board.AddPiece(new Position(7, 3), new Queen(color2));
            Board.AddPiece(new Position(7, 4), new King(color2));
            Board.AddPiece(new Position(7, 5), new Bishop(color2));
            Board.AddPiece(new Position(7, 6), new Knight(color2));
            Board.AddPiece(new Position(7, 7), new Rook(color2));
        }
    }
}
