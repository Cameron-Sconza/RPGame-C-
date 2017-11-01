using System.Xml;
using DataAccess.SplitData;

namespace DataAccess
{
    public class Data
    {
        public static string filePath = "C:\\Users\\Onshore\\Documents\\Visual Studio 2017\\Projects\\RPGame C-sharp\\";
        public static XmlWriterSettings xmlWriterSettings = new XmlWriterSettings() { Indent = true, IndentChars = "\t", NewLineOnAttributes = true, NewLineChars = "\n" };
        public static XmlReaderSettings xmlReaderSettings = new XmlReaderSettings() { IgnoreComments = true, IgnoreWhitespace = true };

        public static ItemData ItemData { get; set; }
        public static PlayerData PlayerData { get; set; }
        public static QuestData QuestData { get; set; }
        public static RecipeData RecipeData { get; set; }

        public static string GetValueForXML(XmlReader xml)
        {
            xml.Read();
            if (xml.NodeType == XmlNodeType.Text)
            {
                return xml.Value;
            }
            else { return null; }
        }

        public Data()
        {
            ItemData = new ItemData();
            PlayerData = new PlayerData();
            QuestData = new QuestData();
            RecipeData = new RecipeData();
        }
    }
}
