using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educ5Ver2
{
    internal class ProductsJSON
    {
        public string Product_Name { get; set; }
        public decimal Price { get; set; }
        public bool Availible { get; set; }
        public int Category_ID { get; set; }
        public int Provider_ID { get; set; }
        public int Maker_ID { get; set; }
    }
}
