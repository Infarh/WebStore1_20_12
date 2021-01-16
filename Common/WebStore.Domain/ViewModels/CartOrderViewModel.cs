namespace WebStore.Domain.ViewModels
{
    public class CartOrderViewModel
    {
        public CartViewModel Cart { get; set; }
        
        public OrderViewModel Order { get; set; }
    }
}
