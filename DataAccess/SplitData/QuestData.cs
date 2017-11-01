using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace DataAccess.SplitData
{
    public class QuestData
    {
        public List<Monster> GetAllMonsters()
        {
            List<Monster> list = new List<Monster>();
            XmlReader xmlReader = XmlReader.Create(Data.filePath + "Monster.xml", Data.xmlReaderSettings);
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
                                mon.Name = Data.GetValueForXML(xmlReader);
                                break;
                            case ("MaxHealthPoints"):
                                mon.MaxHealthPoints = Convert.ToDouble(Data.GetValueForXML(xmlReader));
                                break;
                            case ("Attack"):
                                mon.Attack = Convert.ToDouble(Data.GetValueForXML(xmlReader));
                                break;
                            case ("Defence"):
                                mon.Defence = Convert.ToDouble(Data.GetValueForXML(xmlReader));
                                break;
                            case ("ExpGain"):
                                mon.ExpGain = Convert.ToDouble(Data.GetValueForXML(xmlReader));
                                break;
                            case ("GoldDrop"):
                                mon.GoldDrop = Convert.ToInt32(Data.GetValueForXML(xmlReader));
                                break;
                            case ("ItemDrop"):
                                List<Item> listItem = Data.ItemData.GetAllItems();
                                while (xmlReader.NodeType != XmlNodeType.EndElement && xmlReader.Name == "ItemDrop")
                                {
                                    if (xmlReader.NodeType != XmlNodeType.EndElement)
                                    {
                                        mon.Items.Add(listItem.Where(i => i.Name == Data.GetValueForXML(xmlReader)).FirstOrDefault());
                                    }
                                    xmlReader.Read();
                                }
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

        public Monster GetMonster(string itemName)
        {
            return GetAllMonsters().Where(i => i.Name == itemName).FirstOrDefault();
        }
    }
}
