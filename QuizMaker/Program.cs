using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using static QuizMaker.Data;
using static QuizMaker.UI;
using System.IO;

namespace QuizMaker
{
    internal class Program
    {

        static void Main(string[] args)
        {

            string path = @"C:\Temp\UserQuestionsAndAnswers.xml"; //TODO: look at relative paths
            //GameMode selection = UI.SelectGameMode();

            //switch(selection)
            //{
            //    case GameMode.AddQuestions: //add questions
            //        break;
            //        //etc.
            //}

            List<UserQuestionsAndAnswers> qNaList = new List<UserQuestionsAndAnswers>();
            List<UserCorrectQuestionAndAnswers> corAnQ = new List<UserCorrectQuestionAndAnswers>();
            //1. Create data, UI and logic classes.

            UI.PrintTheCreatingQuestionsRools();

            //2. Get the questions and answers from the user and add them to Objects array.

            string userQuestions;
            if (File.Exists(path))
            {
                qNaList = Data.GetQnAListToXml(path);
                foreach (UserQuestionsAndAnswers answer in qNaList)
                {
                    Console.WriteLine(answer);
                }
                for (int i = 0; i < qNaList.Count; i++)
                {
                    Console.WriteLine(qNaList[i]);
                }
            }
            else
            {
                do
                {
                    userQuestions = UI.GetTheUsersQuestionsAndAnswers();
                    UserQuestionsAndAnswers uQnA = UI.ParseUserQnAString(userQuestions);
                    //UserCorrectQuestionAndAnswers uCorAnsw = UI.ParseCorrectAnswerAndQuestion(userQuestions);

                    
                    qNaList.Add(uQnA);

                    //qNaList.AddRange(corAnQ);
                    List<UserCorrectQuestionAndAnswers> corAnQQ = UI.ParseCorrectAnswerAndQuestion(userQuestions);
                    
                   

                    for (int i = 0; i < corAnQQ.Count; i++)
                    {

                        Console.WriteLine($"{corAnQQ[i]}galutinis");
                    }

                    //qNaList.AddRange(corAnQQ);


                    Data.SaveQnAListToXml(qNaList, path);

                } while (userQuestions.Length > 0);
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
