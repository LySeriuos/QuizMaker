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

        public static void AddingPoints(int points, List<int>userPointsList)
        {
            // add to list and then print total
            int userPoints = points;
            userPointsList.Add(userPoints);
            if(userPointsList.Sum() < 0) 
            {
                userPoints = 0;
                Console.WriteLine($"Your current score is {userPoints}  ");
            } 
            else {
                Console.WriteLine($"Your current score is: {userPointsList.Sum()}");
            }
        }

    }
}
