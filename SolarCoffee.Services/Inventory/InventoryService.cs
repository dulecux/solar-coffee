using System;
using System.Collections.Generic;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SolarCoffee.Services.Inventory
{
    public class InventoryService : IInventoryService
    {

        private readonly SolarDbContext _db;
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(SolarDbContext dbContext, ILogger<InventoryService> logger)
        {
            _db = dbContext;
            _logger = logger;
        }
        
        public ProductInventory GetByProductId(int productId)
        {
            return _db.ProductInventories
                .Include(pi => pi.Product)
                .FirstOrDefault(pi => pi.Product.Id == productId);
        }

        public List<ProductInventory> GetCurrentInventory()
        {
            return _db.ProductInventories
                 .Include(pi => pi.Product)
                 .Where(pi => !pi.Product.IsArchived)
                 .ToList();
        }

        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {
            //for last 6 hours
            var earliest = DateTime.UtcNow - TimeSpan.FromHours(6);

            return _db.ProductInventorySnapshots
                    .Include(snap => snap.Product)
                    .Where(snap => snap.SnapshotTime > earliest && !snap.Product.IsArchived)
                    .ToList();
                    
        }

        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment)
        {
            var now = DateTime.UtcNow;
            try
            {
                 var inventory = _db.ProductInventories
                    .Include(inv => inv.Product)
                    .First(inv => inv.Product.Id == id);

                inventory.QuantityOnHand += adjustment;

                try
                {
                     CreateSnapshot(inventory);
                }
                catch (Exception ex)
                {
                    
                    _logger.LogError("Error creating inventory snapshot");
                    _logger.LogError(ex.StackTrace);
                }

                _db.SaveChanges();

                return new ServiceResponse<ProductInventory> {
                        IsSuccess = true,
                        Data = inventory,
                        Time = now,
                        Message = $"Product {id} inventory adjusted"
                };
            }
            catch (Exception ex)
            {
                
                return new ServiceResponse<ProductInventory> {
                        IsSuccess = false,
                        Data = null,
                        Time = now,
                        Message = ex.StackTrace
                };
            }
        }

        private void CreateSnapshot(ProductInventory inventory)
        {
            var now = DateTime.UtcNow;
            var snapshot = new ProductInventorySnapshot {
                    SnapshotTime = now,
                    Product = inventory.Product,
                    QuantityOnHand = inventory.QuantityOnHand
            };

            _db.Add(snapshot);
        }

    }
}