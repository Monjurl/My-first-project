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
    public partial class frmstockchalan : Form
    {
        SqlConnection cn = new SqlConnection(Properties.Settings.Default.e_purchaseConnectionString);
        frmloadinv dfg;
        public frmstockchalan(frmloadinv flis)
        {
            InitializeComponent();
            dfg = flis;
        }
        public void loadcalculateion()
        {
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //dataGridView1.Rows.Clear();
            //int i;

            //cn.Open();
            //SqlCommand cmd = new SqlCommand("Select * FROM [dbo].[Buyinvoice_RowHeader] Where invoice_no='"+ textinvsearc.Text+ "'", cn);
            //cmd.ExecuteNonQuery();
            //SqlDataReader dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{
            //    i =+ 1;
            //    dataGridView1.Rows.Add(i, dr["item_code"].ToString(), dr["item_name"].ToString(), dr["pack_size"].ToString(), dr["upc"].ToString(), dr["Qtycaton"].ToString(), dr["Qtypice"].ToString(), dr["type"].ToString(), Convert.ToDouble(dr["price3"].ToString()).ToString("0.00"), Convert.ToDouble(dr["total3"].ToString()).ToString("0.00"), Convert.ToDouble(dr["price4"].ToString()).ToString("0.00"), Convert.ToDouble(dr["total4"].ToString()).ToString("0.00"), Convert.ToDouble(dr["f_total"].ToString()).ToString("0.00"));
            //}
            //cn.Close();
            //datagricalculate();
        }

        private void frmstockchalan_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        public void datagricalculate()
        {
            double total1 = 0;

            double total3 = 0;
            double total4 = 0;
            double total5 = 0;
            double total6 = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                total1 += Convert.ToDouble(dataGridView1.Rows[i].Cells["total"].Value.ToString());
                total3 += Convert.ToDouble(dataGridView1.Rows[i].Cells["total2"].Value.ToString());
                total6 += Convert.ToDouble(dataGridView1.Rows[i].Cells["quantity"].Value.ToString());
                total4 += Convert.ToDouble(dataGridView1.Rows[i].Cells["qtypice"].Value.ToString());
                total5 += Convert.ToDouble(dataGridView1.Rows[i].Cells["ftotal"].Value.ToString());
                

            }
            texttotal.Text = total1.ToString("0.00");

            label5.Text = total3.ToString("0.00");
            labeltotalqty.Text = total6.ToString("0");
            labelfamt.Text = total5.ToString("0.00");
            totalpice.Text = total4.ToString("0");
        }
        public void calculatetotal()
        {
            float a, b, c, d, e;
            float.TryParse(texttotal.Text, out a);
            float.TryParse(textdiscount.Text, out b);
            float.TryParse(textparchent.Text, out c);
            float.TryParse(texttaxvalue.Text, out d);
            float.TryParse(textnett.Text, out e);

            e = a - b - c + d;


            textnett.Text = e.ToString("0.00");

        }
        public void calculateparchent()
        {
            float a, b, c, d, e;
            float.TryParse(texttotal.Text, out a);
            float.TryParse(textdiscount.Text, out b);
            float.TryParse(textparchent.Text, out c);
            float.TryParse(textperchent.Text, out d);

            float.TryParse(label17.Text, out e);

            e = a - b;


            label17.Text = e.ToString("0.00");

        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
           
        }

        private void texttotal_TextChanged(object sender, EventArgs e)
        {
            calculatetotal();
            calculateparchent();
        }

        private void textdiscount_TextChanged(object sender, EventArgs e)
        {
            calculatetotal();
        }

        private void textparchent_TextChanged(object sender, EventArgs e)
        {
            calculatetotal();
        }

        private void texttaxvalue_TextChanged(object sender, EventArgs e)
        {
            calculatetotal();
        }

        private void textperchent_TextChanged(object sender, EventArgs e)
        {
            float a, b, c, d, f;
            float.TryParse(texttotal.Text, out a);
            float.TryParse(textdiscount.Text, out b);
            float.TryParse(textparchent.Text, out c);
            float.TryParse(textperchent.Text, out d);

            float.TryParse(label17.Text, out f);

            c = f * d / 100;


            textparchent.Text = c.ToString("0.00");
        }

        private void textinvoice_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i=0;

            cn.Open();
            SqlCommand cmd = new SqlCommand("Select * FROM [dbo].[Buyinvoice_RowHeader] Where invoice_no='" + textinvoice.Text + "'", cn);
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i+=1;
                dataGridView1.Rows.Add(i, dr["item_code"].ToString(), dr["item_name"].ToString(), dr["pack_size"].ToString(), dr["upc"].ToString(), dr["Qtycaton"].ToString(), dr["Qtypice"].ToString(), dr["type"].ToString(), Convert.ToDouble(dr["price3"].ToString()).ToString("0.00"), Convert.ToDouble(dr["total3"].ToString()).ToString("0.00"), Convert.ToDouble(dr["price4"].ToString()).ToString("0.00"), Convert.ToDouble(dr["total4"].ToString()).ToString("0.00"), Convert.ToDouble(dr["f_total"].ToString()).ToString("0.00"));
            }
            cn.Close();
            datagricalculate();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            DateTime dat = DateTime.Now;

            SqlDataAdapter da = new SqlDataAdapter("SELECT [invoice_no] FROM [dbo].[StockadjustInvoice_Header] WHERE invoice_no='" + textinvoice.Text + "'", cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
            {
                MessageBox.Show("Already Received Invoice Number '"+textinvoice.Text+"'", "Existing Invoice No", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {


                if (textinvoice.Text == "")
                {
                    MessageBox.Show("Please Check Invoice Number", "Invoice No Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (textdate.Text=="")
                {
                    MessageBox.Show("Please Check Invoice Date", "Invoice Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (textcompany.Text == "")
                {
                    MessageBox.Show("Please Select Company Name", "Company", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (textuser.Text == "")
                {
                    MessageBox.Show("Please Select Username", "Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
               
                else if (textinvsearc.Text == "")
                {
                    MessageBox.Show("Please Input Confirm Invoice Number", "Confirm Invoice Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
               
                else
                {
                    if (MessageBox.Show("Do You Want To Add Purchase Invoice?", "Invoice Receive", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[StockadjustInvoice_Header]([recdate],[invoice_no],[Confirminvoice_no],[invoice_date],[company],[user_name],[Qty_caton],[Qty_pice],[total_dp],[discount],[P_(%)],[discount(%)],[taxvalue],[grand_total],[Free_Total],[totaltp]) VALUES('" + dat + "','" + textinvoice.Text + "','" + textinvsearc.Text + "','" + textdate.Text + "','" + textcompany.Text + "','" + textuser.Text + "','" + labeltotalqty.Text + "','" + totalpice.Text + "','" + texttotal.Text + "','" + textdiscount.Text + "','" + textperchent.Text + "','" + textparchent.Text + "','" + texttaxvalue.Text + "','" + textnett.Text + "','" + labelfamt.Text + "','" + label5.Text + "')", cn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Save Successfully");
                        cn.Close();

                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            cn.Open();
                            SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[Stockadjustinvoice_RowHeader]([company_name],[recbdate],[invoice_no],[invoice_date],[item_code],[item_name],[pack_size],[upc],[Qtycaton],[Qtypice],[type],[price3],[total3],[unit_tpprice],[total_tp],[f_total]) VALUES('" + textcompany.Text + "','" + dat + "','" + textinvoice.Text + "','" + textdate.Text + "','" + dataGridView1.Rows[i].Cells["pid"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["itemname"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["weiht"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["uom"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["quantity"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["qtypice"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["typ"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["price"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["total"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["price2"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["total2"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["ftotal"].Value.ToString() + "')", cn);
                            cmd1.ExecuteNonQuery();
                            //MessageBox.Show("Data Save Successfully");
                            cn.Close();
                        }


                        for (int p = 0; p < dataGridView1.Rows.Count; p++)
                        {
                            cn.Open();
                            SqlCommand cmd2 = new SqlCommand("UPDATE [dbo].[product_table] SET [S_Quantity]=[S_Quantity]+'" + dataGridView1.Rows[p].Cells["qtypice"].Value.ToString() + "' WHERE [p_code]='" + dataGridView1.Rows[p].Cells["pid"].Value.ToString() + "'", cn);
                            cmd2.ExecuteNonQuery();
                            //MessageBox.Show("Data Save Successfully");
                            cn.Close();
                        }
                        dataGridView1.Rows.Clear();


                        cn.Open();
                        SqlCommand cmd3 = new SqlCommand("UPDATE [dbo].[BuyInvoice_Header] SET [Invoice_Status]='Invoice Has been Received'", cn);
                        cmd3.ExecuteNonQuery();
                        MessageBox.Show("Data Save Successfully");
                        cn.Close();

                        dfg.loadData();
                    }

                }
            }
        }

        private void textinvsearc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }
    }
}
