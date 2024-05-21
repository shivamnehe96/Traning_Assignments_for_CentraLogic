using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventry_Management_System
{
     class Program:Inventory
    {
        static void Main(string[] args)
        {
            Inventory inventory = new Inventory();

            Console.WriteLine("!..........Welcome to Inventory Management System..........!");
            Console.WriteLine("!..........Manage Your Inventory Here ..........!");
            Console.WriteLine("Select the below Option ....");
            try 
            {
                while (true) 
                {
                    Console.WriteLine("1. Add  new Item to the inventory: ");
                    Console.WriteLine("2. Display all Items from inventory: ");
                    Console.WriteLine("3. Search Item by its ID from inventory: ");
                    Console.WriteLine("4. Update Item from inventory: ");
                    Console.WriteLine("5. Delete Item from inventory: ");
                    Console.WriteLine("6. Exit the application: ");

                    Console.WriteLine("Enter your choice: ");
                    int choice=int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            inventory.AddItems();
                            Console.WriteLine("\n");
                            break;

                        case 2:
                            inventory.DisplayAllItems();
                            Console.WriteLine("\n");
                            break;

                        case 3:
                            Console.WriteLine("Enter the Id of item to search from the list");
                            int findID=int.Parse(Console.ReadLine());
                            ItemsForInventory findItem=inventory.SearchItemByID(findID);

                            if (findItem != null)
                            {
                                Console.WriteLine("The item you are searching is found...");
                                findItem.DisplayProducts();
                            }
                            else
                            {
                                Console.WriteLine("Item Not found in the inventory...");
                            }
                              break;
                         case 4:
                            inventory.UpdateItem();
                            Console.WriteLine("\n");
                            break;

                         case 5:
                            Console.WriteLine("Enter item id to delete...");
                            int deleteID=int.Parse(Console.ReadLine());
                            inventory.DeleteItem(deleteID);
                            Console.WriteLine("\n");
                            break;
                         case 6:
                            Console.WriteLine("Exiting from the application,have a good day...!");
                            return;

                        default:
                            Console.WriteLine("You have Entered a Invalid choice ,please select the valid choice");
                            break;
                    }
                }
            }
            catch(Exception )
            {
                Console.WriteLine("Enter a valid choice");
            }
        }
    }
}
