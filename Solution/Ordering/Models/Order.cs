using System.Collections.Generic;
using System;

namespace OrdersApi.Models
{
    public class Order
    {
        protected IList<OrderDetails> _orderDetails;
        public Order()
        {
            this.OrderDetails = new List<OrderDetails>();
        }

        public Guid OrderId { get; set; }

        public string PictureUrl { get; set; }

        public byte[] ImageData { get; set; }

        public string UserEmail { get; set; }

        public Status Status { get; set; }

        public ICollection<OrderDetails> OrderDetails
        {
            get
            {
                return _orderDetails;
            }
            set
            {
                this.OrderDetails = _orderDetails ?? new List<OrderDetails>();
            }

        }
    }
}