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

        public Board Board { get; set; }

        public Position Source { get; set; }

        public Position Destination { get; set; }

        public Dictionary<int, Player> Players { get; set; } = new Dictionary<int, Player>();

        public Player CurrentPlayer { get; set; }

        public Player OtherPlayer { get; set; }

        public bool IsOver { get; set; }

        public void Play(Position source, Position destination)
        {
            Piece pieceToMove = Board.TakePiece(source);

            if (pieceToMove.IsPossibleMove(CurrentPlayer, destination))
            {
                Piece pieceCaptured = MovePiece(pieceToMove, destination);
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

                ChangeTurn(pieceToMove.Color);
                IsCheck();
            }
            else
            {
                Board.AddPiece(source, pieceToMove);
            }
        }

        public Piece MovePiece(Piece pieceToMove, Position destination)
        {
            Piece pieceToCapture = Board.TakePiece(destination);

            if (pieceToCapture != null)
            {
                CurrentPlayer.Capture(pieceToCapture);
            }

            Board.AddPiece(destination, pieceToMove);
            pieceToMove.AddMovement();

            return pieceToCapture;
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
                            Piece pieceToMove = Board.TakePiece(originalPosition);
                            Piece pieceCaptured = MovePiece(pieceToMove, availableDestination);

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

        private void ChangeTurn(PieceColorEnum currentColor)
        {
            CurrentPlayer = Players.Values.FirstOrDefault(x => x.Color != currentColor);
            OtherPlayer = Players.Values.FirstOrDefault(x => x.Color == currentColor);
            Board.CurrentPiece = null;
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
            Board.AddPiece(new Position(0, 1), new Horse(color1));
            Board.AddPiece(new Position(0, 2), new Bishop(color1));
            Board.AddPiece(new Position(0, 3), new Queen(color1));
            Board.AddPiece(new Position(0, 4), new King(color1));
            Board.AddPiece(new Position(0, 5), new Bishop(color1));
            Board.AddPiece(new Position(0, 6), new Horse(color1));
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
            Board.AddPiece(new Position(7, 1), new Horse(color2));
            Board.AddPiece(new Position(7, 2), new Bishop(color2));
            Board.AddPiece(new Position(7, 3), new Queen(color2));
            Board.AddPiece(new Position(7, 4), new King(color2));
            Board.AddPiece(new Position(7, 5), new Bishop(color2));
            Board.AddPiece(new Position(7, 6), new Horse(color2));
            Board.AddPiece(new Position(7, 7), new Rook(color2));
        }
    }
}
