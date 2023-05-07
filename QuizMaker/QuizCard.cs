
using System.Collections.Generic;
using System;

namespace QuizMaker
{
    internal class QuizCard
    {
        public static string GetRandomQuestion(string path)
        {
            var random = new Random();
            UserQuestionsAndAnswers qna = new UserQuestionsAndAnswers();
            List<UserQuestionsAndAnswers> qNaList = Data.GetQnAListToXml(path);
            int index = random.Next(qNaList.Count);
            qna = qNaList[index];
            return qna.ToString();
        }
    }
}
