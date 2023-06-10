
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

        public static List<string> GetMatchedCorrectAnswer(UserQuestionsAndAnswers randomQuestion, List<string> userAnswerList)
        {
            List<string> correctAnswer = randomQuestion.CorrectAnswers;
            int numberOfCorrectAnswers = 0;
            string theCorrectAnswer = "";
            int answerIndex;
            List<int> answersIndexes = new List<int>();
            List<string> userCorrectAnswers = new List<string>();
            for (int userAnswerListIndex = 0; userAnswerListIndex < userAnswerList.Count; userAnswerListIndex++)
            {
                for (int correctAnswerListIndex = 0; correctAnswerListIndex < correctAnswer.Count; correctAnswerListIndex++)
                {
                    if (correctAnswer[correctAnswerListIndex] == userAnswerList[userAnswerListIndex])
                    {
                        numberOfCorrectAnswers++;
                        theCorrectAnswer = correctAnswer[correctAnswerListIndex];
                        answerIndex = correctAnswerListIndex;
                        userCorrectAnswers.Add(theCorrectAnswer);
                        answersIndexes.Add(answerIndex);
                        Console.WriteLine(answerIndex);
                        //Console.WriteLine(correctAnswer[correctAnswerListIndex]);
                        break;
                    }
                }
            }
            return userCorrectAnswers;
        }

        public static void PrintAnswerResponseToUser(UserQuestionsAndAnswers randomQuestion, List<string> userAnswerList, List<string> userCorrectAnswers)
        {

            
            int numberOfCorrectAnswers = userCorrectAnswers.Count;

            
            foreach(string correctAnswer in userCorrectAnswers)
            {

                string theAnswer = correctAnswer;
                int indexNumber = userAnswerList.FindIndex(a => a == theAnswer);
                Console.WriteLine($"{theAnswer} + {indexNumber}");
            }
            // after moving to separate method, use foreach in userCorrect answer and Count method to see how many correct answers it was.
            // use List<T>.FindIndex() to see what index has an userCorrectAnswer has in List<string> correctAnswer = randomQuestion.CorrectAnswers;
            // move this to other method
            if (numberOfCorrectAnswers == 1 && userAnswerList.Count > 1)
            {
                Console.WriteLine($"One answer of two is good {theCorrectAnswer}");
            }
            else if (numberOfCorrectAnswers == 2 && userAnswerList.Count > 1)
            {   
                // this is incorrect because the right answers could be in other indexes
                Console.WriteLine($"Two answers is good {correctAnswer[0]} + {correctAnswer[1]}");
            }
            else if (numberOfCorrectAnswers == 1 && userAnswerList.Count > 2)
            {
                Console.WriteLine($"One answer of two is good {theCorrectAnswer}");
            }
            else if (numberOfCorrectAnswers < 1 && userAnswerList.Count == 2)
            {
                Console.WriteLine("Wrong both answers!");
            }
            else if (numberOfCorrectAnswers < 1 && userAnswerList.Count == 1)
            {
                Console.WriteLine("Wrong answer!");
            }
            else
            {
                Console.WriteLine("Error");
            }
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
                Console.WriteLine(chosedUserAnswer);
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
                        Console.WriteLine("Wrong Selection");
                    }
                }
            }
            return userAnswersList;
        }
    }
}

