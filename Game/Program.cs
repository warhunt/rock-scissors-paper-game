using System;
using System.Linq;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CheckMoves(args);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("EXAMPLE: rock paper scissors");
                Environment.Exit(1);
            }

            Computer comp = new Computer(args);

            Rules rules = new Rules(args);

            Help help = new Help(args, rules);

            Game game = new Game(args, comp, rules, help);

            game.RunGame();
        }

        static private void CheckMoves(string[] moves)
        {
            int argsLenght = moves.Length;

            if (argsLenght <= 1)
            {
                throw new ArgumentException("Moves must be more than 1.");
            }
            if (argsLenght % 2 == 0)
            {
                throw new ArgumentException("The number of moves must be odd.");
            }
            if (moves.Distinct().Count() != argsLenght)
            {
                throw new ArgumentException("Moves must be unique.");
            }
        }


    }
}
