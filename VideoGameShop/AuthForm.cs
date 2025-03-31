using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
namespace VideoGameShop
{
    public partial class AuthForm : Form
    {
        bool isPasswordVisible = false;
        public static string connectionString = "server=10.207.106.12;database=db44;user=user44;password=sc96";
        //private string connectionString = "server=127.0.0.1;database=db44;user=root;password=root";
        public AuthForm()
        {
            InitializeComponent();
            userPassword.UseSystemPasswordChar = true;
            pictureBox2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = userLogin.Text;
            string password = userPassword.Text;
            if (!checkFields())
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                User activeUser = Connection.GetUser(username, password);
                if (activeUser != null)
                {
                    AppData.ActiveUser = activeUser;
                    MessageBox.Show($"Добро пожаловать!\n{username}", "Успешная авторизация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    MainForm frm = new MainForm();
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Неверно введеный логин и пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Если больше нигде не используется - удали эту функцию
        private bool AuthenticateUser(string username, string password)
        {
            string storedHash = string.Empty;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT password FROM user WHERE username = @username";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            storedHash = reader.GetString(0);
                        }
                    }
                }
            }
            // Хэшируем введенный пароль
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                string hash = BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
                return hash == storedHash;
            }
        }
        private bool checkFields()
        {
            return userLogin.Text != "" && userPassword.Text != "";
        }
        private void AuthForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            std.appExit(e);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = true;
            if (isPasswordVisible) { isPasswordVisible = false; goto m1; }
            if (!isPasswordVisible) { isPasswordVisible = true; goto m1; }

            m1: 
            userPassword.UseSystemPasswordChar = isPasswordVisible;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
            if (isPasswordVisible) { isPasswordVisible = false; goto m1; }
            if (!isPasswordVisible) { isPasswordVisible = true; goto m1; }

        m1:
            userPassword.UseSystemPasswordChar = isPasswordVisible;
        }
    }
}
