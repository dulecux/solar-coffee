using System.Collections.Generic;
using SolarCoffee.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace SolarCoffee.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly SolarDbContext _db;

        public CustomerService(SolarDbContext dbContext)
        {
            _db = dbContext;
        }
        public ServiceResponse<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer)
        {
            try
            {
                 _db.Customers.Add(customer);
                 _db.SaveChanges();

                 return new ServiceResponse<Data.Models.Customer> {
                        Data = customer,
                        Message = "New Customer added",
                        IsSuccess = true,
                        Time = DateTime.UtcNow
                 };
            }
            catch (Exception ex)
            {
                
                return new ServiceResponse<Data.Models.Customer> {
                        Data = customer,
                        Message = ex.StackTrace,
                        IsSuccess = false,
                        Time = DateTime.UtcNow
                 };
            }
        }

        public ServiceResponse<bool> DeleteCustomer(int id)
        {
            var customer = _db.Customers.Find(id);
            var now = DateTime.UtcNow;

            if(customer == null) {
                return new ServiceResponse<bool> {
                        Time = now,
                        IsSuccess = false,
                        Message = "Customer to delete not found",
                        Data = false
                };
            }

            try
            {
                _db.Customers.Remove(customer);
                _db.SaveChanges();

                 return new ServiceResponse<bool> {
                        Time = now,
                        IsSuccess = true,
                        Message = "Customer is deleted",
                        Data = true
                };    
            }
            catch (Exception ex)
            {
                
                return new ServiceResponse<bool> {
                        Time = now,
                        IsSuccess = false,
                        Message = ex.StackTrace,
                        Data = false
                };
            }

        }

        public List<Data.Models.Customer> GetAllCustomers()
        {
            return _db.Customers
                .Include(customer => customer.PrimaryAddress)
                .OrderBy(customer => customer.LastName)
                .ToList();
        }

        public Data.Models.Customer GetCustomerById(int id)
        {
            return _db.Customers.Find(id);
        }
    }
}