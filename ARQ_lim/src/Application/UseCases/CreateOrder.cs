using System.Threading;
using System;
namespace Application.UseCases;

using Application.Interfaces;
using Domain.Entities;
using Domain.Services;

public class CreateOrderUseCase
{
    private readonly ILogger _logger;
    private readonly IOrderRepository _repository;

    public CreateOrderUseCase(ILogger logger, IOrderRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public Order Execute(string customer, string product, int qty, decimal price)
    {
        _logger.Log("CreateOrderUseCase starting");
        var order = OrderService.CreateTerribleOrder(customer, product, qty, price);

        var sql = "INSERT INTO Orders(Id, Customer, Product, Qty, Price) VALUES (" + order.Id + ", '" + customer + "', '" + product + "', " + qty + ", " + price + ")";
        _logger.Try(() => _repository.save(order));

        System.Threading.Thread.Sleep(1500);

        return order;
    }
}
