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

            //1. Create data, UI and logic classes.

            UI.PrintTheCreatingQuestionsRools();
            //2. Get the questions and answers from the user and add them to Objects array.

            string userQuestions;


            do
            {
                userQuestions = UI.GetTheUsersQuestionsAndAnswers(qNaList, path);
                UserQuestionsAndAnswers uQnA = UI.ParseUserQnAString(userQuestions);
                List<string> correctAnswers = UI.ParseCorrectAnswers(userQuestions);

                if (selection == GameMode.AddQuestions)
                {
                    qNaList = Data.GetQnAListToXml(path);
                    GetTheUsersQuestionsAndAnswers(qNaList, path);
                    
                    uQnA.CorrectAnswers = correctAnswers;
                    qNaList.Add(uQnA);

                    Data.SaveQnAListToXml(qNaList, path);
                }
                else if(selection == GameMode.PlayGame)
                {
                    //play game 
                }

            } while (userQuestions.Length > 0);

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
