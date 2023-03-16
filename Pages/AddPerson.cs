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
using System.Xml.Linq;
using MyLab2;

namespace MyLab2.Pages
{
    public partial class AddPerson : Form
    {
        public static string name = "";
        public AddPerson()
        {
            InitializeComponent();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            name = textBox1.Text;
            using (var cn = new SqlConnection(Form1.cs))
            {
                cn.Open();
                var sql = @"INSERT INTO person (name, age, gender) VALUES (@name, 20, 'qwe');";

                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@name", textBox1.Text);

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

        private void AddPerson_Load(object sender, EventArgs e)
        {
            Check();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
