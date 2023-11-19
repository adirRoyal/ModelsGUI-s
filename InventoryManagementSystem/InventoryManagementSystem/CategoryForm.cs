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
    public partial class CategoryForm : Form
    {
        SqlConnection conDB = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\adirr\Documents\dbIMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        public CategoryForm()
        {
            InitializeComponent();
            LoadCategory();
        }

        public void LoadCategory()
        {
            int i = 0;
            dgvCategory.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM tbCategory", conDB);
            conDB.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                i++;
                dgvCategory.Rows.Add(i, reader[0].ToString(), reader[1].ToString());
            }
            reader.Close();
            conDB.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CategoryModuleForm formModule = new CategoryModuleForm();
            formModule.btnSave.Enabled = true;
            formModule.btnUpdate.Enabled = false;
            formModule.ShowDialog();
            LoadCategory();
        }

        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCategory.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                CategoryModuleForm formModule = new CategoryModuleForm();
                formModule.lblCatId.Text = dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();
                formModule.txtCatName.Text = dgvCategory.Rows[e.RowIndex].Cells[2].Value.ToString();


                formModule.btnSave.Enabled = false;
                formModule.btnUpdate.Enabled = true;
                formModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure want to Delete this Category?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conDB.Open();
                    cmd = new SqlCommand("DELETE FROM tbCategory WHERE catid LIKE '" + dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", conDB);
                    cmd.ExecuteNonQuery();
                    conDB.Close();
                    MessageBox.Show("Record has been successfully deleted");
                }
            }
            LoadCategory();
        }
    }
}
