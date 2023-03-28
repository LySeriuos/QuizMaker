using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaker
{
    public class UserQuestionsAndAnswers
    {
        public string Question { get; set; }
        public string AnswerOne { get; set; }
        public string AnswerTwo { get; set; }
        public string AnswerThree { get; set; }
        public string AnswerFour  { get; set; }
        public override string ToString()
        {
            return $"Question: {Question} \nA: {AnswerOne} \tB: {AnswerTwo} \nC: {AnswerThree} \tD: {AnswerFour} ";
        }
    }
}
