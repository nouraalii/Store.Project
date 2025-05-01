using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.OrderModel;

namespace Services.Abstraction
{
    public interface IOrderService
    {
        //Get Order By Id Async
        Task<OrderResultDto> GetOrderByIdAsync(Guid Id);

        //Get Order
        Task<IEnumerable<OrderResultDto>> GetOrdersByUserEmailAsync(string userEmail);

        //Create Order
        Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderRequest, string userEmail);

        //Get All Delivery Method
        Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethods();
    }
}
