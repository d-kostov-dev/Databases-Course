namespace ExtractAlbums
{
    using System;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// 8.
    /// Write a program, which (using XmlReader and XmlWriter) reads the file catalog.xml
    /// and creates the file album.xml, in which stores in appropriate way the names of all albums and their authors.
    /// </summary>
    public class EntryPoint
    {
        public static void Main()
        {
            string newFileName = "../../albumToArtist.xml";
            Encoding encoding = Encoding.GetEncoding("windows-1251");
            XmlTextWriter writer = new XmlTextWriter(newFileName, encoding);

            using (writer)
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;

                writer.WriteStartDocument();
                writer.WriteStartElement("library");
                writer.WriteAttributeString("name", "My Albums Catalog");

                writer.WriteStartElement("albums");

                var albumsFilePath = "../../albumsCatalog.xml";
                XmlReader reader = XmlReader.Create(albumsFilePath);

                using (reader)
                {
                    while (reader.Read())
                    {
                        if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "title"))
                        {
                            var albumName = reader.ReadElementString();

                            reader.Read();
                            var albumArtist = reader.ReadElementString();

                            writer.WriteStartElement("album");
                            writer.WriteElementString("title", albumName);
                            writer.WriteElementString("artist", albumArtist);
                            writer.WriteEndElement();
                        }
                    }
                }
        
                writer.WriteEndDocument();
            }
        }
    }
}
