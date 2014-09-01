namespace DirectoryInformation
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;

    /// <summary>
    /// 9.
    /// Write a program to traverse given directory and write to a XML file its contents 
    /// together with all subdirectories and files. 
    /// Use tags <file> and <dir> with appropriate attributes. 
    /// For the generation of the XML document use the class XmlWriter.
    /// 
    /// 10.
    /// Rewrite the last exercises using XDocument, XElement and XAttribute.
    /// </summary>
    public class EntryPoint
    {
        public static void Main()
        {
            string folderToScan = "../../";
            string outputXmlFile = "../../directory.xml";
            DirectoryInfo directoryInfo = new DirectoryInfo(folderToScan);
            Encoding encoding = Encoding.GetEncoding("windows-1251");

            using (XmlTextWriter writer = new XmlTextWriter(outputXmlFile, encoding))
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;

                writer.WriteStartDocument();
                writer.WriteStartElement("directories");
                CreateXML(writer, directoryInfo);
                writer.WriteEndDocument();
            }
        }

        private static void CreateXML(XmlTextWriter writer, DirectoryInfo dir)
        {
            foreach (var file in dir.GetFiles())
            {
                string xml = new XElement("file", new XAttribute("name", file.Name)).ToString();
                XmlReader reader = XmlReader.Create(new StringReader(xml));
                writer.WriteNode(reader, true);
            }

            var subdirectories = dir.GetDirectories().ToList().OrderBy(d => d.Name);
            foreach (var subDir in subdirectories)
            {
                CreateSubdirectoryXML(writer, subDir);
            }
        }

        private static void CreateSubdirectoryXML(XmlTextWriter writer, DirectoryInfo dir)
        {
            string xml = new XElement("dir", new XAttribute("name", dir.Name)).ToString();
            XmlReader reader = XmlReader.Create(new StringReader(xml));
            writer.WriteNode(reader, true);

            foreach (var file in dir.GetFiles())
            {
                xml = new XElement("file", new XAttribute("name", file.Name)).ToString();
                reader = XmlReader.Create(new StringReader(xml));
                writer.WriteNode(reader, true);
            }

            var subdirectories = dir.GetDirectories().ToList().OrderBy(d => d.Name);
            foreach (var subDir in subdirectories)
            {
                CreateSubdirectoryXML(writer, subDir);
            }
        }
    }
}
