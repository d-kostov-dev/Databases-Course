namespace TelerikAcademyRSS
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Xml;

    public class EntryPoint
    {
        public static void Main()
        {
            GetRssFile();

            var jsonData = ParseXMLtoJSON();

            PrintTitles(jsonData);

            var questionsList = ParseJSONtoPOCOwithLINQ(jsonData);
            GenerateHTMLPage(questionsList);
        }

        /// <summary>
        /// 1. The RSS feed is at http://forums.academy.telerik.com/feed/qa.rss
        /// 2. Download the content of the feed programmatically
        ///     - You can use WebClient.DownloadFile()
        /// </summary>
        private static void GetRssFile()
        {
            var webClient = new WebClient();
            string fileToDownload = "http://forums.academy.telerik.com/feed/qa.rss";
            string targetOfDownloadedFile = "../../forumRSS.xml";

            webClient.DownloadFile(fileToDownload, targetOfDownloadedFile);
        }
        
        /// <summary>
        /// 3. Parse the XML from the feed to JSON.
        /// </summary>
        private static string ParseXMLtoJSON()
        {
            string pathToXMLfile = "../../forumRSS.xml";

            var xmlFile = new XmlDocument();
            xmlFile.Load(pathToXMLfile);

            string jsonFromXml = JsonConvert.SerializeObject(xmlFile.DocumentElement.FirstChild);

            return jsonFromXml;
        }

        /// <summary>
        /// 4. Using LINQ-to-JSON select all the question titles and print them to the console
        /// </summary>
        private static void PrintTitles(string jsonData)
        {
            var jsonObj = JObject.Parse(jsonData);

            var titles = jsonObj["channel"]["item"].Select(x => x["title"]);

            foreach (var item in titles)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// 5. Parse the JSON string to POCO
        /// </summary>
        private static IList<Question> ParseJSONtoPOCO(string jsonData)
        {
            var jsonObj = JObject.Parse(jsonData);
            var questionsDataList = new List<Question>();

            var itemsList = jsonObj["channel"]["item"];

            foreach (var item in itemsList)
            {
                var serializedQuestion = JsonConvert.SerializeObject(item);
                var deserializedQuestion = JsonConvert.DeserializeObject<Question>(serializedQuestion);
                questionsDataList.Add(deserializedQuestion);
            }

            return questionsDataList;
        }

        public static IList<Question> ParseJSONtoPOCOwithLINQ(string jsonData)
        {
            var jsonObj = JObject.Parse(jsonData);

            var questionsList = 
                jsonObj["channel"]["item"]
                .Select(x => new Question()
                {
                    Title = x["title"].ToString(),
                    Link = x["link"].ToString(),
                    Description = x["description"].ToString(),
                    Category = x["category"].ToString()
                })
                .ToList();

            return questionsList;
        }

        /// <summary>
        /// 6. Using the parsed objects create a HTML page that lists all questions from the RSS their categories and a link to the question's page
        /// </summary>
        private static void GenerateHTMLPage(IList<Question> questionsList)
        {
            using (FileStream fileStream = new FileStream("../../questions.html", FileMode.Create))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    foreach (var question in questionsList)
                    {
                        streamWriter.WriteLine("<h2>Title: {0}</h2>", question.Title);
                        streamWriter.WriteLine("<p>Description: {0}</p>", question.Description);
                        streamWriter.WriteLine("<category>Category: {0}</category>", question.Category);
                        streamWriter.WriteLine("<a href='{0}'>Link to question...</a>", question.Link);
                        streamWriter.WriteLine("<hr />");
                    }
                }
            } 
        }
    }
}
