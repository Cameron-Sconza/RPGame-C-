using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace DataAccess.SplitData
{
    public class ItemData
    {
        public List<Item> GetAllItems()
        {
            List<Item> list = new List<Item>();
            XmlReader xmlReader = XmlReader.Create(Data.filePath + "Items.xml", Data.xmlReaderSettings);
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
                                item.Name = Data.GetValueForXML(xmlReader);
                                break;
                            case ("BuyPrice"):
                                item.BuyPrice = Convert.ToInt32(Data.GetValueForXML(xmlReader));
                                break;
                            case ("SellPrice"):
                                item.SellPrice = Convert.ToInt32(Data.GetValueForXML(xmlReader));
                                break;
                            case ("Description"):
                                item.Description = Data.GetValueForXML(xmlReader);
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

        public Item GetItem(string itemName)
        {
            return GetAllItems().Where(i => i.Name == itemName).FirstOrDefault();
        }

        public List<Equipment> GetAllEquipment()
        {
            throw new NotImplementedException();
        }

        public Equipment GetEquipment(string itemName)
        {
            return GetAllEquipment().Where(e => e.Name == itemName).FirstOrDefault();
        }
    }
}
