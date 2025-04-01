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
    public partial class BacketForm : Form
    {
        public List<CartItem> bucket = new List<CartItem>();
         private static string connectionString = "server=127.0.0.1;database=db44;user=root;password=root";
        //public static string connectionString = "server=10.207.106.12;database=db44;user=user44;password=sc96";
        private ProductForm pForm;

        public BacketForm(List<CartItem> bucket, ProductForm pForm)
        {
            InitializeComponent();
            UpdateTotal();
            this.bucket = bucket;
            this.pForm = pForm;
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

        private void updateRows()
        {
            dataGridView1.RowTemplate.Height = 100;
            dataGridView1.Rows.Clear();
            
            // picture | name | category | price
            foreach (var bucketItem in bucket)
            {
                dataGridView1.Rows.Add(bucketItem.Videogame.picture,
                    bucketItem.Videogame.name,
                    bucketItem.Videogame.description,
                    GetVideogameCategory(bucketItem.Videogame),
                    GetVideogameProvider(bucketItem.Videogame),
                    bucketItem.Videogame.cost,
                    bucketItem.Quantity);
            }
            
            UpdateTotal();
        }


        private void UpdateTotal()
        {
            decimal total = 0;
            foreach (CartItem item in bucket)
            {
                total += item.Videogame.cost * item.Quantity;
            }
            totalPriceLabel.Text = $"Итого: {total} руб.";
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Quantity"].Index)
            {
                UpdateTotal();
            }
        }

        private void BacketForm_Load(object sender, EventArgs e)
        {
            createColumns();
            updateRows();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Вы уверены что хотите удалить?\n Данный товар будет удален.", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (res == DialogResult.Yes)
            {
                Videogame selectedvideogame = GetVideogame(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                if (selectedvideogame == null) return;
                foreach (var bucketItem in bucket)
                {
                    if (bucketItem.Videogame.id == selectedvideogame.id) { bucket.Remove(bucketItem); break; }
                }

                updateRows();
            }
            else
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var con = new MySqlConnection(connectionString))
            {
                con.Open();
                foreach (var item in bucket)
                {
                    string sql2 = $"SELECT videogamequantity FROM videogame WHERE idvideogame={item.Videogame.id}";
                    var cmd2 = new MySqlCommand(sql2, con);
                    int quantity = Convert.ToInt32(cmd2.ExecuteScalar());

                    string sql3 = $"UPDATE videogame SET videogamequantity=@videogamequantity WHERE idvideogame={item.Videogame.id}";
                    var cmd3 = new MySqlCommand(sql3, con);
                    int total = quantity - item.Quantity;
                    if (total < 0) { MessageBox.Show("Товар отсутствует"); return; }
                    cmd3.Parameters.AddWithValue("videogamequantity", total);
                    cmd3.ExecuteNonQuery();

                    string sql = "INSERT INTO `list_sales`(idvideogame, cost, date) VALUES (@idvideogame, @cost, @date)";
                    var cmd = new MySqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("idvideogame", item.Videogame.id);
                    // cmd.Parameters.AddWithValue("idclient", );   TODO
                    cmd.Parameters.AddWithValue("cost", item.Videogame.cost);
                    cmd.Parameters.AddWithValue("date", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
                pForm.updateRows();
                MessageBox.Show("Покупка прошла успешно"); 
                bucket.Clear();
                Hide();
            }
        }
    }
}
