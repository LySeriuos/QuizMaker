using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaker
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
}
