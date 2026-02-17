namespace StudentManagement
{
    public partial class Form1 : Form
    {
        private FirstYearStudent firstYearStudent;
        private int selectedIndex = -1;

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
            try
            {
                // Validate inputs
                if (!ValidateInputs())
                {
                    return;
                }

                // Read student data from text boxes
                var studentData = ReadStudentDataFromTextBoxes();

                // Create the student entry in the same format as Load
                string studentEntry = $"{studentData.fname},{studentData.lname},{studentData.age},{studentData.program},{studentData.yearOfStudy},{studentData.workterm}";

                // Add to list box
                listBox1.Items.Add(studentEntry);

                // Save to file
                SaveStudentsToFile();

                // Clear text boxes
                ClearTextBoxes();

                MessageBox.Show("Student added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding student: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Validates all input fields
        /// </summary>
        private bool ValidateInputs()
        {
            // Check if all fields have values
            if (string.IsNullOrWhiteSpace(txtFname.Text))
            {
                MessageBox.Show("First name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFname.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLname.Text))
            {
                MessageBox.Show("Last name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLname.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAge.Text))
            {
                MessageBox.Show("Age is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAge.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtProgram.Text))
            {
                MessageBox.Show("Program is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProgram.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtWorkterm.Text))
            {
                MessageBox.Show("Work term status is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtWorkterm.Focus();
                return false;
            }

            // Validate age format and range
            if (!ValidateAge(txtAge.Text, out int age))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates age field - must be numeric and within valid range
        /// </summary>
        private bool ValidateAge(string ageText, out int age)
        {
            if (!int.TryParse(ageText, out age))
            {
                MessageBox.Show("Age must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAge.Focus();
                return false;
            }

            if (age < 1 || age > 99)
            {
                MessageBox.Show("Age must be between 1 and 99.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAge.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Reads student data from text boxes
        /// </summary>
        private (string fname, string lname, int age, string program, int yearOfStudy, string workterm) ReadStudentDataFromTextBoxes()
        {
            return (
                fname: txtFname.Text.Trim(),
                lname: txtLname.Text.Trim(),
                age: int.Parse(txtAge.Text.Trim()),
                program: txtProgram.Text.Trim(),
                yearOfStudy: 1, // First year students always have year 1
                workterm: txtWorkterm.Text.Trim()
            );
        }

        /// <summary>
        /// Clears all text boxes
        /// </summary>
        private void ClearTextBoxes()
        {
            txtFname.Clear();
            txtLname.Clear();
            txtAge.Clear();
            txtProgram.Clear();
            txtWorkterm.Clear();
            txtFname.Focus();
        }

        /// <summary>
        /// Saves all students from list box to file
        /// </summary>
        private void SaveStudentsToFile()
        {
            List<FirstYearStudent> students = new List<FirstYearStudent>();

            foreach (var item in listBox1.Items)
            {
                string[] parts = item.ToString().Split(',');

                if (parts.Length == 6)
                {
                    students.Add(new FirstYearStudent(
                        parts[0].Trim(),
                        parts[1].Trim(),
                        int.Parse(parts[2].Trim()),
                        parts[3].Trim(),
                        parts[5].Trim()
                    ));
                }
            }

            if (firstYearStudent == null)
            {
                firstYearStudent = new FirstYearStudent("temp", "temp", 18, "temp", "temp");
            }

            firstYearStudent.Save(students);
        }

        /// <summary>
        /// Handles list box selection changed event
        /// </summary>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                return;
            }

            try
            {
                // Store the selected index
                selectedIndex = listBox1.SelectedIndex;

                // Parse and load student data into text boxes
                string selectedItem = listBox1.SelectedItem.ToString();
                LoadStudentIntoTextBoxes(selectedItem);

                // Set text boxes to read-only initially
                ToggleTextBoxReadOnly(true);

                // Show Update and Delete buttons
                btnUpdate.Visible = true;
                btnDelete.Visible = true;

                // Ask if user wants to update
                DialogResult result = MessageBox.Show(
                    "Do you want to update this record?",
                    "Update Record",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    // Enable editing
                    ToggleTextBoxReadOnly(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading student data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads student data from list box item into text boxes
        /// </summary>
        private void LoadStudentIntoTextBoxes(string listItem)
        {
            string[] parts = listItem.Split(',');

            if (parts.Length == 6)
            {
                txtFname.Text = parts[0].Trim();
                txtLname.Text = parts[1].Trim();
                txtAge.Text = parts[2].Trim();
                txtProgram.Text = parts[3].Trim();
                txtWorkterm.Text = parts[5].Trim();
            }
            else
            {
                throw new Exception("Invalid student data format.");
            }
        }

        /// <summary>
        /// Toggles read-only state of text boxes
        /// </summary>
        private void ToggleTextBoxReadOnly(bool readOnly)
        {
            txtFname.ReadOnly = readOnly;
            txtLname.ReadOnly = readOnly;
            txtAge.ReadOnly = readOnly;
            txtProgram.ReadOnly = readOnly;
            txtWorkterm.ReadOnly = readOnly;
        }

        /// <summary>
        /// Handles Update button click
        /// </summary>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs
                if (!ValidateInputs())
                {
                    return;
                }

                // Check if we have a valid selected index
                if (selectedIndex < 0 || selectedIndex >= listBox1.Items.Count)
                {
                    MessageBox.Show("No student selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Read updated data from text boxes
                var studentData = ReadStudentDataFromTextBoxes();

                // Update the item in the list box
                string updatedEntry = $"{studentData.fname},{studentData.lname},{studentData.age},{studentData.program},{studentData.yearOfStudy},{studentData.workterm}";
                listBox1.Items[selectedIndex] = updatedEntry;

                // Save to file
                SaveStudentsToFile();

                // Clear and reset UI
                ClearTextBoxes();
                ToggleTextBoxReadOnly(false);
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
                listBox1.ClearSelected();
                selectedIndex = -1;

                MessageBox.Show("Student updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating student: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles Delete button click
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if we have a valid selected index
                if (selectedIndex < 0 || selectedIndex >= listBox1.Items.Count)
                {
                    MessageBox.Show("No student selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Confirm deletion
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this record?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    // Remove the item from list box
                    listBox1.Items.RemoveAt(selectedIndex);

                    // Save to file
                    SaveStudentsToFile();

                    // Clear and reset UI
                    ClearTextBoxes();
                    ToggleTextBoxReadOnly(false);
                    btnUpdate.Visible = false;
                    btnDelete.Visible = false;
                    selectedIndex = -1;

                    MessageBox.Show("Student deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting student: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

              
    }
}

