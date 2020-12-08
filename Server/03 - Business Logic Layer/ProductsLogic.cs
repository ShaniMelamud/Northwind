using System.Collections.Generic;
using System.Linq;

namespace Cool
{
    public class ProductsLogic : BaseLogic
    {
        public ProductsLogic(NorthwindContext db) : base(db){}
        public List<ProductModel> GetAllProducts()
        {
            return DB.Products.Select(p => new ProductModel(p)).ToList();
        }
        public ProductModel GetOneProduct(int id)
        {
            return DB.Products.Where(p => p.ProductId == id).Select(p => new ProductModel(p)).SingleOrDefault();
        }
        public ProductModel AddProduct(ProductModel productModel)
        {
            Product product = productModel.ConvertToProduct();
            DB.Products.Add(product);
            DB.SaveChanges();
            productModel.ID = product.ProductId;
            return productModel;
        }
        public ProductModel UpdateFullProduct(ProductModel productModel)
        {
            Product product = DB.Products.SingleOrDefault(p => p.ProductId == productModel.ID);
            if (product == null)
                return null;

            product.ProductName = productModel.Name;
            product.UnitPrice = productModel.Price;
            product.UnitsInStock = productModel.Stock;
            DB.SaveChanges();
            return productModel;
        }
        public ProductModel UpdatePartialProduct(ProductModel productModel)
        {
            Product product = DB.Products.SingleOrDefault(p => p.ProductId == productModel.ID);
            if (product == null)
                return null;
            if (productModel.Name != null)
                product.ProductName = productModel.Name;
            if (productModel.Price != null)
                product.UnitPrice = productModel.Price;
            if (productModel.Stock != null)
                product.UnitsInStock = productModel.Stock;
            DB.SaveChanges();
            return productModel;
        }
        public void DeleteProduct(int id)
        {
            Product productToDelete = DB.Products.SingleOrDefault(p => p.ProductId == id);
            if (productToDelete == null)
                return;

            DB.Products.Remove(productToDelete);
            DB.SaveChanges();
        }
        public List<ProductModel> GetProductsCheaperThan(decimal price)
        {
            return DB.Products.Where(p => p.UnitPrice < price).OrderBy(p => p.UnitPrice).Select(p => new ProductModel(p)).ToList();
        }
        public List<ProductModel> GetProductsExpensiveThan(decimal price)
        {
            return DB.Products.Where(p => p.UnitPrice > price).OrderBy(p => p.UnitPrice).Select(p => new ProductModel(p)).ToList();
        }
    }
}
