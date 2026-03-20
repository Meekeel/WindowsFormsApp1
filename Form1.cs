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
        public enum States { STUDENTS=0, PRODUCTS, FINANCES, EXPENSES };
        public States currentState = States.STUDENTS;

        List<Product> listProduct = new List<Product>();
        List<Student> listStudent = new List<Student>();
        List<Finance> listFinance = new List<Finance>();
        List<Finance> listExpense = new List<Finance>();
        private int idSelect = 1;
        private string filePath = "students.txt";
        public Form1()
        {
            InitializeComponent();
            loadData();
            refreshTable();
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
            switch (currentState)
            {
                case States.STUDENTS:
                    dataGridView1.DataSource = listStudent;
                    titleLabel.Text = "Список членов семьи";
                    break;
                case States.FINANCES:
                    dataGridView1.DataSource = listFinance;
                    titleLabel.Text = "Список доходов";
                    break;
                case States.EXPENSES:
                    dataGridView1.DataSource = listExpense;
                    titleLabel.Text = "Список расходов";
                    break;
                case States.PRODUCTS:
                    dataGridView1.DataSource = listProduct;
                    titleLabel.Text = "Список товаров";
                    break;
            }
            
        }

        private void buttonDel_Click(object semder, EventArgs e)
        {
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
        }


        private void buttonAdd_Click(object sender, EventArgs e)
        {
            switch (currentState)
            {
                case States.STUDENTS:
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
                    break;
                case States.PRODUCTS:
                    using (var formE = new ProductConstructor(null))
                    {
                        if (formE.ShowDialog() == DialogResult.OK)
                        {
                            Product newProduct = formE.CurrentProduct;
                            newProduct.Id = idSelect++;
                            listProduct.Add(newProduct);
                            refreshTable();
                        }
                    }
                    break;
                case States.FINANCES:
                    using (var formE = new FinanceConstructor(null, true, listProduct, listStudent))
                    {
                        if (formE.ShowDialog() == DialogResult.OK)
                        {
                            Finance result = formE.CurrentFinance;
                            result.Id = idSelect++;
                            listFinance.Add(result);
                            refreshTable();
                        }
                    }
                    break;
                case States.EXPENSES:
                    using (var formE = new FinanceConstructor(null, false, listProduct, listStudent))
                    {
                        if (formE.ShowDialog() == DialogResult.OK)
                        {
                            Finance result = formE.CurrentFinance;
                            result.Id = idSelect++;
                            listExpense.Add(result);
                            refreshTable();
                        }
                    }
                    break;
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Вы не выбрали ни одного студанта!", "Не выбрали", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    $"{line.DayOfBirthd:yyyy-MM-dd}|{line.Telephone}|{line.FamilyMemberSum}");
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
                        Telephone = parts[4],
                        FamilyMemberSum = parts[5]
                    });
                    idSelect = Convert.ToInt32(parts[0] + 1);
                }
            }
            refreshTable();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            listStudent.Clear();
            MessageBox.Show("Данные очищены!", "Данные сохранены!", MessageBoxButtons.OK);
            refreshTable();
        }

        private void buttonFamilyMembers_Click(object sender, EventArgs e)
        {
            currentState = States.STUDENTS;
            refreshTable();
        }

        private void buttonExpenses_Click(object sender, EventArgs e)
        {
            currentState = States.EXPENSES;
            refreshTable();
        }

        private void buttonFinances_Click(object sender, EventArgs e)
        {
            currentState = States.FINANCES;
            refreshTable();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            currentState = States.PRODUCTS;
            refreshTable();
        }
    }
}
