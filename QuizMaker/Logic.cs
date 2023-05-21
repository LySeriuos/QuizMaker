namespace QuizMaker
{
    internal class Logic
    {
        public static UserQuestionsAndAnswers GetRandomQuestion(string path)
        {
            var random = new Random();
            UserQuestionsAndAnswers questionsForUser = new UserQuestionsAndAnswers();
            List<UserQuestionsAndAnswers> qNaList = Data.GetQnAListToXml(path);
            int index = random.Next(qNaList.Count);
            questionsForUser = qNaList[index];            
            return questionsForUser;
        }
    }
}
