namespace BooksStore.ConsoleClient.Utilities
{
    using System;
    using System.Linq;
    using System.Xml.Linq;

    public class XmlQueryExecutor
    {
        private readonly DatabaseContext databaseConnection;
        private readonly string inputFile;
        private readonly string outputFile;

        public XmlQueryExecutor(DatabaseContext context, string inputFile, string outputFile)
        {
            this.inputFile = inputFile;
            this.databaseConnection = context;
            this.outputFile = outputFile;
        }

        public void ExecuteQueries()
        {
            var queryElements = XElement.Load(this.inputFile).Elements();

            var outputXml = new XElement("search-results");
            var resultSet = new XElement("result-set");
            
            foreach (var query in queryElements)
            {
                var queryType = query.Attribute("type").Value;
                if (queryType == "by-period")
                {
                    //this.ProccessPeriodQuery(query, resultSet);
                }
                else if (queryType == "by-author")
                {
                    this.ProccessAuthorQuery(query, resultSet);
                }
            }

            outputXml.Add(resultSet);
            outputXml.Save(this.outputFile);
        }

        private void ProccessAuthorQuery(XElement query, XElement resultSet)
        {
            var authorName = query.Element("author-name").Value;

            var reviewsResult =
                this.databaseConnection.Reviews
                .Where(r => r.Author.Name == authorName)
                .OrderBy(r => r.CreationDate)
                .ThenBy(r => r.Content)
                .ToList();

            foreach (var review in reviewsResult)
            {
                var reviewElement = new XElement("review");

                var dateElement = new XElement("date");
                dateElement.Value = review.CreationDate.ToString("d-MMM-yyyy");
                reviewElement.Add(dateElement);

                var contentElement = new XElement("content");
                contentElement.Value = review.Content;
                reviewElement.Add(contentElement);

                var bookElement = new XElement("book");

                var titleElement = new XElement("title");
                titleElement.Value = review.Book.Title;
                bookElement.Add(titleElement);

                var authorsElement = new XElement("authors");
                authorsElement.Value = string.Join(", ", review.Book.Authors.Select(a => a.Name));
                bookElement.Add(authorsElement);

                if (review.Book.Isbn != null)
                {
                    var isbnElement = new XElement("isbn");
                    isbnElement.Value = review.Book.Isbn;
                    bookElement.Add(isbnElement);
                }

                if (review.Book.Website != null)
                {
                    var websiteElement = new XElement("website");
                    websiteElement.Value = review.Book.Website;
                    bookElement.Add(websiteElement);
                }

                reviewElement.Add(bookElement);
                resultSet.Add(reviewElement);
            }
        }

        private void ProccessPeriodQuery(XElement query, XElement resultSet)
        {
            throw new NotImplementedException();
        }
    }
}
