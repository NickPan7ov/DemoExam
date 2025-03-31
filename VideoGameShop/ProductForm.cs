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
    public partial class ProductForm : Form
    {

        
        private string name = "";
        private string filter = "";
        private string sort = "";
        private bool sort_reversed = false;
        private int limit = -1;
        private int offset = -1;

        public List<CartItem> bucket = new List<CartItem>();

        public ProductForm()
        {
            InitializeComponent();
        }
        // private static string connectionString = "server=127.0.0.1;database=db44;user=root;password=root";
        public static string connectionString = "server=10.207.106.12;database=db44;user=user44;password=sc96";
        private Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(ms);
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
        private void ProductForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            std.appExit(e);
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
        //Добавить продукт менеджер.
        private void button2_Click(object sender, EventArgs e)
        {
            AddProductForm frm = new AddProductForm();
            frm.ShowDialog();
        }
        public static List<Videogame> search(string name = "", string filter = "", string sort = "", int limit = -1, int offset = -1, bool reverse_sort = false)
        {
            string select = $"SELECT * FROM `videogame`";
            string _search = $"WHERE `name` LIKE '%{name}%'";
            string _filter01 = $"AND `category`={filter}";
            string _filter02 = $"WHERE `category`={filter}";
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

            List<Videogame> videogames = new List<Videogame>();

            using (var con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sql, con);

                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        var videogame = new Videogame();
                        videogame.name = reader.GetString(reader.GetOrdinal("name"));
                        videogame.description = reader.GetString(reader.GetOrdinal("description"));
                        videogame.category = reader.GetInt32(reader.GetOrdinal("category"));
                        videogame.provider = reader.GetInt32(reader.GetOrdinal("provider"));
                        videogame.cost = reader.GetInt32(reader.GetOrdinal("cost"));
                        videogame.quantity = reader.GetInt32(reader.GetOrdinal("videogamequantity"));
                        var pictureIndex = reader.IsDBNull(reader.GetOrdinal("picture")) ? -1 : reader.GetOrdinal("picture");

                        if (pictureIndex != -1)
                        {
                            var bytes = (byte[])reader[pictureIndex];
                            using (MemoryStream ms = new MemoryStream(bytes))
                            {
                                videogame.picture = Image.FromStream(ms);
                            }
                        }
                        videogames.Add(videogame);
                    }
            }
            return videogames;
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            createColumns();
            updateRows();
            var categories = GetCategories();
            comboBox1.Items.Add("Все игры");
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            foreach (var category in categories)
                comboBox1.Items.Add(category.Name);

            if (AppData.ActiveUser.roleid == User.MANAGER)
            {
                button4.Visible = false;
                pictureBox1.Visible = false;
            }
            if (AppData.ActiveUser.roleid == User.SELLER)
            {
                button3.Visible = false;
                button2.Visible = false;
                Deletebutton.Visible = false;
            }

        }
        private void createColumns()
        {
            // picture | name | category | price

            var name = new DataGridViewTextBoxColumn();
            name.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            name.HeaderText = "Название игры";
            var description = new DataGridViewTextBoxColumn();
            description.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            description.HeaderText = "Описание";
            var category = new DataGridViewTextBoxColumn();
            category.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            category.HeaderText = "Категория";
            var provider = new DataGridViewTextBoxColumn();
            provider.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            provider.HeaderText = "Поставщик";
            var price = new DataGridViewTextBoxColumn();
            price.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            price.HeaderText = "Цена";
            var quantity = new DataGridViewTextBoxColumn();
            quantity.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            quantity.HeaderText = "Количество";
            var picture = new DataGridViewImageColumn();
            picture.ImageLayout = DataGridViewImageCellLayout.Zoom;
            picture.Width = 100;

            dataGridView1.Columns.Add(picture);
            dataGridView1.Columns.Add(name);
            dataGridView1.Columns.Add(description);
            dataGridView1.Columns.Add(category);
            dataGridView1.Columns.Add(provider);
            dataGridView1.Columns.Add(price);
            dataGridView1.Columns.Add(quantity);
        }

        public void updateRows()
        {
            dataGridView1.RowTemplate.Height = 100;
            dataGridView1.Rows.Clear();
            List<Videogame> Videogames = search(name, filter, sort, limit, offset, sort_reversed);

            // picture | name | category | price
            foreach (Videogame Videogame in Videogames)
            {
                dataGridView1.Rows.Add(Videogame.picture,
                    Videogame.name,
                    Videogame.description,
                    GetVideogameCategory(Videogame),
                    GetVideogameProvider(Videogame),
                    Videogame.cost,
                    Videogame.quantity);
            }
        }

        private void userLogin_TextChanged(object sender, EventArgs e)
        {
            name = userLogin.Text;
            updateRows();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0) { sort = ""; updateRows(); return; }
            sort = "cost";
            sort_reversed = comboBox2.SelectedIndex == 2;
            updateRows();
        }
        public static List<Category> GetCategories()
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
        public string GetVideogameCategory(Videogame game)
        {
            var categories = GetCategories();
            foreach (var category in categories)
                if (category.Id == game.category) return category.Name;

            return string.Empty;
        }
        public string GetVideogameProvider(Videogame game)
        {
            var providers = GetProviders();
            foreach (var provider in providers)
                if (provider.Id == game.provider) return provider.providername;

            return string.Empty;
        }

        // переход на форму редактирования Менеджер
        private void button3_Click(object sender, EventArgs e)
        {
            Videogame game = GetVideogame(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
            MessageBox.Show($"{game.id}, {game.name}");
            if (game == null) { MessageBox.Show("Игра не найдена"); return; }
            //Hide();
            new EditProductForm(game).ShowDialog();
        }

        private Videogame GetVideogame(string name) {
            Videogame game = null;
            using (var con = new MySqlConnection(connectionString))
            {
                con.Open();

                string sql = "SELECT * FROM videogame WHERE name=@name";

                var cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@name", name);
                var reader = cmd.ExecuteReader();
                reader.Read();
                game = new Videogame
                {
                    id = reader.GetInt32("idvideogame"),
                    name = reader.GetString("name"),
                    description = reader.GetString("description"),
                    category = reader.GetInt32("category"),
                    cost = reader.GetInt32("cost"),
                    quantity = reader.GetInt32("videogamequantity"),
                    provider = reader.GetInt32("provider")
                };
                var pictureIndex = reader.IsDBNull(reader.GetOrdinal("picture")) ? -1 : reader.GetOrdinal("picture");
                if (pictureIndex != -1)
                {
                    var bytes = (byte[])reader[pictureIndex];
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        game.picture = Image.FromStream(ms);
                    }
                }
                reader.Close();
                return game;
            }
        }
        // переход в корзину Пользователь
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            BacketForm bForm = new BacketForm(bucket, this);
            bForm.Show();
        }
        //Добавление в корзину Пользователь
        private void button4_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) MessageBox.Show("Нет продуктов");

            Videogame selectedvideogame = GetVideogame(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
            if (selectedvideogame == null) return;

            // проверяем есть ли такая игра в корзине
            foreach (var bucketItem in bucket) {
                if (bucketItem.Videogame.id == selectedvideogame.id) { bucketItem.Quantity++; return; }
            }

            var item = new CartItem {
                Videogame = selectedvideogame,
                Quantity = 1
            };

            bucket.Add(item);
            MessageBox.Show("Товар добавлен в корзину.", "Добавление в корзину", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            DialogResult ans = MessageBox.Show("Вы действительно хотите удалить товар?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (ans == DialogResult.No) return;

            Videogame selectedGame = GetVideogame(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
            if (selectedGame == null) return;
            RemoveVideogame(selectedGame);
            MessageBox.Show("Запись успешно удалена!");
            updateRows();
        }
        
        private void RemoveVideogame(Videogame game) 
        {
            using (var con = new MySqlConnection(connectionString))
            {
                con.Open();

                string sql = $"DELETE FROM videogame WHERE idvideogame={game.id}";
                var cmd = new MySqlCommand(sql, con);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
