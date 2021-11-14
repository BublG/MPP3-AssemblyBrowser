using System;

namespace LibraryTest
{
    public class TestClassPerson
    {
        public int Age;
        public string Name;
        public string Number { get; set; }

        public TestClassPerson(int age, string name, string number)
        {
            Age = age;
            Name = name;
            Number = number;
        }

        public void PrintInfo()
        {
            Console.WriteLine(Name + " " + Age + " " + Number);
        }

        public int GetMultiplication(int n)
        {
            return Age * n;
        }
    }
}