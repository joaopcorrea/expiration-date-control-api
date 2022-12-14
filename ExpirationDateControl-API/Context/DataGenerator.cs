using ExpirationDateControl_API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Text.Json;

namespace ExpirationDateControl_API.Context
{
    public class DataGenerator
    {
        private readonly InMemoryContext _inMemoryContext;

        public DataGenerator(InMemoryContext inMemoryContext)
        {
            _inMemoryContext = inMemoryContext;
        }

        public void Generate()
        {
            if (!_inMemoryContext.Products.Any())
            {
                List<Product> items;
                using (StreamReader reader = new StreamReader("productsData.json"))
                {
                    string json = reader.ReadToEnd();
                    items = JsonConvert.DeserializeObject<List<Product>>(json, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
                }

                _inMemoryContext.Products.AddRange(items);
                _inMemoryContext.SaveChanges();
            }
        }
    }
}
