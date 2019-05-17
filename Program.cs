using System;
using System.Security.Cryptography;
using System.Text;

namespace test_hash
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string input = Console.ReadLine();

            string output = getHash(input);
            
            Console.WriteLine($"{input}: {output}");
        }

        private static string getHash(string text)
        {
            using(var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
