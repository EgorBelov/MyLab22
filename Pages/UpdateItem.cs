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
    public partial class UpdateItem : Form
    {
        int id;

        public UpdateItem()
        {
            InitializeComponent();
        }
        public UpdateItem(int did)
        {
            InitializeComponent();
            id = did;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (var cn = new SqlConnection(Form1.cs))
            {
                cn.Open();
                var sql = @"UPDATE item SET name = @Name WHERE item.id = @id";

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
                var sql = @"select item.name from item where item.id = @id";

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
