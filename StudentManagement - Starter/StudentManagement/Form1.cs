namespace StudentManagement
{
    public partial class Form1 : Form
    {
        private FirstYearStudent firstYearStudent;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            firstYearStudent = new FirstYearStudent("enter your name", "", 14, "", "");
            List<FirstYearStudent> students = firstYearStudent.Load();
            listBox1.Items.Clear();

            foreach (var student in students)
            {
                listBox1.Items.Add(
                    $"{student.Fname},{student.LName},{student.Age},{student.sProgram},{student.yearOfStudy},{student.workTermStatus}");
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            List<FirstYearStudent> students1 = new List<FirstYearStudent>();

            foreach (var item in listBox1.Items)
            {
                string[] parts = item.ToString().Split(',');

                if (parts.Length == 6)
                {
                    students1.Add(new FirstYearStudent(
                        parts[0].Trim(),
                        parts[1].Trim(),
                        int.Parse(parts[2].Trim()),
                        parts[3].Trim(),
                        parts[5].Trim()
                    ));
                }
            }

            firstYearStudent.Save(students1);

        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            
        }

              
    }
}

