
using System.Xml.Serialization;

namespace QuizMaker
{

    public class Data
    {
        public static void SaveQnAListToXml(List<UserQuestionsAndAnswers> qNaList)
        {
            var path = @"C:\Users\shiranco.DESKTOP-HRN41TE\Documents\temp\UserQuestionsAndAnswers.xml";
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(List<UserQuestionsAndAnswers>));
            using (FileStream file = File.Create(path))
            {
                XmlSerializer.Serialize(file, qNaList);
            }
            
        }

    }
}
