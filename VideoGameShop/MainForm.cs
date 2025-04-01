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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Вы уверены что хотите выйти?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (res == DialogResult.Yes)
            {
                this.Hide();
                AuthForm frm = new AuthForm();
                frm.ShowDialog();
            }
            else
            {
                
            }
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            std.appExit(e);
        }
        //просмотр товаров Менеджер Пользователь
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            ProductForm frm = new ProductForm();
            frm.ShowDialog();
        }
        //просмотр спправочников менеджер
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            ReferenceForm frm = new ReferenceForm();
            frm.ShowDialog();
        }
        //просмотр пользователей Администратор
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            UsersForm frm = new UsersForm();
            frm.ShowDialog();
        }
        //просмотр клиентов Продавец Администратор
        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            ClientForm frm = new ClientForm();
            frm.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if(AppData.ActiveUser == null)
            {
                label3.Text = "admin";
                return;
            }

            label3.Text = AppData.ActiveUser.fio;
            if (AppData.ActiveUser.roleid == User.ADMIN) 
            {
                // this.Size = new Size();
                button2.Visible = false;
                button3.Visible = false;
            }
            if (AppData.ActiveUser.roleid == User.MANAGER)
            {
                button4.Visible = false;
                button5.Visible = false;
            }
            if (AppData.ActiveUser.roleid == User.SELLER)
            {
                button3.Visible = false;
                button4.Visible = false;
            }
        }
        //переименование текста
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormRecreate frm = new FormRecreate();
            frm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormImportCSV frm = new FormImportCSV();
            frm.ShowDialog();
        }
    }
}
