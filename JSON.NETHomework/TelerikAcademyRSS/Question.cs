namespace TelerikAcademyRSS
{
    using Newtonsoft.Json;

    public class Question
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        public override string ToString()
        {
            return string.Format("Question: {0}\nCategory:{1}\nLink:{2}", this.Title, this.Category, this.Link);
        }
    }
}
