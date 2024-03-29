﻿
using System.Collections.Generic;
using System.Xml.Serialization;

namespace QuizMaker
{

    public class Data
    {
        /// <summary>
        /// Save the list to the local memory
        /// </summary>
        /// <param name="qNaList">The list with appended class</param>
        /// <param name="path">location destination where list will be saved</param>
        public static void SaveQnAListToXml(List<UserQuestionsAndAnswers> qNaList, string path)
        {
            //    var path = @"C:\Users\shiranco.DESKTOP-HRN41TE\Documents\temp\UserQuestionsAndAnswers.xml";
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(List<UserQuestionsAndAnswers>));
            using (FileStream file = File.Create(path))
            {
                XmlSerializer.Serialize(file, qNaList);
            }
        }

        /// <summary>
        /// Getting saved List 
        /// </summary>
        /// <param name="path">saved list location destination</param>
        /// <returns>Saved List</returns>
        public static List<UserQuestionsAndAnswers> GetQnAListToXml(string path)
        {
            List<UserQuestionsAndAnswers> qNaList = new();
            if (File.Exists(path))
            {
                XmlSerializer XmlSerializer = new XmlSerializer(typeof(List<UserQuestionsAndAnswers>));
                using (FileStream file = File.OpenRead(path))
                {
                    qNaList = XmlSerializer.Deserialize(file) as List<UserQuestionsAndAnswers>;
                }
            }
            return qNaList;
        }
    }
}
