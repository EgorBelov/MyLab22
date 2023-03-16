using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using MyLab2.Pages;

namespace MyLab2
{
    public partial class Form1 : Form
    {
        public static string cs = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = FoodIntakes; Integrated Security = true";
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e) 
        {
            LoadPersons();
            treeView1.SelectedNode = null;

        }

        public void LoadPersons()
        {
            treeView1.Nodes.Clear();
            using (SqlConnection cn = new SqlConnection(cs))
            {
                cn.Open();
                var sql = "select * from person";
                var cmd = new SqlCommand(sql, cn);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    TreeNode n = new TreeNode(dr["Name"].ToString());
                    n.Tag = dr["id"];
                    n.Name = (string)dr["Name"];
                    treeView1.Nodes.Add(n);
                    LoadMeals((int)dr["id"], n);
                }
            }
            
        }
        public void LoadMeals(int personId, TreeNode parent)
        {
            using (SqlConnection cn = new SqlConnection(cs))
            {
                cn.Open();
                var sql = @"select  meals.meal_time, meals.id from meals
                                            where meals.person_id = @persinId;";
                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@persinId", personId);


                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    TreeNode n = new TreeNode(dr["meal_time"].ToString());
                    n.Tag = dr["id"];
                    parent.Nodes.Add(n);
                    LoadDishes((int)dr["id"], n);
                }
            }
        }
        public void LoadDishes(int mealId, TreeNode parent)
        {
            using (SqlConnection cn = new SqlConnection(cs))
            {
                cn.Open();
                var sql = @"select dishes.name, dishes.id from dishes
                                    where dishes.meal_id = @mealId;";
                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@mealId", mealId);


                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    TreeNode n = new TreeNode(dr["name"].ToString());
                    n.Tag = dr["id"];
                    parent.Nodes.Add(n);
                    LoadProducts((int)dr["id"], n);
                }
            }
        }
        public void LoadProducts(int dishId, TreeNode parent)
        {
            using (SqlConnection cn = new SqlConnection(cs))
            {
                cn.Open();
                var sql = @"select products.name, products.id from products
                                            where products.dish_id = @dishId;";
                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@dishId", dishId);


                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    TreeNode n = new TreeNode(dr["name"].ToString());
                    n.Tag = dr["id"];
                    parent.Nodes.Add(n);
                }
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
     
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Level == 0)
            {
                
                using (var cn = new SqlConnection(cs))
                {
                    cn.Open();
                    var sql = @"delete from person where id = @id;";

                    var cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@id", treeView1.SelectedNode.Tag);

                    cmd.ExecuteNonQuery();
                    treeView1.SelectedNode.Remove();
                    LoadPersons();
                }

            }
       
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Level == 1)
            {
                using (var cn = new SqlConnection(cs))
                {
                    cn.Open();
                    var sql = @"delete from meals where id = @id;";

                    var cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@id", treeView1.SelectedNode.Tag);

                    cmd.ExecuteNonQuery();
                    treeView1.SelectedNode.Remove();
                    LoadPersons();
                }
            }
       
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Level == 2)
            {
                using (var cn = new SqlConnection(cs))
                {
                    cn.Open();
                    var sql = @"delete from dishes where id = @id;";

                    var cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@id", treeView1.SelectedNode.Tag);

                    cmd.ExecuteNonQuery();
                    treeView1.SelectedNode.Remove();
                    LoadPersons();
                }
            }

            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Level == 3)
            {
                using (var cn = new SqlConnection(cs))
                {
                    cn.Open();
                    var sql = @"delete from products where id = @id;";

                    var cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@id", treeView1.SelectedNode.Tag);

                    cmd.ExecuteNonQuery();
                    treeView1.SelectedNode.Remove();
                    LoadPersons();
                }
            }
        }

      

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Level == 0)
            {
                AddMeal addMeal = new AddMeal();
                addMeal.personId = (int)treeView1.SelectedNode.Tag;
                addMeal.ShowDialog();
                LoadPersons();
            }
            else if (treeView1.SelectedNode.Level == 1)
            {
                AddDish addDish = new AddDish();
                addDish.mealId = (int)treeView1.SelectedNode.Tag;
                addDish.ShowDialog();
                LoadPersons();
            }
            else if (treeView1.SelectedNode.Level == 2)
            {
                AddProduct addProduct = new AddProduct();
                addProduct.dishId = (int)treeView1.SelectedNode.Tag;
                addProduct.ShowDialog();
                LoadPersons();
            }
            
        }

        private void добавитьПерсонуToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            AddPerson addPerson = new AddPerson();
            addPerson.ShowDialog();
            treeView1.Nodes.Add(new TreeNode(AddPerson.name));
            LoadPersons();
            
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (treeView1.SelectedNode == null)
            {
                добавитьПерсонуToolStripMenuItem.Enabled = true;
                удалитьToolStripMenuItem.Enabled = false;
                добавитьToolStripMenuItem.Enabled = false;
                редактироватьToolStripMenuItem.Enabled = false;
            }
            else if (treeView1.SelectedNode.Level == 3)
            {
                удалитьToolStripMenuItem.Enabled = true;
                добавитьToolStripMenuItem.Enabled = false;
                редактироватьToolStripMenuItem.Enabled = true;
            }
            else 
            {
                удалитьToolStripMenuItem.Enabled = true;
                добавитьToolStripMenuItem.Enabled = true;
                редактироватьToolStripMenuItem.Enabled = true;
            }


        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Level == 0)
            {
                UpdatePerson updatePerson = new UpdatePerson(treeView1.SelectedNode.Name, (int)treeView1.SelectedNode.Tag);
                updatePerson.ShowDialog();
                LoadPersons();
            }
            else if (treeView1.SelectedNode.Level == 1)
            {
                UpdateMeal updateMeal = new UpdateMeal((int)treeView1.SelectedNode.Tag);
                updateMeal.ShowDialog();
                LoadPersons();
            }
            else if (treeView1.SelectedNode.Level == 2)
            {
                UpdateDish updateDish = new UpdateDish((int)treeView1.SelectedNode.Tag);
                updateDish.ShowDialog();
                LoadPersons();
            }
            else if (treeView1.SelectedNode.Level == 3)
            {
                UpdateProduct updateProduct = new UpdateProduct((int)treeView1.SelectedNode.Tag);
                updateProduct.ShowDialog();
                LoadPersons();
            }
        }
    }
}
