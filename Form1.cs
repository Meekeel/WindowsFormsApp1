using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        List<Student> listStudent = new List<Student>();
        private int idSelect = 1;
        private string filePath = "students.txt";
        public Form1()
        {
            InitializeComponent();
            loadData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void refreshTable()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listStudent;
        }

        private void buttonDel_Click(object semder, EventArgs e)
        {
            switch (currentState)
            {
                case States.STUDENTS:
                    if (dataGridView1.SelectedRows.Count <= 0)
                    {
                        MessageBox.Show("Вы не выбрали ни одного студента"
                            , "Не выбран студент",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                            );
                        return;
                    }
                    Student selectedStudent = (Student)dataGridView1.SelectedRows[0].DataBoundItem;
                    if (MessageBox.Show("Вы уверены, что хотите удалить этого студента?", "Точно удалить?",
                        MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes
                        )
                    {
                        Student remStudent = (Student)dataGridView1.SelectedRows[0].DataBoundItem;
                        listStudent.Remove(remStudent);
                        refreshTable();
                    }
                    break;

                case States.FINANCES:
                    if (dataGridView1.SelectedRows.Count <= 0)
                    {
                        MessageBox.Show("Вы не выбрали ни одного студента"
                            , "Не выбран студент",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                            );
                        return;
                    }
                    Finance selectedFinance = (Finance)dataGridView1.SelectedRows[0].DataBoundItem;
                    if (MessageBox.Show("Вы уверены, что хотите удалить этого студента?", "Точно удалить?",
                        MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes
                        )
                    {
                        Finance remFinance = (Finance)dataGridView1.SelectedRows[0].DataBoundItem;
                        listFinance.Remove(remFinance);
                        refreshTable();
                    }
                    break;

                case States.EXPENSES:
                    if (dataGridView1.SelectedRows.Count <= 0)
                    {
                        MessageBox.Show("Вы не выбрали ни одного студента"
                            , "Не выбран студент",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                            );
                        return;
                    }
                    Finance selectedExpense = (Finance)dataGridView1.SelectedRows[0].DataBoundItem;
                    if (MessageBox.Show("Вы уверены, что хотите удалить этого студента?", "Точно удалить?",
                        MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes
                        )
                    {
                        Finance remExpense = (Finance)dataGridView1.SelectedRows[0].DataBoundItem;
                        listExpense.Remove(remExpense);
                        refreshTable();
                    }
                    break;

                case States.PRODUCTS:
                    if (dataGridView1.SelectedRows.Count <= 0)
                    {
                        MessageBox.Show("Вы не выбрали ни одного студента"
                            , "Не выбран студент",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                            );
                        return;
                    }
                    Product selectedProduct = (Product)dataGridView1.SelectedRows[0].DataBoundItem;
                    if (MessageBox.Show("Вы уверены, что хотите удалить этого студента?", "Точно удалить?",
                        MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes
                        )
                    {
                        Product remProduct = (Product)dataGridView1.SelectedRows[0].DataBoundItem;
                        listProduct.Remove(remProduct);
                        refreshTable();
                    }
                    break;
            }
        }


        private void buttonAdd_Click(object sender, EventArgs e)
        {
            using (var formE = new Form2(null))
            {
                if (formE.ShowDialog() == DialogResult.OK)
                {
                    Student newStudent = formE.CurrentStudent;
                    newStudent.Id = idSelect++;
                    listStudent.Add(newStudent);
                    refreshTable();
                }
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Вы не выбрали ни одного студанта!", "Не выбрали", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Student selectedStudent = (Student)dataGridView1.SelectedRows[0].DataBoundItem;
            using (var formE = new Form2(selectedStudent))
            {
                if (formE.ShowDialog() == DialogResult.OK)
                {
                    refreshTable();
                }
            }

        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            List<string> lineData = new List<string>();
            foreach (var line in listStudent)
            {
                lineData.Add(
                    $"{line.Id}|{line.Name}|{line.Name2}|" +
                    $"{line.DayOfBirthd:yyyy-MM-dd}|{line.Telephone}");
            }
            File.WriteAllLines(filePath, lineData);
            MessageBox.Show("Данные сохранены!", "Данные сохранены!", MessageBoxButtons.OK);
            refreshTable();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            buttonEdit_Click(sender, e);
            buttonDel_Click(sender, e);
        }

        private void loadData()
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            listStudent.Clear();
            idSelect = 1;

            foreach (string line in File.ReadAllLines(filePath))
            {
                string[] parts = line.Split('|');
                if (parts.Length == 5)
                {
                    listStudent.Add(new Student()
                    {
                        Id = Convert.ToInt32(parts[0]),
                        Name = parts[1],
                        Name2 = parts[2],
                        DayOfBirthd = Convert.ToDateTime(parts[3]),
                        Telephone = parts[4]
                    });
                    idSelect = Convert.ToInt32(parts[0] + 1);
                }
            }
            refreshTable();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            switch (currentState)
            {
                case States.STUDENTS:
                    listStudent.Clear();
                    MessageBox.Show("Данные очищены!", "Данные сохранены!", MessageBoxButtons.OK);
                    refreshTable();
                    break;
                case States.FINANCES:
                    listFinance.Clear();
                    MessageBox.Show("Данные очищены!", "Данные сохранены!", MessageBoxButtons.OK);
                    refreshTable();
                    break;
                case States.EXPENSES:
                    listExpense.Clear();
                    MessageBox.Show("Данные очищены!", "Данные сохранены!", MessageBoxButtons.OK);
                    refreshTable();
                    break;
                case States.PRODUCTS:
                    listProduct.Clear();
                    MessageBox.Show("Данные очищены!", "Данные сохранены!", MessageBoxButtons.OK);
                    refreshTable();
                    break;
            }
        }

        private void buttonFamilyMembers_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listStudent;
        }

        private void buttonExpenses_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listStudent;
        }

        private void buttonFinances_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listStudent;
        }
    }
}
