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
using System.IO;
namespace VideoGameShop
{
    public partial class FormImportCSV : Form
    {
        public FormImportCSV()
        {
            InitializeComponent();
        }
        //public static string connectionString = "server=10.207.106.12;database=db44;user=user44;password=sc96";
        private string connectionString = "server=127.0.0.1;database=db44;user=root;password=root";
        private void AddButton_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Таблица не была выбрана!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("CSV файл не был выбран!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string selectedtable = comboBox1.Text;
            string filePath = textBox1.Text;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using(var transaction = connection.BeginTransaction())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.Transaction = transaction;
                        if (!File.Exists(filePath))
                        {
                            MessageBox.Show("Путь файла не корректен!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            using (var reader = new StreamReader(filePath))
                            {
                                string line;
                                int lineNumber = 0;
                                while ((line = reader.ReadLine()) != null)
                                {
                                    if (lineNumber == 0)
                                    {
                                        lineNumber++;
                                        continue;
                                    }
                                    var values = line.Split(',');
                                    if (selectedtable == "category")
                                    {
                                        command.CommandText = $"INSERT INTO category (idcategory, name) VALUES (@id, @name)";
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("@id", values[0]);
                                        command.Parameters.AddWithValue("@name", values[1]);
                                        command.ExecuteNonQuery();
                                        lineNumber++;
                                    }
                                    if (selectedtable == "provider")
                                    {
                                        command.CommandText = $"INSERT INTO provider (id, providername) VALUES (@id, @name)";
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("@id", values[0]);
                                        command.Parameters.AddWithValue("@name", values[1]);
                                        command.ExecuteNonQuery();
                                        lineNumber++;
                                    }
                                    if (selectedtable == "role")
                                    {
                                        command.CommandText = $"INSERT INTO role (id, rolename) VALUES (@id, @name)";
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("@id", values[0]);
                                        command.Parameters.AddWithValue("@name", values[1]);
                                        command.ExecuteNonQuery();
                                        lineNumber++;
                                    }
                                    if (selectedtable == "user")
                                    {
                                        command.CommandText = $"INSERT INTO user (iduser, username, password, fio, role) VALUES (@id, @name, @password, @fio, @role)";
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("@id", values[0]);
                                        command.Parameters.AddWithValue("@name", values[1]);
                                        command.Parameters.AddWithValue("@password", values[2]);
                                        command.Parameters.AddWithValue("@fio", values[3]);
                                        command.Parameters.AddWithValue("@role", values[4]);
                                        command.ExecuteNonQuery();
                                        lineNumber++;
                                    }
                                    if (selectedtable == "videogame")
                                    {
                                        command.CommandText = $"INSERT INTO videogame (idvideogame, name, description, category, cost, videogamequantity, provider) VALUES (@id, @name, @description, @category, @cost, @quantity, @provider)";
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("@id", values[0]);
                                        command.Parameters.AddWithValue("@name", values[1]);
                                        command.Parameters.AddWithValue("@description", values[2]);
                                        command.Parameters.AddWithValue("@category", values[3]);
                                        command.Parameters.AddWithValue("@cost", values[5]);
                                        command.Parameters.AddWithValue("@quantity", values[6]);
                                        command.Parameters.AddWithValue("@provider", values[7]);
                                        command.ExecuteNonQuery();
                                        lineNumber++;
                                    }
                                    if (selectedtable == "client")
                                    {
                                        command.CommandText = $"INSERT INTO client (id, fio, number, adress) VALUES (@id, @fio, @number, @adress)";
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("@id", values[0]);
                                        command.Parameters.AddWithValue("@fio", values[1]);
                                        command.Parameters.AddWithValue("@number", values[2]);
                                        command.Parameters.AddWithValue("@adress", values[3]);
                                        command.ExecuteNonQuery();
                                        lineNumber++;
                                    }
                                }
                                MessageBox.Show("Операция завершена", "Импорт", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    transaction.Commit();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV files (*.csv) |*.csv";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    textBox1.Text = filePath;
                    MessageBox.Show($"Выбран файл: {filePath}", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void FormImportCSV_Load(object sender, EventArgs e)
        {

        }
    }
}
