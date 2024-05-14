using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TaskList_Application
{
    internal class Program
    {
        static List<(string title, string description)> tasks = new List<(string, string)>();

        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the TaskList Application...!");
            Console.WriteLine("Choose the specific operation you want to perform within the tasklist application");

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Choose the option below to perform TaskList operations:");
                Console.WriteLine("Option 1. Create a task for tasklist:");
                Console.WriteLine("Option 2. Read a tasks for tasklist:");
                Console.WriteLine("Option 3. Update a task for tasklist: ");
                Console.WriteLine("Option 4. Delete a task for tasklist:");
                Console.WriteLine("Option 5. Exit for tasklist application: ");

                Console.Write("Enter your choice to perform the operation for tasklist application: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        CreateTask();
                        break;
                    case 2:
                        ReadTasks();
                        break;
                    case 3:
                        UpdateTask();
                        break;
                    case 4:
                        DeleteTask();
                        break;
                    case 5:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("You have choosed the invalid option for tasklist application ");
                        break;
                }
                Console.WriteLine("Press any button to continue with the application:...");
                Console.ReadLine();
            }
            
        }

        static void CreateTask()
        {
            Console.WriteLine("Create a Task to add into the tasklist");
            Console.Write("Enter a task title: ");
            string titleForTask = Console.ReadLine();
            Console.Write("Enter a task description: ");
            string descriptionForTask = Console.ReadLine();
            tasks.Add((titleForTask, descriptionForTask));
            Console.WriteLine("Task created successfully.");
        }

        static void ReadTasks()
        {
            Console.WriteLine("Reading a Task List");
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks available to Read.");
                return;
            }
            else
            {
                // using for loop
                for (int i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. Title: {tasks[i].title}, Description: {tasks[i].description}");
                }

                //using foreach loop
                /*
                foreach (var task in tasks)
                {
                    Console.WriteLine($"Title: {task.title}");
                    Console.WriteLine($"Description: {task.description}");
                    Console.WriteLine();
                }
                */
            }
        }

        static void UpdateTask()
        {
            Console.WriteLine("Update a Task");
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks available to update from the tasklist.");
                return;
               
            }

            Console.Write("Enter task number to update in the tasklist: ");
            int taskNumber = Convert.ToInt32(Console.ReadLine()) - 1;

            if (taskNumber < 0 || taskNumber >= tasks.Count)
            {
                Console.WriteLine("Invalid task number.");
                return;
             
            }

            Console.Write("Enter new  title to update: ");
            string newTitleForTask = Console.ReadLine();
            Console.Write("Enter new task description: ");
            string newDescriptionForTask = Console.ReadLine();

            tasks[taskNumber] = (newTitleForTask, newDescriptionForTask);
            Console.WriteLine("Task updated successfully in the tasklist.");
        }

        static void DeleteTask()
        {
            Console.WriteLine("Delete a Task");
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks available to delete.");
                return;
            }

            Console.Write("Enter task number to delete: ");
            int taskNumber = Convert.ToInt32(Console.ReadLine()) - 1;

            if (taskNumber < 0 || taskNumber >= tasks.Count)
            {
                Console.WriteLine("Invalid task number.");
                return;
            }

            tasks.RemoveAt(taskNumber);
            Console.WriteLine("Task deleted successfully from the tasklist.");
        }

       
    }
}
