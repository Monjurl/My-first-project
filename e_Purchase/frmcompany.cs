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
namespace e_Purchase
{

    public partial class frmcompany : Form
    {
        //SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-079T3M0\SQLEXPRESS;Initial Catalog=e_purchase;Persist Security Info=True;User ID=sa;Password=123456");
        SqlConnection cn = new SqlConnection(Properties.Settings.Default.e_purchaseConnectionString);
        public frmcompany()
        {
            InitializeComponent();
            loadcompany();
            combostatus.Text = "--Select--";
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        public void loadcombocom()
        {
            //cmbcompany.Text = "--Select--";
            //con.Open();
            //SqlCommand cmd = new SqlCommand("Select * from company where status='Active'", con);
            //SqlDataReader dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{
            //    cmbcompany.Items.Add(dr["Name"].ToString());
            //}


            //con.Close();
        }
        public void loadcompany()
        {
            dataGridView2.Rows.Clear();
            int i = 0;

            cn.Open();
            SqlCommand cmd = new SqlCommand("Select * from co_table where status='Active' order By id ASC", cn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i += 1;
                dataGridView2.Rows.Add(i, dr["co_ID"].ToString(), dr["co_name"].ToString());

            }
            cn.Close();
        }
        public void loadcombo()
        {
            combocompany.Items.Clear();
            cn.Open();
            SqlCommand cmd = new SqlCommand("Select * from co_table where status='Active' order By id ASC", cn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                combocompany.Items.Add(dr["co_name"].ToString());
            }


            cn.Close();

        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[co_table]([co_ID],[co_name],[address],[mobile],[status]) VALUES('" + textcode.Text + "','" + textcom.Text + "','" + textaddress.Text + "','" + textmobile.Text + "','" + combostatus.Text + "')", cn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data Save");

            cn.Close();
            textcode.Clear();
            textcom.Clear();
            textaddress.Clear();
            textmobile.Clear();
            combostatus.Text = "--Select--";
            loadcompany();
        }

        private void frmcompany_Load(object sender, EventArgs e)
        {
            loadcombo();
            GeneratOrderNumber();
            combocompany.Text = "--Select--";
            comboBstatus.Text = "--Select--";
            //LoadProductDatagrid();
        }
        private string GeneratOrderNumber()
        {
            string Ordernumber;
            Random rnd = new Random();
            long orderpart1 = rnd.Next(1, 999);
            //int orderpart2 = rnd.Next(1, 10);
            Ordernumber = "2" + orderpart1;

            textpid.Text = Ordernumber;
            return Ordernumber;

        }
        public void LoadProductDatagrid()
        {
            dataGridView1.Rows.Clear();
            int i = 0;

            cn.Open();
            SqlCommand cmd = new SqlCommand("Select * from product_table where status='Active' order By id ASC", cn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i += 1;
                dataGridView1.Rows.Add(i, dr["p_code"].ToString(), dr["p_name"].ToString(), dr["weight"].ToString(), dr["upc"].ToString(), Convert.ToDouble(dr["dp_unitprice"].ToString()).ToString("0.00"), Convert.ToDouble(dr["tp_unitprice"].ToString()).ToString("0.00"), Convert.ToDouble(dr["Fixxed_price"].ToString()).ToString("0.00"), dr["Status"].ToString(), Convert.ToDouble(dr["DP_Price"].ToString()).ToString("0.00"), Convert.ToDouble(dr["Sale_Price"].ToString()).ToString("0.00"), Convert.ToDouble(dr["DAM_Price"].ToString()).ToString("0.00"), Convert.ToDouble(dr["Buy_Price"].ToString()).ToString("0.00"), "Edit");

            }
            cn.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {

            if (combocompany.Text == "--Select--" || textproduct.Text == "" || textupc.Text == "" || textdpprice.Text == "" || texttpprice.Text == "" || textweight.Text == "" || textfixedprice.Text == "" || comboBstatus.Text == "--Select--")
            {
                MessageBox.Show("Please Check Information", "Data Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (button3.Text == "Save")
                {
                    SqlDataAdapter da = new SqlDataAdapter("Select p_code from product_table where p_code='" + textpid.Text + "'", cn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count >= 1)
                    {
                        MessageBox.Show("Item Code Already Existing", "Item Code", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        cn.Open();
                        if (MessageBox.Show("Do you want to Save this Records", "Save Records", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[product_table]([company],[p_code],[p_name],[weight],[upc],[stock_qty],[dp_unitprice],[tp_unitprice],[Fixxed_price],[Status]) VALUES('" + combocompany.Text + "','" + textpid.Text + "','" + textproduct.Text + "','" + textweight.Text + "','" + textupc.Text + "','0','" + textdpprice.Text + "','" + texttpprice.Text + "','" + textfixedprice.Text + "','" + comboBstatus.Text + "')", cn);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Data Save Successfully", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cn.Close();

                        }
                        textdpprice.Clear();
                        textproduct.Clear();
                        texttpprice.Clear();
                        textupc.Clear();
                        textweight.Clear();
                        comboBstatus.Text = "--Select--";

                        GeneratOrderNumber();
                        LoadData();
                    }
                }
                else if (button3.Text == "Update")
                {

                    cn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE [dbo].[product_table] SET [p_name] ='" + textproduct.Text + "' ,[weight] ='" + textweight.Text + "' ,[upc] ='" + textupc.Text + "' ,[dp_unitprice] ='" + textdpprice.Text + "' ,[tp_unitprice] ='" + texttpprice.Text + "',[Status] ='" + comboBstatus.Text + "',[Fixxed_price] ='" + textfixedprice.Text + "',[DP_Price]='" + textpurchaseprice.Text + "',[Sale_Price]='" + textsalesprice.Text + "',[DAM_Price]='" + textdamprice.Text + "',[Buy_Price]='" + textbuyprice.Text + "' WHERE  [p_code] = '" + textpid.Text + "'", cn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("This Record Updated.....", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cn.Close();
                    LoadData();


                    textdpprice.Clear();
                    textproduct.Clear();
                    texttpprice.Clear();
                    textupc.Clear();
                    textweight.Clear();
                    comboBstatus.Text = "--Select--";
                    textdamprice.Clear();
                    textfixedprice.Clear();
                    textdpprice.Clear();
                    textsalesprice.Clear();
                    textbuyprice.Clear();
                    textpurchaseprice.Clear();

                    button3.Text = "Save";
                }


            }

        }

        private void combocompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;

            cn.Open();
            SqlCommand cmd = new SqlCommand("Select * from product_table where company='" + combocompany.Text + "' and status='Active' order By id ASC", cn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i += 1;
                dataGridView1.Rows.Add(i, dr["p_code"].ToString(), dr["p_name"].ToString(), dr["weight"].ToString(), dr["upc"].ToString(), Convert.ToDouble(dr["dp_unitprice"].ToString()).ToString("0.00"), Convert.ToDouble(dr["tp_unitprice"].ToString()).ToString("0.00"), Convert.ToDouble(dr["Fixxed_price"].ToString()).ToString("0.00"), dr["Status"].ToString(), Convert.ToDouble(dr["DP_Price"].ToString()).ToString("0.00"), Convert.ToDouble(dr["Sale_Price"].ToString()).ToString("0.00"), Convert.ToDouble(dr["DAM_Price"].ToString()).ToString("0.00"), Convert.ToDouble(dr["Buy_Price"].ToString()).ToString("0.00"), "Edit");

            }
            cn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GeneratOrderNumber();
        }
        public void LoadData()
        {
            dataGridView1.Rows.Clear();
            int i = 0;

            cn.Open();
            SqlCommand cmd = new SqlCommand("Select * from product_table where company='" + combocompany.Text + "' and status='Active' order By id ASC", cn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i += 1;
                dataGridView1.Rows.Add(i, dr["p_code"].ToString(), dr["p_name"].ToString(), dr["weight"].ToString(), dr["upc"].ToString(), Convert.ToDouble(dr["dp_unitprice"].ToString()).ToString("0.00"), Convert.ToDouble(dr["tp_unitprice"].ToString()).ToString("0.00"), Convert.ToDouble(dr["Fixxed_price"].ToString()).ToString("0.00"), dr["Status"].ToString(), Convert.ToDouble(dr["DP_Price"].ToString()).ToString("0.00"), Convert.ToDouble(dr["Sale_Price"].ToString()).ToString("0.00"), Convert.ToDouble(dr["DAM_Price"].ToString()).ToString("0.00"), Convert.ToDouble(dr["Buy_Price"].ToString()).ToString("0.00"), "Edit");

            }
            cn.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            string ColNam = dataGridView1.Columns[e.ColumnIndex].Name;
            if (ColNam == "edit")
            {

                //frmedit edt = new frmedit(this);
                textpid.Text = dataGridView1[1, e.RowIndex].Value.ToString();
                textproduct.Text = dataGridView1[2, e.RowIndex].Value.ToString();
                textweight.Text = dataGridView1[3, e.RowIndex].Value.ToString();
                textupc.Text = dataGridView1[4, e.RowIndex].Value.ToString();
                textdpprice.Text = dataGridView1[5, e.RowIndex].Value.ToString();
                texttpprice.Text = dataGridView1[6, e.RowIndex].Value.ToString();
                textfixedprice.Text = dataGridView1[7, e.RowIndex].Value.ToString();
                comboBstatus.Text = dataGridView1[8, e.RowIndex].Value.ToString();
                textpurchaseprice.Text = dataGridView1[9, e.RowIndex].Value.ToString();
                textsalesprice.Text = dataGridView1[10, e.RowIndex].Value.ToString();
                textdamprice.Text = dataGridView1[11, e.RowIndex].Value.ToString();
                textbuyprice.Text = dataGridView1[12, e.RowIndex].Value.ToString();

            }
            button3.Text = "Update";
            textfixedprice.ReadOnly = true;
        }

        private void textproduct_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != ' ' & e.KeyChar != '.' & e.KeyChar != '+')
            {
                e.Handled = true;
            }
        }

        private void textupc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textdpprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void texttpprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void textweight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void textsearch_Enter(object sender, EventArgs e)
        {
            //if (textsearch.Text == "Search")
            //{
            //    textsearch.Text = "";

            //    textsearch.ForeColor = Color.Black;
            //}

        }

        private void textsearch_Leave(object sender, EventArgs e)
        {
            //if (textsearch.Text == "")
            //{
            //    textsearch.Text = "Search";

            //    textsearch.ForeColor = Color.DarkGray;
            //}

        }

        private void textsearch_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.e_purchaseConnectionString);
            dataGridView1.Rows.Clear();
            int i = 0;
            if (textsearch.Text == "")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from product_table where company='" + combocompany.Text + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    i += 1;
                    dataGridView1.Rows.Add(i, dr["p_code"].ToString(), dr["p_name"].ToString(), dr["weight"].ToString(), dr["upc"].ToString(), Convert.ToDouble(dr["dp_unitprice"].ToString()).ToString("0.00"), Convert.ToDouble(dr["tp_unitprice"].ToString()).ToString("0.00"), Convert.ToDouble(dr["Fixxed_price"].ToString()).ToString("0.00"), dr["Status"].ToString(), Convert.ToDouble(dr["DP_Price"].ToString()).ToString("0.00"), Convert.ToDouble(dr["Sale_Price"].ToString()).ToString("0.00"), Convert.ToDouble(dr["DAM_Price"].ToString()).ToString("0.00"), Convert.ToDouble(dr["Buy_Price"].ToString()).ToString("0.00"), "Edit");

                }
                con.Close();
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from product_table where p_code LIKE'" + textsearch.Text + "%' or upc ='" + textsearch.Text + "' and company='" + combocompany.Text + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    i += 1;
                    dataGridView1.Rows.Add(i, dr["p_code"].ToString(), dr["p_name"].ToString(), dr["weight"].ToString(), dr["upc"].ToString(), Convert.ToDouble(dr["dp_unitprice"].ToString()).ToString("0.00"), Convert.ToDouble(dr["tp_unitprice"].ToString()).ToString("0.00"), Convert.ToDouble(dr["Fixxed_price"].ToString()).ToString("0.00"), dr["Status"].ToString(), Convert.ToDouble(dr["DP_Price"].ToString()).ToString("0.00"), Convert.ToDouble(dr["Sale_Price"].ToString()).ToString("0.00"), Convert.ToDouble(dr["DAM_Price"].ToString()).ToString("0.00"), Convert.ToDouble(dr["Buy_Price"].ToString()).ToString("0.00"), "Edit");

                }
                con.Close();
            }

        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.e_purchaseConnectionString);
            dataGridView1.Rows.Clear();
            int i = 0;

            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from product_table where p_code ='" + textsearch.Text + "' or upc='" + textsearch.Text + "' and company='" + combocompany.Text + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i += 1;
                dataGridView1.Rows.Add(i, dr["p_code"].ToString(), dr["p_name"].ToString(), dr["weight"].ToString(), dr["upc"].ToString(), Convert.ToDouble(dr["dp_unitprice"].ToString()).ToString("0.00"), Convert.ToDouble(dr["tp_unitprice"].ToString()).ToString("0.00"), Convert.ToDouble(dr["Fixxed_price"].ToString()).ToString("0.00"), dr["Status"].ToString(), Convert.ToDouble(dr["DP_Price"].ToString()).ToString("0.00"), Convert.ToDouble(dr["Sale_Price"].ToString()).ToString("0.00"), Convert.ToDouble(dr["DAM_Price"].ToString()).ToString("0.00"), Convert.ToDouble(dr["Buy_Price"].ToString()).ToString("0.00"), "Edit");

            }
            con.Close();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ////string ColNam = dataGridView1.Columns[e.ColumnIndex].Name;
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                frmfixedprice edt = new frmfixedprice();
                edt.textpid.Text = dataGridView1[1, e.RowIndex].Value.ToString();
                edt.textproduct.Text = dataGridView1[2, e.RowIndex].Value.ToString();
                edt.textweight.Text = dataGridView1[3, e.RowIndex].Value.ToString();
                edt.textupc.Text = dataGridView1[7, e.RowIndex].Value.ToString();

                edt.ShowDialog();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {


        }

        private void textproduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textweight.Focus();
            }
        }

        private void textweight_TextChanged(object sender, EventArgs e)
        {

        }

        private void textweight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textupc.Focus();
            }
        }

        private void textupc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textdpprice.Focus();
            }
        }

        private void texttpprice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textfixedprice.Focus();
            }
        }

        private void textfixedprice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textpurchaseprice.Focus();
            }
        }

        private void textpurchaseprice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textsalesprice.Focus();
            }
        }

        private void textsalesprice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textdamprice.Focus();
            }
        }

        private void textdamprice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textbuyprice.Focus();
            }
        }

        private void textbuyprice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3.Focus();
            }
        }

        private void textdpprice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                texttpprice.Focus();
            }
        }

        private void textfixedprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void textpurchaseprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void textsalesprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void textdamprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void textbuyprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textfixedprice.ReadOnly = false;
            }
            else
            {
                textfixedprice.ReadOnly = true;
            }
        }
    }
}
