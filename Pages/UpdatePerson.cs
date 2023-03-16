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
    public partial class UpdatePerson : Form
    {
        string nm;
        int id;
        public UpdatePerson()
        {
            InitializeComponent();
        }
        public UpdatePerson(string name, int d)
        {
            InitializeComponent();
            nm = name;
            id = d;
            textBox1.Text = nm;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (var cn = new SqlConnection(Form1.cs))
            {
                cn.Open();
                var sql = @"UPDATE person SET name = @Name WHERE person.id = @id";

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

        private void FacultyChange_Load(object sender, EventArgs e)
        {
            textBox1.Text = nm;
            Check();
        }
    }
}
