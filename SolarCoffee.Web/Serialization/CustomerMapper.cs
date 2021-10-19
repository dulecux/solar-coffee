using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Serialization
{
    public class CustomerMapper
    {
        public static CustomerModel SerializeCustomer(Customer customer) {

                return new CustomerModel {
                    Id = customer.Id,
                    CreatedOn = customer.CreatedOn,
                    UpdatedOn = customer.UpdatedOn,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PrimaryAddress = AddressMapper.SerializeAddress(customer.PrimaryAddress)
                };   
        }

         public static Customer SerializeCustomer(CustomerModel customer) {

                return new Customer {
                    Id = customer.Id,
                    CreatedOn = customer.CreatedOn,
                    UpdatedOn = customer.UpdatedOn,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PrimaryAddress = AddressMapper.SerializeAddress(customer.PrimaryAddress)
                };   
        }



    }
}