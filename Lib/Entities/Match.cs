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

        public Board Board { get; set; }

        public Position Origin { get; set; }

        public Position Destination { get; set; }

        public List<Player> Players { get; set; } = new List<Player>();

        public Player CurrentPlayer { get; set; }

        public Player OtherPlayer { get; set; }

        public bool Over { get; set; }

        public void MovePiece(Position origin, Position destination)
        {
            Piece pieceToMove = Board.TakePiece(origin);

            if (pieceToMove.PossibleMoves(CurrentPlayer)[destination.Row, destination.Column])
            {
                Piece pieceToCapture = Board.TakePiece(destination);
                if (pieceToCapture != null)
                    CurrentPlayer.Capture(pieceToCapture);
                Board.AddPiece(destination, pieceToMove);
                pieceToMove.AddMovement();
                ChangeTurn(pieceToMove.Color);
            }
            else
            {
                Board.AddPiece(origin, pieceToMove);
            }
        }

        private void ChangeTurn(PieceColorEnum currentColor)
        {
            CurrentPlayer = Players.FirstOrDefault(x => x.Color != currentColor);
            OtherPlayer = Players.FirstOrDefault(x => x.Color == currentColor);
            Board.CurrentPiece = null;
        }

        private void StartNewGame(PieceColorEnum color1, PieceColorEnum color2)
        {
            Piece.Board = Board;

            Players.Add(new Player(1, color1));
            Players.Add(new Player(2, color2));

            ChangeTurn(color1);

            AddPieces(color1, color2);
        }

        private void AddPieces(PieceColorEnum color1, PieceColorEnum color2)
        {
            //Player 1
            Board.AddPiece(new Position(0, 0), new Rook(color1));
            Board.AddPiece(new Position(0, 1), new Horse(color1));
            Board.AddPiece(new Position(0, 2), new Bishop(color1));
            Board.AddPiece(new Position(0, 3), new King(color1));
            Board.AddPiece(new Position(0, 4), new Queen(color1));
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
            Board.AddPiece(new Position(7, 3), new King(color2));
            Board.AddPiece(new Position(7, 4), new Queen(color2));
            Board.AddPiece(new Position(7, 5), new Bishop(color2));
            Board.AddPiece(new Position(7, 6), new Horse(color2));
            Board.AddPiece(new Position(7, 7), new Rook(color2));
        }
    }
}
