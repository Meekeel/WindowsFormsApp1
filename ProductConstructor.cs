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
    public partial class ProductConstructor : Form
    {
        public Product CurrentProduct { get; private set; }

        private void fillForm()
        {
            textBoxName.Text = CurrentProduct.Name;
            categoryBox.Text = CurrentProduct.Category;
            priceBox.Text = Convert.ToString(CurrentProduct.Price);
        }

        public ProductConstructor(Product selectedStudent)
        {
            InitializeComponent();
            if (selectedStudent == null)
            {
                CurrentProduct = new Product();
            }
            else
            {
                CurrentProduct = selectedStudent;
                fillForm();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            CurrentProduct.Name = textBoxName.Text;
            CurrentProduct.Category = categoryBox.Text;
            CurrentProduct.Price = Convert.ToInt32(priceBox.Text);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
