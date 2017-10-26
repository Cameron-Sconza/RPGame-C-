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
        private static XmlWriterSettings xmlWriterSettings = new XmlWriterSettings() { Indent = true, IndentChars = "\t", NewLineOnAttributes = true, NewLineChars = "\n" };
        private static XmlReaderSettings xmlReaderSettings = new XmlReaderSettings() { IgnoreComments = true, IgnoreWhitespace = true };

        public void SaveGame(Player Player)
        {
            XmlWriter xmlWriter = XmlWriter.Create(filePath + "Save.xml", xmlWriterSettings);
            using (xmlWriter)
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Player");
                xmlWriter.WriteElementString("Name", Player.Name);
                xmlWriter.WriteElementString("Gold", Player.Gold.ToString());
                xmlWriter.WriteStartElement("Mercenaries");
                if (Player.Mercenaries != null)
                {
                    foreach (var merc in Player.Mercenaries)
                    {
                        xmlWriter.WriteStartElement("Mercenary");
                        xmlWriter.WriteElementString("Name", merc.Name);
                        xmlWriter.WriteElementString("Profession", merc.Profession);
                        xmlWriter.WriteElementString("Level", merc.Level.ToString());
                        xmlWriter.WriteElementString("Strength", merc.Strength.ToString());
                        xmlWriter.WriteElementString("Dexterity", merc.Dexterity.ToString());
                        xmlWriter.WriteElementString("Constitution", merc.Constitution.ToString());
                        xmlWriter.WriteElementString("Intellegence", merc.Intellegence.ToString());
                        xmlWriter.WriteElementString("Attack", merc.Attack.ToString());
                        xmlWriter.WriteElementString("Defence", merc.Defence.ToString());
                        xmlWriter.WriteElementString("CurrentExp", merc.CurrentExp.ToString());
                        xmlWriter.WriteElementString("NextLevelExp", merc.NextLevelExp.ToString());
                        xmlWriter.WriteElementString("CurrentHealthPoints", merc.CurrentHealthPoints.ToString());
                        xmlWriter.WriteElementString("MaxHealthPoints", merc.MaxHealthPoints.ToString());
                        xmlWriter.WriteElementString("MainHand", merc.MainHand != null ? merc.MainHand.Name : string.Empty);
                        xmlWriter.WriteElementString("OffHand", merc.OffHand != null ? merc.OffHand.Name : string.Empty);
                        xmlWriter.WriteElementString("Armour", merc.Armour != null ? merc.Armour.Name : string.Empty);
                        xmlWriter.WriteEndElement();
                    }
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("ItemBackpack");
                if (Player.ItemBackpack != null)
                {
                    foreach (var item in Player.ItemBackpack)
                    {
                        xmlWriter.WriteElementString("Item", item.Name);
                    }
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("EquipmentBackpack");
                if (Player.EquipmentBackpack != null)
                {
                    foreach (var equipment in Player.EquipmentBackpack)
                    {
                        xmlWriter.WriteElementString("Item", equipment.Name);
                    }
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }
        }

        public Player LoadGame()
        {
            Player Player = new Player();
            XmlReader xmlReader = XmlReader.Create(filePath + "Save.xml", xmlReaderSettings);
            using (xmlReader)
            {
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType != XmlNodeType.EndElement && !xmlReader.IsEmptyElement)
                    {
                        switch (xmlReader.Name)
                        {
                            case ("Name"):
                                Player.Name = GetValueForXML(xmlReader);
                                break;
                            case ("Gold"):
                                Player.Gold = int.Parse(GetValueForXML(xmlReader));
                                break;
                            case ("Mercenaries"):
                                Mercenary mercenary = new Mercenary();
                                while (!(xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.Name == "Mercenaries"))
                                {
                                    if (xmlReader.NodeType != XmlNodeType.EndElement && !xmlReader.IsEmptyElement)
                                    {
                                        
                                        switch (xmlReader.Name)
                                        {
                                            case ("Name"):
                                                mercenary.Name = GetValueForXML(xmlReader);
                                                break;
                                            case ("Profession"):
                                                mercenary.Profession = GetValueForXML(xmlReader);
                                                break;
                                            case ("Level"):
                                                mercenary.Level = Convert.ToInt32(GetValueForXML(xmlReader));
                                                break;
                                            case ("Strength"):
                                                mercenary.Strength = Convert.ToInt32(GetValueForXML(xmlReader));
                                                break;
                                            case ("Dexterity"):
                                                mercenary.Dexterity = Convert.ToInt32(GetValueForXML(xmlReader));
                                                break;
                                            case ("Constitution"):
                                                mercenary.Constitution = Convert.ToInt32(GetValueForXML(xmlReader));
                                                break;
                                            case ("Intellegence"):
                                                mercenary.Intellegence = Convert.ToInt32(GetValueForXML(xmlReader));
                                                break;
                                            case ("Attack"):
                                                mercenary.Attack = Convert.ToDouble(GetValueForXML(xmlReader));
                                                break;
                                            case ("Defence"):
                                                mercenary.Defence = Convert.ToDouble(GetValueForXML(xmlReader));
                                                break;
                                            case ("CurrentExp"):
                                                mercenary.CurrentExp = Convert.ToDouble(GetValueForXML(xmlReader));
                                                break;
                                            case ("NextLevelExp"):
                                                mercenary.NextLevelExp = Convert.ToDouble(GetValueForXML(xmlReader));
                                                break;
                                            case ("CurrentHealthPoints"):
                                                mercenary.CurrentHealthPoints = Convert.ToDouble(GetValueForXML(xmlReader));
                                                break;
                                            case ("MaxHealthPoints"):
                                                mercenary.MaxHealthPoints = Convert.ToDouble(GetValueForXML(xmlReader));
                                                break;
                                            case ("MainHand"):
                                                mercenary.MainHand = GetAllEquipment().Where(i => i.Name == GetValueForXML(xmlReader)).FirstOrDefault();
                                                break;
                                            case ("OffHand"):
                                                mercenary.OffHand = GetAllEquipment().Where(i => i.Name == GetValueForXML(xmlReader)).FirstOrDefault();
                                                break;
                                            case ("Armour"):
                                                mercenary.Armour = GetAllEquipment().Where(i => i.Name == GetValueForXML(xmlReader)).FirstOrDefault();
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        if(xmlReader.Name == "Mercenary")
                                        {
                                            Player.Mercenaries.Add(mercenary);
                                            mercenary = new Mercenary();
                                        }
                                    }
                                    xmlReader.Read();
                                }
                                break;
                            case ("ItemBackpack"):
                                while (xmlReader.NodeType != XmlNodeType.EndElement && xmlReader.Name == "ItemBackpack")
                                {
                                    if (xmlReader.NodeType != XmlNodeType.EndElement)
                                    {
                                        Player.ItemBackpack.Add(GetAllItems().Where(i => i.Name == GetValueForXML(xmlReader)).FirstOrDefault());
                                    }
                                    xmlReader.Read();
                                }
                                break;
                            case ("EquipmentBackpack"):
                                while (xmlReader.NodeType != XmlNodeType.EndElement && xmlReader.Name == "EquipmentBackpack")
                                {
                                    if (xmlReader.NodeType != XmlNodeType.EndElement)
                                    {
                                        Player.EquipmentBackpack.Add(GetAllEquipment().Where(i => i.Name == GetValueForXML(xmlReader)).FirstOrDefault());
                                    }
                                    xmlReader.Read();
                                }
                                break;
                        }
                    }
                }
            }
            if (Player.Name != null) { return Player; }
            else { return null; }
        }

        private List<Equipment> GetAllEquipment()
        {
            throw new NotImplementedException();
        }

        public List<Item> GetAllItems()
        {
            List<Item> list = new List<Item>();
            XmlReader xmlReader = XmlReader.Create(filePath + "Items.xml", xmlReaderSettings);
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
                                item.Name = GetValueForXML(xmlReader);
                                break;
                            case ("BuyPrice"):
                                item.BuyPrice = int.Parse(GetValueForXML(xmlReader));
                                break;
                            case ("SellPrice"):
                                item.SellPrice = int.Parse(GetValueForXML(xmlReader));
                                break;
                            case ("Description"):
                                item.Description = GetValueForXML(xmlReader);
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

        public List<Monster> GetMonster()
        {
            List<Monster> list = new List<Monster>();
            XmlReader xmlReader = XmlReader.Create(filePath + "Monster.xml", xmlReaderSettings);
            using (xmlReader)
            {
                Monster mon = new Monster();
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType != XmlNodeType.EndElement)
                    {
                        if (xmlReader.Name == "Name")
                        {
                            mon.Name = GetValueForXML(xmlReader);
                        }
                        else if (xmlReader.Name == "MaxHealthPoints")
                        {
                            mon.MaxHealthPoints = double.Parse(GetValueForXML(xmlReader));
                        }
                        else if (xmlReader.Name == "Attack")
                        {
                            mon.Attack = double.Parse(GetValueForXML(xmlReader));
                        }
                        else if (xmlReader.Name == "Defence")
                        {
                            mon.Defence = double.Parse(GetValueForXML(xmlReader));
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

        private string GetValueForXML(XmlReader xml)
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
