using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asynchronous_Programming
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting parallel tasks...");

            // Run tasks in parallel
            await RunParallelTasks();

            Console.WriteLine("Parallel tasks completed.");
        }

        static async Task RunParallelTasks()
        {
            // Create a list of tasks
            var tasks = new List<Task>
            {
                Task.Run(() => DoWork("Task 1")),
                Task.Run(() => DoWork("Task 2")),
                Task.Run(() => DoWork("Task 3")),
            };

            // Await all tasks to complete
            await Task.WhenAll(tasks);
        }

        static void DoWork(string taskName)
        {
            Console.WriteLine($"{taskName} starting...");
            // Simulate some work
            Task.Delay(2000).Wait(); // Wait for 2 seconds
            Console.WriteLine($"{taskName} completed.");
        }
    }
}
