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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace MyLab2.Pages
{
    public partial class AddDish : Form
    {
        public int mealId;
        public AddDish()
        {
            InitializeComponent();
        }
  
        private void button1_Click(object sender, EventArgs e)
        {
            using (var cn = new SqlConnection(Form1.cs))
            {
                cn.Open();
                var sql = @"INSERT INTO dishes (meal_Id, name) VALUES (@mealId, @name);";

                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                cmd.Parameters.AddWithValue("@mealId", mealId);
                cmd.ExecuteNonQuery();

                Close();
            }
        }
        void Check()
        {
            if (textBox1.Text == null || textBox1.Text.Length == 0)
            {
                button1.Enabled = false;
            }
            else
                button1.Enabled = true;

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Check();
        }
        private void AddGroup_Load(object sender, EventArgs e)
        {
            Check();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
