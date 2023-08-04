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
                    QuizCard.PrintQuestionsAndAnswers(randomQuestion); //TODO move to ui
                    string userAnswer = Console.ReadLine().ToUpper();

                    userAnswer = CheckIfNullOrEmpty(userAnswer);
                    userAnswer = CheckIfINputIsALetter(userAnswer);
                    userAnswer = GetCorrectUsrInput(userAnswer, randomQuestion);

                    List<string> userInputArray = UI.GetUserAnswerOption(userAnswer, randomQuestion);                    
                    List<string> userCorrectAnswers = Logic.GetMatchedCorrectAnswer(randomQuestion, userInputArray);
                    int points = UI.CountingGamePoints(userCorrectAnswers, randomQuestion);
                    UI.PrintPointsResponse(points, userCorrectAnswers);
                    Console.WriteLine(points);
                    sumOfAllPoints = sumOfAllPoints + points;// = UI.AddingPoints(points, userPointsList);
                    Console.WriteLine(sumOfAllPoints);
                   
                    questionsPlayed++;
                } while (questionsPlayed < 20);
                Console.WriteLine($"Your total points after 20 questions: {sumOfAllPoints}");
            }
        }
        //4. On game play Print out Random questons and 4 different answers to choose from.
        //7. Add winning points if it was correct. Print it later at the end of the game.
        // Bonus:
        //8. Save players name and scores to the txt file and show the top score at the beggining of the game.
    }
}
