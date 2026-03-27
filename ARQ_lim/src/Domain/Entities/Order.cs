using System;
using System.Collections.Generic;

namespace Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public decimal CalculateTotal()
    {
        var total = Quantity * UnitPrice; 
        return total;
    }
}
