using System;
using System.Collections.Generic;

namespace BullsAndCows
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> possibleAnswers = GetAllAnswers();

            string answer = GetOneAnswer(possibleAnswers);


            Console.WriteLine(answer);

            Console.ReadLine();
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
