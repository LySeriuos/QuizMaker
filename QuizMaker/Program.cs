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
            // path where the xml file with the list data is
            string path = @"C:\Temp\UserQuestionsAndAnswers.xml";
            // special method to have control over the main menu
            GameMode selection = UI.SelectGameMode();
            // the main game menu
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
            // creating new List using the class
            List<UserQuestionsAndAnswers> qNaList = new List<UserQuestionsAndAnswers>();
            string userQuestions;
            // code is lounching if player chosed to add questions
            if (selection == GameMode.AddQuestions)
            {
                UI.PrintRoolsToCreateQnA();
                do
                {
                    userQuestions = UI.TakeUserInputAsQnA(qNaList, path);
                    UserQuestionsAndAnswers uQnA = UI.ParseUserQnAString(userQuestions);
                    List<string> correctAnswers = UI.ParseCorrectAnswers(userQuestions);
                    // Creating empty list if the "path" doesn't exist
                    qNaList = Data.GetQnAListToXml(path);
                    uQnA.CorrectAnswers = correctAnswers;
                    bool questionExist = CheckIfQuestionAlreadyExsist(qNaList, uQnA);                    
                    switch(questionExist)
                    {
                        case true:
                            Console.WriteLine("Question already exist, please add other question");
                            continue;
                        case false:
                            qNaList.Add(uQnA);
                            Data.SaveQnAListToXml(qNaList, path);
                            Console.WriteLine("Your question and answers has been added!");
                            break;
                    }

                } while (userQuestions.Length > 0 && Console.ReadKey().Key != ConsoleKey.E); // how to quit adding questionor going back to the main menu.
            }

            if (selection == GameMode.PlayGame)
            {
                UI.GamePlayRools();
                int questionsPlayed = 0;
                int sumOfAllPoints = 0;
                int totalQuestionsToPlay = 20;
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
                    int points = Logic.CountingGamePoints(userCorrectAnswers, randomQuestion);
                    sumOfAllPoints = sumOfAllPoints + points;
                    questionsPlayed++;
                } while (questionsPlayed < totalQuestionsToPlay);
                if (sumOfAllPoints < 0)
                {
                    sumOfAllPoints = 0;
                }
                else
                {
                    sumOfAllPoints = sumOfAllPoints;
                }
                Console.WriteLine($"Your total points after 20 questions: {sumOfAllPoints}");
            }
        }
        //4. On game play Print out Random questons and 4 different answers to choose from.
        //7. Add winning points if it was correct. Print it later at the end of the game.
        // Bonus:
        //8. Save players name and scores to the txt file and show the top score at the beggining of the game.
    }
}
