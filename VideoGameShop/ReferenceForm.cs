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
    public partial class ReferenceForm : Form
    {
        private string name = "";
        private string filter = "";
        private string sort = "";
        private bool sort_reversed = false;
        private int limit = -1;
        private int offset = -1;
        public ReferenceForm()
        {
            InitializeComponent();
        }

        //private static string connectionString = "server=127.0.0.1;database=db44;user=root;password=root";
        public static string connectionString = "server=10.207.106.12;database=db44;user=user44;password=sc96";
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
        private void createColumns()
        {
            var provider = new DataGridViewTextBoxColumn();
            provider.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            provider.HeaderText = "Поставщик";
            dataGridView1.Columns.Add(provider);
        }
        private void createColumns2()
        {
            var name = new DataGridViewTextBoxColumn();
            name.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            name.HeaderText = "Жанр";
            dataGridView2.Columns.Add(name);
        }
        private void createColumns3()
        {
            var role = new DataGridViewTextBoxColumn();
            role.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            role.HeaderText = "Роль";
            dataGridView3.Columns.Add(role);
        }

        private void OtchetForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            std.appExit(e);
        }
        public static List<Provider> GetProviders()
        {
            var providers = new List<Provider>();
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
        public static List<Category> GetCategoryes()
        {
            var categories = new List<Category>();
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
        public static List<Role> GetRoles()
        {
            var roles = new List<Role>();
            using (var con = new MySqlConnection(connectionString))
            {
                con.Open();

                string sql = "SELECT * FROM role";
                var cmd = new MySqlCommand(sql, con);
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        var role = new Role
                        {
                            Id = reader.GetInt32("id"),
                            namerole = reader.GetString("rolename")
                        };

                        roles.Add(role);
                    }
            }
            return roles;
        }

        private void ReferenceForm_Load(object sender, EventArgs e)
        {
            createColumns();
            updateRows();
            createColumns2();
            updateRows2();
            createColumns3();
            updateRows3();
        }
        public static List<Category> search2(string name = "", string filter = "", string sort = "", int limit = -1, int offset = -1, bool reverse_sort = false)
        {
            string select = $"SELECT * FROM `category`";
            string _search = $"WHERE `name` LIKE '%{name}%'";
            string _filter01 = $"AND `fio`={filter}";
            string _filter02 = $"WHERE `role`={filter}";
            string _sort = $"ORDER BY {sort}";
            string _limit = $"LIMIT {limit}";
            string _offset = $"OFFSET {offset}";

            string sql = select;

            bool name_not_empty = name != null && !name.Equals(string.Empty);
            bool filter_not_empty = filter != null && !filter.Equals(string.Empty);
            bool sort_not_empty = sort != null && !sort.Equals(string.Empty);

            if (name_not_empty) sql += $" {_search}";

            if (name_not_empty && filter_not_empty) sql += $" {_filter01}";
            else if (filter_not_empty) sql += $" {_filter02}";

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
        public static List<Provider> search(string name = "", string filter = "", string sort = "", int limit = -1, int offset = -1, bool reverse_sort = false)
        {
            string select = $"SELECT * FROM `provider`";
            string _search = $"WHERE `providername` LIKE '%{name}%'";
            string _filter01 = $"AND `fio`={filter}";
            string _filter02 = $"WHERE `role`={filter}";
            string _sort = $"ORDER BY {sort}";
            string _limit = $"LIMIT {limit}";
            string _offset = $"OFFSET {offset}";

            string sql = select;

            bool name_not_empty = name != null && !name.Equals(string.Empty);
            bool filter_not_empty = filter != null && !filter.Equals(string.Empty);
            bool sort_not_empty = sort != null && !sort.Equals(string.Empty);

            if (name_not_empty) sql += $" {_search}";

            if (name_not_empty && filter_not_empty) sql += $" {_filter01}";
            else if (filter_not_empty) sql += $" {_filter02}";

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
        public static List<Role> search3(string name = "", string filter = "", string sort = "", int limit = -1, int offset = -1, bool reverse_sort = false)
        {
            string select = $"SELECT * FROM `role`";
            string _search = $"WHERE `rolename` LIKE '%{name}%'";
            string _filter01 = $"AND `fio`={filter}";
            string _filter02 = $"WHERE `role`={filter}";
            string _sort = $"ORDER BY {sort}";
            string _limit = $"LIMIT {limit}";
            string _offset = $"OFFSET {offset}";

            string sql = select;

            bool name_not_empty = name != null && !name.Equals(string.Empty);
            bool filter_not_empty = filter != null && !filter.Equals(string.Empty);
            bool sort_not_empty = sort != null && !sort.Equals(string.Empty);

            if (name_not_empty) sql += $" {_search}";

            if (name_not_empty && filter_not_empty) sql += $" {_filter01}";
            else if (filter_not_empty) sql += $" {_filter02}";

            if (sort_not_empty && reverse_sort) sql += $" {_sort} DESC";
            else if (sort_not_empty) sql += $" {_sort}";

            if (limit != -1) sql += $" {_limit}";
            if (offset != -1) sql += $" {_offset}";

            List<Role> roles = new List<Role>();

            using (var con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sql, con);

                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        var role = new Role();
                        role.Id = reader.GetInt32(reader.GetOrdinal("id"));
                        role.namerole = reader.GetString(reader.GetOrdinal("rolename"));
                        roles.Add(role);
                    }
            }
            return roles;
        }
        private void updateRows()
        {
            dataGridView1.RowTemplate.Height = 100;
            dataGridView1.Rows.Clear();
            List<Provider> providers = search(name, filter, sort, limit, offset, sort_reversed);
            foreach (Provider provider in providers)
            {
                dataGridView1.Rows.Add(
                    provider.providername
                    );
            }
        }
        private void updateRows2()
        {
            dataGridView2.RowTemplate.Height = 100;
            dataGridView2.Rows.Clear();
            List<Category> categories = search2(name, filter, sort, limit, offset, sort_reversed);
            foreach (Category category in categories)
            {
                dataGridView2.Rows.Add(
                    category.Name
                    );
            }
        }
        private void updateRows3()
        {
            dataGridView3.RowTemplate.Height = 100;
            dataGridView3.Rows.Clear();
            List<Role> roles = search3(name, filter, sort, limit, offset, sort_reversed);
            foreach (Role role in roles)
            {
                dataGridView3.Rows.Add(
                    role.namerole
                    );
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string textBox2Value = textBox2.Text;

                    string query = "INSERT INTO provider (providername) VALUES (@value2)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@value2", textBox2Value);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show("Запись добавлена", "Успешное добавление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            updateRows();
        }

        private int GetProviderId(string name) 
        {
            using (var con = new MySqlConnection(connectionString))
            {
                con.Open();

                string sql = $"SELECT id FROM provider WHERE providername=@providername";

                var cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("providername", name);
                return (int)cmd.ExecuteScalar();
            }
        }
        private int GetCategoryId(string name)
        {
            using (var con = new MySqlConnection(connectionString))
            {
                con.Open();

                string sql = $"SELECT idcategory FROM category WHERE name=@name";

                var cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("name", name);
                return (int)cmd.ExecuteScalar();
            }
        }
        private int GetRoleId(string name)
        {
            using (var con = new MySqlConnection(connectionString))
            {
                con.Open();

                string sql = $"SELECT id FROM role WHERE rolename=@rolename";

                var cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("rolename", name);
                return (int)cmd.ExecuteScalar();
            }
        }


        // кнопка редактирования
        private void RedButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            int id = GetProviderId(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            string newValue = textBox2.Text;
            UpdateRecord(id, newValue);
            updateRows();
        }
        private void UpdateRecord(int id, string newValue)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE provider SET providername = @newValue WHERE id = @id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@newValue", newValue);
                    command.Parameters.AddWithValue("@id", id);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show($"{rowsAffected} запись обновлена.", "Успешное обновление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void AddButton2_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string textBox2Value = textBox4.Text;

                    string query = "INSERT INTO category (name) VALUES (@value2)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@value2", textBox2Value);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show("Запись добавлена", "Успешное добавление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}","Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RedButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            int id = GetCategoryId(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
            string newValue = textBox4.Text;
            UpdateRecord2(id, newValue);
            updateRows2();
        }
        private void UpdateRecord2(int id, string newValue)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE category SET name = @newValue WHERE idcategory = @id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@newValue", newValue);
                    command.Parameters.AddWithValue("@id", id);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show($"{rowsAffected} запись обновлена.", "Успешное обновление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void AddButton3_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string textBox2Value = textBox6.Text;

                    string query = "INSERT INTO role (rolename) VALUES (@value2)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@value2", textBox2Value);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show("Запись добавлена", "Успешное добавление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RedButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            int id = GetRoleId(dataGridView3.SelectedRows[0].Cells[0].Value.ToString());
            string newValue = textBox6.Text;
            UpdateRecord3(id, newValue);
            updateRows3();
        }
        private void UpdateRecord3(int id, string newValue)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE role SET rolename = @newValue WHERE id = @id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@newValue", newValue);
                    command.Parameters.AddWithValue("@id", id);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show($"{rowsAffected} запись обновлена.", "Успешное обновление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
