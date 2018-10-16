using System.Xml;
using System.Text.RegularExpressions;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using DataAccess.SplitData;
using System.Linq;

namespace DataAccess
{
    public class Data
    {
        public static string filePath = "C:\\Users\\Onshore\\Documents\\Visual Studio 2017\\Projects\\RPGame C-sharp\\Library\\XMLFiles\\";
        public static XmlWriterSettings xmlWriterSettings = new XmlWriterSettings() { Indent = true, IndentChars = "\t", NewLineOnAttributes = true, NewLineChars = "\n" };
        public static XmlReaderSettings xmlReaderSettings = new XmlReaderSettings() { IgnoreComments = true, IgnoreWhitespace = true };

        public static string TestPath = GetDirectory();
        private static string GetDirectory()
        {
            string LibraryFolder = string.Empty;
            var currentDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()).ToString()).ToString();
            var DirList = Directory.GetDirectories(currentDirectory, "Library", SearchOption.AllDirectories);
            return DirList.Where(d => Regex.IsMatch(d, "Library")).FirstOrDefault();
        }

        public static ItemData ItemData = new ItemData();
        public static PlayerData PlayerData = new PlayerData();
        public static QuestData QuestData = new QuestData();
        public static RecipeData RecipeData = new RecipeData();

        public static string GetValueForXML(XmlReader xml)
        {
            xml.Read();
            if (xml.NodeType == XmlNodeType.Text)
            {
                return xml.Value;
            }
            else { return null; }
        }
    }
}
