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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace MyLab2.Pages
{
    public partial class AddMeal : Form
    {
        public int personId;
        public AddMeal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = "";
            if (radioButton1.Checked == true)
            {
                s = "zavtrak";
            }
            else if (radioButton2.Checked == true)
            {
                s = "obed";
            }
            else if (radioButton3.Checked == true)
            {
                s = "dinner";
            }
            using (var cn = new SqlConnection(Form1.cs))
            {
                cn.Open();
                var sql = @"INSERT INTO meals (person_Id, meal_time) VALUES (@personId, @Name);";

                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@Name", s);
                cmd.Parameters.AddWithValue("@personId", personId);
                cmd.ExecuteNonQuery();

                Close();
            }
        }
        void Check()
        {
            if (radioButton1.Checked == true || radioButton2.Checked == true || radioButton3.Checked == true)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }
        private void AddGroup_Load(object sender, EventArgs e)
        {
            Check();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            Check();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Check();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Check();
        }
    }
}
