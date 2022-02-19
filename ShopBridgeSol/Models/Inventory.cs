using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridgeSol.Models
{
    public class Inventory
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public double Price { get; set; }
        public string ItemCategory { get; set; }
        public string ItemImageName { get; set; }
        public string ImageBase64Code { get; set; }
        public string UserID
        {
            get; set;


        }
    }
}
