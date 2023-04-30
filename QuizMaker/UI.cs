
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using static QuizMaker.Data;

namespace QuizMaker
{
    internal class UI
    {
        public static void PrintTheCreatingQuestionsRools()
        {
            Console.WriteLine("Write your questions and answers in one line.");
            Console.WriteLine("Mark correct answer with the star symbol.If there is more than one correct answers add star symbols to all good answers.");
            Console.WriteLine("Here is example:");
            Console.WriteLine("“What color is the sky? | red | blue* | green”.");
            Console.WriteLine("Type “finish” in the console to finish adding questions with answers.");
            Console.WriteLine("Type “start” to begin Quiz game or exit to quit it!");
        }
        public static bool Continue()
        {
            Console.WriteLine("Want to continue?");
            return Console.ReadLine().ToUpper() == "Y";
        }
        public static GameMode SelectGameMode()
        {
            Console.WriteLine("Please make your selection");
            Console.WriteLine("0 Add Questions");
            Console.WriteLine("1 Play Game");
            int Selection = int.Parse(Console.ReadLine());
            switch(Selection)
            {
                case 0:
                    return GameMode.AddQuestions;
                
                case 1:
                    return GameMode.PlayGame;
             
                default:
                    return GameMode.INVALID;
            }
        }

        public static string GetTheUsersQuestionsAndAnswers()
        {
            Console.WriteLine();
            Console.WriteLine("Write your questions with the answers!");
            string userQuestion = Console.ReadLine();
            return userQuestion;
        }

        public static UserQuestionsAndAnswers ParseUserQnAString(string userQna)
        {
            UserQuestionsAndAnswers qna = new UserQuestionsAndAnswers();
          //  int arrayPosition;
            string[] userQnaArray = userQna.Split(" | ");
            // magic numbers
            for (int arrayPosition = 0; arrayPosition < userQnaArray.Length; arrayPosition++)
            {
                userQnaArray[arrayPosition] = userQnaArray[arrayPosition].Trim('*');
                //Console.WriteLine(userQnaArray[i]);
            }

            qna.Question = userQnaArray[0];
            qna.AnswerOne = userQnaArray[1];
            qna.AnswerTwo = userQnaArray[2];
            qna.AnswerThree = userQnaArray[3];
            qna.AnswerFour = userQnaArray[4];

            return qna;
        }

        public static List<string> ParseCorrectAnswers(string userQna)
        {
            List<string> corAnQ = new List<string>();
            string[] userQnaArray = userQna.Split(" | ");
            // magic number
            int arrayPosition;
            for (arrayPosition = 0; arrayPosition < userQnaArray.Length; arrayPosition++)
            {
                string qca = "";
                if (userQnaArray[arrayPosition].Contains('*'))
                {
                    userQnaArray[arrayPosition] = userQnaArray[arrayPosition].Trim('*');
                    qca = userQnaArray[arrayPosition];
                    Console.WriteLine($"{qca}");
                    corAnQ.Add(qca);
                }
            }
            return corAnQ;
        }
    }
}

