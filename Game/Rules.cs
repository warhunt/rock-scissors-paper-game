using System.Collections.Generic;
using System.Linq;

namespace Game
{
    enum Result
    {
        Win = 1,
        Lose,
        Draw
    }

    class Rules
    {
        public Result[,] RullesArray { get; set; }

        public Rules(string[] moves)
        {
            int moves_lenght = moves.Length;
            RullesArray = new Result[moves_lenght, moves_lenght];

            List<Result> answer_array = new List<Result>();

            answer_array.Add(Result.Draw);

            for (int i = 0; i < (moves_lenght - 1) / 2; i++)
            {
                answer_array.Add(Result.Lose);
            }

            for (int i = 0; i < (moves_lenght - 1) / 2; i++)
            {
                answer_array.Add(Result.Win);
            }

            for (int i = 0; i < moves_lenght; i++)
            {
                for (int j = 0; j < moves_lenght; j++)
                {
                    RullesArray[i, j] = answer_array[j];
                }
                
                Result last = answer_array.Last();
                answer_array.RemoveAt(moves_lenght-1);
                answer_array.Insert(0, last);
            }
        }

        public Result ImWin(int user_move, int computer_move)
        {
            return RullesArray[user_move, computer_move];
        }
    }
}
