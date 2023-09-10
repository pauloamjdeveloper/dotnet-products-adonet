using Newtonsoft.Json;
using Products.ADO.NET.WebAPI.Utilities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Products.ADO.NET.WebAPI.Models
{
    public class Product
    {
        public int Id { get; set; }

        [JsonProperty("name")]
        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(3)]
        [MaxLength(50)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(5)]
        [MaxLength(100)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [JsonProperty("price")]
        [Required(ErrorMessage = "The Price is Required")]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DisplayName("Price")]
        [DecimalRange(ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }


        [JsonProperty("quantity")]
        [Required(ErrorMessage = "The Quantity is Required")]
        [Range(1, 9999, ErrorMessage = "Quantity must be between 1 and 9999")]
        [DisplayName("Quantity")]
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
    }
}
