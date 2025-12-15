using Bookstore.Domain.Addresses;
using Bookstore.Domain.Books;
using Bookstore.Domain.Customers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Bookstore.Domain.Orders
{
    [Table("orders", Schema = "bobsusedbookstore_dbo")]
    public class Order : Entity
    {
        public Order(int customerId, int addressId)
        {
            CustomerId = customerId;
            AddressId = addressId;
        }

        private readonly List<OrderItem> orderItems = new List<OrderItem>();

        [Column("customerid")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Column("addressid")]
        public int AddressId { get; set; }
        public Address Address { get; set; }

        // Navigation property - DO NOT map to database column
        public IEnumerable<OrderItem> OrderItems => orderItems;

        [Column("deliverydate")]
        public DateTime DeliveryDate { get; set; } = DateTime.UtcNow.AddDays(7);

        [Column("orderstatus")]
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        // Computed property - DO NOT map to database column
        public decimal Tax => SubTotal * 0.1m;

        // Computed property - DO NOT map to database column
        public decimal SubTotal => OrderItems.Sum(x => x.Book.Price);

        // Computed property - DO NOT map to database column
        public decimal Total => SubTotal + Tax;

        public void AddOrderItem(Book book, int quantity)
        {
            orderItems.Add(new OrderItem(this, book, quantity));
        }
    }
}