namespace StudentsSystem.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Homework
    {
        [Key]   
        public int ID { get; set; }

        public string Content { get; set; }

        public DateTime TimeSent { get; set; }

        public Course Course { get; set; }
    }
}
