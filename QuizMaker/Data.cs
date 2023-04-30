
using System.Collections.Generic;
using System.Xml.Serialization;

namespace QuizMaker
{

    public class Data
    {
        public static void SaveQnAListToXml(List<UserQuestionsAndAnswers> qNaList, string path)
        {
        //    var path = @"C:\Users\shiranco.DESKTOP-HRN41TE\Documents\temp\UserQuestionsAndAnswers.xml";
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(List<UserQuestionsAndAnswers>));
            using (FileStream file = File.Create(path))
            {
                XmlSerializer.Serialize(file, qNaList);
            }
            
        }
        public static List<UserQuestionsAndAnswers> GetQnAListToXml(string path)
        {
            List<UserQuestionsAndAnswers> qNaList;
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(List<UserQuestionsAndAnswers>));
            using (FileStream file = File.OpenRead(path))
            {
                qNaList = XmlSerializer.Deserialize(file) as List<UserQuestionsAndAnswers>;
                return qNaList;
            }

        }

    }
}
