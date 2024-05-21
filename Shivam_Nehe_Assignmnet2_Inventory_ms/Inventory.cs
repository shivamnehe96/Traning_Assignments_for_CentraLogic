using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Inventry_Management_System
{
    internal class Inventory
    {
        public List<ItemsForInventory> items;
        public Inventory() 
        {
            items = new List<ItemsForInventory>();

        }

        //Validating the numeric input provided by user
        public static bool TryParseDouble(string input, out double result) 
        { 
            return double.TryParse(input, out result);
        }

        public static bool TryParseInt(string input, out int result)
        {
            return int.TryParse(input, out result);
        }


        //Validating the string 

        public static bool ValidateString(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

       
        //CRUD Operations for Inventory.
        //Adding the methods 

        public void AddItems()
        {
            int productID = GenerateID();

            Console.WriteLine("Enter the item name to add :");
            string productName=Console.ReadLine();
            while (!ValidateString(productName)) 
            {
                Console.WriteLine("Name Can not be empty,Please Enter a appropriate/valid Name of product");
                Console.WriteLine("Enter the item name to add:");
                productName= Console.ReadLine();
            }
            Console.WriteLine("Enter the price for item:");
            double productPrice;
            while (!TryParseDouble(Console.ReadLine(),out productPrice))
            {
                Console.WriteLine("The Price You Entered is Inavlid or Not Appropriate,Please Enter a valid Price");
                Console.WriteLine("Enter the item price:");
                productPrice=double.Parse(Console.ReadLine());

            }

            Console.WriteLine("Enter the item Quantity:");
            int productQuantity;
            while (!TryParseInt(Console.ReadLine(), out productQuantity))
            {
                Console.WriteLine("The Quantity you Entered in Inavalid or Not Appropriate,Please Enter the valid Quantity of item:");
                Console.WriteLine("Enter the valid Quantity:");
                productQuantity=int.Parse(Console.ReadLine());
            
            }

            ItemsForInventory newItem = new ItemsForInventory(productID, productName, productPrice, productQuantity);
            items.Add(newItem);
            Console.WriteLine("Product details addes succesfully in the inventory...");
            Console.WriteLine("\n");

        }

        //Method for Displaying the items.

        public void DisplayAllItems()
        {
            if (items.Count == 0)
            {
                Console.WriteLine("Item not found in the inventory");


            }
            else {
                Console.WriteLine("\n!.......... Inventory List..........!");
                Console.WriteLine("ProductID      ProductName      ProductPrice      ProductQuantity");
                foreach (ItemsForInventory item in items)
                {
                    Console.WriteLine($"{item.ProductID}   {item.ProductName}    {item.ProductPrice}      {item.ProductQuantity}");
                    Console.WriteLine("\n");
                }
            }
        }


        //Methos for searching the item with id
        public ItemsForInventory SearchItemByID(int id)
        {
            return items.Find(item => item.ProductID == id);
        }

        //Method For generating the ID 
        public int GenerateID()
        {
            return items.Count + 1;       
        }

        //Method For Updating the item's information

        public void UpdateItem()
        {
            Console.WriteLine("Enter the  ID of item to update:");
            int ProductID = int.Parse(Console.ReadLine());
            ItemsForInventory itemToUpdate=SearchItemByID(ProductID);

            if (itemToUpdate != null)
            {
                bool Update = true;
                while (Update) 
                {
                    Console.WriteLine("\nSelect the field to update:");
                    Console.WriteLine("1.  Product Name");
                    Console.WriteLine("2.  Product Price");
                    Console.WriteLine("3.  Product Quantity");
                    Console.WriteLine("4. Update all fields");
                    Console.WriteLine("5. Finish updating");
                   
                    Console.Write(" Enter your Choice: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.Write($"Current Name: {itemToUpdate.ProductName}, New Name: ");
                            string newName = Console.ReadLine();
                            itemToUpdate.ProductName = newName;
                            Console.WriteLine("Name updated Succesfully");
                            break;

                        case "2":
                            Console.Write($"Current Price: {itemToUpdate.ProductPrice}, New Price: ");
                            double newPriceInput= double.Parse(Console.ReadLine());
                            itemToUpdate.ProductPrice = newPriceInput;
                            Console.WriteLine("Price Updated Successfully");

                            break;

                        case "3":
                            Console.Write($"Current Quantity: {itemToUpdate.ProductQuantity}, New Quantity: ");
                            int  newQuantityInput = int.Parse(Console.ReadLine());
                            itemToUpdate.ProductQuantity = newQuantityInput;
                            Console.WriteLine("Quantity Updated succesfully");

                            break;

                        case "4":
                            Console.Write($"Current Name: {itemToUpdate.ProductName}, New Name: ");
                            string updatedName = Console.ReadLine();
                            itemToUpdate.ProductName = updatedName;
                            Console.WriteLine("Name updated Succesfully");

                            Console.Write($"Current Price: {itemToUpdate.ProductPrice}, New Price: ");
                            double updatedPriceInput = double.Parse(Console.ReadLine());
                            itemToUpdate.ProductPrice = updatedPriceInput;
                            Console.WriteLine("Price Updated Successfully");

                            Console.Write($"Current Quantity: {itemToUpdate.ProductQuantity}, New Quantity: ");
                            int updatedQuantityInput = int.Parse(Console.ReadLine());
                            itemToUpdate.ProductQuantity = updatedQuantityInput;
                            Console.WriteLine("Quantity Updated succesfully");
                            break;

                        case "5":
                            Update = false;
                            Console.WriteLine("Information updated succesfully");
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Item not found.");
            }
            Console.WriteLine("");


        }

        public void DeleteItem(int id)
        {
            ItemsForInventory itemToDelete = SearchItemByID(id);
            if (itemToDelete != null)
            {
                items.Remove(itemToDelete);
                Console.WriteLine("Item deleted succesfully");
            }
            else 
            {
                Console.WriteLine("Item Not found in the inventory.");
            }
            Console.WriteLine("\n");

        }
    }
}
        
    

