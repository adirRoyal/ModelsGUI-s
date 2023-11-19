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
    public partial class OrdersForm : Form
    {
        SqlConnection conDB = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\adirr\Documents\dbIMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        public OrdersForm()
        {
            InitializeComponent();
            LoadOrder();
        }

        public void LoadOrder()
        {
            double total = 0;
            int i = 0;
            dgvOrder.Rows.Clear();
            cmd = new SqlCommand("SELECT orderid, odate, O.pid, P.pname, O.cid, C.cname, qty, price, total FROM tbOrders AS O JOIN tbCustomers AS C ON O.cid=C.cid JOIN tbProduct AS P ON O.pid=P.pid WHERE CONCAT(orderid, odate, O.pid, P.pname, O.cid, C.cname, qty, price) LIKE '%"+ txtSearch.Text +"%' ", conDB);
            conDB.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                i++;
                dgvOrder.Rows.Add(i, reader[0].ToString(),Convert.ToDateTime(reader[1].ToString()).ToString("dd/MM/yyyy"), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString());
                total += Convert.ToInt32(reader[8].ToString());
            }
            reader.Close();
            conDB.Close();

            lblQty.Text = i.ToString();
            lblTotal.Text = total.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OrdersModuleForm moduleForm = new OrdersModuleForm();
            moduleForm.ShowDialog();
            LoadOrder();
        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvOrder.Columns[e.ColumnIndex].Name;
           
            if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure want to Delete this user?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conDB.Open();
                    cmd = new SqlCommand("DELETE FROM tbOrders WHERE orderid LIKE '" + dgvOrder.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", conDB);
                    cmd.ExecuteNonQuery();
                    conDB.Close();
                    MessageBox.Show("Record has been successfully deleted");

                    cmd = new SqlCommand("UPDATE tbProduct SET pqty=(pqty+@pqty) WHERE pid LIKE '" + dgvOrder.Rows[e.RowIndex].Cells[3].Value.ToString() + "'", conDB);
                    cmd.Parameters.AddWithValue("@pqty", Convert.ToInt16(dgvOrder.Rows[e.RowIndex].Cells[5].Value.ToString()));

                    conDB.Open();
                    cmd.ExecuteNonQuery();
                    conDB.Close();
                }
            }
            LoadOrder();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadOrder();

        }
    }
}
