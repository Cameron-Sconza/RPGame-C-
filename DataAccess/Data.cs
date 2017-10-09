using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XmlConfiguration;
using System.Xml.Serialization;
using System.Xml.XPath;


namespace DataAccess
{
    public class Data
    {
        private static string filePath = "C:\\Users\\Onshore\\Documents\\Visual Studio 2017\\Projects\\RPGame C-sharp\\";

        public Character LoadGame()
        {
            Character character = new Character();
            List<string> contents;
            using (StreamReader reader = new StreamReader(filePath + "Save.txt"))
            {
                contents = reader.ReadToEnd().Split('\n').ToList();
                reader.Close();
            }
            if(contents.Count < 11)
            {
                return null;
            }
            character.Name = contents[0];
            character.CharacterClass = contents[1];
            character.ArmourID = contents[2];
            character.OffHandID = contents[3];
            character.MainHandID = contents[4];
            character.Strength = int.Parse(contents[5]);
            character.Dexterity = int.Parse(contents[6]);
            character.Constitution = int.Parse(contents[7]);
            character.Intellegence = int.Parse(contents[8]);
            character.Gold = int.Parse(contents[9]);
            character.Level = int.Parse(contents[10]);
            character.CurrentExp = int.Parse(contents[11]);
            character.NextLevelExp = int.Parse(contents[12]);
            character.Backpack = contents.GetRange(13, (contents.Count - 1));
            return character;
        }

        public void SaveGameXML(Character character)
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings { Indent = true };
            XmlWriter xmlWriter = XmlWriter.Create(filePath + "Save.xml");
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Character");
            xmlWriter.WriteElementString("Name", character.Name);
            xmlWriter.WriteElementString("CharacterClass", character.CharacterClass);
            xmlWriter.WriteElementString("ArmourID", character.ArmourID);
            xmlWriter.WriteElementString("OffHandID", character.OffHandID);
            xmlWriter.WriteElementString("MainHandID", character.MainHandID);
            xmlWriter.WriteElementString("Strength", character.Strength.ToString());
            xmlWriter.WriteElementString("Dexterity", character.Dexterity.ToString());
            xmlWriter.WriteElementString("Constitution", character.Constitution.ToString());
            xmlWriter.WriteElementString("Intellegence", character.Intellegence.ToString());
            xmlWriter.WriteElementString("Gold", character.Gold.ToString());
            xmlWriter.WriteElementString("Level", character.Level.ToString());
            xmlWriter.WriteElementString("CurrentExp", character.CurrentExp.ToString());
            xmlWriter.WriteElementString("NextLevelExp", character.NextLevelExp.ToString());
            xmlWriter.WriteStartElement("Backpack");
            if (character.Backpack != null || character.Backpack.Count == 0)
            {
                foreach (var item in character.Backpack)
                {
                    xmlWriter.WriteElementString("item", item);
                }
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
        }

        public void SaveGame(Character character)
        {
            using (StreamWriter writer = new StreamWriter(filePath+ "Save.txt"))
            {
                writer.Write(character.Name + '\n' +
                character.CharacterClass + '\n' +
                character.ArmourID + '\n' +
                character.OffHandID + '\n' +
                character.MainHandID + '\n' +
                character.Strength + '\n' +
                character.Dexterity + '\n' +
                character.Constitution + '\n' +
                character.Intellegence + '\n' +
                character.Gold + '\n' +
                character.Level + '\n' +
                character.CurrentExp + '\n' +
                character.NextLevelExp + '\n');
                foreach(var item in character.Backpack)
                {
                    writer.Write(item + '\n');
                }
                writer.Flush();
                writer.Close();
            }
        }

        public List<MonsterLoot> GetMonsterLoot()
        {
            throw new NotImplementedException();
        }
    }
}
