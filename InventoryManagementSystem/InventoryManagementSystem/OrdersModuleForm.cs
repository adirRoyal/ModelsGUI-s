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
    public partial class OrdersModuleForm : Form
    {
        SqlConnection conDB = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\adirr\Documents\dbIMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        int qty = 0;
        public OrdersModuleForm()
        {
            InitializeComponent();
            LoadCustomer();
            LoadProduct();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cmd = new SqlCommand("SELECT cid, cname FROM tbCustomers WHERE CONCAT(cid, cname) LIKE '%"+textSearchCust.Text+"%' ", conDB);
            conDB.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, reader[0].ToString(), reader[1].ToString());
            }
            reader.Close();
            conDB.Close();
        }

        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(pid, pname, pprice, pdescription, pcategory) LIKE '%" + textSearchProd.Text + "%'", conDB);
            conDB.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString());
            }
            reader.Close();
            conDB.Close();
        }

        private void textSearchCust_TextChanged(object sender, EventArgs e)
        {
            LoadCustomer();
        }

        private void textSearchProd_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

       

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            GetQty();
            if(Convert.ToInt32(UDQty.Value) > qty)
            {
                MessageBox.Show("Instock quantity is not enough!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                UDQty.Value = UDQty.Value - 1;
                return;
            }
            if (Convert.ToInt32(UDQty.Value) > 0)
            {
                int total = Convert.ToInt32(txtPrice.Text) * Convert.ToInt32(UDQty.Value);
                txtTotal.Text = total.ToString();
            }
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCid.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtCName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPid.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCid.Text == "")
                {
                    MessageBox.Show("Please select customer!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtPid.Text == "")
                {
                    MessageBox.Show("Please select product!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Are you sure want to Insert this Order?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO tbOrders(odate, pid, cid, qty, price, total)VALUES(@odate, @pid, @cid, @qty, @price, @total)", conDB);
                    cmd.Parameters.AddWithValue("@odate", dtOrder.Value);
                    cmd.Parameters.AddWithValue("@pid", Convert.ToInt32(txtPid.Text));
                    cmd.Parameters.AddWithValue("@cid", Convert.ToInt32(txtCid.Text));
                    cmd.Parameters.AddWithValue("@qty", Convert.ToInt32(UDQty.Value));
                    cmd.Parameters.AddWithValue("@price", Convert.ToInt32(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@total", Convert.ToInt32(txtTotal.Text));
                    conDB.Open();
                    cmd.ExecuteNonQuery();
                    conDB.Close();
                    MessageBox.Show("Order has been successfully inserted.");

                    cmd = new SqlCommand("UPDATE tbProduct SET pqty=(pqty-@pqty) WHERE pid LIKE '" + txtPid.Text + "'", conDB);
                    cmd.Parameters.AddWithValue("@pqty", Convert.ToInt16(UDQty.Text));

                    conDB.Open();
                    cmd.ExecuteNonQuery();
                    conDB.Close();
                    Clear();
                    LoadProduct();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Clear()
        {
            txtCid.Clear();
            txtCName.Clear();

            txtPid.Clear();
            txtPName.Clear();

            txtPrice.Clear();
            UDQty.Value = 0;
            txtTotal.Clear();
            dtOrder.Value = DateTime.Now;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void GetQty()
        {
            cmd = new SqlCommand("SELECT pqty FROM tbProduct WHERE pid ='" + txtPid.Text + "'", conDB);
            conDB.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                qty = Convert.ToInt32(reader[0].ToString());
            }
            reader.Close();
            conDB.Close();
        }
    }
}
