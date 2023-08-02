using System.Collections.Generic;

namespace QuizMaker
{
    internal class Logic
    {
        public static UserQuestionsAndAnswers GetRandomQuestion(string path)
        {
            // Knuth shuffle

            var random = new Random();
            UserQuestionsAndAnswers questionsForUser = new UserQuestionsAndAnswers();
            List<UserQuestionsAndAnswers> qNaList = Data.GetQnAListToXml(path);
            // For each unshuffled item in the collection
            for (int n = qNaList.Count() - 1; n > 0; --n)
            {
                // Randomly picking an item which has not been shuffled
                int k = random.Next(n + 1);

                // Swaping the selected item with the last "unstruck" question in collection
                UserQuestionsAndAnswers temp = qNaList[n];
                qNaList[n] = qNaList[k];
                qNaList[k] = temp;

                // adding value to the method output 
                questionsForUser = qNaList[k];
            }            
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
