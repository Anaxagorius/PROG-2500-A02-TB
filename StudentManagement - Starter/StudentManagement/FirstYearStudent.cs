using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.Devices;
using System.Xml.Linq;

namespace StudentManagement
{
    //what is the use of ':' in C#.
    public class FirstYearStudent:Student,IStudentData
    {
        private List<FirstYearStudent> fstudents;
        //Discuss about the properties....
        public int yearOfStudy { get; set; }
        public string workTermStatus { get; set; }


        // discuss how to use the parent constructor to initialize 
        public FirstYearStudent(string fname, string lname,int age, string program,string workTerm):base(fname,lname,age,program)
        {
            yearOfStudy = 1;
            workTermStatus = workTerm;
        }

        //discuss why override here?....
        public override void DisplayStudentDetails()
        {
            MessageBox.Show($"Student Name: {Fname} {LName}\nAge: {Age}\n Program: {sProgram}");
        }



        public List<FirstYearStudent> Load()
        {

            MessageBox.Show("Loading student data...");

            fstudents = new List<FirstYearStudent>();

            try
            {
                string file = "StudentMaster.txt";
                using (StreamReader reader = new StreamReader(file))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');

                        if (parts.Length == 6) // Ensure correct format
                        {
                            string firstName = parts[0].Trim();
                            string lastName = parts[1].Trim();
                            int age = int.Parse(parts[2].Trim());
                            string program = parts[3].Trim();
                            string workStatus = parts[5].Trim();

                            fstudents.Add(new FirstYearStudent(firstName, lastName, age, program, workStatus));
                        }
                    }
                }

                MessageBox.Show("Student data loaded successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading student data: {ex.Message}");
            }



            return fstudents;

        
                       
        }


        //public void Save(List<FirstYearStudent> students)
        //{

        //    MessageBox.Show("Saving student data...");
        //}



        //Now let us save the data to a file............

        public void Save(List<FirstYearStudent> students)
        {

            string StudentMasterFile = "StudentMaster.txt";
            fstudents = students;

            try
            {
                using (StreamWriter writer = new StreamWriter(StudentMasterFile))
                {
                    foreach (var student in fstudents)
                    {
                        FirstYearStudent st1 = (FirstYearStudent)student;
                        writer.WriteLine($"{st1.Fname},{st1.LName},{st1.Age},{st1.sProgram},{st1.yearOfStudy},{st1.workTermStatus}");
                    }
                }
                MessageBox.Show("Student data saved to file successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}");
            }


        }
        

        }
    }
