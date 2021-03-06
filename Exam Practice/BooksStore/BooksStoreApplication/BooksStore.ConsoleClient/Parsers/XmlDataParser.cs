﻿namespace BooksStore.ConsoleClient.Parsers
{
    using System.Collections.Generic;
    using System.Xml.Linq;

    using BooksStore.ConsoleClient.Interfaces;
    using BooksStore.ConsoleClient.Utilities;

    public abstract class XmlDataParser : IXmlDataParser
    {
        protected readonly DatabaseContext databaseConnection;
        protected readonly string pathToFile;

        public XmlDataParser(DatabaseContext context, string fileToParse)
        {
            this.databaseConnection = context;
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
