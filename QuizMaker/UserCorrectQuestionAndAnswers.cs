﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaker
{
    public class UserCorrectQuestionAndAnswers
    {
        public string CorrectAnswer;

        public override string ToString()
        {
            return $"\nCorrect Answer:{CorrectAnswer}";
        }
    }
}
