
namespace QuizMaker
{
    internal class Data
    {
        public class UserQuestionsAndAnswers
        {
            public string Question;
            public string AnswerOne;
            public string AnswerTwo;
            public string AnswerThree;
            public string AnswerFour;
            public override string ToString()
            {
                return $"Question: {Question} \nA: {AnswerOne} \tB: {AnswerTwo} \nC: {AnswerThree} \tD: {AnswerFour} ";
            }
        }
        public class UsersCorrectQuestionAndAnswers
        {
            public string Question;
            public int ID = 0;
            public string CorrectAnswer;
            
            public static int Count { get; internal set; }
            public override string ToString()
            {
                return $"Question: {Question} \nID:{ID} \nCorrect Answer:{CorrectAnswer}";
            }
            public static void AddToList(string qca)
            {
                List<String> questAnsw = new List<String>();
                questAnsw.Add(qca);
                return;
            }
        }
      
    }
}
