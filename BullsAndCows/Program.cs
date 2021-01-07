using System;
using System.Collections.Generic;

namespace BullsAndCows
{
    class Program
    {
        static void Main(string[] args)
        {
            

            

            StartGame();
            Console.WriteLine();

            Console.ReadLine();
        }

        private static void StartGame()
        {
            List<string> possibleAnswers = GetAllAnswers();
            string answer = GetOneAnswer(possibleAnswers);


            Console.WriteLine($"Lets go. Your number is {answer} ?");
            Console.Write("Enter bools: ");
            int bull = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter cows: ");
            int cow = Convert.ToInt32(Console.ReadLine());

            if (bull < 0 || bull > 4 || cow < 0 || cow > 4)
            {
                Console.WriteLine("Your digit must be: 1<= digit <=4 ");
            }
            else if (bull == 4 && cow == 0)
            {
                Console.WriteLine($"Game over! Your number is {answer}");
            }


        }

        //Create list with all possible answers
        static List<string> GetAllAnswers()
        {
            List<string> answers = new List<string>();

            for(int i = 0; i < 10000; i++)
            {
                string fmd = "0000";
                string fmt = i.ToString(fmd);
                if (fmt[0] != fmt[1] && fmt[0] != fmt[2] && fmt[0] != fmt[3] && fmt[1] != fmt[2] && fmt[1] != fmt[3] && fmt[2] != fmt[3])
                {
                    answers.Add(i.ToString(fmd));
                }
            }
            return answers;
        }

        //get one answer from possible list
        static string GetOneAnswer(List<string> answers)
        {
            Random rnd = new Random();
            int random = rnd.Next(0, answers.Count);
            string answer = answers[random];
            return answer;
        }
        }
    }
}
