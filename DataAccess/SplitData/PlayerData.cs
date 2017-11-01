using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace DataAccess.SplitData
{
    public class PlayerData
    {

        public void SaveGame(Player Player)
        {
            XmlWriter xmlWriter = XmlWriter.Create(Data.filePath + "Save.xml", Data.xmlWriterSettings);
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
            XmlReader xmlReader = XmlReader.Create(Data.filePath + "Save.xml", Data.xmlReaderSettings);
            using (xmlReader)
            {
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType != XmlNodeType.EndElement && !xmlReader.IsEmptyElement)
                    {
                        switch (xmlReader.Name)
                        {
                            case ("Name"):
                                Player.Name = Data.GetValueForXML(xmlReader);
                                break;
                            case ("Gold"):
                                Player.Gold = int.Parse(Data.GetValueForXML(xmlReader));
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
                                                mercenary.Name = Data.GetValueForXML(xmlReader);
                                                break;
                                            case ("Profession"):
                                                mercenary.Profession = Data.GetValueForXML(xmlReader);
                                                break;
                                            case ("Level"):
                                                mercenary.Level = Convert.ToInt32(Data.GetValueForXML(xmlReader));
                                                break;
                                            case ("Strength"):
                                                mercenary.Strength = Convert.ToInt32(Data.GetValueForXML(xmlReader));
                                                break;
                                            case ("Dexterity"):
                                                mercenary.Dexterity = Convert.ToInt32(Data.GetValueForXML(xmlReader));
                                                break;
                                            case ("Constitution"):
                                                mercenary.Constitution = Convert.ToInt32(Data.GetValueForXML(xmlReader));
                                                break;
                                            case ("Intellegence"):
                                                mercenary.Intellegence = Convert.ToInt32(Data.GetValueForXML(xmlReader));
                                                break;
                                            case ("Attack"):
                                                mercenary.Attack = Convert.ToDouble(Data.GetValueForXML(xmlReader));
                                                break;
                                            case ("Defence"):
                                                mercenary.Defence = Convert.ToDouble(Data.GetValueForXML(xmlReader));
                                                break;
                                            case ("CurrentExp"):
                                                mercenary.CurrentExp = Convert.ToDouble(Data.GetValueForXML(xmlReader));
                                                break;
                                            case ("NextLevelExp"):
                                                mercenary.NextLevelExp = Convert.ToDouble(Data.GetValueForXML(xmlReader));
                                                break;
                                            case ("CurrentHealthPoints"):
                                                mercenary.CurrentHealthPoints = Convert.ToDouble(Data.GetValueForXML(xmlReader));
                                                break;
                                            case ("MaxHealthPoints"):
                                                mercenary.MaxHealthPoints = Convert.ToDouble(Data.GetValueForXML(xmlReader));
                                                break;
                                            case ("MainHand"):
                                                mercenary.MainHand = ItemData.GetEquipment(Data.GetValueForXML(xmlReader));
                                                break;
                                            case ("OffHand"):
                                                mercenary.OffHand = ItemData.GetEquipment(Data.GetValueForXML(xmlReader));
                                                break;
                                            case ("Armour"):
                                                mercenary.Armour = ItemData.GetEquipment(Data.GetValueForXML(xmlReader));
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        if (xmlReader.Name == "Mercenary")
                                        {
                                            Player.Mercenaries.Add(mercenary);
                                            mercenary = new Mercenary();
                                        }
                                    }
                                    xmlReader.Read();
                                }
                                break;
                            case ("ItemBackpack"):
                                List<Item> listItem = ItemData.GetAllItems();
                                while (xmlReader.NodeType != XmlNodeType.EndElement && xmlReader.Name == "ItemBackpack")
                                {
                                    if (xmlReader.NodeType != XmlNodeType.EndElement)
                                    {
                                        Player.ItemBackpack.Add(listItem.Where(i => i.Name == Data.GetValueForXML(xmlReader)).FirstOrDefault());
                                    }
                                    xmlReader.Read();
                                }
                                break;
                            case ("EquipmentBackpack"):
                                List<Equipment> listEquip = ItemData.GetAllEquipment();
                                while (xmlReader.NodeType != XmlNodeType.EndElement && xmlReader.Name == "EquipmentBackpack")
                                {
                                    if (xmlReader.NodeType != XmlNodeType.EndElement)
                                    {
                                        Player.EquipmentBackpack.Add(listEquip.Where(i => i.Name == Data.GetValueForXML(xmlReader)).FirstOrDefault());
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

    }
}
