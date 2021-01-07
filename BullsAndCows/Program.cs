using System;
using System.Collections.Generic;

namespace BullsAndCows
{
    class Program
    {
        static List<string> possibleAnswers = GetAllAnswers();
        static void Main(string[] args)
        {
            possibleAnswers = GetAllAnswers();

            //while (true)
            //{
            //    var tuple = (bulls: 0, cows: 0);
            //    string bulls = Console.ReadLine();
            //    string cows = Console.ReadLine();
            //    tuple = GetCountOfBullsAndCowsInTwoNumbers(bulls, cows);
            //    Console.WriteLine($"bulls: {tuple.bulls}, cows: {tuple.cows}");
            //}


            StartGame();
            Console.WriteLine();

            Console.ReadLine();
        }

        private static void StartGame()
        {
            string currentAnswer = GetOneAnswer(possibleAnswers);
            List<string> currentPossibleAnswers = possibleAnswers;

            Console.WriteLine($"Lets go. Your number is {currentAnswer} ?");
            Console.Write("Enter bools: ");
            int bulls = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter cows: ");
            int cows = Convert.ToInt32(Console.ReadLine());

            if (bulls < 0 || bulls > 4 || cows < 0 || cows > 4)
            {
                Console.WriteLine("Your digit must be: 1<= digit <=4 ");
            }
            else if (bulls == 4 && cows == 0)
            {
                Console.WriteLine($"Game over! Your number is {currentAnswer}");
            }

            possibleAnswers = Sieve(currentAnswer, bulls, cows, currentPossibleAnswers);
        }

        private static List<string> Sieve(string currentAnswer, int bulls, int cows, List<string> currentPossibleAnswers)
        {
            List<string> newPossibleAnswers = new List<string>();
            for(int i = 0; i < currentPossibleAnswers.Count; i++)
            {
                var tuple = GetCountOfBullsAndCowsInTwoNumbers(currentAnswer, currentPossibleAnswers[i]);
                if(bulls == tuple.Item1 && cows == tuple.Item2)
                {
                    newPossibleAnswers.Add(currentPossibleAnswers[i]);
                }
            }
            return newPossibleAnswers;
        }

        private static (int, int) GetCountOfBullsAndCowsInTwoNumbers(string currentAnswer, string answerFromPossibleMassive)
        {
            int[] current = GetIntMass(currentAnswer);
            int[] possible = GetIntMass(answerFromPossibleMassive);

            var tuple = (bulls: 0, cows: 0);

            for(int i = 0; i < current.Length; i++)
            {
                if (current[i] == possible[i])
                {
                    tuple.bulls++;
                }
                else
                {
                    for (int k = 0; k < possible.Length; k++)
                    {
                        if (current[i] == possible[k])
                        {
                            tuple.cows++;
                        }
                    }
                }
            }

            
            return tuple;
        }

        static int[] GetIntMass(string str)
        {
            int[] massOfNumbers = new int[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                massOfNumbers[i] = Convert.ToInt32(str[i]);
            }
            return massOfNumbers;
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
