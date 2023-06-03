using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaker
{
    internal class QuizCard
    {
        public static void GetTheListToString(UserQuestionsAndAnswers randomQuestion)
        {
            string question = randomQuestion.Question;
            string answerOne = randomQuestion.AnswerOne;
            string answerTwo = randomQuestion.AnswerTwo;
            string answerThree = randomQuestion.AnswerThree;
            string answerFour = randomQuestion.AnswerFour;
            Console.WriteLine($"\nQuestion: {question} \n\nA: {answerOne} \nB: {answerTwo} \nC: {answerThree} \nD: {answerFour}");
        }
        
    }
}




