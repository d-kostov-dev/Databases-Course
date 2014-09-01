namespace DeleteAlbums
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    /// <summary>
    /// 4.
    /// Using the DOM parser write a program to delete from catalog.xml all albums having price > 20.
    /// </summary>
    public class EntryPoint
    {
        public static void Main()
        {
            DeleteWithPriceHigherThan();
        }

        private static void DeleteWithPriceHigherThan()
        {
            XmlDocument xmlDoc = new XmlDocument();
            var path = "../../albumsCatalog.xml";
            xmlDoc.Load(path);
            Console.WriteLine("Document Loaded\n");

            XmlNode rootNode = xmlDoc.DocumentElement;

            foreach (XmlNode node in rootNode.ChildNodes)
            {
                foreach (XmlNode album in node.ChildNodes)
                {
                    var albumPrice = double.Parse(album["price"].InnerText);

                    if (albumPrice > 20.00)
                    {
                       node.RemoveChild(album);
                    }
                }
            }

            xmlDoc.Save(path);
        }
    }
}
