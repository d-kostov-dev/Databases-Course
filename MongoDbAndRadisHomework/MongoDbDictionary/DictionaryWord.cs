namespace MongoDbDictionary
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class DictionaryWord
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Value { get; set; }

        public string Translation { get; set; }

        public DictionaryWord(string wordValue, string wordTranslation)
        {
            this.Value = wordValue;
            this.Translation = wordTranslation;
        }

        public override string ToString()
        {
            return string.Format("Word: {0}\nTranslation: {1}\n", this.Value, this.Translation);
        }
    }
}
