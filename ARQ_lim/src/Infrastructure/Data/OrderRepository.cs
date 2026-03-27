using Domain.Entities;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class OrderRepository: IOrderRepository
    {
        public void save(Order order)
        {
            var sql = "INSERT INTO Orders(Id, Customer, Product, Qty, Price) VALUES (" +
                      order.Id + ", '" +
                      order.CustomerName + "', '" +
                      order.ProductName + "', " +
                      order.Quantity + ", " +
                      order.UnitPrice + ")";

            BadDb.ExecuteNonQueryUnsafe(sql);
        }
    }
}
