﻿using HBOnlineTyresApp.Data.Cart;
using Microsoft.AspNetCore.Mvc;

namespace HBOnlineTyresApp.Data.ViewModels
{
    public class ShoppingCartVM
    {
        public ShoppingCart ShoppingCart { get; set; }
        public double ShoppingCartTotal { get; set; }
        public double ShoppingSubTotal { get; set; }
        public double ShoppingTaxTotal { get; set; }
        
      

    }
}
