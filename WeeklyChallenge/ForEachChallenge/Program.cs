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

            //Goal
            List<string> nameList = new List<string>() { "Bob Bobby", "Ray Ray", "Jack Box", "Tester Surname", "Wade Holton" };

            foreach (string name in nameList)
            {
                Console.WriteLine(name);
            }


            //Bonus
            Console.WriteLine("\nBonus: \n");
            List<Person> peopleList = new List<Person>()
            {
                new Person("Mike","Magick"),
                new Person("Helen","Magick"),
                new Person("Tester","Surname"),
                new Person("Wade","Box")
            };

            foreach(Person person in peopleList)
            {
                Console.WriteLine("Hello, My name is " + person.Name);
            }


            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
    }
}
