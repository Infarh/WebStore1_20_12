using System.Linq;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.Entities.Orders;

namespace WebStore.Services.Mapping
{
    public static class OrderMapper
    {
        public static OrderItemDTO ToDTO(this OrderItem Item) => Item is null
            ? null
            : new OrderItemDTO(Item.Id, Item.Price, Item.Quantity);

        public static OrderItem FromDTO(this OrderItemDTO Item) => Item is null
            ? null
            : new OrderItem
            {
                Id = Item.Id, 
                Price = Item.Price,
                Quantity = Item.Quantity
            };

        public static OrderDTO ToDTO(this Order Order) => Order is null
            ? null
            : new OrderDTO(Order.Id, Order.Name, Order.Phone, Order.Address, Order.Date, Order.Items.Select(ToDTO));

        public static Order FromDTO(this OrderDTO Order) => Order is null
            ? null
            : new Order
            {
                Id = Order.Id,
                Name = Order.Name,
                Phone = Order.Phone,
                Address = Order.Address,
                Date = Order.Date,
                Items = Order.Items.Select(FromDTO).ToList()
            };
    }
}
