﻿using Lib.Entities.Pieces;
using System;
using System.Text;

namespace Lib.Entities
{
    public class Board
    {
        public int Rows { get; set; }

        public int Columns { get; set; }

        private Piece[,] Pieces { get; set; }

        public Board()
        {
        }

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
            piece.Color = piece.DefaultColor;
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
            pieceFound.Select();
            Pieces[pieceFound.Position.Row, pieceFound.Position.Column] = pieceFound;
        }
        public void DeselectPiece(Piece pieceFound)
        {
            pieceFound.Deselect();
            Pieces[pieceFound.Position.Row, pieceFound.Position.Column] = pieceFound;
        }


        public bool ExistsPiece(Position position)
        {
            position.Validate();
            return Piece(position) != null;
        }

        public void Print()
        {
            for (int i = 0; i < Rows; i++)
            {
                GameMode(false);
                Console.Write(8 - i+" ");

                for (int j = 0; j < Columns; j++)
                {
                    GameMode(true,i,j);
                    Console.Write(" ");

                    var piece = GetPieceByIndex(i, j);
                    if (piece != null)
                    {
                        Console.ForegroundColor = (ConsoleColor)piece.Color;
                        Console.Write(piece);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(" ");
                    }

                    Console.Write(" ");
                }

                GameMode(false);
                Console.WriteLine();
            }

            GameMode(false);
            Console.Write("   ");
            for (int k =0; k < Columns; k++)
            {
                int charNumber = k+65;
                Console.Write((char)charNumber+"  ");
            }

            Console.WriteLine();
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