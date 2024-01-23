﻿using GeekShopping.CartAPI.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CartAPI.Model
{
    
    public class CartHeaderVO 
    {
        public long Id { get; set; }
        public string UserId { get; set; }        
        public string CouponCode { get; set; }
    }
}
