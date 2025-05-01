using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderModel
{
    public class OrderResultDto
    {
        //Id
        public Guid Id { get; set; }

        //User Email
        public string UserEmail { get; set; }

        //Shipping Address
        public AddressDto ShippingAddress { get; set; }

        //Order Items
        public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>(); //Navigational Property

        //Delivery Method
        public string DeliveryMethod { get; set; } 

        //Payment Status
        public string PaymentStatus { get; set; }

        //SubTotal 
        public decimal SubTotal { get; set; }

        //OrderDate
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        //Payment
        public string PaymentIntentId { get; set; } = string.Empty;

        public decimal Total { get; set; }
    }
}
