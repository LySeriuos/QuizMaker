
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
        }

        public static void GamePlayRools()
        {
            Console.WriteLine("You will get random question with 4 options to choose from.");
            Console.WriteLine("Read question carefully, some questions has two answers.");
            Console.WriteLine("To put answer you need to write A,B,C or D.");
            Console.WriteLine("If there is more than one correct answers you should separate it by “,” As example c,d.");
            Console.WriteLine("You will get points for each correct answer, but for wrong answers it willl be taken.");
            Console.WriteLine("Good luck!");
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
            switch (Selection)
            {
                case 0:
                    return GameMode.AddQuestions;

                case 1:
                    return GameMode.PlayGame;

                default:
                    return GameMode.INVALID;
            }
        }

        public static string GetTheUsersQuestionsAndAnswers(List<UserQuestionsAndAnswers> qNaList, string path)
        {
            if (File.Exists(path))
            {
                qNaList = Data.GetQnAListToXml(path);
            }
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

        public static void PrintPointResponse(int points, List<string> userCorrectAnswers)
        {

            switch (points)
            {                
                case 1:
                    Console.WriteLine($"One answer is good! It is {userCorrectAnswers[0]}");
                    break;
                case 2:
                    Console.WriteLine($"Both answers is good! They are {userCorrectAnswers[0]} and {userCorrectAnswers[1]}"); break;
                case -1:
                    Console.WriteLine("Wrong answer!");
                    break;
                case -2:
                    Console.WriteLine("Wrong both answers!");
                    break;
                default:
                    Console.WriteLine("Something went wrong!"); break;

            }
        }


        public static int CountingGamePoints( List<string> userCorrectAnswers, UserQuestionsAndAnswers randomQuestion)
        {
            int matchedCorrectAnswers = userCorrectAnswers.Count;            
            int savedCorrectAnswers = randomQuestion.CorrectAnswers.Count;
            int points;
            if(matchedCorrectAnswers < savedCorrectAnswers )
            {
                points = matchedCorrectAnswers - savedCorrectAnswers;
                Console.WriteLine(points);
            }
            else
            {
                points = matchedCorrectAnswers;
            }

            return points;
        }

        public static string CheckIfNullOrEmpty(string userAnswer)
        {
            while (string.IsNullOrEmpty(userAnswer))
            {
                Console.WriteLine("Oops, it can't be empty! Try again!");
                userAnswer = Console.ReadLine().ToUpper();
            }
            return userAnswer;
        }

        public static string GetCorrectUsrInput(string userAnswer, UserQuestionsAndAnswers randomQuestion)
        {
            int usrInpLength = userAnswer.Length;
            int countedCorrAnsw = randomQuestion.CorrectAnswers.Count();
            bool mltplAnsw = countedCorrAnsw == 2 && usrInpLength == 3;
            bool snglAnsw = countedCorrAnsw == 1 && usrInpLength == 1;
            bool mltpOrSnglAnsw = mltplAnsw || snglAnsw;

            while (!mltpOrSnglAnsw)
            {
                Console.WriteLine("Check if your answer was proprely formulated! Single answer should be: A or B or C or D. Multiple answers should have ',' between Multiple answers and it should be: A,B or B,C or C,D... ");
                Console.WriteLine("Write your answer again");
                userAnswer = Console.ReadLine().ToUpper();
                break;
            }

            return userAnswer;
        }

        public static string CheckIfINputIsALetter(string userAnswer)
        {
            char userLetterChar = userAnswer[0];

            while (!Char.IsLetter(userLetterChar))
            {
                Console.WriteLine("Wrong character used! Should be A,B,C or D");
                userAnswer = Console.ReadLine().ToUpper();
                break;
            }
            return userAnswer;
        }

        // trying to add all the checks together! This is not functioning properly.
        //public static string UserInputCheck(string userAnswer, UserQuestionsAndAnswers randomQuestion)
        //{            

        //    int usrInpLength = userAnswer.Length;
        //    int countedCorrAnsw = randomQuestion.CorrectAnswers.Count();
        //    bool mltplAnsw = countedCorrAnsw == 2 && usrInpLength == 3;
        //    bool snglAnsw = countedCorrAnsw == 1 && usrInpLength == 1;
        //    bool mltpOrSnglAnsw = mltplAnsw || snglAnsw;
        //    while (string.IsNullOrEmpty(userAnswer))
        //    {
        //        Console.WriteLine("Oops, it can't be empty! Try again!");
        //        userAnswer = Console.ReadLine().ToUpper();
        //        bool usrInptIsALetter = Char.IsLetter(userAnswer[0]);
        //        while (!mltpOrSnglAnsw || !usrInptIsALetter)
        //        {
        //            Console.Write($"usrInputIsNotEmpty:{string.IsNullOrEmpty(userAnswer)}, usrInptIsALetter: {usrInptIsALetter}, mltpOrSnglAnsw: {mltpOrSnglAnsw}");
        //            Console.WriteLine("Check if your answer was proprely formulated! Single answer should be: A or B or C or D. Multiple answers should have ',' between Multiple answers and it should be: A,B or B,C or C,D... ");
        //            Console.WriteLine("Write your answer again");
        //            userAnswer = Console.ReadLine().ToUpper();
        //            break;

        //        }
        //    }
        //    return userAnswer;
        //}

        public static List<string> GetUserAnswerOption(string userAnswer, UserQuestionsAndAnswers randomQuestion)
        {
            // spliting user input for multiple answers
            string[] userInputArray = userAnswer.Split(",");

            List<string> userAnswersList = new List<string>();
            string chosedUserAnswer;
            for (int i = 0; i < userInputArray.Length; i++)
            {
                chosedUserAnswer = userInputArray[i];
                foreach (string input in userInputArray)
                {
                    if (chosedUserAnswer == "A")
                    {
                        chosedUserAnswer = randomQuestion.AnswerOne;
                        userAnswersList.Add(chosedUserAnswer);
                        break;
                    }
                    else if (chosedUserAnswer == "B")
                    {
                        chosedUserAnswer = randomQuestion.AnswerTwo;
                        userAnswersList.Add(chosedUserAnswer);
                        break;
                    }
                    else if (chosedUserAnswer == "C")
                    {
                        chosedUserAnswer = randomQuestion.AnswerThree;
                        userAnswersList.Add(chosedUserAnswer);
                        break;
                    }
                    else if (chosedUserAnswer == "D")
                    {
                        chosedUserAnswer = randomQuestion.AnswerFour;
                        userAnswersList.Add(chosedUserAnswer);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return userAnswersList;
        }
        public static int AddingPoints(int points, List<int> userPointsList)
        {
            // add to list and then print total
            int userPoints = points;
            userPointsList.Add(userPoints);
            return userPointsList.Sum();
        }

    }
}

