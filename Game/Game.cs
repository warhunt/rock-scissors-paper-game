using System;

namespace Game
{
    class Game
    {
        public Rules _rules { get; set; }
        public Help _help { get; set; }
        public Computer _computer { get; set; }
        public string[] _moves { get; set; }
        public Game(string[] moves, Computer comp, Rules rules, Help help)
        {
            _computer = comp;
            _rules = rules;
            _help = help;
            _moves = moves;
        }

        private void GetMenu()
        {
            Console.WriteLine($"HMAC: {BitConverter.ToString(_computer.Hmac)}");
            Console.WriteLine("Available moves:");
            for (int index = 0; index < _moves.Length; index++)
            {
                Console.WriteLine($"{index + 1} - {_moves[index]}");
            }
            Console.WriteLine("0 - exit");
            Console.WriteLine("? - help");
            Console.Write("Enter your move: ");
        }

        public void RunGame()
        {
            while (true)
            {
                _computer.MakeMove();

                GetMenu();

                string user_move = Console.ReadLine();

                if (Int32.TryParse(user_move, out int res) && 0 < res && res <= _moves.Length)
                {
                    GetResult(res);
                    continue;
                }
                if (user_move == "?")
                {
                    _help.GetHelp();
                    continue;
                }
                if (user_move == "0")
                {
                    break;
                }
                Console.WriteLine("An incorrect value was entered. Try again");
                
            }

        }

        private void GetResult(int user_move)
        {
            Console.WriteLine($"Your move: {_moves[user_move - 1]}");
            Console.WriteLine($"Computer move: {_moves[_computer.Move]}");
            Result result = _rules.ImWin(user_move - 1, _computer.Move);
            switch (result)
            {
                case Result.Win:
                    {
                        Console.WriteLine("You win!");
                        break;
                    }
                case Result.Lose:
                    {
                        Console.WriteLine("You lose!");
                        break;
                    }
                case Result.Draw:
                    {
                        Console.WriteLine("Draw!");
                        break;
                    }
            }
            Console.WriteLine($"HMAC key: {BitConverter.ToString(_computer.SecretKey)}");
        }
    }
}
