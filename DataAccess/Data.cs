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

        public void SaveGame(Character character)
        {
            XmlWriter xmlWriter = XmlWriter.Create(filePath + "Save.xml");
            using (xmlWriter)
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
                xmlWriter.WriteElementString("ArmourID", null);
                xmlWriter.WriteElementString("OffHandID", character.OffHandID);
                xmlWriter.WriteElementString("MainHandID", character.MainHandID);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }
        }

        public Character LoadGame()
        {
            Character character = new Character();
            XmlReader xmlReader = XmlReader.Create(filePath + "Save.xml");
            using (xmlReader)
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

        public List<Item> GetAllItmes()
        {
            List<Item> list = new List<Item>();
            XmlReader xmlReader = XmlReader.Create(filePath + "Items.xml");
            using (xmlReader)
            {
                Item item = new Item();
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType != XmlNodeType.EndElement)
                    {
                        if (xmlReader.Name == "Name")
                        {
                            item.Name = GetValueXML(xmlReader);
                        }
                        else if (xmlReader.Name == "BuyPrice")
                        {
                            item.BuyPrice = int.Parse(GetValueXML(xmlReader));
                        }
                    }
                    else
                    {
                        if (xmlReader.Name == "Item")
                        {
                            item.SellPrice = Convert.ToInt32(Math.Ceiling(a:item.BuyPrice/10));
                            list.Add(item);
                            item = new Item();
                        }
                    }
                }
            }
            return list;
        }

        public List<MonsterLoot> GetMonsterLoot()
        {
            List<MonsterLoot> list = new List<MonsterLoot>();
            XmlReader xmlReader = XmlReader.Create(filePath + "MonsterLoot.xml");
            using (xmlReader)
            {
                MonsterLoot mon = new MonsterLoot();
                while (xmlReader.Read())
                {
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
                    else
                    {
                        if (xmlReader.Name == "MonsterLoot")
                        {
                            list.Add(mon);
                            mon = new MonsterLoot();
                        }
                    }
                }
            }
            return list;
        }

        public List<Monster> GetMonster()
        {
            List<Monster> list = new List<Monster>();
            XmlReader xmlReader = XmlReader.Create(filePath + "Monster.xml");
            using (xmlReader)
            {
                Monster mon = new Monster();
                while (xmlReader.Read())
                {
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
                    else
                    {
                        if (xmlReader.Name == "Monster")
                        {
                            list.Add(mon);
                            mon = new Monster();
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
