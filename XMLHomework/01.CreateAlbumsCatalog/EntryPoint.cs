namespace CreateAlbumsCatalog
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// 1.
    /// Create a XML file representing catalogue. 
    /// The catalog should contain albums of different artists. 
    /// For each album you should define: name, artist, year, producer, price and a list of songs. 
    /// Each song should be described by title and duration.
    /// </summary>
    public class EntryPoint
    {
        private const string CharsForRandomString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly Random GetRandomNumber = new Random();

        public static void Main()
        {
            string fileName = "../../albumsCatalog.xml";
            Encoding encoding = Encoding.GetEncoding("windows-1251");
            XmlTextWriter writer = new XmlTextWriter(fileName, encoding);

            using (writer)
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;

                writer.WriteStartDocument();
                writer.WriteStartElement("library");
                writer.WriteAttributeString("name", "My Albums Catalog");

                writer.WriteStartElement("albums");
                for (int i = 0; i < 10; i++)
                {
                    AddAlbumToXmlFile(
                        writer,
                        GetRandomString(),
                        GetRandomString(),
                        GetRandomDate().Year,
                        GetRandomString(),
                        GetRandomPrice(),
                        new List<string>() 
                        { 
                            GetRandomString(), 
                            GetRandomString(),
                            GetRandomString(),
                            GetRandomString(),
                            GetRandomString(),
                            GetRandomString(),
                            GetRandomString(),
                            GetRandomString(),
                        });
                }

                writer.WriteEndDocument();
            }

            Console.WriteLine("Document {0} created.", fileName);
        }

        private static void AddAlbumToXmlFile(XmlWriter writer, string title, string artist, int year, string producer, double price, IList<string> songs)
        {
            writer.WriteStartElement("album");
            writer.WriteElementString("title", title);
            writer.WriteElementString("artist", artist);
            writer.WriteElementString("year", year.ToString());
            writer.WriteElementString("producer", producer);
            writer.WriteElementString("price", Math.Round(price, 2).ToString());

            writer.WriteStartElement("songs");

            foreach (var item in songs)
            {
                writer.WriteElementString("songName", item);
            }

            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        private static string GetRandomString(int size = 8)
        {
            char[] charsSelected = new char[size];

            for (int i = 0; i < size; i++)
            {
                charsSelected[i] = CharsForRandomString[GetRandomNumber.Next(CharsForRandomString.Length)];
            }

            return new string(charsSelected);
        }

        private static double GetRandomPrice()
        {
            var numberGenerated = GetRandomNumber.NextDouble();

            return 0.99 + (numberGenerated * (19.99 - 0.99));
        }

        private static DateTime GetRandomDate()
        {
            DateTime startDate = new DateTime(1990, 1, 1);
            int range = (DateTime.Today - startDate).Days;
            return startDate.AddDays(GetRandomNumber.Next(range));
        }
    }
}
