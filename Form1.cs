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
        public static string cs = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Rent; Integrated Security = true";
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
                    LoadRents((int)dr["id"], n);
                }
            }
            
        }
        public void LoadRents(int personId, TreeNode parent)
        {
            using (SqlConnection cn = new SqlConnection(cs))
            {
                cn.Open();
                var sql = @"select  rent.name, rent.id from rent
                                            where rent.person_id = @personId;";
                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@personId", personId);


                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    TreeNode n = new TreeNode(dr["name"].ToString());
                    n.Tag = dr["id"];
                    parent.Nodes.Add(n);
                    LoadItems((int)dr["id"], n);
                }
            }
        }
        public void LoadItems(int itemId, TreeNode parent)
        {
            using (SqlConnection cn = new SqlConnection(cs))
            {
                cn.Open();
                var sql = @"select item.name, item.id from item
                                    where item.rent_id = @itemId;";
                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@itemId", itemId);


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
                    var sql = @"delete from rent where id = @id;";

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
                    var sql = @"delete from item where id = @id;";

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
                AddRent addDish = new AddRent();
                addDish.mealId = (int)treeView1.SelectedNode.Tag;
                addDish.ShowDialog();
                LoadPersons();
            }
            else if (treeView1.SelectedNode.Level == 1)
            {
                AddProduct addProduct = new AddProduct();
                addProduct.rentId = (int)treeView1.SelectedNode.Tag;
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
                UpdateRent updateDish = new UpdateRent((int)treeView1.SelectedNode.Tag);
                updateDish.ShowDialog();
                LoadPersons();
            }
            else if (treeView1.SelectedNode.Level == 2)
            {
                UpdateItem updateProduct = new UpdateItem((int)treeView1.SelectedNode.Tag);
                updateProduct.ShowDialog();
                LoadPersons();
            }
        }
    }
}
