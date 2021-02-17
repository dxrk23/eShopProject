using System.Collections.Generic;

namespace Catalog.API.Entities
{
    public class BasketCart
    {
        public BasketCart()
        {
        }

        public BasketCart(string userName)
        {
            Username = userName;
        }

        public string Username { get; set; }
        public List<BasketCartItem> Items { get; set; } = new List<BasketCartItem>();

        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = 0;
                foreach (var item in Items) totalprice += item.Price * item.Quantity;

                return totalprice;
            }
        }
    }
}