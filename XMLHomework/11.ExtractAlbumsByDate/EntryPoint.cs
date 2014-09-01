namespace ExtractAlbumsByDate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    
    /// <summary>
    /// 11.
    /// Write a program, which extract from the file catalog.xml the prices for all albums, 
    /// published 5 years ago or earlier. Use XPath query.
    /// 
    /// 12.
    /// Rewrite the previous using LINQ query.
    /// </summary>
    public class EntryPoint
    {
        public static void Main()
        {
            GetAlbumPricesXpath();
            GetAlbumPricesLINQ();
        }

        private static void GetAlbumPricesXpath()
        {
            Console.WriteLine("Albums price for the last 5 years - xPath");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../../albumsCatalog.xml");
            string queryForXPath = "/library/albums/album[year>2009]/price";
            XmlNodeList pricesList = xmlDoc.SelectNodes(queryForXPath);

            foreach (XmlNode price in pricesList)
            {
                Console.WriteLine(price.InnerText);
            }

            Console.WriteLine();
        }

        private static void GetAlbumPricesLINQ()
        {
            Console.WriteLine("Albums price for the last 5 years - LINQ");
            var albumsFilePath = "../../albumsCatalog.xml";
            XDocument xmlDoc = XDocument.Load(albumsFilePath);

            var pricesList = xmlDoc.Descendants("album")
                .Where(x => int.Parse(x.Element("year").Value) >= 2009)
                .Select(x => x.Element("price"));

            foreach (var price in pricesList)
            {
                Console.WriteLine(price.Value); 
            }

            Console.WriteLine();
        }
    }
}
