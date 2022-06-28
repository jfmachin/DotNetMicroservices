﻿namespace Basket.API.Models.Entities {
    public class ShoppingCart {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        public ShoppingCart() { }
        public ShoppingCart(string username) {
            UserName = username;
        }
        public decimal TotalPrice {
            get {
                decimal total = 0;
                foreach (var item in Items)
                    total += item.Price * item.Quantity;
                return total;
            }
        }
    }
}
