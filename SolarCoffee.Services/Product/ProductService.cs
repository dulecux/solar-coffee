using System.Collections.Generic;
using SolarCoffee.Data;
using System.Linq;
using SolarCoffee.Data.Models;
using System;

namespace SolarCoffee.Services.Product
{
    public class ProductService : IProductService
    {

        private readonly SolarDbContext _db;

        public ProductService(SolarDbContext db)
        {
           _db = db;   
        }
        public ServiceResponse<Data.Models.Product> ArchiveProduct(int id)
        {
            try
            {
                 var product = _db.Products.Find(id);
                 product.IsArchived = true;
                 _db.SaveChanges();

                  return new ServiceResponse<Data.Models.Product> {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Archived product",
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                
                return new ServiceResponse<Data.Models.Product> {
                    Data = null,
                    Time = DateTime.UtcNow,
                    Message = e.StackTrace,
                    IsSuccess = false
                };
            }
        }

    
        public ServiceResponse<Data.Models.Product> CreateProduct(Data.Models.Product product)
        {

            try
            {
                 _db.Products.Add(product);

                var newInventory = new ProductInventory {
                    Product = product,
                    QuantityOnHand = 0,
                    IdealQuantity = 10
                };

                _db.ProductInventories.Add(newInventory);

                _db.SaveChanges();

                return new ServiceResponse<Data.Models.Product> {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Saved new product",
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Product> {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = e.StackTrace,
                    IsSuccess = false
                };
            }
           
        }

        public List<Data.Models.Product> GetAllProducts()
        {
            return _db.Products.ToList();
        }

        public Data.Models.Product GetProductById(int id)
        {
            return _db.Products.Find(id);
        }
    }
}