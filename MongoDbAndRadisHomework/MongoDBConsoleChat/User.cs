namespace MongoDBConsoleChat
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Username { get; set; }

        public User(string username)
        {
            this.Username = username;
        }

        public override string ToString()
        {
            return string.Format("User: {0}", this.Username);
        }
    }
}
