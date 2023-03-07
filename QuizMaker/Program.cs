using System.Security.Cryptography.X509Certificates;
using static QuizMaker.Data;
using static QuizMaker.UI;

namespace QuizMaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. Create data, UI and logic classes.
            UI.PrintTheCreatingQuestionsRools();
            //2. Get the questions and answers from the user and add them to Objects array.
            string userQuestions = UI.GetTheUsersQuestionsAndAnswers();
            //3. Push array to txt file.
            // Create a list and add questions with answers to that list
            //            string myList = UI.ParseUserQnAString(userQuestions).ToString();
            //            foreach(var author in myList)
            //{
            //                Console.WriteLine("Author: {0},{1},{2},{3},{4}");
            //            }
            Console.WriteLine(UI.ParseUserQnAString(userQuestions).ToString());
            string myList2 = UI.ParseCorrectAnswerAndQuestion(userQuestions).ToString();




            // Spliting questions from answers


            //4. On game play Print out Random questons and 3 different answers to choose from.
            //5. Ask user to enter correct answer.
            //6. Check if user's answer is matching with correct answer in txt file. Can be multiple answers correct.
            //7. Add winning points if it was correct. Print it later at the end of the game.
            // Bonus:
            //8. Save players name and scores to the txt file and show the top score at the beggining of the game.


        }
    }
}