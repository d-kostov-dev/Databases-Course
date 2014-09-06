namespace BooksStore.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Review
    {
        public int Id { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
        
        [Required]
        public string Content { get; set; }

        //public int AuthorId { get; set; }

        public virtual Book Book { get; set; }

        public virtual Author Author { get; set; }
    }
}
