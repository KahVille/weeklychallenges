using System;
using System.Collections.Generic;
using System.Text;


//Bonus: use class intead of strings and print these to the console

namespace ForEachChallenge
{
    class Person
    {

        public Person(string firstName = null, string lastName= null)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }

        private string firstName;
        private string lastName;

        public string Name => $"{firstName} {lastName}";

    }
}
