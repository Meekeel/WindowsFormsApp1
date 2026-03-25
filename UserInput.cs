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
    public partial class UserInput : Form
    {
        public string CurrentFilter { get; private set; }

        public UserInput(string title)
        {
            titleLabel.Text = title;
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            CurrentFilter = textBox.Text;
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
