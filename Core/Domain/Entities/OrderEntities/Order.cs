﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntities
{
    public class Order:BaseEntity<Guid>
    {
        public Order()
        {
            
        }
        public Order(string _UserEmail, string _ShipingAddress, ICollection<OrderItem> _orderItems, OrderPaymentStatus _paymentStatus, DeliveryMethod _deliveryMethod, decimal _SubTotal, string _PaymentIntendId, DateTimeOffset _OrderDate)
        {
            UserEmail = _UserEmail;
            ShipingAddress = _ShipingAddress;
            orderItems = _orderItems;
            paymentStatus = _paymentStatus;
            deliveryMethod = _deliveryMethod;
            SubTotal = _SubTotal;
            PaymentIntendId = _PaymentIntendId;
            OrderDate = _OrderDate;
        }
        public string UserEmail { get; set; }
        public string ShipingAddress { get; set; }
        public ICollection<OrderItem> orderItems { get; set; }
        public OrderPaymentStatus paymentStatus { get; set; }
        public DeliveryMethod deliveryMethod { get; set; }
        public decimal SubTotal { get; set; }
        public string PaymentIntendId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
    }
}
