using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoGameShop
{
    public partial class ClientShowForm : Form
    {
        public ClientShowForm(string column1, string column2, string column3)
        {
            InitializeComponent();
            fioText.Text = column1;
            numberText.Text = column2;
            adressText.Text = column3;
        }

        private void ClientShowForm_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
