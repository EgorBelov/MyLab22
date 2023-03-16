using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyLab2.Pages
{
    public partial class UpdateProduct : Form
    {
        int id;

        public UpdateProduct()
        {
            InitializeComponent();
        }
        public UpdateProduct(int did)
        {
            InitializeComponent();
            id = did;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (var cn = new SqlConnection(Form1.cs))
            {
                cn.Open();
                var sql = @"UPDATE products SET name = @Name WHERE products.id = @id";

                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateProduct_Load(object sender, EventArgs e)
        {
            
            using (var cn = new SqlConnection(Form1.cs))
            {
                cn.Open();
                var sql = @"select products.name from products where products.id = @id";

                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBox1.Text = (string)reader.GetValue(0);
                }
            }
        }

    }
}
