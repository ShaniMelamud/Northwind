
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cool
{
    public class ProductModel : IValidatableObject
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Missing Name")]
        [MinLength(2, ErrorMessage = "Name must be minimum 2 chars")]
        [MaxLength(100, ErrorMessage = "Name can't exceeds 100 chars")]
        [IllegalChars]
        public string Name { get; set; }
        [Required(ErrorMessage = "Missing Price")]
        [Range(0,1000, ErrorMessage = "Price must be 0 - 1000")]
        public decimal? Price { get; set; }
        [Range(0, 10000, ErrorMessage = "Stock must be 0 - 10000")]
        public short? Stock { get; set; }

        public ProductModel(){}
        public ProductModel(Product product)
        {
            ID = product.ProductId;
            Name = product.ProductName;
            Price = product.UnitPrice;
            Stock = product.UnitsInStock;
        }
        public Product ConvertToProduct()
        {
            Product product = new Product
            {
                ProductId = ID,
                ProductName = Name,
                UnitPrice = Price,
                UnitsInStock = Stock
            };
            return product;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (Price == 0 && Stock == 0)
            {
                List<string> members = new List<string> { nameof(Price), nameof(Stock) };
                errors.Add(new ValidationResult("Both price and stock can't be 0!", members));
            }
            return errors;
        }
    }
}
