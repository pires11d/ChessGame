using Lib.Entities.Pieces;
using Lib.Enums.Pieces;
using System;

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

        public void MovePiece(Position origin, Position destination)
        {
            Piece pieceToMove = Board.TakePiece(origin);

            if (pieceToMove.PossibleMoves()[destination.Row, destination.Column])
            {
                Piece pieceToCapture = Board.TakePiece(destination);
                Board.AddPiece(destination, pieceToMove);
                pieceToMove.AddMovement();
            }
            else
            {
                Board.AddPiece(origin, pieceToMove);
            }
        }

        private void StartNewGame(PieceColorEnum color1, PieceColorEnum color2)
        {
            Piece.Board = Board;

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
