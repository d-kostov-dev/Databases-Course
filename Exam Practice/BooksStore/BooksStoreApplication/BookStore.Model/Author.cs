namespace BooksStore.Model
{
    using System.Collections.Generic;

    public class Author
    {
        private ICollection<Book> books;
        private ICollection<Review> reviews;

        public Author()
        {
            this.books = new HashSet<Book>();
            this.reviews = new HashSet<Review>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
