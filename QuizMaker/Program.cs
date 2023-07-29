using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using static QuizMaker.Data;
using static QuizMaker.UI;
using System.IO;
using System;
using System.Linq;
using System.Collections;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace QuizMaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Temp\UserQuestionsAndAnswers.xml"; //TODO: look at relative paths
            // ask this
            GameMode selection = UI.SelectGameMode();

            switch (selection)
            {
                case GameMode.AddQuestions: //add questions
                    break;

                case GameMode.PlayGame: // play game
                    break;

                default:
                    Console.WriteLine("Unknown choice");
                    return;
            }

            List<UserQuestionsAndAnswers> qNaList = new List<UserQuestionsAndAnswers>();
            string userQuestions;



            if (selection == GameMode.AddQuestions)
            {
                UI.PrintTheCreatingQuestionsRools();
                do
                {

                    userQuestions = UI.GetTheUsersQuestionsAndAnswers(qNaList, path);
                    UserQuestionsAndAnswers uQnA = UI.ParseUserQnAString(userQuestions);
                    List<string> correctAnswers = UI.ParseCorrectAnswers(userQuestions);
                    // Creating empty list if the "path" doesn't exist
                    qNaList = Data.GetQnAListToXml(path);
                    uQnA.CorrectAnswers = correctAnswers;
                    qNaList.Add(uQnA);
                    Data.SaveQnAListToXml(qNaList, path);

                } while (userQuestions.Length > 0);
            }

            if (selection == GameMode.PlayGame)
            {
                UI.GamePlayRools();
                List<int> userPointsList = new List<int>();

                int questionsPlayed = 0;
                int sumOfAllPoints = 0;
                do
                {

                    // getting random question from the list
                    UserQuestionsAndAnswers randomQuestion = Logic.GetRandomQuestion(path);
                    // printing out random question and asnwers to the user
                    QuizCard.GetTheListToString(randomQuestion);
                    string userAnswer = Console.ReadLine().ToUpper();

                    userAnswer = CheckIfNullOrEmpty(userAnswer);
                    userAnswer = GetCorrectUsrInput(userAnswer, randomQuestion);

                    //int usrInpLength = userAnswer.Length;
                    //Console.Write(usrInpLength);
                    //int countedCorrAnsw = randomQuestion.CorrectAnswers.Count();
                    //Console.Write(countedCorrAnsw);
                    //bool mltplAnsw = countedCorrAnsw == 2 && usrInpLength == 3;
                    //Console.WriteLine(mltplAnsw);
                    //bool snglAnsw = countedCorrAnsw == 1 && usrInpLength == 1;
                    //Console.WriteLine(snglAnsw);
                    //bool sandwich = mltplAnsw || snglAnsw;
                    //Console.WriteLine(sandwich);
                    //bool usrInput;
                    //char charResult;

                    //while (!sandwich)
                    //{

                    //    Console.WriteLine("bad input, put nother one");
                    //    userAnswer = Console.ReadLine().ToUpper();
                    //    break;

                    //}



                    List<string> userInputArray = UI.GetUserAnswerOption(userAnswer, randomQuestion);

                    //while (userAnswer.Length > 1)
                    //{
                    //    if (!Char.IsLetter(userLetter))
                    //    {
                    //        Console.WriteLine("Wrong character used! Should be A,B,C or D");
                    //        userAnswer = Console.ReadLine().ToUpper();
                    //        continue;
                    //    }
                    //    else if (userInputArray.Count() < randomQuestion.CorrectAnswers.Count())
                    //    {
                    //        Console.WriteLine("Missing one");
                    //        break;
                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine("Error 3");
                    //        break;
                    //    }
                    //}

                    List<string> userCorrectAnswers = Logic.GetMatchedCorrectAnswer(randomQuestion, userInputArray);
                    int points = UI.PrintAnswerResponseToUser(userInputArray, userCorrectAnswers, randomQuestion, userAnswer);
                    sumOfAllPoints = UI.AddingPoints(points, userPointsList);
                    //UI.CheckingInput(userLetter, randomQuestion, userAnswer, userInputArray);


                    questionsPlayed++;
                } while (questionsPlayed < 20);
                Console.WriteLine($"Your total points after 20 questions: {sumOfAllPoints} ");
            }
        }

        //3. Push array to txt file.
        // Create a list and add questions with answers to that list

        // Spliting questions from answers


        //4. On game play Print out Random questons and 3 different answers to choose from.
        //5. Ask user to enter correct answer.
        //6. Check if user's answer is matching with correct answer in txt file. Can be multiple answers correct.
        //7. Add winning points if it was correct. Print it later at the end of the game.
        // Bonus:
        //8. Save players name and scores to the txt file and show the top score at the beggining of the game.
    }
}
