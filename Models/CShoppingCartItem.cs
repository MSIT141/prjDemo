using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace prjDemo.Models
{
    public class CShoppingCartItem
    {
        public int id { set; get; }
        [DisplayName("數量")]
        public int count { set; get; }
        public decimal price { set; get; }
        public decimal 小計 { get { return this.count * this.price; } }
        public tProduct product { set; get; }
        
    }
}