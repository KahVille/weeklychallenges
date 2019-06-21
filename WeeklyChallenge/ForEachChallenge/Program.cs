using System;
using System.Collections.Generic;


/* Todo:
 * Create a console app
 * Create a list of strings
 * Loop trough the list
 * Print every name to the console
 */

//This program print set of names to the console.

namespace ForEachChallenge
{
    class Program
    {
        static void Main(string[] args)
        {

            List<string> nameList = new List<string>() { "Bob Bobby", "Ray Ray", "Jack Box", "Tester Surname", "Wade Holton" };

            foreach (string name in nameList)
            {
                Console.WriteLine(name);
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
    }
}
