namespace QuizMaker
{
    internal class Logic
    {
        public static string AddQuestions(string userQuestions)
        {
            while (userQuestions == null || userQuestions == "")
            {
                userQuestions = UI.GetTheUsersQuestionsAndAnswers();
            }
            return userQuestions;
        }
    }
}
