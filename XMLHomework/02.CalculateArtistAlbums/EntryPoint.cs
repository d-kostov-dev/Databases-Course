namespace CalculateArtistAlbums
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    /// <summary>
    /// 2.
    /// Write program that extracts all different artists which are found in the catalog.xml.
    /// For each author you should print the number of albums in the catalogue.
    /// Use the DOM parser and a hash-table.
    /// 
    /// 3.
    /// Implement the previous using XPath.
    /// </summary>
    public class EntryPoint
    {
        public static void Main()
        {
            ReadWithXmlDocument();
            Console.WriteLine();
            ReadWithXPath();
        }

        private static void ReadWithXmlDocument()
        {
            var artistAlbumsCount = new Dictionary<string, int>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../../albumsCatalog.xml");
            Console.WriteLine("Document Loaded\n");

            XmlNode rootNode = xmlDoc.DocumentElement;

            foreach (XmlNode node in rootNode.ChildNodes)
            {
                foreach (XmlNode album in node.ChildNodes)
                {
                    if (!artistAlbumsCount.ContainsKey(album["artist"].InnerText))
                    {
                        artistAlbumsCount.Add(album["artist"].InnerText, 0);
                    }

                    artistAlbumsCount[album["artist"].InnerText]++;
                }
            }

            foreach (var artist in artistAlbumsCount.OrderByDescending(x => x.Value))
            {
                Console.WriteLine("Artist: {0}, Albums: {1}", artist.Key, artist.Value);
            }
        }

        private static void ReadWithXPath()
        {
            var artistAlbumsCount = new Dictionary<string, int>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../../albumsCatalog.xml");
            string queryForXPath = "/library/albums/album/child::artist";
            XmlNodeList artistsList = xmlDoc.SelectNodes(queryForXPath);

            Console.WriteLine("Document With xPath Loaded\n");
            foreach (XmlNode artistNode in artistsList)
            {
                string artistName = artistNode.InnerText;

                if (!artistAlbumsCount.ContainsKey(artistName))
                {
                    artistAlbumsCount.Add(artistName, 0);
                }

                artistAlbumsCount[artistName]++;
            }

            foreach (var artist in artistAlbumsCount.OrderByDescending(x => x.Value))
            {
                Console.WriteLine("Artist: {0}, Albums: {1}", artist.Key, artist.Value);
            }
        }
    }
}
