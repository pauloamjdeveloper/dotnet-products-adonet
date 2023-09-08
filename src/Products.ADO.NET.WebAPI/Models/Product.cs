using Newtonsoft.Json;
using Products.ADO.NET.WebAPI.Exceptions;

namespace Products.ADO.NET.WebAPI.Models
{
    public class Product
    {
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        public Product() { }

        public Product(int id, string name, string description, decimal price, int quantity)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
        }

        private void Validate(int id, string name, string description, decimal price, int quantity) 
        {
            ModelExceptionValidation.When(id <= 0, "Invalid Id value");

            ModelExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name, Name is required");

            ModelExceptionValidation.When(name.Length < 3, "Invalid name, too short, minimum 3 characters");

            ModelExceptionValidation.When(string.IsNullOrEmpty(description), "Invalid description. Description is required");

            ModelExceptionValidation.When(description.Length < 5, "Invalid description, too short, minimum 5 characters");

            ModelExceptionValidation.When(price <= 0, "Invalid price value");

            ModelExceptionValidation.When(quantity <= 0, "Invalid stock value");

            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
        }
    }
}
