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
            // this must be moved to separate method which going to count all the points
            int userPoints = 0;

            //1. Create data, UI and logic classes.

            UI.PrintTheCreatingQuestionsRools();
            //2. Get the questions and answers from the user and add them to Objects array.


            if (selection == GameMode.AddQuestions)
            {
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
                List<int> userPointsList = new List<int>();
                string userAnswer;
                do
                {
                    // getting random question from the list
                    UserQuestionsAndAnswers randomQuestion = Logic.GetRandomQuestion(path);
                    // printing out random question and asnwers to the user
                    QuizCard.GetTheListToString(randomQuestion);
                    userAnswer = Console.ReadLine().ToUpper();
                    char userLetter = userAnswer[0];
                    if (!Char.IsLetter(userLetter))
                    {
                        Console.WriteLine("Wrong character used! Should be A,B,C or D");
                        continue;
                    }
                    List<string> userInputArray = UI.GetUserAnswerOption(userAnswer, randomQuestion);
                    List<string> userCorrectAnswers = Logic.GetMatchedCorrectAnswer(randomQuestion, userInputArray);                    
                    int points = UI.PrintAnswerResponseToUser(userInputArray, userCorrectAnswers);
                    UI.AddingPoints(points, userPointsList);
                } while (userAnswer != "");
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
