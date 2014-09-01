namespace ExtractSongTitles
{
    using System;
    using System.Xml;
    using System.Xml.Linq;

    /// <summary>
    /// 5.
    /// Write a program, which using XmlReader extracts all song titles from catalog.xml.
    /// 
    /// 6.
    /// Rewrite the same using XDocument and LINQ query.
    /// </summary>
    public class EntryPoint
    {
        public static void Main()
        {
            GetSongsWithXMLReader();
            GetSongsWithLINQ();
        }

        private static void GetSongsWithXMLReader()
        {
            Console.WriteLine("Song titles in the albums catalog - Xml Reader");
            var albumsFilePath = "../../albumsCatalog.xml";
            XmlReader reader = XmlReader.Create(albumsFilePath);

            using (reader)
            {
                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "songName"))
                    {
                        Console.WriteLine(reader.ReadElementString());
                    }
                }
            }

            Console.WriteLine();
        }

        private static void GetSongsWithLINQ()
        {
            Console.WriteLine("Song titles in the albums catalog - LINQ");
            var albumsFilePath = "../../albumsCatalog.xml";
            XDocument xmlDoc = XDocument.Load(albumsFilePath);
            var songTitles = xmlDoc.Descendants("songName");

            foreach (var song in songTitles)
            {
                Console.WriteLine(song.Value);
            }
        }
    }
}
