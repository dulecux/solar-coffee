using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using SolarCoffee.Data.Models;
using SolarCoffee.Web.Serialization;

namespace SolarCoffee.Web.ViewModels
{
    public static class OrderMapper
    {
        public static SalesOrder SerializeInvoiceToOrder(InvoiceModel invoice) {

            var salesOrderItems = invoice.LineItems
                        .Select(item => new SalesOrderItem{
                            Id = item.Id,
                            Quantity = item.Quantity,
                            Product = ProductMapper.SerializeProductModel(item.Product)
                        }).ToList();
            
            return new SalesOrder {
                SalesOrderItems = salesOrderItems,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
            };
        }

        public static List<OrderModel> SerializeOrdersToViewModels(IEnumerable<SalesOrder> orders) {
            
            return orders.Select(order => new OrderModel{
                Id = order.Id,
                CreatedOn = order.CreatedOn,
                UpdatedOn = order.UpdatedOn,
                SalesOrderItems = SerializeSalesOrderItems(order.SalesOrderItems),
                Customer = CustomerMapper.SerializeCustomer(order.Customer),
                IsPaid = order.IsPaid
            }).ToList();
        }

        private static List<SalesOrderItemModel> SerializeSalesOrderItems(IEnumerable<SalesOrderItem> orderItems) {

            return orderItems.Select(orderItem => new SalesOrderItemModel{
                    Id = orderItem.Id,
                    Quantity = orderItem.Quantity,
                    Product = ProductMapper.SerializeProductModel(orderItem.Product)
            }).ToList();
        }
    }
}