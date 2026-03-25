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
using System.Collections;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public enum States { STUDENTS=0, PRODUCTS, FINANCES, EXPENSES };
        public States currentState = States.STUDENTS;

        SortableBindingList<Product> listProduct = new SortableBindingList<Product>();
        SortableBindingList<Student> listStudent = new SortableBindingList<Student>();
        SortableBindingList<Finance> listFinance = new SortableBindingList<Finance>();
        SortableBindingList<Finance> listExpense = new SortableBindingList<Finance>();
        
        private int idSelect = 1;
        private string filePath = "saved_data.txt";

        private ListSortDirection previousSortType = ListSortDirection.Descending;

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

            long totalSum = 0;

            foreach (Student student in listStudent)
            {
                totalSum += Convert.ToInt64(student.FamilyMemberSum);
            }
            foreach (Finance finance in listFinance)
            {
                totalSum += finance.Amount;
            }
            foreach (Finance expense in listExpense)
            {
                totalSum -= expense.Amount;
            }

            familySumLabel.Text = "Сумма: " + string.Format("{0:C2}", totalSum);
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
                        MessageBox.Show("Вы не выбрали ни один доход"
                            , "Не выбран доход",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                            );
                        return;
                    }
                    Finance selectedFinance = (Finance)dataGridView1.SelectedRows[0].DataBoundItem;
                    if (MessageBox.Show("Вы уверены, что хотите удалить этот доход?", "Точно удалить?",
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
                        MessageBox.Show("Вы не выбрали ни один расход"
                            , "Не выбран расход",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                            );
                        return;
                    }
                    Finance selectedExpense = (Finance)dataGridView1.SelectedRows[0].DataBoundItem;
                    if (MessageBox.Show("Вы уверены, что хотите удалить этот расход?", "Точно удалить?",
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
                        MessageBox.Show("Вы не выбрали ни одного продукта"
                            , "Не выбран продукт",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                            );
                        return;
                    }
                    Product selectedProduct = (Product)dataGridView1.SelectedRows[0].DataBoundItem;
                    if (MessageBox.Show("Вы уверены, что хотите удалить этот продукт?", "Точно удалить?",
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
                        }
                    }
                    break;
            }
            refreshTable();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            switch (currentState)
            {
                case States.STUDENTS:
                    if (dataGridView1.SelectedRows.Count <= 0)
                    {
                        MessageBox.Show("Вы не выбрали ни одного студента!", "Не выбрали", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    break;

                case States.PRODUCTS:
                    if (dataGridView1.SelectedRows.Count <= 0)
                    {
                        MessageBox.Show("Вы не выбрали ни одного продукта!", "Не выбрали", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    Product selectedProduct = (Product)dataGridView1.SelectedRows[0].DataBoundItem;
                    using (var formE = new ProductConstructor(selectedProduct))
                    {
                        if (formE.ShowDialog() == DialogResult.OK)
                        {
                            refreshTable();
                        }
                    }
                    break;

                case States.EXPENSES:
                    if (dataGridView1.SelectedRows.Count <= 0)
                    {
                        MessageBox.Show("Вы не выбрали ни один расход!", "Не выбрали", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    Finance selectedExpense = (Finance)dataGridView1.SelectedRows[0].DataBoundItem;
                    using (var formE = new FinanceConstructor(selectedExpense, true, listProduct, listStudent))
                    {
                        if (formE.ShowDialog() == DialogResult.OK)
                        {
                            refreshTable();
                        }
                    }
                    break;

                case States.FINANCES:
                    if (dataGridView1.SelectedRows.Count <= 0)
                    {
                        MessageBox.Show("Вы не выбрали ни один доход!", "Не выбрали", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    Finance selectedFinance = (Finance)dataGridView1.SelectedRows[0].DataBoundItem;
                    using (var formE = new FinanceConstructor(selectedFinance, true, listProduct, listStudent))
                    {
                        if (formE.ShowDialog() == DialogResult.OK)
                        {
                            refreshTable();
                        }
                    }
                    break;
            }

        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            List<string> lineData = new List<string>();
            foreach (var line in listStudent)
            {
                lineData.Add(
                    $"S|{line.Id}|{line.Name}|{line.Name2}|" +
                    $"{line.DayOfBirthd:yyyy-MM-dd}|{line.Telephone}|{line.GroupNumber}|{line.FamilyMemberSum}");
            }

            foreach (var line in listProduct)
            {
                lineData.Add($"P|{line.Id}|{line.Name}|{line.Price}|{line.Category}");
            }

            foreach (var line in listFinance)
            {
                lineData.Add($"F|{line.Id}|{line.product}|{line.Owner}|{line.Amount}");
            }

            foreach (var line in listExpense)
            {
                lineData.Add($"E|{line.Id}|{line.product}|{line.Owner}|{line.Amount}");
            }

            File.Delete(filePath);
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
            listProduct.Clear();
            listExpense.Clear();
            listFinance.Clear();
            idSelect = 1;

            foreach (string line in File.ReadAllLines(filePath))
            {
                string[] parts = line.Split('|');
                if (parts.Length >= 1)
                {
                    char field = parts[0][0];
                    switch (field)
                    {
                        case 'S':
                            listStudent.Add(new Student()
                            {
                                Id = Convert.ToInt32(parts[1]),
                                Name = parts[2],
                                Name2 = parts[3],
                                DayOfBirthd = Convert.ToDateTime(parts[4]),
                                Telephone = parts[5],
                                GroupNumber = parts[6],
                                FamilyMemberSum = parts[7]
                            });
                            break;
                        case 'P':
                            listProduct.Add(new Product()
                            {
                                Id = Convert.ToInt32(parts[1]),
                                Name = parts[2],
                                Price = Convert.ToInt32(parts[3]),
                                Category = parts[4]
                            });
                            break;
                        case 'E':
                            listExpense.Add(new Finance()
                            {
                                Id= Convert.ToInt32(parts[1]),
                                product = parts[2],
                                Owner = parts[3],
                                Amount = Convert.ToInt32(parts[4])
                            });
                            break;
                        case 'F':
                            listFinance.Add(new Finance()
                            {
                                Id = Convert.ToInt32(parts[1]),
                                product = parts[2],
                                Owner = parts[3],
                                Amount = Convert.ToInt32(parts[4])
                            });
                            break;
                        default:
                            break;
                    }
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            previousSortType = (previousSortType == ListSortDirection.Descending) ? ListSortDirection.Ascending : ListSortDirection.Descending;
            dataGridView1.Sort(dataGridView1.Columns[e.ColumnIndex], previousSortType);
            refreshTable();
        }
    }
}
