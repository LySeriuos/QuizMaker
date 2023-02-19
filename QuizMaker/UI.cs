
namespace QuizMaker
{
    internal class UI
    {
        public static void PrintTheCreatingQuestionsRools()
        {
            Console.WriteLine("Write your questions and answers in one line.");
            Console.WriteLine("Mark correct answer with the star symbol.If there is more than one correct answers add star symbols to all good answers.");
            Console.WriteLine("Here is example:");
            Console.WriteLine("“What color is the sky? | red | blue* | green”.");
            Console.WriteLine("Type “finish” in the console to finish adding questions with answers.");
            Console.WriteLine("Type “start” to begin Quiz game or exit to quit it!");
        }
       


    }
}
