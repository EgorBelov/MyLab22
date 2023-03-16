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

namespace MyLab2.Pages
{
    public partial class UpdateMeal : Form
    {
        int id;

        public UpdateMeal()
        {
            InitializeComponent();
        }
        public UpdateMeal(int person_id)
        {
            InitializeComponent();
            id = person_id;
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
                var sql = @"UPDATE meals SET meal_time = @Name WHERE meals.id = @id";

                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@Name", s);
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
      

        private void radioButton1_Click(object sender, EventArgs e)
        {
            Check();
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            Check();
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            Check();
        }
    }
}
