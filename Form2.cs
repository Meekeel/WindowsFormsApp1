using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Student CurrentStudent { get; private set; }
        private void fillForm()
        {
            textBoxName.Text = CurrentStudent.Name;
            textBoxName2.Text = CurrentStudent.Name2;
            dateTimePicker1.Value = CurrentStudent.DayOfBirthd;
            textBoxNumber.Text = CurrentStudent.Telephone;

        }
        public Form2(Student selectedStudent)
        {
            InitializeComponent();
            if (selectedStudent == null)
            {
                CurrentStudent = new Student();
            }
            else
            {
                CurrentStudent = selectedStudent;
                fillForm();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            CurrentStudent.Name = textBoxName.Text;
            CurrentStudent.Name2 = textBoxName2.Text;
            CurrentStudent.DayOfBirthd = dateTimePicker1.Value;
            CurrentStudent.Telephone = textBoxNumber.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_click(object seder, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxNumber_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
