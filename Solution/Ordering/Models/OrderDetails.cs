using System;

namespace OrdersApi.Models
{
    public class OrderDetails
    {
         public Guid OrderId { get; set; }

         public int OrderDetailId { get; set; }

         public byte[] FaceData { get; set; }

         public Order Order { get; set; }
    }
}