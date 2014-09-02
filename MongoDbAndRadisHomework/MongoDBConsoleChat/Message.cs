namespace MongoDBConsoleChat
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;

    public class Message
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string MessageText { get; set; }

        public string User { get; set; }

        public DateTime DateAdded { get; set; }

        public Message(string message, string user, DateTime date)
        {
            this.MessageText = message;
            this.User = user;
            this.DateAdded = date;
        }

        public override string ToString()
        {
            return string.Format("From: {0}\nMessage: {1}\non: {2}", this.User, this.MessageText, this.DateAdded);
        }
    }
}
