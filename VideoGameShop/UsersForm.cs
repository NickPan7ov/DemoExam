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
    public partial class UsersForm : Form
    {
        private string name = "";
        private string filter = "";
        private string sort = "";
        private bool sort_reversed = false;
        private int limit = -1;
        private int offset = -1;
        public UsersForm()
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
        private void UsersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult ex = MessageBox.Show("Вы уверены что хотите выйти из приложения?", "Выход из приложения", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (ex == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {

            }
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {
            createColumns();
            updateRows();
        }
        private void createColumns()
        {
            var name = new DataGridViewTextBoxColumn();
            name.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            name.HeaderText = "Логин пользователя";
            var fio = new DataGridViewTextBoxColumn();
            fio.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            fio.HeaderText = "ФИО";
            var role = new DataGridViewTextBoxColumn();
            role.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            role.HeaderText = "Роль";
            dataGridView1.Columns.Add(name);
            dataGridView1.Columns.Add(fio);
            dataGridView1.Columns.Add(role);
        }
        
        public static List<User> search(string name = "", string filter = "", string sort = "", int limit = -1, int offset = -1, bool reverse_sort = false)
        {
            string select = $"SELECT * FROM `user`";
            string _search = $"WHERE `username` LIKE '%{name}%'";
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

            List<User> users = new List<User>();

            using (var con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sql, con);

                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        var user = new User();
                        user.username = reader.GetString(reader.GetOrdinal("username"));
                        user.fio = reader.GetString(reader.GetOrdinal("fio"));
                        user.roleid = reader.GetInt32(reader.GetOrdinal("role"));
                        users.Add(user);
                    }
            }
            return users;
        }
        private void updateRows()
        {
            dataGridView1.RowTemplate.Height = 100;
            dataGridView1.Rows.Clear();
            List<User> users = search(name, filter, sort, limit, offset, sort_reversed);
            foreach (User user in users)
            {
                dataGridView1.Rows.Add(
                    user.username,
                    user.fio,
                    GetRoleName(user)
                    ) ;
            }
        }
        public string GetRoleName(User user)
        {
            var roles = GetRoles();
            foreach (var role in roles)
                if (role.Id == user.roleid) return role.namerole;

            return string.Empty;
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

        private void button2_Click(object sender, EventArgs e)
        {
            AddUserForm frm = new AddUserForm();
            frm.ShowDialog();
        }
    }
}
