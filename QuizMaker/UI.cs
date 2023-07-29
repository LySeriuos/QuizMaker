
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

                //for (int i = 0; i < qNaList.Count; i++)
                //{
                //    Console.WriteLine(qNaList[i]);
                //}

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

        public static int PrintAnswerResponseToUser(List<string> userAnswerList, List<string> userCorrectAnswers, UserQuestionsAndAnswers randomQuestion, string userAnswer)
        {
            List<string> matchedCorrectAnswers = userCorrectAnswers;
            List<string> savedCorrectAnswers = randomQuestion.CorrectAnswers;
            int totalCorrectAnswers = savedCorrectAnswers.Count;
            // change numberOfCorrectAnswer to totalCorrectAnswers!!
            int points = 0;

            //for (int i = 0; i < totalCorrectAnswers; i++)
            //{
            //    if (userAnswerList.Count != totalCorrectAnswers)
            //    {
            //        Console.WriteLine("There is two correct answers! You entered only 1");
            //        userAnswer = Console.ReadLine();
            //        break;
            //    }
            //    else
            //    {
            //        Console.WriteLine("Error + sitas ");
            //        break;
            //    }
            //}



            if (totalCorrectAnswers == 1 && userAnswerList.Count == 2)
            {
                points = +1;
                Console.WriteLine($"One answer of two is good! It is {matchedCorrectAnswers[0]}");
            }
            else if (totalCorrectAnswers == 2 && userAnswerList.Count == 2)
            {
                points = +2;
                Console.WriteLine($"Two answers is good! They are {matchedCorrectAnswers[0]} and {matchedCorrectAnswers[1]}");
            }
            else if (totalCorrectAnswers == 2 && userAnswerList.Count == 1)
            {
                Console.WriteLine("Should be two options!");
            }
            else if (totalCorrectAnswers == 1 && userAnswerList.Count == 1)
            {
                points = +1;
                Console.WriteLine($"Answer is good! It is {matchedCorrectAnswers[0]}");
            }
            else if (totalCorrectAnswers == 0 && userAnswerList.Count == 2)
            {
                points = -2;
                Console.WriteLine("Wrong both answers!");
            }
            else if (totalCorrectAnswers == 0 && userAnswerList.Count == 1)
            {
                points = -1;
                Console.WriteLine("Wrong answer!");
            }
            else
            {
                Console.WriteLine("Error");
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

        public static bool GetCorrectUsrInput(string userAnswer, UserQuestionsAndAnswers randomQuestion)
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

            return mltpOrSnglAnsw;
        }

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
            //if (userPointsList.Sum() < 0)
            //{
            //    userPoints = 0;
            //    Console.WriteLine($"Your current score is {userPoints}  ");
            //}
            //else
            //{
            //    Console.WriteLine($"Your current score is: {userPointsList.Sum()}");
            //}
            return userPointsList.Sum();
        }

        public static void CheckingInput(char userLetter, UserQuestionsAndAnswers randomQuestion, string userAnswer, List<string> userAnswerList)
        {
            List<string> savedCorrectAnswers = randomQuestion.CorrectAnswers;
            string[] splitedAnswers = userAnswer.Split(',');
            int userInputCount = splitedAnswers.Length;
            bool userInput = false;
            if (userAnswerList.Count == 2)
            {
                userInput = true;
            }
            else
            {
                userInput = false;
            }
            while (userInput)
            {
                Console.WriteLine("Missing one option to play");
                return;
            }
            //while (userAnswer == "")
            //{

            //    if(!Char.IsLetter(userLetter))
            //    {
            //        Console.WriteLine("Wrong character used! Should be A,B,C or D");
            //        continue;
            //    }
            //    else if(savedCorrectAnswers.Count() == 2 && userAnswerList.Count() == 1)
            //    {

            //        Console.WriteLine("Missing one option to play");
            //        continue;
            //    }
            //    else
            //    { 
            //        Console.WriteLine("Error");
            //        break;
            //    }

            //}

            //while(savedCorrectAnswers.Count() == 2 && user < 2)
            //{
            //    Console.WriteLine("Missing one option to play");
            //    continue;
            //}
        }
    }
}

