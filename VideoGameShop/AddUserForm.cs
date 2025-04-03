using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
namespace VideoGameShop
{
    public partial class AddUserForm : Form
    {
        public AddUserForm()
        {
            InitializeComponent();
        }
        //private static string connectionString = "server=127.0.0.1;database=db44;user=root;password=root";
        public static string connectionString = "server=10.207.106.12;database=db44;user=user44;password=sc96";
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Вы уверены что хотите выйти?\n Все несохраненные изменения будут удалены!", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (res == DialogResult.Yes)
            {
                this.Hide();
            }
            else
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var login = userLogin.Text;
            var fio = UserFIO.Text;
            var password = userPassword.Text;
            string elda = cmbRole.SelectedItem.ToString();
            Role role = Connection.GetRole(elda);
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(fio) || string.IsNullOrEmpty(password) || role == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }
            string hashedPassword = ComputeSha256Hash(password);

            InsertIntoDatabase(login, fio, hashedPassword, role.Id);
        }
        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        private void InsertIntoDatabase(string username, string fio, string password, int role)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO user (username, fio, password, role) VALUES (@username, @fio, @password, @role)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@fio", fio);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@role", role);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Данные успешно добавлены.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при добавлении данных: {ex.Message}");
                    }
                }
            }
        }

        private void AddUserForm_Load(object sender, EventArgs e)
        {
            //cmbRole
            List<Role> roles = Connection.GetRoles();
            foreach (Role role in roles)
            {
                cmbRole.Items.Add(role.namerole);
            }
            cmbRole.SelectedIndex = 1;
        }

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}