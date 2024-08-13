using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLINQ
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting PLINQ query...");

            // Generate a list of numbers
            var numbers = Enumerable.Range(1, 1000);

            // Execute a PLINQ query
            var results = numbers.AsParallel()
                .Where(n => IsPrime(n))
                .ToList();

            Console.WriteLine($"Found {results.Count} prime numbers.");
        }

        // Check if a number is prime
        static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;
            for (int i = 3; i <= Math.Sqrt(number); i += 2)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }
}
