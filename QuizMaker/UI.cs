﻿
using System.ComponentModel;
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
        public static string GetTheUsersQuestionsAndAnswers()
        {
            Console.WriteLine();
            Console.WriteLine("Write your questions with the answers!");
            string userQuestion = Console.ReadLine();
            return userQuestion;
        }

        public static UserQuestionsAndAnswers ParseUserQnAString(string userQna)
        {
            UserQuestionsAndAnswers qna = new UserQuestionsAndAnswers();
            

            string[] userQuestionsArray = userQna.Split(" | ");

            for (int i = 0; i < userQuestionsArray.Length; i++)
            {
                
                userQuestionsArray[i] = userQuestionsArray[i].Trim('*');
                //Console.WriteLine(userQuestionsArray[i]);        

            }

            qna.Question = userQuestionsArray[0];
            qna.AnswerOne = userQuestionsArray[1];
            qna.AnswerTwo = userQuestionsArray[2];
            qna.AnswerThree = userQuestionsArray[3];
            qna.AnswerFour = userQuestionsArray[4];            

            return qna;
        }


        public static List <UserCorrectQuestionAndAnswers> ParseCorrectAnswerAndQuestion(string userQna)
        {            
            List<UserCorrectQuestionAndAnswers> corAnQ = new List<UserCorrectQuestionAndAnswers>();

            string[] userQuestionsArray = userQna.Split(" | ");

            for (int i = 0; i < userQuestionsArray.Length; i++)
            {
                UserCorrectQuestionAndAnswers qca = new UserCorrectQuestionAndAnswers();
                if (userQuestionsArray[i].Contains('*'))
                {
                    userQuestionsArray[i] = userQuestionsArray[i].Trim('*');

                    qca.CorrectAnswer = userQuestionsArray[i];

                    Console.WriteLine($"{qca.CorrectAnswer}");
                    corAnQ.Add(qca);                   
                }
            }           
           
            return corAnQ;
        }












    }




}

