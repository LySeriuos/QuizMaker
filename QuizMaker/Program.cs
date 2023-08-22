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
using static System.Net.Mime.MediaTypeNames;

namespace QuizMaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
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
                    case GameMode.Exit:
                        return;
                    default:
                        Console.WriteLine("Unknown choice");
                        return;
                }
                // creating new List using the class
                List<UserQuestionsAndAnswers> qNaList = new List<UserQuestionsAndAnswers>();
                string userQuestions;
                // code is launching if player chosed to add questions
                if (selection == GameMode.AddQuestions)
                {
                    UI.PrintRoolsToCreateQnA();
                    do
                    {
                        // taking user questions and answers
                        userQuestions = UI.TakeUserInputAsQnA(qNaList, path);

                        // quiting adding questions and coming back to game menu after user pressed "e"
                        if (userQuestions.ToUpper() == "E")
                        {
                            break;
                        }
                        // splitting user input and assigning these as variables using class
                        UserQuestionsAndAnswers uQnA = UI.ParseUserQnAString(userQuestions);

                        // splitting user input to get and assign marked correct answer 
                        List<string> correctAnswers = UI.ParseCorrectAnswers(userQuestions);

                        // Gettig List if it was saved or creating empty list if the "path" doesn't exist
                        qNaList = Data.GetQnAListToXml(path);

                        // assigning correct answers to the same class as questions and answers
                        uQnA.CorrectAnswers = correctAnswers;

                        // checking if exactly the same question already is in the saved list
                        bool questionExist = CheckIfQuestionAlreadyExsist(qNaList, uQnA);

                        // using switch case to add questions and answers if these are unique or throw error to ask for unique question
                        switch (questionExist)
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

                    } while (userQuestions.Length > 0); // how to quit adding question or going back to the main menu.
                }
                // if user chose option to play a game this code will be lounched
                else if (selection == GameMode.PlayGame)
                {
                    UI.GamePlayRools();
                    int questionsPlayed = 0;
                    int sumOfAllPoints = 0;
                    int totalQuestionsToPlay = 20;
                    List<UserQuestionsAndAnswers> savedQnAList = Data.GetQnAListToXml(path);
                    do
                    {
                        // getting and pritning to user random question from the list
                        UserQuestionsAndAnswers randomQuestion = Logic.GetRandomQuestion(savedQnAList);

                        // printing out random question and asnwer options to the user
                        UI.PrintQuestionsAndAnswers(randomQuestion);
                        string userAnswer = Console.ReadLine().ToUpper();
                        if (userAnswer.ToUpper() == "E")
                        {
                            break;
                        }
                        // user input validation
                        userAnswer = CheckIfNullOrEmpty(userAnswer);
                        userAnswer = CheckIfINputIsALetter(userAnswer);
                        userAnswer = GetCorrectUsrInput(userAnswer, randomQuestion);

                        // Taking user input as answer after validation
                        List<string> userInputArray = UI.TakeFromUserAnswerOption(userAnswer, randomQuestion);

                        // Get matched correct answers
                        List<string> userCorrectAnswers = Logic.GetMatchedCorrectAnswer(randomQuestion, userInputArray);

                        // counting points for good and bad answers
                        int points = Logic.CountingGamePoints(userCorrectAnswers, randomQuestion);
                        sumOfAllPoints = sumOfAllPoints + points;
                        questionsPlayed++;
                    } while (questionsPlayed < totalQuestionsToPlay);

                    // No negative result after played game
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
            //7. Add winning points if it was correct. Print it later at the end of the game.
            // Bonus:
            //8. Save players name and scores to the txt file and show the top score at the beggining of the game.
        }        
    }
    
}
