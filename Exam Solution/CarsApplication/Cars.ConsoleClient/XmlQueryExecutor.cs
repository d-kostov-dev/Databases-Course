namespace Cars.ConsoleClient
{
    using Cars.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Xml.Linq;

    public class XmlQueryExecutor
    {
        private readonly CarsContext databaseConnection;
        private readonly string inputFile;
        private readonly string outputFile;

        public XmlQueryExecutor(CarsContext context, string inputFile)
        {
            this.inputFile = inputFile;
            this.databaseConnection = context;
        }

        public void ExecuteQueries()
        {
            var queryElements = XElement.Load(this.inputFile).Elements();

            foreach (var query in queryElements)
            {
                //XNamespace carsNamespace = "http://www.w3.org/2001/XMLSchema-instance";
                var outputXml = new XElement("Cars");
                //outputXml.SetAttributeValue("xmlns:xsi", carsNamespace);
                //outputXml.SetAttributeValue("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");

                var resultFile = query.Attribute("OutputFileName").Value;
                var orderBy = GetSingleValue("OrderBy", query);
                var whereClauses = GetClauses(query.Element("WhereClauses"));

                var cars = this.databaseConnection.Cars.AsQueryable();

                foreach (var clause in whereClauses)
                {
                    if (clause.Type == "Contains")
                    {
                        if (clause.PropertyName == "Model")
                        {
                            cars = cars.Where(c => c.Model.Contains(clause.Value)).AsQueryable();
                        }

                        if (clause.PropertyName == "Manufacturer")
                        {
                            cars = cars.Where(c => c.Manufacturer.Name.Contains(clause.Value)).AsQueryable();
                        }

                        if (clause.PropertyName == "Dealer")
                        {
                            cars = cars.Where(c => c.Dealer.Name.Contains(clause.Value)).AsQueryable();
                        }
                    }

                    if (clause.Type == "Equals")
                    {
                        if (clause.PropertyName == "Id")
                        {
                            var cityId = int.Parse(clause.Value);
                            cars = cars.Where(c => c.Id == cityId).AsQueryable();
                        }

                        if (clause.PropertyName == "Year")
                        {
                            var year = int.Parse(clause.Value);
                            cars = cars.Where(c => c.Year == year).AsQueryable();
                        }

                        if (clause.PropertyName == "Price")
                        {
                            var price = decimal.Parse(clause.Value);
                            cars = cars.Where(c => c.Price == price).AsQueryable();
                        }

                        if (clause.PropertyName == "Model")
                        {
                            cars = cars.Where(c => c.Model == clause.Value).AsQueryable();
                        }

                        if (clause.PropertyName == "Manufacturer")
                        {
                            cars = cars.Where(c => c.Manufacturer.Name == clause.Value).AsQueryable();
                        }

                        if (clause.PropertyName == "Dealer")
                        {
                            cars = cars.Where(c => c.Dealer.Name == clause.Value).AsQueryable();
                        }

                        if (clause.PropertyName == "City")
                        {
                            cars = cars.Where(c => c.Dealer.Cities.Any(city => city.Name == clause.Value)).AsQueryable();
                        }
                    }

                    if (clause.Type == "GreaterThan")
                    {
                        if (clause.PropertyName == "Id")
                        {
                            var cityId = int.Parse(clause.Value);
                            cars = cars.Where(c => c.Id > cityId).AsQueryable();
                        }

                        if (clause.PropertyName == "Year")
                        {
                            var year = int.Parse(clause.Value);
                            cars = cars.Where(c => c.Year > year).AsQueryable();
                        }

                        if (clause.PropertyName == "Price")
                        {
                            var price = decimal.Parse(clause.Value);
                            cars = cars.Where(c => c.Price > price).AsQueryable();
                        }
                    }

                    if (clause.Type == "LessThan")
                    {
                        if (clause.PropertyName == "Id")
                        {
                            var cityId = int.Parse(clause.Value);
                            cars = cars.Where(c => c.Id < cityId).AsQueryable();
                        }

                        if (clause.PropertyName == "Year")
                        {
                            var year = int.Parse(clause.Value);
                            cars = cars.Where(c => c.Year < year).AsQueryable();
                        }

                        if (clause.PropertyName == "Price")
                        {
                            var price = decimal.Parse(clause.Value);
                            cars = cars.Where(c => c.Price < price).AsQueryable();
                        }
                    }
                }

                cars = cars.OrderBy(orderBy).AsQueryable();
                var carsResult = cars.ToList();

                foreach (var carFound in carsResult)
                {
                    var carElement = new XElement("Car");
                    carElement.SetAttributeValue("Manufacturer", carFound.Manufacturer.Name);
                    carElement.SetAttributeValue("Model", carFound.Model);
                    carElement.SetAttributeValue("Year", carFound.Year);
                    carElement.SetAttributeValue("Price", carFound.Price);

                    var transmissionElement = new XElement("TransmissionType");
                    transmissionElement.Value = carFound.Transmission.ToString();
                    carElement.Add(transmissionElement);

                    var dealerElement = new XElement("Dealer");
                    dealerElement.SetAttributeValue("Name", carFound.Dealer.Name);

                    var citiesElement = new XElement("Cities");

                    foreach (var city in carFound.Dealer.Cities)
                    {
                        var currentCityElement = new XElement("City");
                        currentCityElement.Value = city.Name;
                        citiesElement.Add(currentCityElement);
                    }

                    dealerElement.Add(citiesElement);
                    carElement.Add(dealerElement);
                    outputXml.Add(carElement);
                }

                outputXml.Save("../../../Queries/" + resultFile);
            }
        }

        private string GetSingleValue(string tag, XElement item)
        {
            if (item != null)
            {
                var result = item.Element(tag);

                if (result != null)
                {
                    return result.Value;
                }
            }

            return null;
        }


        private IList<WhereClause> GetClauses(XElement clausesCollection)
        {
            var resultList = new List<WhereClause>();

            if (clausesCollection != null)
            {
                foreach (var element in clausesCollection.Elements("WhereClause"))
                {
                    var property = element.Attribute("PropertyName").Value;
                    var type = element.Attribute("Type").Value;
                    var value = element.Value;

                    var currentClause = new WhereClause(property, type, value);

                    resultList.Add(currentClause);
                }
            }

            return resultList;
        }

        // TODO: Export to own file.
        public class WhereClause
        {
            public WhereClause(string property, string type, string value)
            {
                this.PropertyName = property;
                this.Type = type;
                this.Value = value;
            }

            public string PropertyName { get; set; }

            public string Type { get; set; }

            public string Value { get; set; }

            public override string ToString()
            {
                var result = "";

                if (this.Type == "Contains")
                {
                    result = string.Format(" WHERE {0} LIKE '%{1}%' ", this.PropertyName, this.Value);
                }

                if (this.Type == "Equals")
                {
                    if (this.PropertyName == "Id" || this.PropertyName == "Price" || this.PropertyName == "Year")
                    {
                        result = string.Format(" WHERE {0} == {1} ", this.PropertyName, this.Value);
                    }
                    else
                    {
                        result = string.Format(" WHERE {0} == '{1}' ", this.PropertyName, this.Value);
                    }
                }

                if (this.Type == "GreaterThan")
                {
                    if (this.PropertyName == "Id" || this.PropertyName == "Price" || this.PropertyName == "Year")
                    {
                        result = string.Format(" WHERE {0} > {1} ", this.PropertyName, this.Value);
                    }
                    else
                    {
                        result = string.Format(" WHERE {0} > '{1}' ", this.PropertyName, this.Value);
                    }
                }

                if (this.Type == "LessThan")
                {
                    if (this.PropertyName == "Id" || this.PropertyName == "Price" || this.PropertyName == "Year")
                    {
                        result = string.Format(" WHERE {0} < {1} ", this.PropertyName, this.Value);
                    }
                    else
                    {
                        result = string.Format(" WHERE {0} < '{1}' ", this.PropertyName, this.Value);
                    }
                }

                return result;
            }
        }

        //public static class MyExtensions
        //{
        //    public static object GetProperty<T>(this T obj, string name) where T : class
        //    {
        //        Type t = typeof(T);
        //        return t.GetProperty(name).GetValue(obj, null);
        //    }
        //}
    }
}
