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

        public static List<string> GetMatchedCorrectAnswer(UserQuestionsAndAnswers randomQuestion, List<string> userAnswerList)
        {
            List<string> correctAnswer = randomQuestion.CorrectAnswers;
            string theCorrectAnswer = "";
            List<string> userCorrectAnswers = new List<string>();
            for (int userAnswerListIndex = 0; userAnswerListIndex < userAnswerList.Count; userAnswerListIndex++)
            {
                for (int correctAnswerListIndex = 0; correctAnswerListIndex < correctAnswer.Count; correctAnswerListIndex++)
                {
                    if (correctAnswer[correctAnswerListIndex] == userAnswerList[userAnswerListIndex])
                    {
                        theCorrectAnswer = correctAnswer[correctAnswerListIndex];
                        userCorrectAnswers.Add(theCorrectAnswer);
                        break;
                    }
                }
            }
            return userCorrectAnswers;
        }

    }
}
