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

namespace InventoryManagementSystem
{
    public partial class ProductModuleForm : Form
    {
        SqlConnection conDB = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\adirr\Documents\dbIMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public ProductModuleForm()
        {
            InitializeComponent();
            LoadCategory();
        }

        public void LoadCategory()
        {
            comboQty.Items.Clear();
            cmd = new SqlCommand("SELECT catname FROM tbCategory", conDB);
            conDB.Open();
            dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                comboQty.Items.Add(dr[0].ToString());
            }
            dr.Close();
            conDB.Close();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure want to Save this Product?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO tbProduct(pname,pqty,pprice,pdescription,pcategory)VALUES(@pname, @pqty, @pprice, @pdescription, @pcategory)", conDB);
                    cmd.Parameters.AddWithValue("@pname", txtProductName.Text);
                    cmd.Parameters.AddWithValue("@pqty", Convert.ToInt16(txtQuantiy.Text));
                    cmd.Parameters.AddWithValue("@pprice", Convert.ToInt16(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@pdescription", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@pcategory", comboQty.Text);
                    conDB.Open();
                    cmd.ExecuteNonQuery();
                    conDB.Close();
                    MessageBox.Show("Product has been successfully saved.");
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            txtProductName.Clear();
            txtQuantiy.Clear();
            txtPrice.Clear();
            txtDescription.Clear();
            comboQty.Text = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure want to Update this Product?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("UPDATE tbProduct SET pname=@pname, pqty=@pqty, pprice=@pprice, pdescription=@pdescription, pcategory=@pcategory WHERE pid LIKE '" + lblPid.Text + "'", conDB);
                    cmd.Parameters.AddWithValue("@pname", txtProductName.Text);
                    cmd.Parameters.AddWithValue("@pqty", Convert.ToInt16(txtQuantiy.Text));
                    cmd.Parameters.AddWithValue("@pprice", Convert.ToInt16(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@pdescription", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@pcategory", comboQty.Text);
                    conDB.Open();
                    cmd.ExecuteNonQuery();
                    conDB.Close();
                    MessageBox.Show("Product has been successfully Update.");
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
