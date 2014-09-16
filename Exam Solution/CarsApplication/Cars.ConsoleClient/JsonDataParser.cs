namespace Cars.ConsoleClient
{
    using System;
    using System.IO;
    using System.Linq;

    using Cars.Data;
    using Cars.Model;
    using Newtonsoft.Json.Linq;
    
    public class JsonDataParser
    {
        private readonly CarsContext databaseConnection;
        private string pathToFiles;
        private string fileNameFormat;

        public JsonDataParser(CarsContext databaseContext, string filesPath, string nameFormat)
        {
            this.databaseConnection = databaseContext;
            this.pathToFiles = filesPath;
            this.fileNameFormat = nameFormat;
        }

        public void ParseData()
        {
            int filesCount = this.FilesInFolder();

            for (int i = 0; i < filesCount; i++)
            {
                var currentFile = this.pathToFiles + string.Format(this.fileNameFormat, i);
                var jsonData = JArray.Parse(File.ReadAllText(currentFile));

                foreach (var item in jsonData)
                {
                    var currentCar = this.CreateCar(item);
                    currentCar.Manufacturer = this.GetManufacturer(item);
                    currentCar.Dealer = this.GetDealer(item);

                    // Save the car
                    this.databaseConnection.Cars.Add(currentCar);
                    this.databaseConnection.SaveChanges();
                }

                Console.WriteLine(string.Format(this.fileNameFormat, i) + " Parsed");
            }
        }

        private Dealer GetDealer(JToken item)
        {
            string dealerName = item["Dealer"]["Name"].ToString();
            string dealerCity = item["Dealer"]["City"].ToString();

            var currentDealer = this.databaseConnection.Dealers
                .Where(d => d.Name == dealerName)
                .FirstOrDefault();

            // If there is such dealer.
            if (currentDealer != null)
            {
                // Check if he has this town.
                if (!this.IsInTown(currentDealer, dealerCity))
                {
                    currentDealer.Cities.Add(this.GetCity(dealerCity));
                }
            }
            else
            {
                currentDealer = new Dealer() { Name = dealerName };
                currentDealer.Cities.Add(this.GetCity(dealerCity));
            }

            return currentDealer;
        }

        private City GetCity(string name)
        {
            var city = this.databaseConnection.Cities.Where(c => c.Name == name).FirstOrDefault();

            if (city == null)
            {
                city = new City()
                {
                    Name = name,
                };
            }

            return city;
        }

        private bool IsInTown(Dealer dealer, string name)
        {
            if (dealer.Cities.Any(c => c.Name == name))
            {
                return true;
            }

            return false;
        }

        private Car CreateCar(JToken item)
        {
            var currentCar = new Car();

            currentCar.Year = int.Parse(this.GetItemValue("Year", item));
            currentCar.Transmission = (TransmissionType)int.Parse(this.GetItemValue("TransmissionType", item));
            currentCar.Model = this.GetItemValue("Model", item);
            currentCar.Price = decimal.Parse(this.GetItemValue("Price", item));

            return currentCar;
        }

        private Manufacturer GetManufacturer(JToken item)
        {
            string manufacturerName = this.GetItemValue("ManufacturerName", item);

            var manufacturer = this.databaseConnection.Manufacturers.Where(m => m.Name == manufacturerName).FirstOrDefault();

            if (manufacturer == null)
            {
                manufacturer = new Manufacturer()
                {
                    Name = manufacturerName,
                };
            }

            return manufacturer;
        }

        private string GetItemValue(string value, JToken item)
        {
            return item[value].ToString();
        }

        private int FilesInFolder()
        {
            int filesCount = Directory.GetFiles(this.pathToFiles, "*.json", SearchOption.TopDirectoryOnly).Length;

            return filesCount;
        }
    }
}
