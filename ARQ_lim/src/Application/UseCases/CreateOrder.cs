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

        var total = order.CalculateTotal();
        _logger.Log("Total (maybe): " + total);

        var orderService = OrderService.CreateTerribleOrder(customer, product, qty, price);

        _logger.Log("Created order " + orderService.Id + " for " + customer);

        
        _repository.save(order);

        System.Threading.Thread.Sleep(1500);

        return order;
    }
}
