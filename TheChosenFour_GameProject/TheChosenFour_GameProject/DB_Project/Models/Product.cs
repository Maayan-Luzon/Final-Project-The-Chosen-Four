using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Project.Models
{
    public class Product
    {
        public int ProductId { get; set; } // the product's id
        public int ProductPrice { get; set; } // the product's price
        public string ProductName { get; set; } // the product's name
        public string ProductType { get; set; } // the product's type (Shoe, Shirt...)
        public int ProductLives { get; set; } // the amount of lives the product can add to the warrior
        public int ProductStrength { get; set; } // the amount of strength the product can add to the warrior

    }
}
