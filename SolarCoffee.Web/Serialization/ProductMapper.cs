using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Serialization
{
    public static class ProductMapper
    {
        public static ProductModel SerializeProductModel(Data.Models.Product product) {
            return new ProductModel {
                Id = product.Id,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.UpdatedOn,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived
            };
        }

         public static Data.Models.Product SerializeProductModel(ProductModel product) {
            return new Data.Models.Product {
                Id = product.Id,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.UpdatedOn,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived
            };
        }
    }
}