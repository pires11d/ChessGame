using Lib.Entities.Pieces;
using Lib.Enums.Pieces;
using System;
using System.Collections.Generic;

namespace Lib.Entities
{
    public class Board
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        internal Piece[,] Pieces { get; set; }
        public Piece CurrentPiece { get; set; }
        public Piece EnPassant { get; set; } = null;

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Pieces = new Piece[rows, columns];

            Position.MaxRows = rows;
            Position.MaxColumns = columns;
        }

        public void AddPiece(Position position, Piece piece)
        {
            if (ExistsPiece(position))
                throw new ApplicationException("Já existe uma peça nessa posição!");
            piece.Position = position;
            piece.CurrentColor = piece.Color;
            Pieces[position.Row, position.Column] = piece;
        }

        public Piece TakePiece(Position position)
        {
            Piece pieceFound = null;
            if (ExistsPiece(position))
            {
                pieceFound = Piece(position);
                Pieces[position.Row, position.Column] = null;
            }

            return pieceFound;
        }

        public Piece Piece(Position position)
        {
            return GetPieceByIndex(position.Row, position.Column);
        }

        private Piece GetPieceByIndex(int row, int column)
        {
            return Pieces[row, column];
        }

        public void SelectPiece(Piece pieceFound)
        {
            if (pieceFound != null)
            {
                pieceFound.Select();
                Pieces[pieceFound.Position.Row, pieceFound.Position.Column] = pieceFound;
            }
        }

        public void DeselectPiece(Piece pieceFound)
        {
            if (pieceFound != null)
            {
                pieceFound.Deselect();
                Pieces[pieceFound.Position.Row, pieceFound.Position.Column] = pieceFound;
            }
        }

        public List<Piece> GetPieces(PieceColorEnum color)
        {
            var pieceList = new List<Piece>();

            foreach (var piece in Pieces)
            {
                if (piece?.CurrentColor == color)
                    pieceList.Add(piece);
            }

            return pieceList;
        }

        public King GetKing(PieceColorEnum color)
        {
            foreach (var piece in Pieces)
            {
                if (piece is King && piece.CurrentColor == color)
                    return (King)piece;
            }

            return null;
        }

        public bool ExistsPiece(Position position)
        {
            position.Validate();
            return Piece(position) != null;
        }

        public void Print(Player player = null)
        {
            Console.Clear();

            for (int i = 0; i < Rows; i++)
            {
                GameMode(false);
                Console.Write(8 - i + " ");

                for (int j = 0; j < Columns; j++)
                {
                    GameMode(true, i, j);
                    Console.Write(" ");

                    var piece = GetPieceByIndex(i, j);
                    if (piece != null)
                    {
                        Console.ForegroundColor = (ConsoleColor)piece.CurrentColor;
                        Console.Write(piece);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        GetPossibleMoves(i, j, player);
                    }

                    Console.Write(" ");
                }

                GameMode(false);
                Console.WriteLine();
            }

            GameMode(false);
            Console.Write("   ");
            for (int k = 0; k < Columns; k++)
            {
                int charNumber = k + 65;
                Console.Write((char)charNumber + "  ");
            }

            Console.WriteLine("\n");
        }

        private void GetPossibleMoves(int i, int j, Player player)
        {
            if (player != null && 
                CurrentPiece != null && 
                CurrentPiece.PossibleMoves(player)[i, j])
                Console.Write("*");
            else
                Console.Write(" ");
        }

        public void GameMode(bool isGame, int i = 0, int j = 0)
        {
            if (isGame)
            {
                if (i % 2 == 0)
                    if (j % 2 == 0)
                        Console.BackgroundColor = ConsoleColor.White;
                    else
                        Console.BackgroundColor = ConsoleColor.Black;
                else
                    if (j % 2 != 0)
                        Console.BackgroundColor = ConsoleColor.White;
                    else
                        Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
