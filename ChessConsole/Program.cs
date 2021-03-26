using Lib.Entities;
using System;
using System.Linq;

namespace ChessConsole
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
                        game.Source = new Position(input1);
                        game.Board.CurrentPiece = game.Board.Piece(game.Source);

                        if (game.Board.CurrentPiece != null)
                        {
                            if (game.Board.CurrentPiece.CurrentColor == game.CurrentPlayer.Color)
                            {
                                found = true;
                                game.Board.SelectPiece(game.Board.CurrentPiece);
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
                    game.Board.DeselectPiece(game.Board.CurrentPiece);
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
                    var (x,p) = game.Play(game.Source, game.Destination);
                }
            }
            catch (Exception ex)
            {
                game.Board.DeselectPiece(game.Board.CurrentPiece);
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

            if (!game.IsOver)
                PlayChess(game, input1, input2);
        }

        private static void PrintRoundInfo(Match game)
        {
            Console.BackgroundColor = ConsoleColor.Black;

            game.Board.Print(game.CurrentPlayer);

            PrintCapturedPieces(game,1);
            PrintCapturedPieces(game,2);

            PrintCurrentPlayer(game);

            Console.Write("Escolha uma peça do tabuleiro para mover: ");
        }

        private static void PrintCapturedPieces(Match game, int n)
        {
            Player current = game.Players[n];
            Player other = game.Players.Values.FirstOrDefault(x => x != current);

            Console.Write("Peças ");
            Console.ForegroundColor = (ConsoleColor)other.Color;
            Console.Write($"{other.Color.GetDescription().Pluralize()} ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("capturadas: [");
            Console.ForegroundColor = (ConsoleColor)other.Color;
            Console.Write($"{string.Join(",", current.PiecesCaptured.Select(x => x.Code).ToList())}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("]\n");
        }

        private static void PrintCurrentPlayer(Match game)
        {
            Console.Write("\nJogador da vez: Cor ");
            Console.ForegroundColor = (ConsoleColor)game.CurrentPlayer.Color;
            Console.Write($"{game.CurrentPlayer.Color.GetDescription()} \n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Turno: {game.Turn}\n");
        }
    }
}
