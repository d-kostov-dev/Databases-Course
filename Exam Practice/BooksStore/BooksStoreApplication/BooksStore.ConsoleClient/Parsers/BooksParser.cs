﻿namespace BooksStore.ConsoleClient.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    using BooksStore.Model;
    using BooksStore.ConsoleClient.Utilities;

    public class BooksParser : XmlDataParser
    {
        public BooksParser(DatabaseContext context, string filePath)
            : base(context, filePath)
        {
        }

        public override void ParseFile()
        {
            var dataElements = XElement.Load(this.pathToFile).Elements();

            foreach (var item in dataElements)
            {
                var title = this.GetSingleValue("title", item);
                var webSite = this.GetSingleValue("web-site", item);
                var price = this.GetSingleValue("price", item);
                var isbn = this.GetIsbn(item);

                var currentBook = new Book()
                {
                    Title = title,
                    Isbn = isbn,
                    Website = webSite,
                };

                if (price != null)
                {
                    currentBook.Price = decimal.Parse(price);
                }

                var authors = this.GetAuthors(item);
                foreach (var author in authors)
                {
                    currentBook.Authors.Add(author);
                }

                var reviews = this.GetReviews(item);
                foreach (var review in reviews)
                {
                    currentBook.Reviews.Add(review);
                }

                this.databaseConnection.Books.Add(currentBook);
                this.databaseConnection.SaveChanges();
            }
        }

        private Author GetAuthor(string name)
        {
            var authorToInsert = this.databaseConnection.Authors.FirstOrDefault(a => a.Name == name);

            if (authorToInsert == null)
            {
                authorToInsert = new Author() { Name = name };
            }

            return authorToInsert;
        }

        private string GetIsbn(XElement item)
        {
            var isbn = this.GetSingleValue("isbn", item);

            if (isbn != null)
            {
                if (this.databaseConnection.Books.Any(b => b.Isbn == isbn))
                {
                    throw new ArgumentException("Duplicated ISBN");
                }
            }

            return isbn;
        }

        private IList<Author> GetAuthors(XElement item)
        {
            var authorsList = new List<Author>();

            var authors = this.GetMultipleValues("author", item.Element("authors"));

            foreach (var name in authors)
            {
                var authorToInsert = this.GetAuthor(name);
                authorsList.Add(authorToInsert);
            }

            return authorsList;
        }

        private IList<Review> GetReviews(XElement item)
        {
            var reviewsList = new List<Review>();

            var reviews = item.Element("reviews");

            if (reviews != null)
            {
                foreach (var review in reviews.Elements("review"))
                {
                    if (review != null)
                    {
                        var reviewContent = review.Value;
                        var reviewDate = review.Attribute("date");
                        var reviewAuthor = review.Attribute("author");

                        var currentReview = new Review()
                        {
                            Content = reviewContent,
                            CreationDate = reviewDate == null ? DateTime.Now : DateTime.Parse(reviewDate.Value),
                        };

                        if (reviewAuthor != null)
                        {
                            var authorToInsert = this.GetAuthor(reviewAuthor.Value);
                            currentReview.Author = authorToInsert;
                        }

                        reviewsList.Add(currentReview);
                    }
                }
            }

            return reviewsList;
        }
    }
}
