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
using MySql.Data.MySqlClient;
namespace VideoGameShop
{
    public partial class ClientForm : Form
    {
        // private static string connectionString = "server=127.0.0.1;database=db44;user=root;password=root";
        public static string connectionString = "server=10.207.106.12;database=db44;user=user44;password=sc96";
        public ClientForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Вы уверены что хотите выйти?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (res == DialogResult.Yes)
            {
                this.Hide();
                MainForm frm = new MainForm();
                frm.ShowDialog();
            }
            else
            {

            }
        }
        private void ClientFormClosing_FormClosing(object sender, FormClosingEventArgs e)
        {
            std.appExit(e);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        public static List<Client> search(string name = "")
        {
            string select = $"SELECT * FROM `client`";
            string _search = $"WHERE `fio` LIKE '%{name}%'";

        string sql = select;

            bool name_not_empty = name != null && !name.Equals(string.Empty);

            if (name_not_empty) sql += $" {_search}";


            List<Client> clients = new List<Client>();

            using (var con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sql, con);

                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        var client = new Client();
                        client.fio = reader.GetString(reader.GetOrdinal("fio"));
                        client.number = reader.GetString(reader.GetOrdinal("number"));
                        client.adress = reader.GetString(reader.GetOrdinal("adress"));
                        clients.Add(client);
                    }
            }
            return clients;

        }
        private void ClientForm_Load(object sender, EventArgs e)
        {
            createColumns();
            updateRows();
        }
        private void createColumns()
        {
            // picture | name | category | price

            var fio = new DataGridViewTextBoxColumn();
            fio.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            fio.HeaderText = "Фио клиента";
            var number = new DataGridViewTextBoxColumn();
            number.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            number.HeaderText = "Номер телефона";
            var adress = new DataGridViewTextBoxColumn();
            adress.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            adress.HeaderText = "Адресс";

            dataGridView1.Columns.Add(fio);
            dataGridView1.Columns.Add(number);
            dataGridView1.Columns.Add(adress);
        }
        private void updateRows()
        {
            dataGridView1.RowTemplate.Height = 100;
            dataGridView1.Rows.Clear();
            List<Client> Client = search();

            foreach (Client Clients in Client)
            {
                dataGridView1.Rows.Add(
                    Clients.fio,
                    Clients.number,
                    Clients.adress);
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.Value != null)
            {
                string phoneNumber = e.Value.ToString();
                if (!string.IsNullOrEmpty(phoneNumber))
                {
                    string maskedNumber = phoneNumber[0] + new string('*', phoneNumber.Length - 1);
                    e.Value = maskedNumber;
                    e.FormattingApplied = true;
                }
            }
        }
    }
}
