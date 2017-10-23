using System;
using System.Collections.Generic;
using Library.Models;
using System.Xml;
using System.Linq;

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
                xmlWriter.WriteStartElement("ArmourBackpack");
                if (character.ArmourBackpack != null)
                {
                    foreach (var item in character.ArmourBackpack)
                    {
                        xmlWriter.WriteElementString("Item", item.Name);
                    }
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("ItemBackpack");
                if (character.ItemBackpack != null)
                {
                    foreach (var item in character.ItemBackpack)
                    {
                        xmlWriter.WriteElementString("Item", item.Name);
                    }
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("MainHandBackpack");
                if (character.MainHandBackpack != null)
                {
                    foreach (var item in character.MainHandBackpack)
                    {
                        xmlWriter.WriteElementString("Item", item.Name);
                    }
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("OffHandBackpack");
                if (character.OffHandBackpack != null)
                {
                    foreach (var item in character.OffHandBackpack)
                    {
                        xmlWriter.WriteElementString("Item", item.Name);
                    }
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteElementString("Armour", character.Armour.Name);
                xmlWriter.WriteElementString("OffHand", character.OffHand.Name);
                xmlWriter.WriteElementString("MainHand", character.MainHand.Name);
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
                            case ("MainHand"):
                                    character.MainHand = GetAllMainHands().Where(m => m.Name == GetValueXML(xmlReader)).FirstOrDefault();
                                break;
                            case ("OffHand"):
                                    character.OffHand = GetAllOffHands().Where(m => m.Name == GetValueXML(xmlReader)).FirstOrDefault();
                                break;
                            case ("Armour"):
                                    character.Armour = GetAllArmours().Where(m => m.Name == GetValueXML(xmlReader)).FirstOrDefault();
                                break;
                            case ("Item"):
                                    character.ItemBackpack.Add(GetAllItems().Where(i => i.Name == GetValueXML(xmlReader)).FirstOrDefault());
                                break;
                        }
                    }
                }
            }
            if (character.Name != null) { return character; }
            else { return null; }
        }

        public List<Armour> GetAllArmours()
        {
            List<Armour> list = new List<Armour>();
            XmlReader xmlReader = XmlReader.Create(filePath + "Armours.xml");
            using (xmlReader)
            {
                Armour item = new Armour();
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType != XmlNodeType.EndElement)
                    {
                        switch (xmlReader.Name)
                        {
                            case ("Name"):
                                item.Name = GetValueXML(xmlReader);
                                break;
                            case ("BuyPrice"):
                                item.BuyPrice = int.Parse(GetValueXML(xmlReader));
                                break;
                            case ("SellPrice"):
                                item.SellPrice = int.Parse(GetValueXML(xmlReader));
                                break;
                            case ("Description"):
                                item.Description = GetValueXML(xmlReader);
                                break;
                        }
                    }
                    else
                    {
                        if (xmlReader.Name == "Item")
                        {
                            list.Add(item);
                            item = new Armour();
                        }
                    }
                }
            }
            return list;
        }

        public List<Item> GetAllItems()
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
                        switch (xmlReader.Name)
                        {
                            case ("Name"):
                                item.Name = GetValueXML(xmlReader);
                                break;
                            case ("BuyPrice"):
                                item.BuyPrice = int.Parse(GetValueXML(xmlReader));
                                break;
                            case ("SellPrice"):
                                item.SellPrice = int.Parse(GetValueXML(xmlReader));
                                break;
                            case ("Description"):
                                item.Description = GetValueXML(xmlReader);
                                break;
                        }
                    }
                    else
                    {
                        if (xmlReader.Name == "Item")
                        {
                            list.Add(item);
                            item = new Item();
                        }
                    }
                }
            }
            return list;
        }

        public List<MainHand> GetAllMainHands()
        {
            List<MainHand> list = new List<MainHand>();
            XmlReader xmlReader = XmlReader.Create(filePath + "MainHands.xml");
            using (xmlReader)
            {
                MainHand item = new MainHand();
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType != XmlNodeType.EndElement)
                    {
                        switch (xmlReader.Name)
                        {
                            case ("Name"):
                                item.Name = GetValueXML(xmlReader);
                                break;
                            case ("BuyPrice"):
                                item.BuyPrice = int.Parse(GetValueXML(xmlReader));
                                break;
                            case ("SellPrice"):
                                item.SellPrice = int.Parse(GetValueXML(xmlReader));
                                break;
                            case ("Description"):
                                item.Description = GetValueXML(xmlReader);
                                break;
                        }
                    }
                    else
                    {
                        if (xmlReader.Name == "Item")
                        {
                            list.Add(item);
                            item = new MainHand();
                        }
                    }
                }
            }
            return list;
        }

        public List<OffHand> GetAllOffHands()
        {
            List<OffHand> list = new List<OffHand>();
            XmlReader xmlReader = XmlReader.Create(filePath + "OffHands.xml");
            using (xmlReader)
            {
                OffHand item = new OffHand();
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType != XmlNodeType.EndElement)
                    {
                        switch (xmlReader.Name)
                        {
                            case ("Name"):
                                item.Name = GetValueXML(xmlReader);
                                break;
                            case ("BuyPrice"):
                                item.BuyPrice = int.Parse(GetValueXML(xmlReader));
                                break;
                            case ("SellPrice"):
                                item.SellPrice = int.Parse(GetValueXML(xmlReader));
                                break;
                            case ("Description"):
                                item.Description = GetValueXML(xmlReader);
                                break;
                        }
                    }
                    else
                    {
                        if (xmlReader.Name == "Item")
                        {
                            list.Add(item);
                            item = new OffHand();
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
                        if (xmlReader.Name == "Name")
                        {
                            mon.Name = GetValueXML(xmlReader);
                        }
                        else if (xmlReader.Name == "MaxHealthPoints")
                        {
                            mon.MaxHealthPoints = double.Parse(GetValueXML(xmlReader));
                        }
                        else if (xmlReader.Name == "Attack")
                        {
                            mon.Attack = double.Parse(GetValueXML(xmlReader));
                        }
                        else if (xmlReader.Name == "Defence")
                        {
                            mon.Defence = double.Parse(GetValueXML(xmlReader));
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
