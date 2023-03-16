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
    public partial class UpdateRent : Form
    {
        int id;
        public UpdateRent()
        {
            InitializeComponent();
        }
        public UpdateRent(int mid)
        {
            InitializeComponent();
            id = mid;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var cn = new SqlConnection(Form1.cs))
            {
                cn.Open();
                var sql = @"UPDATE rent SET name = @Name WHERE rent.id = @id";

                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                Close();
            }
        }

        private void UpdateDish_Load(object sender, EventArgs e)
        {
            Check();
            using (var cn = new SqlConnection(Form1.cs))
            {
                cn.Open();
                var sql = @"select rent.name from rent where rent.id = @id";

                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBox1.Text = (string)reader.GetValue(0);
                }
            }
        }
        void Check()
        {
            if (textBox1.Text.Length == 0)
            {
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Check();
        }
    }
}
