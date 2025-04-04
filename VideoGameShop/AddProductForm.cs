using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace VideoGameShop
{
    public partial class AddProductForm : Form
    {
        private string name = ""; 
        private string filter = "";
        private string sort = "";
        private bool sort_reversed = false;
        private int limit = -1;
        private int offset = -1;

        private static List<Category> categories = new List<Category>(); //aka genres
        private static List<Provider> providers = new List<Provider>();

        public AddProductForm()
        {
            InitializeComponent();

        }
        public static string connectionString = "server=10.207.106.12;database=db44;user=user44;password=sc96";
        //private static string connectionString = "server=127.0.0.1;database=db44;user=root;password=root";

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Вы уверены что хотите выйти?\nВсе несохраненные изменения будут удалены!", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (res == DialogResult.Yes)
            {
                this.Hide();
            }
            else
            {
                
            }
        }
        private void AddProductForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           Hide();
           // new ProductForm().ShowDialog();
        }
        private void updateRows2()
        {
            List<Provider> providers = search2(name, sort, limit, offset, sort_reversed);
        }
        public static List<Provider> search2(string name = "", string sort = "", int limit = -1, int offset = -1, bool reverse_sort = false)
        {
            string select = $"SELECT * FROM `provider`";
            string _search = $"WHERE `providername` LIKE '%{name}%'";
            string _sort = $"ORDER BY {sort}";
            string _limit = $"LIMIT {limit}";
            string _offset = $"OFFSET {offset}";

            string sql = select;

            bool name_not_empty = name != null && !name.Equals(string.Empty);
            bool sort_not_empty = sort != null && !sort.Equals(string.Empty);

            if (name_not_empty) sql += $" {_search}";

            if (sort_not_empty && reverse_sort) sql += $" {_sort} DESC";
            else if (sort_not_empty) sql += $" {_sort}";

            if (limit != -1) sql += $" {_limit}";
            if (offset != -1) sql += $" {_offset}";

            List<Provider> providers = new List<Provider>();

            using (var con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sql, con);

                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        var provider = new Provider();
                        provider.Id = reader.GetInt32(reader.GetOrdinal("id"));
                        provider.providername = reader.GetString(reader.GetOrdinal("providername"));
                        providers.Add(provider);
                    }
            }
            return providers;
        }
        public static List<Category> search(string name = "", string sort = "", int limit = -1, int offset = -1, bool reverse_sort = false)
        {
            string select = $"SELECT * FROM `category`";
            string _search = $"WHERE `name` LIKE '%{name}%'";
            string _sort = $"ORDER BY {sort}";
            string _limit = $"LIMIT {limit}";
            string _offset = $"OFFSET {offset}";

            string sql = select;

            bool name_not_empty = name != null && !name.Equals(string.Empty);
            bool sort_not_empty = sort != null && !sort.Equals(string.Empty);

            if (name_not_empty) sql += $" {_search}";

            if (sort_not_empty && reverse_sort) sql += $" {_sort} DESC";
            else if (sort_not_empty) sql += $" {_sort}";

            if (limit != -1) sql += $" {_limit}";
            if (offset != -1) sql += $" {_offset}";

            List<Category> categories = new List<Category>();

            using (var con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sql, con);

                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        var category = new Category();
                        category.Id = reader.GetInt32(reader.GetOrdinal("idcategory"));
                        category.Name = reader.GetString(reader.GetOrdinal("name"));
                        categories.Add(category);
                    }
            }
            return categories;
        }
        private void updateRows()
        {
            List<Category> categories = search(name, sort, limit, offset, sort_reversed);
        }
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0) return;
            if (comboBox1.SelectedIndex == 0) { filter = ""; updateRows(); return; }

            foreach (var category in GetCategories())
            {
                if (category.Name == comboBox1.SelectedItem.ToString())
                {
                    filter = category.Id.ToString();
                    break;
                }
            }
            updateRows();
        }
        public static List<Category> GetCategories()
        {
            //var categories = new List<Category>();
            using (var con = new MySqlConnection(connectionString))
            {
                con.Open();

                string sql = "SELECT * FROM category";
                var cmd = new MySqlCommand(sql, con);
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        var category = new Category
                        {
                            Id = reader.GetInt32("idcategory"),
                            Name = reader.GetString("name")
                        };

                        categories.Add(category);
                    }
            }
            return categories;
        }

        private void AddProductForm_Load(object sender, EventArgs e)
        {
            updateRows();
            var categories = GetCategories();
            comboBox1.Items.Add("");
            comboBox1.SelectedIndex = 0;
            foreach (var category in categories)
            comboBox1.Items.Add(category.Name);
            updateRows2();
            var providers = GetProviders();
            comboBox2.Items.Add("");
            comboBox2.SelectedIndex = 0;
            foreach (var provider in providers)
                comboBox2.Items.Add(provider.providername);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex < 0) return;
            if (comboBox2.SelectedIndex == 0) { filter = ""; updateRows2(); return; }

            foreach (var provider in GetProviders())
            {
                if (provider.providername == comboBox2.SelectedItem.ToString())
                {
                    filter = provider.Id.ToString();
                    break;
                }
            }
            updateRows2();
        }
        public static List<Provider> GetProviders()
        {
            //var providers = new List<Provider>();
            using (var con = new MySqlConnection(connectionString))
            {
                con.Open();

                string sql = "SELECT * FROM provider";
                var cmd = new MySqlCommand(sql, con);
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        var provider = new Provider
                        {
                            Id = reader.GetInt32("id"),
                            providername = reader.GetString("providername")
                        };

                        providers.Add(provider);
                    }
            }
            return providers;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private int GetCategoryId(string name) 
        {
            foreach (var genre in categories){
                if (genre.Name == name) return genre.Id;
            }
            return -1;
        }

        private int GetProviderId(string name){
            foreach(var provider in providers) {
                if (provider.providername == name) return provider.Id;
            }
            return -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string text1 = textBox4.Text;
            string text2 = textBox1.Text;
            string text3 = textBox2.Text;
            string text4 = textBox3.Text;
            string comboBox1Value = comboBox1.SelectedItem?.ToString();
            string comboBox2Value = comboBox2.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(text1) || string.IsNullOrWhiteSpace(text2) || string.IsNullOrWhiteSpace(text3) || string.IsNullOrWhiteSpace(text4) ||
                comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }
            string query = "INSERT INTO videogame (name, videogamequantity, cost, description, category, provider, picture) VALUES (@text1, @text2, @text3,@text4, @combo1, @combo2, @picture)";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@text1", text1);
                cmd.Parameters.AddWithValue("@text2", text2);
                cmd.Parameters.AddWithValue("@text3", text3);
                cmd.Parameters.AddWithValue("@text4", text4);

                int categoryId = GetCategoryId(comboBox1Value);
                if (categoryId == -1) 
                {
                    MessageBox.Show($"Ошибка неверная жанр", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int providerId = GetProviderId(comboBox2Value);
                if (providerId == -1)
                {
                    MessageBox.Show($"Ошибка неверный поставщик", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                byte[] imgBytes = convertToByte(pictureBox1.Image);
                cmd.Parameters.AddWithValue("@combo1", categoryId); // категория
                cmd.Parameters.AddWithValue("@combo2", providerId); // provider
                cmd.Parameters.AddWithValue("@picture", imgBytes);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Данные успешно добавленны в базу данных", "Успешное добавление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                   // ProductForm fm = new ProductForm();
                    //fm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private byte[] convertToByte(Image image){
            using (var ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           using (var fileDialog = new OpenFileDialog())
           {
                fileDialog.InitialDirectory = "C:\\";
                fileDialog.Filter = "Изображения (*.png)(*.jpg)|*.png;*.jpg";
                
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    var filepath = fileDialog.FileName;

                    pictureBox1.ImageLocation = filepath; 
                }
           }
           
        }
    }
}
