using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaker
{
    public class UserCorrectQuestionAndAnswers
    {
        public string Question;
        public int ID = 0;
        public string CorrectAnswer;


        public override string ToString()
        {
            return $"Question: {Question} \nID:{ID} \nCorrect Answer:{CorrectAnswer}";
        }
    }
}
