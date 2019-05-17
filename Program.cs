using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace test_hash
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string text2hash = Console.ReadLine();
            tryIt(text2hash);
            Console.ReadLine();
        }

        private static string getHash(string text, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                string toHash = string.Concat(text, salt);

                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(toHash));

                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                return hash;
            }
        }

        private static string getSalt()
        {
            byte[] bytes = new byte[128 / 8];
            using (var keyGenerator = RandomNumberGenerator.Create())
            {
                keyGenerator.GetBytes(bytes);
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        private static void tryIt(string text)
        {
            string saltSolution = "";
            string puzzleStart = "ab";
            int count = 1;
            Stopwatch watch = new Stopwatch();
            watch.Start();
            TimeSpan timeSpan = new TimeSpan();

            List<string> listUsedSalts = new List<string>();

            while (true)
            {
                saltSolution = getSalt();

                //if (!listUsedSalts.Contains(saltSolution))
                //{
                    string hashSolution = getHash(text, saltSolution);
                    //listUsedSalts.Add(saltSolution);

                    if (hashSolution.Substring(0, 2) == puzzleStart)
                    {
                        timeSpan = watch.Elapsed;
                        Console.WriteLine($"Solution found! - Seconds needed: {timeSpan.TotalSeconds.ToString()}");
                        //Console.WriteLine($"text: {text} - salt: {saltSolution} - hash: {hashSolution}");
                        Console.WriteLine($"try {count.ToString()} - hash: {hashSolution}");
                    break;
                    }
                    else if (count > 10000000)
                    {
                        timeSpan = watch.Elapsed;
                        Console.WriteLine($"Soluton not found! Seconds needed: {timeSpan.TotalSeconds.ToString()}");
                        break;
                    }
                    else if ((count % 100000) == 0)
                    {
                        timeSpan = watch.Elapsed;
                        Console.Clear();
                        Console.WriteLine($"Try: {count} - Seconds running: {timeSpan.TotalSeconds.ToString()}");
                    }
                    //count++;
                //}
                count++;
            }

            watch.Stop();
        }
    }
}