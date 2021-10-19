using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Serialization
{
    public class AddressMapper
    {
        public static CustomerAddress SerializeAddress(CustomerAddressModel address) {

                return new CustomerAddress{
                        AddressLine1 = address.AddressLine1,
                        AddressLine2 = address.AddressLine2,
                        City = address.City,
                        State = address.State,
                        Country = address.Country,
                        PostalCode = address.PostalCode,
                        CreatedOn = address.CreatedOn,
                        UpdatedOn = address.UpdatedOn
                };
            
        }

         public static CustomerAddressModel SerializeAddress(CustomerAddress address) {

                return new CustomerAddressModel{
                        Id = address.Id,
                        AddressLine1 = address.AddressLine1,
                        AddressLine2 = address.AddressLine2,
                        City = address.City,
                        State = address.State,
                        Country = address.Country,
                        PostalCode = address.PostalCode,
                        CreatedOn = address.CreatedOn,
                        UpdatedOn = address.UpdatedOn
                };
            
        }
    }
}