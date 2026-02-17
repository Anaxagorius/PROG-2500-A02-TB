using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement
{
    public abstract class Student
    {
        //Discuss on the use of feilds and properties
        //Discuss about auto-implemented properties and encapsulated feilds in visual studio
        //Discuss about the naming of properties and feilds.

        //Discuss about expression - bodies
        //Discuss about these and good proper coding practices.


        private string fName;
        
        private string lName;

        private int age;

        private string Program;

        //public string FName { get => fName; set => fName = value; }
        public string Fname
        {
            get => fName;

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    fName = value;

                }
                else
                    throw new Exception("Enter a valid name");
            }
        }


        public string LName { get => lName; set => lName = value; }
       // public int Age { get => age; set => age = value; }
        public string sProgram { get => Program; set => Program = value; }

        public int Age
        {
            get => age;
            set
            {
                if(value>0 && value < 100)
                {
                    age = value;
                }
                else
                    throw new ArgumentOutOfRangeException("Age must be between 1 and 99.");
            }

        }

        public Student(string firstName,string lastName, int studentAge, string studentProgram )
        {
            Fname = firstName;
            LName = lastName;
            Age = studentAge;
            sProgram = studentProgram;
        }

        public void printClassDetails()
        {
            MessageBox.Show("This is non-abstract member in abstract class");
        }
        //Explain about this method. 
        //Why the 'virtual' keyword is missing here?....
        public abstract void DisplayStudentDetails();



    }
}
