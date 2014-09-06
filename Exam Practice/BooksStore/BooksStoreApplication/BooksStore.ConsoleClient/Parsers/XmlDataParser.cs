namespace BooksStore.ConsoleClient.Parsers
{
    using System.Collections.Generic;
    using System.Xml.Linq;

    using BooksStore.ConsoleClient.Interfaces;
    using BooksStore.Data;

    public abstract class XmlDataParser : IXmlDataParser
    {
        protected readonly ContextFactory contextFactory;
        protected readonly string pathToFile;

        public XmlDataParser(ContextFactory factory, string fileToParse)
        {
            this.contextFactory = factory;
            this.pathToFile = fileToParse;
        }

        public abstract void ParseFile();
        
        protected string GetSingleValue(string tag, XElement item)
        {
            if (item != null)
            {
                var result = item.Element(tag);

                if (result != null)
                {
                    return result.Value;
                }
            }

            return null;
        }

        /// <summary>
        /// Only for same tag values.
        /// </summary>
        protected ISet<string> GetMultipleValues(string tag, XElement itemsCollection)
        {
            var resultSet = new HashSet<string>();

            if (itemsCollection != null)
            {
                foreach (var element in itemsCollection.Elements(tag))
                {
                    resultSet.Add(element.Value);
                }
            }

            return resultSet;
        }
    }
}
