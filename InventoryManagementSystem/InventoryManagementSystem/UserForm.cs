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
    public partial class UserForm : Form
    {
        SqlConnection conDB = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\adirr\Documents\dbIMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        public UserForm()
        {
            InitializeComponent();
            LoadUser();
        }

        public void LoadUser()
        {
            int i = 0;
            dgvUser.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM tbUser",conDB);
            conDB.Open();
            reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                i++;
                dgvUser.Rows.Add(i,reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString());
            }
            reader.Close();
            conDB.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            UserModuleForm userModule = new UserModuleForm();
            userModule.btnSave.Enabled = true;
            userModule.btnUpdate.Enabled = false;
            userModule.ShowDialog();
            LoadUser();
        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvUser.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                UserModuleForm userModule = new UserModuleForm();
                userModule.txtUserName.Text = dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString();
                userModule.txtFullName.Text = dgvUser.Rows[e.RowIndex].Cells[2].Value.ToString();
                userModule.txtPass.Text = dgvUser.Rows[e.RowIndex].Cells[3].Value.ToString();
                userModule.txtPhone.Text = dgvUser.Rows[e.RowIndex].Cells[4].Value.ToString();

                userModule.btnSave.Enabled=false;
                userModule.btnUpdate.Enabled=true;
                userModule.txtUserName.Enabled=false;
                userModule.ShowDialog();
            }
            else if(colName == "Delete")
            {
                if (MessageBox.Show("Are you sure want to Delete this user?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conDB.Open();
                    cmd = new SqlCommand("DELETE FROM tbUser WHERE username LIKE '" + dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString() + "'",conDB);
                    cmd.ExecuteNonQuery();
                    conDB.Close();
                    MessageBox.Show("Record has been successfully deleted");
                }
            }
            LoadUser();
        }

 
    }
}
