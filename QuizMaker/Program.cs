namespace QuizMaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. Create data, UI and logic classes.
            UI.PrintTheCreatingQuestionsRools();
            //2. Get the questions and answers from the user and add them to Objects array.
            string userQuestions = UI.GetTheUsersQuestionsAndAnswers();
            //3. Push array to txt file.
            UI.SplitTheUserStringWithQuestionsAndAnswers(userQuestions);
            //4. On game play Print out Random questons and 3 different answers to choose from.
            //5. Ask user to enter correct answer.
            //6. Check if user's answer is matching with correct answer in txt file. Can be multiple answers correct.
            //7. Add winning points if it was correct. Print it later at the end of the game.
            // Bonus:
            //8. Save players name and scores to the txt file and show the top score at the beggining of the game.
        }
    }
}