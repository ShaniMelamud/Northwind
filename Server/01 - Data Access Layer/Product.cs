using System;
using System.Collections.Generic;

//Scaffold-DbContext -Connection "Server=.\SqlExpress;Database=Northwind;Trusted_Connection=True" -Provider Microsoft.EntityFrameWorkCore.SqlServer -Tables Products
namespace Cool
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
    }
}
