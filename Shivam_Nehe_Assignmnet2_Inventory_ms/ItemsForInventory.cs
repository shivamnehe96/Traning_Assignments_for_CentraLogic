using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Inventry_Management_System
{
    internal class ItemsForInventory
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int ProductQuantity { get; set; }

        //Constructor with fields
        public ItemsForInventory(int productID, string productName, double productPrice, int productQuantity)
        {
            ProductID = productID;
            ProductName = productName;
            ProductPrice = productPrice;
            ProductQuantity = productQuantity;
        }
        //ToString methos to override the string representation of the object.
        public override string ToString()
        {
            return $"ProductID:{ProductID},ProductName:{ProductName},ProductPrice:{ProductPrice},ProductQuantity:{ProductQuantity}";

            //we can use "ProductPrice:{ProductPrice:C}" for formatiing the value of price as per specific country's symbol. 
        }

        public void DisplayProducts()  
        {
            Console.WriteLine($"ProductID:{ProductID},ProductName:{ProductName},ProductPrice:{ProductPrice},ProductQuantity:{ProductQuantity}");
        }

    }
}
