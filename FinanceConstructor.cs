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

        private void fillForm()
        {
            productComboBox.Text = CurrentFinance.Owner;
            customerComboBox.Text = CurrentFinance.product;
        }

        public FinanceConstructor(Finance parent, bool isFinance, List<Product> products, List<Student> students)
        {
            InitializeComponent();
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
        }

        private void FinanceConstructor_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            CurrentFinance.Owner = customerComboBox.Text;
            CurrentFinance.product = productComboBox.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
