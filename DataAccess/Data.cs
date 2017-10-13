using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models;
using System.IO;
using System.Xml;

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
                reader.Dispose();
                reader.Close();
            }
            if (contents.Count > 11) { return null; }
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

        public void SaveGame(Character character)
        {
            using (StreamWriter writer = new StreamWriter(filePath + "Save.txt"))
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
                foreach (var item in character.Backpack)
                {
                    writer.Write(item + '\n');
                }
                writer.Flush();
                writer.Close();
            }
        }

        public void SaveGameXML(Character character)
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings { Indent = true, NewLineOnAttributes = true };
            using (XmlWriter xmlWriter = XmlWriter.Create(filePath + "Save.xml"))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Character");
                xmlWriter.WriteElementString("Name", character.Name);
                xmlWriter.WriteElementString("CharacterClass", character.CharacterClass);
                xmlWriter.WriteElementString("Strength", character.Strength.ToString());
                xmlWriter.WriteElementString("Dexterity", character.Dexterity.ToString());
                xmlWriter.WriteElementString("Constitution", character.Constitution.ToString());
                xmlWriter.WriteElementString("Intellegence", character.Intellegence.ToString());
                xmlWriter.WriteElementString("Gold", character.Gold.ToString());
                xmlWriter.WriteElementString("Level", character.Level.ToString());
                xmlWriter.WriteElementString("CurrentExp", character.CurrentExp.ToString());
                xmlWriter.WriteElementString("NextLevelExp", character.NextLevelExp.ToString());
                xmlWriter.WriteStartElement("Backpack");
                if (character.Backpack != null)
                {
                    foreach (var item in character.Backpack)
                    {
                        xmlWriter.WriteElementString("Item", item);
                    }
                }
                xmlWriter.WriteElementString("ArmourID", character.ArmourID);
                xmlWriter.WriteElementString("OffHandID", character.OffHandID);
                xmlWriter.WriteElementString("MainHandID", character.MainHandID);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }
        }

        public Character LoadGameXml()
        {
            Character character = new Character();
            XmlReaderSettings readerSettings = new XmlReaderSettings() { IgnoreComments = true, IgnoreWhitespace = true };
            using (XmlReader xmlReader = XmlReader.Create(filePath + "Save.xml"))
            {
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType != XmlNodeType.EndElement)
                    {
                        switch (xmlReader.Name)
                        {
                            case ("Name"):
                                character.Name = GetValueXML(xmlReader);
                                break;
                            case ("CharacterClass"):
                                character.CharacterClass = GetValueXML(xmlReader);
                                break;
                            case ("Level"):
                                character.Level = int.Parse(GetValueXML(xmlReader));
                                break;
                            case ("Strength"):
                                character.Strength = int.Parse(GetValueXML(xmlReader));
                                break;
                            case ("Dexterity"):
                                character.Dexterity = int.Parse(GetValueXML(xmlReader));
                                break;
                            case ("Constitution"):
                                character.Constitution = int.Parse(GetValueXML(xmlReader));
                                break;
                            case ("Intellegence"):
                                character.Intellegence = int.Parse(GetValueXML(xmlReader));
                                break;
                            case ("Gold"):
                                character.Gold = int.Parse(GetValueXML(xmlReader));
                                break;
                            case ("CurrentExp"):
                                character.CurrentExp = double.Parse(GetValueXML(xmlReader));
                                break;
                            case ("NextLevelExp"):
                                character.NextLevelExp = double.Parse(GetValueXML(xmlReader));
                                break;
                            case ("MainHandID"):
                                character.MainHandID = GetValueXML(xmlReader);
                                break;
                            case ("OffHandID"):
                                character.OffHandID = GetValueXML(xmlReader);
                                break;
                            case ("ArmourID"):
                                character.ArmourID = GetValueXML(xmlReader);
                                break;
                            case ("Item"):
                                character.Backpack.Add(GetValueXML(xmlReader));
                                break;
                        }
                    }
                }
            }
            if (character.Name != null) { return character; }
            else { return null; }
        }

        public List<MonsterLoot> GetMonsterLoot()
        {
            List<MonsterLoot> list = new List<MonsterLoot>();
            XmlReaderSettings readerSettings = new XmlReaderSettings() { IgnoreComments = true, IgnoreWhitespace = true };
            using (XmlReader xmlReader = XmlReader.Create(filePath + "MonsterLoot.xml"))
            {
                MonsterLoot mon = new MonsterLoot();
                while (xmlReader.Read())
                {
                    if (mon.MonsterID != null && mon.ExpGain != 0 && mon.GoldDrop != 0 && mon.ItemDropOneID != null && mon.ItemDropTwoID != null && mon.ItemDropThreeID != null && mon.ItemDropFourID != null)
                    {
                        list.Add(mon);
                        mon = new MonsterLoot();
                    }
                    if (xmlReader.NodeType != XmlNodeType.EndElement)
                    {
                        switch (xmlReader.Name)
                        {
                            case ("Name"):
                                mon.MonsterID = GetValueXML(xmlReader);
                                break;
                            case ("ExpGain"):
                                mon.ExpGain = double.Parse(GetValueXML(xmlReader));
                                break;
                            case ("GoldDrop"):
                                mon.GoldDrop = int.Parse(GetValueXML(xmlReader));
                                break;
                            case ("ItemDropOneID"):
                                mon.ItemDropOneID = GetValueXML(xmlReader);
                                break;
                            case ("ItemDropTwoID"):
                                mon.ItemDropTwoID = GetValueXML(xmlReader);
                                break;
                            case ("ItemDropThreeID"):
                                mon.ItemDropThreeID = GetValueXML(xmlReader);
                                break;
                            case ("ItemDropFourID"):
                                mon.ItemDropFourID = GetValueXML(xmlReader);
                                break;
                        }
                    }
                }
            }
            return list;
        }

        public List<Monster> GetMonster()
        {
            List<Monster> list = new List<Monster>();
            XmlReaderSettings readerSettings = new XmlReaderSettings() { IgnoreComments = true, IgnoreWhitespace = true };
            using (XmlReader xmlReader = XmlReader.Create(filePath + "Monster.xml"))
            {
                Monster mon = new Monster();
                while (xmlReader.Read())
                {
                    if (mon.Name != null && mon.MaxHealthPoints != 0 && mon.Attack != 0 && mon.Defence != 0)
                    {
                        mon.CurrentHealthPoints = mon.MaxHealthPoints;
                        list.Add(mon);
                        mon = new Monster();
                    }
                    if (xmlReader.NodeType != XmlNodeType.EndElement)
                    {
                        switch (xmlReader.Name)
                        {
                            case ("Name"):
                                mon.Name = GetValueXML(xmlReader);
                                break;
                            case ("MaxHealthPoints"):
                                mon.MaxHealthPoints = double.Parse(GetValueXML(xmlReader));
                                break;
                            case ("Attack"):
                                mon.Attack = double.Parse(GetValueXML(xmlReader));
                                break;
                            case ("Defence"):
                                mon.Defence = double.Parse(GetValueXML(xmlReader));
                                break;
                        }
                    }
                }
            }
            return list;
        }

        private string GetValueXML(XmlReader xml)
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
