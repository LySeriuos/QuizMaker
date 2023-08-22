
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

        public static void PrintRoolsToCreateQnA()
        {
            Console.WriteLine("Write your questions and answers in one line.");
            Console.WriteLine("Mark correct answer with the star symbol.If there is more than one correct answers add star symbols to all good answers.");
            Console.WriteLine("Here is example:");
            Console.WriteLine("“What color is the sky? | red | blue* | green”.");
            Console.WriteLine("To quit adding questions please press e and enter!");
        }

        public static void GamePlayRools()
        {
            Console.WriteLine("You will get 20 random questions with 4 options to choose from.");
            Console.WriteLine("Read question carefully, some questions has two answers.");
            Console.WriteLine("To put answer you need to write A,B,C or D.");
            Console.WriteLine("If there is more than one correct answers you should separate it by “,” As example c,d.");
            Console.WriteLine("You will get points for each correct answer. For wrong answers will be minus points.");
            Console.WriteLine("Your points will be counted after all 20 questions");
            Console.WriteLine("Good luck!");
        }
        /// <summary>
        /// Continue object
        /// </summary>
        /// <returns>if user input is "Y"</returns>
        public static bool Continue()
        {
            Console.WriteLine("Want to continue?");
            return Console.ReadLine().ToUpper() == "Y";
        }
        /// <summary>
        /// The main game menu
        /// </summary>
        /// <returns>return chosed GameMode</returns>
        public static GameMode SelectGameMode()
        {
            Console.WriteLine("Please make your selection\n");
            Console.WriteLine("0 Add Questions");
            Console.WriteLine("1 Play Game");
            Console.WriteLine("2 Exit Game");
            int Selection = int.Parse(Console.ReadLine());
            switch (Selection)
            {
                case 0:
                    return GameMode.AddQuestions;
                case 1:
                    return GameMode.PlayGame;
                case 2:
                    return GameMode.Exit;

                default:
                    return GameMode.INVALID;
            }
        }
        /// <summary>
        /// checking if the file with questions exists and then continue to add questions in it
        /// </summary>
        /// <param name="path">Path to saved local file with questions and asnwers</param>
        /// <returns>User input</returns>
        public static string TakeUserInputAsQnA(string path)
        {
            if (File.Exists(path))
            {
                Data.GetQnAListToXml(path);
            }
            Console.WriteLine();
            Console.WriteLine("Write your questions with the answers!");
            string userQuestion = Console.ReadLine();
            return userQuestion;
        }
        /// <summary>
        /// Spliting user input to separate strings and then assigning to the object class variables
        /// </summary>
        /// <param name="userQna">Users questions and answers input</param>
        /// <returns> Assigned object</returns>

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

        /// <summary>
        /// Spliting user input and assigning to Correct answer List
        /// </summary>
        /// <param name="userQna"></param>
        /// <returns>Correct Answers List</returns>
        public static List<string> ParseCorrectAnswers(string userQna)
        {
            List<string> corAnQ = new List<string>();
            string[] userQnaArray = userQna.Split(" | ");
            int arrayPosition;
            for (arrayPosition = 0; arrayPosition < userQnaArray.Length; arrayPosition++)
            {
                string qca;
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

        /// <summary>
        /// Printing out random question to the User
        /// </summary>
        /// <param name="randomQuestion">Random Questtion from QnA file</param>
        public static void PrintQuestionsAndAnswers(UserQuestionsAndAnswers randomQuestion)
        {
            string question = randomQuestion.Question;
            string answerOne = randomQuestion.AnswerOne;
            string answerTwo = randomQuestion.AnswerTwo;
            string answerThree = randomQuestion.AnswerThree;
            string answerFour = randomQuestion.AnswerFour;
            Console.WriteLine($"\nQuestion: {question} \n\nA: {answerOne} \nB: {answerTwo} \nC: {answerThree} \nD: {answerFour}");
        }

        /// <summary>
        /// Method to check if the question already exists
        /// </summary>
        /// <param name="qNaList"> Saved QnA List in the Local drive </param>
        /// <param name="uQnA">QnA class. It will be used to compare question to question to check if there allready is one</param>
        /// <returns>true or false if question exist or no</returns>
        public static bool CheckIfQuestionAlreadyExsist(List<UserQuestionsAndAnswers> qNaList, UserQuestionsAndAnswers uQnA)
        {
            bool questionExist = true;
            List<string> list = new List<string>();
            foreach (UserQuestionsAndAnswers item in qNaList)
            {
                questionExist = qNaList.Exists(x => x.Question == uQnA.Question);
                Console.WriteLine($"{questionExist}");
                break;
            }
            return questionExist;
        }

        /// <summary>
        /// Qounting points for the user
        /// </summary>
        /// <param name="points">Uer points for good and bad answers</param>
        /// <param name="userCorrectAnswers"> Printing feedback on witch answers was good</param>
        public static void PrintPointsResponse(int points, List<string> userCorrectAnswers)
        {
            switch (points)
            {
                case 1:
                    Console.WriteLine($"One answer is good! It is {userCorrectAnswers[0]}");
                    break;
                case 2:
                    Console.WriteLine($"Both answers is good! They are {userCorrectAnswers[0]} and {userCorrectAnswers[1]}");
                    break;
                case -1:
                    Console.WriteLine("Wrong answer!");
                    break;
                case -2:
                    Console.WriteLine("Wrong both answers!");
                    break;
                default:
                    Console.WriteLine("Something went wrong!");
                    break;
            }
        }

        /// <summary>
        /// Validation method 1
        /// </summary>
        /// <param name="userAnswer">User input</param>
        /// <returns>Validated user input</returns>
        public static string CheckIfNullOrEmpty(string userAnswer)
        {
            while (string.IsNullOrEmpty(userAnswer))
            {
                Console.WriteLine("Oops, it can't be empty! Try again!");
                userAnswer = Console.ReadLine().ToUpper();
            }
            return userAnswer;
        }

        /// <summary>
        /// Validation method to check if user gave multiple answers when random question has more than one correct answer. 
        /// Or single answer to single correct answer.
        /// </summary>
        /// <param name="userAnswer">User input</param>
        /// <param name="randomQuestion">/Random question</param>
        /// <returns>Validated user input</returns>
        public static string GetCorrectUsrInput(string userAnswer, UserQuestionsAndAnswers randomQuestion)
        {
            int usrInpLength = userAnswer.Length;
            int countedCorrAnsw = randomQuestion.CorrectAnswers.Count();
            bool mltplAnsw = countedCorrAnsw == 2 && usrInpLength == 3;
            Console.WriteLine(mltplAnsw);
            bool snglAnsw = countedCorrAnsw == 1 && usrInpLength == 1;
            bool mltpOrSnglAnsw = mltplAnsw && countedCorrAnsw == 2 || snglAnsw && countedCorrAnsw == 1;

            while (!mltpOrSnglAnsw)
            {
                Console.WriteLine("Check if your answer was proprely formulated! Single answer should be: A or B or C or D. Multiple answers should have ',' between Multiple answers and it should be: A,B or B,C or C,D... ");
                Console.WriteLine("Write your answer again");
                userAnswer = Console.ReadLine().ToUpper();
                break;
            }
            return userAnswer;
        }

        /// <summary>
        /// Validation method to check if input is a letter and not a sign
        /// </summary>
        /// <param name="userAnswer">User input</param>
        /// <returns>Validated user input</returns>
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

        /// <summary>
        /// This method takes answer option or options by A,B,C,D letters and then assign them to the real value.
        /// Later it add to the answer list for later.
        /// </summary>
        /// <param name="userAnswer">User Input as answer </param>
        /// <param name="randomQuestion">Random Question</param>
        /// <returns>The list with assigned user answers</returns>
        public static List<string> TakeFromUserAnswerOption(string userAnswer, UserQuestionsAndAnswers randomQuestion)
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
    }
}

