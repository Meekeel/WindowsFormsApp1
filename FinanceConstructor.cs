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
    public partial class FinanceConstructor : Form
    {
        public Finance CurrentFinance { get; private set; }
        private SortableBindingList<Product> products { get; }
        private bool isFinance { get; }

        private void fillForm()
        {
            productComboBox.Text = CurrentFinance.Owner;
            customerComboBox.Text = CurrentFinance.product;
        }

        public FinanceConstructor(Finance parent, bool isFinance, SortableBindingList<Product> products, SortableBindingList<Student> students)
        {
            InitializeComponent();

            this.isFinance = isFinance;
            this.products = products;

            if (parent == null)
            {
                CurrentFinance = new Finance();
            }
            else
            {
                CurrentFinance = parent;
                fillForm();
            }

            foreach (Product product in products)
            {
                productComboBox.Items.Add(product.Name);
            }
            if (products != null && products.Count > 0)
            {
                productComboBox.SelectedIndex = 0;
            }

            foreach (Student student in students)
            {
                customerComboBox.Items.Add(student.Name);
            }
            if (students != null && students.Count > 0)
            {
                customerComboBox.SelectedIndex = 0;
            }
            
            titleLabel.Text = isFinance ? "Добавление дохода" : "Добавление расхода";
            label3.Text = isFinance ? "Продавец" : "Покупатель";
        }

        private void FinanceConstructor_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!customerComboBox.Items.Contains(customerComboBox.Text))
            {
                MessageBox.Show("Укажите существующего " + (isFinance ? "продавца" : "покупателя"));
                return;
            }

            if (!productComboBox.Items.Contains(productComboBox.Text))
            {
                MessageBox.Show("Укажите существующий продукт");
                return;
            }

            CurrentFinance.Owner = customerComboBox.Text;
            CurrentFinance.product = productComboBox.Text;

            Product product = products.First();
            foreach (Product p in products)
            {
                if (p.Name == productComboBox.Text)
                {
                    product = p;
                }
            }

            CurrentFinance.Amount = Convert.ToInt32(amountNumberic.Value) * product.Price;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
