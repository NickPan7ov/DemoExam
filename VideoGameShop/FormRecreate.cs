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
namespace VideoGameShop
{
    public partial class FormRecreate : Form
    {
        public FormRecreate()
        {
            InitializeComponent();
        }
        private static string connectionString = "server=127.0.0.1;user=root;password=root";
        private static string database = "db44";
        //public static string connectionString = "server=10.207.106.12;database=db44;user=user44;password=sc96";
        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                CreateDatabase();
                CreateTableUsers();
                CreateTableClient();
                CreateTableCategory();
                CreateTableProvider();
                CreateTableRole();
                CreateTableVideogame();
                MessageBox.Show("База данных успешно востановленна!", "Восстановление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"{ex}");
            }
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
        private void CreateDatabase()
        {
            using(MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = $"CREATE DATABASE IF NOT EXISTS `{database}`";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        private void CreateTableUsers()
        {
            string connectionTableUsers = $"{connectionString};Database={database}";
            using (MySqlConnection connection = new MySqlConnection(connectionTableUsers))
            {
                connection.Open();
                string query = $"CREATE TABLE IF NOT EXISTS `user`(`iduser` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,`username` varchar(100) DEFAULT NULL,`password` varchar(100) DEFAULT NULL,`fio` varchar(100) DEFAULT NULL,`role` varchar(100) DEFAULT NULL) ";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        private void CreateTableClient()
        {
            string connectionTableUsers = $"{connectionString};Database={database}";
            using (MySqlConnection connection = new MySqlConnection(connectionTableUsers))
            {
                connection.Open();
                string query = $"CREATE TABLE IF NOT EXISTS `client`(`id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,`fio` varchar(100) DEFAULT NULL,`number` varchar(100) DEFAULT NULL,`adress` varchar(100) DEFAULT NULL) ";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        private void CreateTableCategory()
        {
            string connectionTableUsers = $"{connectionString};Database={database}";
            using (MySqlConnection connection = new MySqlConnection(connectionTableUsers))
            {
                connection.Open();
                string query = $"CREATE TABLE IF NOT EXISTS `category`(`idcategory` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,`name` varchar(100) DEFAULT NULL) ";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        private void CreateTableProvider()
        {
            string connectionTableUsers = $"{connectionString};Database={database}";
            using (MySqlConnection connection = new MySqlConnection(connectionTableUsers))
            {
                connection.Open();
                string query = $"CREATE TABLE IF NOT EXISTS `provider`(`id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,`providername` varchar(100) DEFAULT NULL) ";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        private void CreateTableRole()
        {
            string connectionTableUsers = $"{connectionString};Database={database}";
            using (MySqlConnection connection = new MySqlConnection(connectionTableUsers))
            {
                connection.Open();
                string query = $"CREATE TABLE IF NOT EXISTS `role`(`id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,`rolename` varchar(100) DEFAULT NULL) ";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        private void CreateTableVideogame()
        {
            string connectionTableUsers = $"{connectionString};Database={database}";
            using (MySqlConnection connection = new MySqlConnection(connectionTableUsers))
            {
                connection.Open();
                string query = $"CREATE TABLE IF NOT EXISTS `videogame`(`idvideogame` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,`name` varchar(255) DEFAULT NULL,`description` varchar(255) DEFAULT NULL,`category` varchar(255) DEFAULT NULL,`picture` longblob,`cost` int DEFAULT NULL,`videogamequantity` int DEFAULT NULL,`provider` varchar(255) DEFAULT NULL)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
