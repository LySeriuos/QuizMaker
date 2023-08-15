namespace QuizMaker
{
    internal class Logic
    {
        /// <summary>
        /// Knuth shuffle to get non repeatable questions rinted for the user
        /// </summary>
        /// <param name="path">stored questions and answers list path in local memory</param>
        /// <returns> Random question to the user</returns>
        public static UserQuestionsAndAnswers GetRandomQuestion(string path, List<UserQuestionsAndAnswers> savedQnAList)
        {
            // Knuth shuffle
            var random = new Random();
            UserQuestionsAndAnswers questionsForUser;

            int index = random.Next(savedQnAList.Count);
            questionsForUser = savedQnAList[index];
            savedQnAList.RemoveAt(index);           
            return questionsForUser;
        }


        /// <summary>
        /// Creating a List to store matched correct answers 
        /// </summary>
        /// <param name="randomQuestion">Chosed Random question from the GetRandomQuestion method</param>
        /// <param name="userAnswerList">List to add all user Answers before is checked if these are correct </param>
        /// <returns>matched correct answers</returns>
        public static List<string> GetMatchedCorrectAnswer(UserQuestionsAndAnswers randomQuestion, List<string> userAnswerList)
        {
            List<string> correctAnswer = randomQuestion.CorrectAnswers;
            string theCorrectAnswer;
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

        /// <summary>
        /// Method to count points for user answers while playing
        /// </summary>
        /// <param name="userCorrectAnswers">Matched answers with user guessed and saved in the data list answers</param>
        /// <param name="randomQuestion">This used to count all saved correct answers to that question</param>
        /// <returns></returns>
        public static int CountingGamePoints(List<string> userCorrectAnswers, UserQuestionsAndAnswers randomQuestion)
        {
            int matchedCorrectAnswers = userCorrectAnswers.Count;
            int savedCorrectAnswers = randomQuestion.CorrectAnswers.Count;
            int points;
            if (matchedCorrectAnswers < savedCorrectAnswers)
            {
                points = matchedCorrectAnswers - savedCorrectAnswers;
            }
            else
            {
                points = matchedCorrectAnswers;
            }

            return points;
        }
    }
}
