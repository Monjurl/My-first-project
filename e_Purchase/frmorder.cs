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
    public partial class frmorder : Form
    {
        SqlConnection cn = new SqlConnection(Properties.Settings.Default.e_purchaseConnectionString);

        int index;
        public frmorder()
        {
            InitializeComponent();
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
        public void datagricalculate1()
        {
            double total1 = 0;
            double total2 = 0;
            double total3 = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                total1 += Convert.ToDouble(dataGridView1.Rows[i].Cells["amount"].Value.ToString());
                total3 += Convert.ToDouble(dataGridView1.Rows[i].Cells["tptotal"].Value.ToString());
                total2 += Convert.ToDouble(dataGridView1.Rows[i].Cells["qty"].Value.ToString());
            }
            texttotal.Text = total1.ToString("0.00");
            lblqty.Text = total2.ToString("0.00");
            labeltotal.Text = total3.ToString("0.00");
        }
        private void btnadd_Click(object sender, EventArgs e)
        {
            if (textQuantity.Text == "" || comboproduct.Text == "--Select--")
            {
                MessageBox.Show("Please Check Quantity Or Item", "Add Item", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Do you want to Add this Records", "Add Records", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    dataGridView1.Rows.Add(sl, textpidc.Text, comboproduct.Text, textQuantity.Text, labeldp.Text, textamount.Text, labeltp.Text, labelamount.Text, "Edit", "Delete");

                    datagricalculate();

                }
            }
        }
        public void offer()
        {
            float a, b, c;
            float.TryParse(texttotal.Text, out a);
            float.TryParse(textoffer.Text, out b);
            float.TryParse(labelofqty.Text, out c);
            c = a / b;
            labelofqty.Text = c.ToString("0.00");
        }
        private void frmorder_Load(object sender, EventArgs e)
        {
            loadcombo();
            GeneratOrderNumber();
            loaduser();
            combouser.Text = "--Select--";
            combocompany.Text = "--Select--";

        }

        private void combocompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboproduct.Items.Clear();
            cn.Open();
            SqlCommand cmd = new SqlCommand("Select p_name from product_table where company='" + combocompany.Text + "' and status='Active' order By p_name ASC", cn);
            SqlDataReader dr = cmd.ExecuteReader();
            AutoCompleteStringCollection mycollcol = new AutoCompleteStringCollection();
            while (dr.Read())
            {
                comboproduct.Items.Add(dr["p_name"].ToString());
                mycollcol.Add(dr["p_name"].ToString());

            }
            comboproduct.AutoCompleteCustomSource = mycollcol;


            cn.Close();

            cn.Open();
            SqlCommand cmd1 = new SqlCommand("Select p_code from product_table where company='" + combocompany.Text + "'", cn);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            AutoCompleteStringCollection mycollcol1 = new AutoCompleteStringCollection();
            while (dr1.Read())
            {

                mycollcol1.Add(dr1["p_code"].ToString());
            }

            textpidc.AutoCompleteCustomSource = mycollcol1;

            cn.Close();
        }
        public void calculate1()
        {
            float a, b, c, d, e;
            float.TryParse(textQuantity.Text, out a);
            float.TryParse(textamount.Text, out b);
            float.TryParse(labeldp.Text, out c);
            float.TryParse(labeltp.Text, out d);
            float.TryParse(labelamount.Text, out e);

            b = a * c;
            e = a * d;

            textamount.Text = b.ToString("0.00");
            labelamount.Text = e.ToString("0.00");
        }
        public void grosstotal()
        {
            float a, b, c;
            float.TryParse(texttotal.Text, out a);
            float.TryParse(textparchent.Text, out b);
            float.TryParse(textnett.Text, out c);


            c = a - b;


            textnett.Text = c.ToString("0.00");

        }
        private void comboproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand("Select * from product_table where p_name='" + comboproduct.Text + "' and status='Active' order By p_name ASC", cn);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                textpidc.Text = (dr["p_code"].ToString());
                label33.Text = (dr["weight"].ToString());
                label34.Text = (dr["upc"].ToString());
                labeldp.Text = Convert.ToDouble(dr["Dp_Unitprice"].ToString()).ToString("0.00");
                labeltp.Text = Convert.ToDouble(dr["Tp_Unitprice"].ToString()).ToString("0.00");

            }



            cn.Close();
        }

        private void textQuantity_TextChanged(object sender, EventArgs e)
        {
            calculate();
        }

        private void labeldp_TextChanged(object sender, EventArgs e)
        {
            calculate();
        }
        private string GeneratOrderNumber()
        {
            string Ordernumber;
            Random rnd = new Random();
            long orderpart1 = rnd.Next(10, 99999);
            //int orderpart2 = rnd.Next(1, 10);
            Ordernumber = "500" + orderpart1;

            textorder.Text = Ordernumber;
            return Ordernumber;

        }
        private void labeltp_TextChanged(object sender, EventArgs e)
        {
            calculate();
        }
        public void loaduser()
        {
            comboproduct.Items.Clear();
            cn.Open();
            SqlCommand cmd = new SqlCommand("Select * from Login_new", cn);
            SqlDataReader dr = cmd.ExecuteReader();
            AutoCompleteStringCollection mycollcol = new AutoCompleteStringCollection();
            while (dr.Read())
            {
                combouser.Items.Add(dr["username"].ToString());
                mycollcol.Add(dr["username"].ToString());

            }
            combouser.AutoCompleteCustomSource = mycollcol;

            cn.Close();
        }
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells["sl"].Value = (e.RowIndex + 1).ToString();
        }
       
        private void textpidc_TextChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.e_purchaseConnectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from product_table where p_code LIKE'" + textpidc.Text + "' and status='Active' order By p_name ASC", conn);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comboproduct.Text = (dr["p_name"].ToString());
                labeldp.Text = (dr["Dp_Unitprice"].ToString());
                labeltp.Text = (dr["Tp_Unitprice"].ToString());

            }



            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textpidc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textQuantity.Focus();
            }
        }

        private void textQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnadd.Focus();
            }
        }

        private void comboproduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textQuantity.Focus();
            }
        }

        private void textQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textnett_TextChanged(object sender, EventArgs e)
        {
            grosstotal();
        }

        private void textdiscount_TextChanged(object sender, EventArgs e)
        {
            grosstotal();
        }

        private void texttotal_TextChanged(object sender, EventArgs e)
        {
            grosstotal();
            offer();
        }
        public void textclear()
        {
            textpidc.Clear();
            comboproduct.Text = "--Select--";
            textQuantity.Clear();
            textamount.Clear();
            labeldp.Text = "0.00";
            labeltp.Text = "0.00";
            labelamount.Text = "0.00";



        }

        public void datagricalculate()
        {
            double total1 = 0;
            double total2 = 0;
            double total3 = 0;
            double total4 = 0;
            double total5 = 0;
            double total6 = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

               
                total6 += Convert.ToDouble(dataGridView1.Rows[i].Cells["qty"].Value.ToString());
                total1 += Convert.ToDouble(dataGridView1.Rows[i].Cells["amount"].Value.ToString());
                total3 += Convert.ToDouble(dataGridView1.Rows[i].Cells["tptotal"].Value.ToString());
                //total4 += Convert.ToDouble(dataGridView1.Rows[i].Cells["qtyb"].Value.ToString());
                total5 += Convert.ToDouble(dataGridView1.Rows[i].Cells["fAmount"].Value.ToString());
                

            }
            texttotal.Text = total1.ToString("0.00");
            lblqty.Text = total6.ToString("0");
            totaltp.Text = total3.ToString("0.00");
           
            lblfamount.Text = total5.ToString("0.00");
            
        }
        public void calculate()
        {
            float a, b, c, d, e;
            float.TryParse(textQuantity.Text, out a);
            float.TryParse(textamount.Text, out b);
            float.TryParse(labeldp.Text, out c);
            float.TryParse(labeltp.Text, out d);
            float.TryParse(labelamount.Text, out e);

            b = a * c;
            e = a * d;

            textamount.Text = b.ToString("0.00");
            labelamount.Text = e.ToString("0.00");
        }


        int indexrow;
        private void button3_Click(object sender, EventArgs e)
        {
            if (btnadd.Text == "Add Item")
            {
                if (textQuantity.Text == "" || comboproduct.Text == "--Select--")
                {
                    MessageBox.Show("Please Check Quantity Or Item", "Add Item", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show("Do you want to Add this Records", "Add Records", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        dataGridView1.Rows.Add(sl, textpidc.Text, comboproduct.Text, label33.Text, label34.Text, textQuantity.Text, "Sales", labeldp.Text, textamount.Text, labeltp.Text, labelamount.Text, 0.00, "Edit", "Delete");

                        datagricalculate();
                        textclear();
                    }
                }
            }
            else if (btnadd.Text == "Free Item")
            {
                if (textQuantity.Text == "" || comboproduct.Text == "--Select--")
                {
                    MessageBox.Show("Please Check Quantity Or Item", "Add Item", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show("Do you want to Add this Records", "Add Records", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        dataGridView1.Rows.Add(sl, textpidc.Text, comboproduct.Text, label33.Text, label34.Text, textQuantity.Text, "Free", labeldp.Text, 0.00, labeltp.Text, 0.00, textamount.Text, "Edit", "Delete");

                        datagricalculate();
                        textclear();

                    }

                }
            }
            else if (btnadd.Text == "Update")
            {

                if (textQuantity.Text == "" || comboproduct.Text == "--Select--")
                {
                    MessageBox.Show("Please Check Quantity Or Item", "Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show("Do you want to Update this Records", "Update Records", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {

                        DataGridViewRow row = dataGridView1.SelectedRows[indexrow];
                        row.Cells[1].Value = textpidc.Text;
                        row.Cells[2].Value = comboproduct.Text;
                        row.Cells[3].Value = label33.Text;
                        row.Cells[4].Value = label34.Text;
                        row.Cells[5].Value = textQuantity.Text;
                        row.Cells[7].Value = labeldp.Text;
                        row.Cells[8].Value = textamount.Text;
                        row.Cells[9].Value = labeltp.Text;
                        row.Cells[10].Value = labelamount.Text;
                        row.Cells[11].Value = 0.00;



                        //dataGridView1.Rows.Add(sl, textpidc.Text, comboproduct.Text, 0.00, textQuantity.Text, labeldp.Text, 0.00, labeltp.Text, 0.00, textamount.Text, "--", "Delete");

                        datagricalculate();
                        textclear();
                        btnadd.Text = "Add Item";
                    }
                }
            }

            else if (btnadd.Text == "F_Update")
            {

                if (textQuantity.Text == "" || comboproduct.Text == "--Select--")
                {
                    MessageBox.Show("Please Check Quantity Or Item", "Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show("Do you want to Update this Records", "Update Records", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {

                        DataGridViewRow row = dataGridView1.SelectedRows[indexrow];
                        row.Cells[1].Value = textpidc.Text;
                        row.Cells[2].Value = comboproduct.Text;
                        row.Cells[3].Value = label33.Text;
                        row.Cells[4].Value = label34.Text;

                        row.Cells[5].Value = textQuantity.Text;
                        row.Cells[7].Value = labeldp.Text;
                        row.Cells[8].Value = 0.00;
                        row.Cells[9].Value = labeltp.Text;
                        row.Cells[10].Value = 0.00;
                        row.Cells[11].Value = textamount.Text;



                        //dataGridView1.Rows.Add(sl, textpidc.Text, comboproduct.Text, 0.00, textQuantity.Text, labeldp.Text, 0.00, labeltp.Text, 0.00, textamount.Text, "--", "Delete");

                        datagricalculate();
                        textclear();
                        btnadd.Text = "Add Item";
                    }
                }
            }
        }

        private void dataGridView1_RowPostPaint_1(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            
        }

        private void dataGridView1_RowPostPaint_2(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells["sl"].Value = (e.RowIndex + 1).ToString();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            btnadd.Text = "Add Item";
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            btnadd.Text = "Free Item";
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string Colname = dataGridView1.Columns[e.ColumnIndex].Name;
            if (Colname == "edit")
            {
                textpidc.Text = dataGridView1[1, e.RowIndex].Value.ToString();
                comboproduct.Text = dataGridView1[2, e.RowIndex].Value.ToString();
                textQuantity.Text = dataGridView1[3, e.RowIndex].Value.ToString();

                datagricalculate();

                btnadd.Text = "Update";

            }
            else if (Colname == "delete")
            {
                if (MessageBox.Show("Do You Want to Delete This Records..", "Delete Records", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {

                    //dataGridView1.Rows.RemoveAt(e.RowIndex);
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        if (!row.IsNewRow) dataGridView1.Rows.Remove(row);

                    MessageBox.Show("This Recods Deleted Successfuly", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                datagricalculate();
                btnadd.Text = "Add Item";
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            btnadd.Text = "F_Update";
        }

        private void dataGridView1_RowPostPaint_3(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells["sl"].Value = (e.RowIndex + 1).ToString();
        }

        private void dataGridView1_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string Colname = dataGridView1.Columns[e.ColumnIndex].Name;
            if (Colname == "edit")
            {
                textpidc.Text = dataGridView1[1, e.RowIndex].Value.ToString();
                comboproduct.Text = dataGridView1[2, e.RowIndex].Value.ToString();
                textQuantity.Text = dataGridView1[5, e.RowIndex].Value.ToString();

                datagricalculate();

                btnadd.Text = "Update";

            }
            else if (Colname == "delete")
            {
                if (MessageBox.Show("Do You Want to Delete This Records..", "Delete Records", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {

                    //dataGridView1.Rows.RemoveAt(e.RowIndex);
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        if (!row.IsNewRow) dataGridView1.Rows.Remove(row);

                    MessageBox.Show("This Recods Deleted Successfuly", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                datagricalculate();
                btnadd.Text = "Add Item";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            float a, b, c, d;
            float.TryParse(texttotal.Text, out a);
            float.TryParse(textparchent.Text, out b);
            float.TryParse(parchent.Text, out c);
            float.TryParse(textnett.Text, out d);

            b = a * c / 100;
            textparchent.Text = b.ToString("0.00");

        }

        private void textoffer_TextChanged(object sender, EventArgs e)
        {
            offer();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT order_no FROM [dbo].[Order_Header] WHERE order_no='" + textorder.Text + "'", cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
            {
                MessageBox.Show("Already Exist Order No", "Existing Order No", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else
            {

                if (textorder.Text == "")
                {
                    MessageBox.Show("Please Check Order Number", "Order No Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
             
                else if (combocompany.Text == "--Select--")
                {
                    MessageBox.Show("Please Select Company Name", "Company", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (combouser.Text == "--Select--")
                {
                    MessageBox.Show("Please Select Username", "Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (texttotal.Text == "0.00")
                {
                    MessageBox.Show("Please check Total Taka", "Total", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                else
                {
                    if (MessageBox.Show("Do You Want To Add Order?", "Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Order_Header]([orderdate],[order_no],[company],[user_name],[Qty],[total_dp],[P_(%)],[discount(%)],[grand_total],[total_tp],[Free_Total]) VALUES('" + dateTimePicker1.Value.ToString() + "','" + textorder.Text + "','" + combocompany.Text + "','" + combouser.Text + "','" + lblqty.Text + "','" + texttotal.Text + "','" + parchent.Text + "','" + textparchent.Text + "','" + textnett.Text + "','" + totaltp.Text + "','" + lblfamount.Text + "')", cn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Save Successfully");
                        cn.Close();

                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            cn.Open();
                            SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[Order_RowHeader]([company_name],[orderdate],[order_no],[item_code],[item_name],[pack_size],[upc],[Quantity],[type],[unit_price],[total_dp],[unit_tpprice],[total_tp],[f_total]) VALUES('" + combocompany.Text + "','" + dateTimePicker1.Value.ToString() + "','" + textorder.Text + "','" + dataGridView1.Rows[i].Cells["pcode"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["Item"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["Weight"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["upc"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["qty"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["qtyb"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["Unit"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["amount"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["tp"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["tptotal"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["fAmount"].Value.ToString() + "')", cn);
                            cmd1.ExecuteNonQuery();
                            //MessageBox.Show("Data Save Successfully");
                            cn.Close();
                        }


                        for (int p = 0; p < dataGridView1.Rows.Count; p++)
                        {
                            cn.Open();
                            SqlCommand cmd2 = new SqlCommand("UPDATE [dbo].[product_table] SET [stock_qty]=stock_qty+'" + dataGridView1.Rows[p].Cells["qty"].Value.ToString() + "' WHERE [p_code]='" + dataGridView1.Rows[p].Cells["pcode"].Value.ToString() + "'", cn);
                            cmd2.ExecuteNonQuery();
                            //MessageBox.Show("Data Save Successfully");
                            cn.Close();
                        }
                        dataGridView1.Rows.Clear();

                        GeneratOrderNumber();
                        textamount.Clear();
                        combouser.Text = "--Select--";
                        combocompany.Text = "--Select--";
                        texttotal.Text = "0.00";
                        textparchent.Text = "0.00";
                        parchent.Text = "0.00";
                        textnett.Text = "0.00";
                        totaltp.Text = "0.00";
                        lblfamount.Text = "0.00";
                        lblqty.Text = "0";
                    }

                }
            }
        }
    }
}
