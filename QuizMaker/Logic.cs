namespace QuizMaker
{
    internal class Logic
    {
        /// <summary>
        /// Printing out random question to the user without repeating it more than 1 time
        /// </summary>
        /// <param name="savedQnAList">Getting list from loacl storage and using to to get random question</param>
        /// <returns>Structurized question data </returns>
       
        public static UserQuestionsAndAnswers GetRandomQuestion(List<UserQuestionsAndAnswers> savedQnAList)
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
