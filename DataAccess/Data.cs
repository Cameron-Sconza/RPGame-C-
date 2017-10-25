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
                        xmlWriter.WriteElementString("Item", merc.Name);
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
                    if (xmlReader.NodeType != XmlNodeType.EndElement)
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
                                while (xmlReader.NodeType != XmlNodeType.EndElement && xmlReader.Name == "Mercenaries")
                                {
                                    if (xmlReader.NodeType != XmlNodeType.EndElement)
                                    {
                                        switch (xmlReader.Name)
                                        {
                                            case (""):
                                                break;
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
