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
    public partial class EditProductForm : Form
    {
        //private static string connectionString = "server=127.0.0.1;database=db44;user=root;password=root";
        public static string connectionString = "server=10.207.106.12;database=db44;user=user44;password=sc96";
        private static List<Category> categories = new List<Category>();
        private static List<Provider> providers = new List<Provider>();
        private Videogame game = null;

        public EditProductForm(Videogame game)
        {
            InitializeComponent();
            this.game = game;
        }

        private void EditProductForm_Load(object sender, EventArgs e)
        {
            categories = GetCategories();
            providers = GetProviders();

            int categoryIndex = 0;
            int providerIndex = 0;
            foreach(Category genre in categories)
            {
                comboBox1.Items.Add(genre.Name);
                if (genre.Id == game.category) categoryIndex = categories.IndexOf(genre);
            }

            foreach(Provider provider in providers)
            {
                comboBox2.Items.Add(provider.providername);
                if (provider.Id == game.provider) providerIndex = providers.IndexOf(provider);
            }

            comboBox1.SelectedIndex = categoryIndex;
            comboBox2.SelectedIndex = providerIndex;

            textBox4.Text = game.name;
            textBox1.Text = game.quantity.ToString();
            textBox2.Text = game.cost.ToString();
            textBox3.Text = game.description;
            pictureBox1.Image = game.picture;
            
        }

        // редактирование
        private void button1_Click(object sender, EventArgs e)
        {
            using (var con = new MySqlConnection(connectionString))
            {
                con.Open();

                // =============== TODO ==============
                string sql = @"UPDATE videogame SET 
                name=@Name,
                description=@Description,
                videogamequantity=@Quantity,
                cost=@Cost";
                // ===================================

                var cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("Name", game.name);
                cmd.Parameters.AddWithValue("Description", game.description);
                cmd.Parameters.AddWithValue("Quantity", game.quantity);
                cmd.Parameters.AddWithValue("Cost", game.cost);
                
                byte[] gamePicture = convertToBytes(pictureBox1.Image);
                if (game.picture != pictureBox1.Image && gamePicture != null) 
                {
                    sql += ",picture=@Picture";
                    cmd.Parameters.AddWithValue("Picture", gamePicture);
                }

                sql += $" WHERE idvideogame={game.id}";
                cmd.CommandText = sql;
                MessageBox.Show(sql);
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Данные успешно обновлены!", "Успешное обновление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
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
                    game.picture = pictureBox1.Image; 
                }
           }
        }

        // выход из формы
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Вы уверены что хотите выйти?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (res == DialogResult.Yes)
            {
                Hide();
            }
            else
            {

            }
        }

       private Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(ms);
            }
        }

        // жанр
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var categoryId = GetCategoryId(comboBox1.SelectedItem.ToString());
            if (categoryId == -1) return;
            game.category = categoryId;
        }

        // поставщик
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var providerId = GetProviderId(comboBox2.SelectedItem.ToString());
            if (providerId == -1) return;
            game.provider = providerId;
        }
        //Обработка на ввод чисел
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        //Обработка на ввод чисел
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public static List<Category> GetCategories(){
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

        public static List<Provider> GetProviders(){
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

        public static int GetCategoryId(string name) {
            foreach (Category genre in categories)
                if (genre.Name == name) return genre.Id;
                
            return -1;
        }

        public static int GetProviderId(string name) {
            foreach (Provider provider in providers)
                if (provider.providername == name) return provider.Id;
                
            return -1;
        }

        public static string GetCategoryName(int id) {
            foreach(Category genre in categories)
                if(genre.Id == id) return genre.Name;

            return string.Empty;
        }

        public static string GetProviderName(int id) {
            foreach(Provider provider in providers)
                if(provider.Id == id) return provider.providername;

            return string.Empty;
        }

        public static byte[] convertToBytes(Image img)
        {
            try
            {
                if (img == null) return null;
                using (var ms = new MemoryStream())
                {
                    img.Save(ms, img.RawFormat);
                    return ms.ToArray();
                }
            }
            catch 
            {
                return null;
            }
        }
        
        public static Image convertToImage(byte[] imgData) {
            if (imgData == null) return null;
            using (var ms = new MemoryStream(imgData))
            {
                return Image.FromStream(ms);
            }
        }

        //name
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            bool isNameValid = !textBox4.Text.Equals(string.Empty); // Добавь ещё проверок если хочешь
            if (!isNameValid) return;
            game.name = textBox4.Text;
        }
        //quantity
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            bool isValidQuantity = !textBox1.Text.Equals(string.Empty);
            if (!isValidQuantity) return;
            game.quantity = Convert.ToInt32(textBox1.Text);
        }
        //cost
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            bool isValidCost = !textBox2.Text.Equals(string.Empty);
            if (!isValidCost) return;
            game.cost = Convert.ToInt32(textBox2.Text);
        }
        //description
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            bool isDescriptionValid = !textBox3.Text.Equals(string.Empty);
            if (!isDescriptionValid) return;
            game.description = textBox3.Text;
        }
    }
}
