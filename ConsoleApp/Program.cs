using Lib.Entities;
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

            bool found = false;
            while (!found)
            {
                try
                {
                    PrintRoundInfo(game);
                    input1 = Console.ReadLine();

                    if (!string.IsNullOrEmpty(input1))
                    {
                        game.Origin = new Position(input1);
                        var pieceFound = game.Board.Piece(game.Origin);

                        if (pieceFound != null)
                        {
                            if (pieceFound.Color == game.CurrentPlayer.Color)
                            {
                                found = true;
                                game.Board.SelectPiece(pieceFound);
                            }
                            else
                            {
                                found = false;
                                Console.WriteLine("Esta peça pertence ao adversário! Por favor escolha outra.");
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            found = false;
                            Console.WriteLine("Peça não encontrada... Tente novamente!");
                            Console.ReadKey();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }
            }

            game.Board.Print(game.CurrentPlayer);

            try
            {
                Console.Write("Digite a posição para onde deseja mover a peça: ");
                input2 = Console.ReadLine();

                if (!string.IsNullOrEmpty(input2))
                {
                    game.Destination = new Position(input2);
                    game.MovePiece(game.Origin, game.Destination);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

            if (!game.isOver)
                PlayChess(game, input1, input2);
        }

        private static void PrintRoundInfo(Match game)
        {
            Console.BackgroundColor = ConsoleColor.Black;

            game.Board.Print(game.CurrentPlayer);

            Console.WriteLine($"Peças capturadas pelo jogador 1: {game.CurrentPlayer.PiecesCaptured.Count}");
            Console.WriteLine($"Peças capturadas pelo jogador 2: {game.OtherPlayer.PiecesCaptured.Count}\n");

            Console.Write($"Jogador da vez: Cor ");
            Console.ForegroundColor = (ConsoleColor)game.CurrentPlayer.Color;
            Console.Write($"{game.CurrentPlayer.Color.GetDescription()} \n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Escolha uma peça do tabuleiro para mover: ");
        }
    }
}
