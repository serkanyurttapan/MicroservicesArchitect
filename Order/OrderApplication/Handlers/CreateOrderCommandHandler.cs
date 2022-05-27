using MediatR;
using OrderApplication.Commands;
using OrderApplication.Dtos;
using OrderDomain.OrderAggregate;
using OrderInfrastructure;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderApplication.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
    {
        private readonly OrderDbContext _orderDbContext;
        public CreateOrderCommandHandler(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }
        public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAddress = new Address(request.Address.Province,
                                         request.Address.District,
                                         request.Address.Street,
                                         request.Address.ZipCode,
                                         request.Address.Line);

            Order newOrDer = new(request.BuyyerId, newAddress);

            request.OrderItems.ForEach(x =>
            {
                newOrDer.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl);
            });

            await _orderDbContext.Orders.AddAsync(newOrDer, cancellationToken);
            await _orderDbContext.SaveChangesAsync(cancellationToken);

            return Response<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = newOrDer.Id }, 200);

        }
    }
}
