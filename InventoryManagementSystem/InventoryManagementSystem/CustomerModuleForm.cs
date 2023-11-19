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

namespace InventoryManagementSystem
{
    public partial class CustomerModuleForm : Form
    {
        SqlConnection conDB = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\adirr\Documents\dbIMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        public CustomerModuleForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (MessageBox.Show("Are you sure want to Save this Customer?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO tbCustomers(cname,cphone)VALUES(@cname, @cphone)", conDB);
                    cmd.Parameters.AddWithValue("@cname", txtCName.Text);
                    cmd.Parameters.AddWithValue("@cphone", txtCPhone.Text);
                    conDB.Open();
                    cmd.ExecuteNonQuery();
                    conDB.Close();
                    MessageBox.Show("User has been successfully saved.");
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
            txtCName.Clear();
            txtCPhone.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            { 
                if (MessageBox.Show("Are you sure want to Update this Customer?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("UPDATE tbCustomers SET cname=@cname, cphone=@cphone WHERE cid LIKE '" + lblCId.Text + "'", conDB);
                    cmd.Parameters.AddWithValue("@cname", txtCName.Text);
                    cmd.Parameters.AddWithValue("@cphone", txtCPhone.Text);
                    conDB.Open();
                    cmd.ExecuteNonQuery();
                    conDB.Close();
                    MessageBox.Show("Customer has been successfully Update.");
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
