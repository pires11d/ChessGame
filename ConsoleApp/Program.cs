using Lib.Entities;
using Lib.Enums.Pieces;
using System;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PlayChess();
        }

        private static void PlayChess(Match game = null, string input1 = null, string input2 = null)
        {
            if (game == null)
                game = new Match();

            Console.Clear();
            game.Board.Print();
            Console.WriteLine();

            bool found = false;
            while (!found)
            {
                try
                {
                    Console.Write("Escolha uma peça do tabuleiro para mover: ");
                    input1 = Console.ReadLine();

                    if (!String.IsNullOrEmpty(input1))
                    {
                        game.Origin = new Position(input1);
                        found = game.Board.ExistsPiece(game.Origin);
                        var pieceFound = game.Board.Piece(game.Origin);
                        if (pieceFound != null)
                            game.Board.SelectPiece(pieceFound);
                        else
                            Console.WriteLine("Peça não encontrada... Tente novamente!");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }

            Console.Clear();
            game.Board.Print();
            Console.WriteLine();

            try
            {
                Console.Write("Digite a posição para onde deseja mover a peça: ");
                input2 = Console.ReadLine();

                if (!String.IsNullOrEmpty(input2))
                {
                    game.Destination = new Position(input2);
                    game.MovePiece(game.Origin, game.Destination);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }

            PlayChess(game, input1, input2);
        }
    }
}
