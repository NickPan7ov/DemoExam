using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
namespace VideoGameShop
{
    public static class Connection
    {
        public static string connectionString= "server=10.207.106.12;database=db44;user=user44;password=sc96";

        public static List<Role> GetRoles() {
            List<Role> roles = new List<Role>();
            using (var con = new MySqlConnection(connectionString)){
                con.Open();

                string sql = "SELECT * FROM role";
                var cmd = new MySqlCommand(sql, con);
                using(var reader = cmd.ExecuteReader())
                while(reader.Read()){
                    roles.Add(new Role {
                        Id = reader.GetInt32("id"),
                        namerole = reader.GetString("rolename")
                    });
                }
            }
            return roles;
        }

        public static Role GetRole(string name) {
            using (var con = new MySqlConnection(connectionString)) {
                con.Open();

                string sql = "SELECT * FROM role WHERE rolename=@username";
                var cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("username", name);
                using(var reader = cmd.ExecuteReader())
                while(reader.Read()){
                    return new Role {
                        Id = reader.GetInt32("id"),
                        namerole = reader.GetString("rolename")
                    };
                }
            }
            return null;
        }

        public static User GetUser(string username, string password) 
        {
            using (var con = new MySqlConnection(connectionString)) 
            {
                con.Open();

                string sql = "SELECT * FROM user WHERE username = @username AND password = @password";
                var cmd = new MySqlCommand(sql, con);
                string hash = string.Empty;
                using (var sha256 = SHA256.Create())
                {
                    byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    hash = BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
                }
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", hash);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new User 
                        {
                            iduser = reader.GetInt32("iduser"),
                            username = reader.GetString("username"),
                            fio = reader.GetString("fio"),
                            roleid = reader.GetInt32("role")
                        };
                    }
                }
            }
            return null;
        }
    }
}
