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
    public partial class frmpurchase : Form
    {
        SqlConnection cn = new SqlConnection(Properties.Settings.Default.e_purchaseConnectionString);

        Deshboard frmdesh;
        public frmpurchase(Deshboard fg)
        {
            InitializeComponent();
            if (checkBox1.Checked == false)
            {
                dateTimePicker1.Enabled = false;
            }
            frmdesh = fg;
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
        public void depoautocomplate()
        {
            cn.Open();
            SqlCommand cmd1 = new SqlCommand("Select depo_name from depo_table order By depo_name ASC", cn);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            AutoCompleteStringCollection mycollcol1 = new AutoCompleteStringCollection();
            while (dr1.Read())
            {


                mycollcol1.Add(dr1["depo_name"].ToString());
            }

            textfactory.AutoCompleteCustomSource = mycollcol1;

            cn.Close();
        }
        public void transportautocomplate()
        {
            cn.Open();
            SqlCommand cmd1 = new SqlCommand("Select transport_name from transport_table order By transport_name ASC", cn);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            AutoCompleteStringCollection mycollcol1 = new AutoCompleteStringCollection();
            while (dr1.Read())
            {


                mycollcol1.Add(dr1["transport_name"].ToString());
            }

            textBox7.AutoCompleteCustomSource = mycollcol1;

            cn.Close();
        }
        private void frmpurchase_Load(object sender, EventArgs e)
        {
            loadcombo();
            transportautocomplate();
            depoautocomplate();
            panel3.Visible = false;
            panel2.Visible = false;
            loaduser();
            comboproduct.Text = "--Select--";
            combocompany.Text = "--Select--";
            combouser.Text = "--Select--";
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
        private void textQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        

        private void textamount_TextChanged(object sender, EventArgs e)
        {
            //calculate();
        }

        

       

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            calculatetotal();
        }

       

        private void textpidc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textQuantity.Focus();
            }
        }

        private void comboproduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textQuantity.Focus();
            }
        }

        private void textQuantity_KeyDown(object sender, KeyEventArgs e)
        {
           
        }
        int indexrow;
        
        public void textclear()
        {
            textpidc.Clear();
            comboproduct.Text = "--Select--";
            textQuantity.Clear();
            textamount.Clear();
            labeldp.Text = "0.00";
            labeltp.Text = "0.00";
            labelamount.Text = "0.00";
            labelqtypice.Text = "0.00";
            labelprice3.Text = "0.00";

            labelprice4.Text = "0.00";
            labeltotal3.Text = "0.00";
            labeltotal4.Text = "0.00";
            



        }

        public void datagricalculate()
        {
            double total1=0;
           
            double total3=0;
            double total4 = 0;
            double total5 =0;
            double total6 = 0;
            for (int i = 0; i< dataGridView1.Rows.Count; i++)
            {
                
                total1 += Convert.ToDouble(dataGridView1.Rows[i].Cells["total"].Value.ToString());
                total3 += Convert.ToDouble(dataGridView1.Rows[i].Cells["total2"].Value.ToString());

                total4 += Convert.ToDouble(dataGridView1.Rows[i].Cells["qtypice"].Value.ToString());
                total5 += Convert.ToDouble(dataGridView1.Rows[i].Cells["ftotal"].Value.ToString());
                total6 += Convert.ToDouble(dataGridView1.Rows[i].Cells["Quantity"].Value.ToString());

            }
            texttotal.Text = total1.ToString("0.00");
           
            labeltotal.Text = total3.ToString("0.00");
            labelqtypices.Text = total4.ToString("0");
            labelfamt.Text = total5.ToString("0.00");
            labeltotalqty.Text= total6.ToString("0");
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            //panelpurchase.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //panelpurchase.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

       

      

        private void textdepo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) & (Keys)e.KeyChar!=Keys.Back & e.KeyChar!=' ')
            {
                e.Handled = true;
            }
        }

        private void texttransport_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

       
        public void calculatetotal()
        {
            float a, b, c, d, e;
            float.TryParse(texttotal.Text, out a);
            float.TryParse(textdiscount.Text, out b);
            float.TryParse(textparchent.Text, out c);
            float.TryParse(texttaxvalue.Text, out d);
            float.TryParse(textnett.Text, out e);

            e = a -b-c+d;
            

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

        private void textBox3_TextChanged_1(object sender, EventArgs e)
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

       

        private void texttotal_TextChanged(object sender, EventArgs e)
        {
            calculatetotal();
            calculateparchent();

            float a, b, c, d, f;
            float.TryParse(texttotal.Text, out a);
            float.TryParse(textdiscount.Text, out b);
            float.TryParse(textparchent.Text, out c);
            float.TryParse(textperchent.Text, out d);

            float.TryParse(label17.Text, out f);

            c = f * d / 100;


            textparchent.Text = c.ToString("0.00");
        }

      

        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //string Colname = dataGridView1.Columns[e.ColumnIndex].Name;
            //if(Colname=="edit")
            //{
            //    textpidc.Text = dataGridView1[1, e.RowIndex].Value.ToString();
            //    comboproduct.Text = dataGridView1[2, e.RowIndex].Value.ToString();
            //    textQuantity.Text = dataGridView1[3, e.RowIndex].Value.ToString();

            //    datagricalculate();

            //    btnadd.Text = "Update";

            //}
            //else if(Colname == "delete")
            //{
            //    if(MessageBox.Show("Do You Want to Delete This Records..","Delete Records", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            //    {

            //        //dataGridView1.Rows.RemoveAt(e.RowIndex);
            //        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            //            if (!row.IsNewRow) dataGridView1.Rows.Remove(row);
                    
            //            MessageBox.Show("This Recods Deleted Successfuly","Deleted",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    
            //    }
            //    datagricalculate();
            //    btnadd.Text = "Add Item";
            //}
        }

      
        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }
        public void cleartext()
        {
            textinvoice.Clear();
            combocompany.Text = "--Select--";
            combouser.Text = "--Select--";
            texttransport.Clear();
            textdriver.Clear();
            textvehicle.Clear();
            textfactory.Clear();
            texttotal.Text = "0.00";
            textdiscount.Text = "0.00";
            textparchent.Text = "0.00";
            texttaxvalue.Text = "0.00";
            textnett.Text = "0.00";
            textinvoicetk.Clear();
            labeltotalqty.Text = "0";
            labelfamt.Text = "0.00";

        }
        
        public void adfrm(object form)
        {
            if (this.panelpurchase.Controls.Count > 0)
                this.panelpurchase.Controls.RemoveAt(0);
            Form f = form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.panelpurchase.Enabled = true;
            this.panelpurchase.Controls.Add(f);
            this.panelpurchase.Tag = f;
            f.Show();
        }
        private void button7_Click_1(object sender, EventArgs e)
        {
           
        }

        private void button7_Click_2(object sender, EventArgs e)
        {
            frmdesh.Loadfrm(new frmloadinv());
           
            //frmloadinv ld = new frmloadinv();
            //ld.Show();
            //this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void btnsave_Click_1(object sender, EventArgs e)
        {
            DateTime dat = DateTime.Now;

            SqlDataAdapter da = new SqlDataAdapter("SELECT [invoice_no] FROM [dbo].[BuyInvoice_Header] WHERE invoice_no='" + textinvoice.Text + "'", cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
            {
                MessageBox.Show("Already Exist Invoice No", "Existing Invoice No", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else
            {


                if (textinvoice.Text == "")
                {
                    MessageBox.Show("Please Check Invoice Number", "Invoice No Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dateTimePicker1.Enabled == false)
                {
                    MessageBox.Show("Please Check Invoice Date", "Invoice Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (combocompany.Text == "--Select--")
                {
                    MessageBox.Show("Please Select Company Name", "Company", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (combouser.Text == "--Select--")
                {
                    MessageBox.Show("Please Select Username", "Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (textdriver.Text == "")
                {
                    MessageBox.Show("Please Enter Driver Name", "Driver", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (textvehicle.Text == "")
                {
                    MessageBox.Show("Please Input Vehicle No", "Vehicle No", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (textBox7.Text == "")
                {
                    MessageBox.Show("Please Select Transport Name", "Transport Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (textfactory.Text == "")
                {
                    MessageBox.Show("Please Select Depo", "Depo Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (textinvoicetk.Text == "")
                {
                    MessageBox.Show("Add Invoice Amount", "Invoice Amount", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show("Do You Want To Add Purchase Invoice?", "Invoice Receive", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[BuyInvoice_Header]([recdate],[invoice_no],[invoice_date],[company],[user_name],[driver_name],[vehicle_no],[transport_name],[depo_factory],[Qty_caton],[Qty_pice],[total_dp],[discount],[P_(%)],[discount(%)],[taxvalue],[grand_total],[total_tp],[invoce_value],[Free_Total],[price3],[price4],[total3],[total4]) VALUES('" + dat + "','" + textinvoice.Text + "','" + dateTimePicker1.Value.ToString() + "','" + combocompany.Text + "','" + combouser.Text + "','" + textdriver.Text + "','" + textvehicle.Text + "','" + textBox7.Text + "','" + textfactory.Text + "','" + labeltotalqty.Text + "','"+ labelqtypices.Text+ "','" + texttotal.Text + "','" + textdiscount.Text + "','" + textperchent.Text + "','" + textparchent.Text + "','" + texttaxvalue.Text + "','" + textnett.Text + "','" + labeltotal.Text + "','" + textinvoicetk.Text + "','" + labelfamt.Text + "','" + labelprice3.Text+ "','" + labelprice4.Text+ "','" + labeltotal3.Text+ "','" + labeltotal4.Text+ "')", cn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Save Successfully");
                        cn.Close();

                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            cn.Open();
                            SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[Buyinvoice_RowHeader]([company_name],[recbdate],[invoice_no],[invoice_date],[item_code],[item_name],[pack_size],[upc],[Qtycaton],[Qtypice],[type],[unit_price],[total_dp],[unit_tpprice],[total_tp],[f_total],[price3],[price4],[total3],[total4]) VALUES('" + combocompany.Text + "','" + dat + "','" + textinvoice.Text + "','" + dateTimePicker1.Value.ToString() + "','" + dataGridView1.Rows[i].Cells["pid"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["itemname"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["weiht"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["uom"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["quantity"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["qtypice"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["typ"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["price"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["total"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["price2"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["total2"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["ftotal"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["price3"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["price4"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["total3"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["total4"].Value.ToString() + "')", cn);
                            cmd1.ExecuteNonQuery();
                            //MessageBox.Show("Data Save Successfully");
                            cn.Close();
                        }


                        for (int p = 0; p < dataGridView1.Rows.Count; p++)
                        {
                            cn.Open();
                            SqlCommand cmd2 = new SqlCommand("UPDATE [dbo].[product_table] SET [stock_qty]=stock_qty-'" + dataGridView1.Rows[p].Cells["quantity"].Value.ToString() + "' WHERE [p_code]='" + dataGridView1.Rows[p].Cells["pid"].Value.ToString() + "'", cn);
                            cmd2.ExecuteNonQuery();
                            //MessageBox.Show("Data Save Successfully");
                            cn.Close();
                        }
                        dataGridView1.Rows.Clear();
                        cleartext();
                    }

                }
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void combocompany_SelectedIndexChanged_1(object sender, EventArgs e)
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
                //mycollcol.Add(dr["p_code"].ToString());
            }
            comboproduct.AutoCompleteCustomSource = mycollcol;
            //textpidc.AutoCompleteCustomSource = mycollcol;

            cn.Close();


            cn.Open();
            SqlCommand cmd1 = new SqlCommand("Select * from product_table where company='" + combocompany.Text + "' and status='Active' order By p_name ASC", cn);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            AutoCompleteStringCollection mycollcol1 = new AutoCompleteStringCollection();
            while (dr1.Read())
            {


                mycollcol1.Add(dr1["p_code"].ToString());
            }

            textpidc.AutoCompleteCustomSource = mycollcol1;

            cn.Close();


            combocompany.Enabled = false;
        }

        private void comboproduct_SelectedIndexChanged_1(object sender, EventArgs e)
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
                labelprice3.Text = Convert.ToDouble(dr["DP_Price"].ToString()).ToString("0.00");
                labelprice4.Text = Convert.ToDouble(dr["Sale_Price"].ToString()).ToString("0.00");

            }



            cn.Close();
        }
        public void calculatetotal1()
        {
            float a, b, c, d, e, f, g, i;
            float.TryParse(label34.Text, out a);
            float.TryParse(textQuantity.Text, out b);
            float.TryParse(labelqtypice.Text, out c);
            float.TryParse(labelprice3.Text, out d);
            float.TryParse(labelprice4.Text, out e);
            float.TryParse(labeltotal3.Text, out f);
            float.TryParse(labeltotal4.Text, out g);
            c = a * b;
            f = c * d;
            g = c * e;
           
            labelqtypice.Text = c.ToString("0");
            labeltotal3.Text = f.ToString("0.00");
            labeltotal4.Text = g.ToString("0.00");
        }
        private void btnadd_Click_1(object sender, EventArgs e)
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
                        dataGridView1.Rows.Add(sl, textpidc.Text, comboproduct.Text, label33.Text, label34.Text, textQuantity.Text,labelqtypice.Text, "Sales", labeldp.Text, textamount.Text, labeltp.Text, labelamount.Text, 0.00, labelprice3.Text, labelprice4.Text, labeltotal3.Text, labeltotal4.Text, "Edit", "Delete");

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
                        dataGridView1.Rows.Add(sl, textpidc.Text, comboproduct.Text, label33.Text, label34.Text, textQuantity.Text, labelqtypice.Text, "Free", labeldp.Text, 0.00, labeltp.Text, 0.00, textamount.Text, labelprice3.Text, labelprice4.Text,0.00,0.00, "Edit", "Delete");

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
                        row.Cells[6].Value = labelqtypice.Text;
                        row.Cells[8].Value = labeldp.Text;
                        row.Cells[9].Value = textamount.Text;
                        row.Cells[10].Value = labeltp.Text;
                        row.Cells[11].Value = labelamount.Text;
                        row.Cells[12].Value = 0.00;
                        row.Cells[13].Value = labelprice3.Text;
                        row.Cells[14].Value = labelprice4.Text;
                        row.Cells[15].Value = labeltotal3.Text;
                        row.Cells[16].Value = labeltotal4.Text;



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
                        row.Cells[6].Value = labelqtypice.Text;
                        row.Cells[8].Value = labeldp.Text;
                        row.Cells[9].Value = 0.00;
                        row.Cells[10].Value = labeltp.Text;
                        row.Cells[11].Value = 0.00;
                        row.Cells[12].Value = textamount.Text;
                        row.Cells[13].Value = labelprice3.Text;
                        row.Cells[14].Value = labelprice4.Text;
                        row.Cells[15].Value = 0.00;
                        row.Cells[16].Value = 0.00;


                        //dataGridView1.Rows.Add(sl, textpidc.Text, comboproduct.Text, 0.00, textQuantity.Text, labeldp.Text, 0.00, labeltp.Text, 0.00, textamount.Text, "--", "Delete");

                        datagricalculate();
                        textclear();
                        btnadd.Text = "Add Item";
                    }
                }
            }
        }

        private void textQuantity_TextChanged_1(object sender, EventArgs e)
        {
            calculate();
            calculatetotal1();
        }

        private void textdiscount_TextChanged_1(object sender, EventArgs e)
        {
            calculatetotal();
            calculateparchent();

            float a, b, c, d, f;
            float.TryParse(texttotal.Text, out a);
            float.TryParse(textdiscount.Text, out b);
            float.TryParse(textparchent.Text, out c);
            float.TryParse(textperchent.Text, out d);

            float.TryParse(label17.Text, out f);

            c = f * d / 100;


            textparchent.Text = c.ToString("0.00");
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                combocompany.Enabled = true;
            }
            else
            {
                combocompany.Enabled = false;
            }
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            btnadd.Text = "Free Item";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            btnadd.Text = "Add Item";
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells["sl"].Value = (e.RowIndex + 1).ToString();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void textparchent_TextChanged_1(object sender, EventArgs e)
        {
            calculatetotal();
        }

        private void labeldp_TextChanged_1(object sender, EventArgs e)
        {
            calculate();
        }

        private void labeltp_TextChanged_1(object sender, EventArgs e)
        {
            calculate();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            if (textdepo.Text == "")
            {
                MessageBox.Show("Please Check Depo", "Add Depo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Do you want to Save this Records", "Save Records", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[depo_table] ([depo_name]) VALUES('" + textdepo.Text + "')", cn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Save Successfully", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cn.Close();
                    textdepo.Clear();
                }
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (texttransport.Text == "")
            {
                MessageBox.Show("Please Check Depo", "Add Depo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Do you want to Save this Records", "Save Records", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[transport_table] ([transport_name]) VALUES('" + texttransport.Text + "')", cn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Save Successfully", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cn.Close();
                    texttransport.Clear();
                }
            }
        }

        private void textpidc_TextChanged_1(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.e_purchaseConnectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from product_table where p_code LIKE'" + textpidc.Text + "' and status='Active' order By p_name ASC", conn);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comboproduct.Text = (dr["p_name"].ToString());
                label33.Text = (dr["weight"].ToString());
                label34.Text = (dr["upc"].ToString());
                labeldp.Text = Convert.ToDouble(dr["Dp_Unitprice"].ToString()).ToString("0.00");
                labeltp.Text = Convert.ToDouble(dr["Tp_Unitprice"].ToString()).ToString("0.00"); ;

            }



            conn.Close();
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                dateTimePicker1.Enabled = true;
            }
            else
            {
                dateTimePicker1.Enabled = false;
            }
        }

        private void textamount_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowPostPaint_1(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells["sln"].Value = (e.RowIndex + 1).ToString();
        }

        private void dataGridView1_CellContentDoubleClick_2(object sender, DataGridViewCellEventArgs e)
        {
            string Colname = dataGridView1.Columns[e.ColumnIndex].Name;
            if (Colname == "btnedit")
            {
                textpidc.Text = dataGridView1[1, e.RowIndex].Value.ToString();
                comboproduct.Text = dataGridView1[2, e.RowIndex].Value.ToString();
                textQuantity.Text = dataGridView1[5, e.RowIndex].Value.ToString();

                datagricalculate();

                btnadd.Text = "Update";

            }
            else if (Colname == "btmdelete")
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

        private void textQuantity_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnadd.Focus();
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void textpidc_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textQuantity.Focus();
            }
        }

        private void comboproduct_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textQuantity.Focus();
            }
        }

        private void texttotal_TextChanged_1(object sender, EventArgs e)
        {
            calculatetotal();
            calculateparchent();

            //float a, b, c, d, f;
            //float.TryParse(texttotal.Text, out a);
            //float.TryParse(textdiscount.Text, out b);
            //float.TryParse(textparchent.Text, out c);
            //float.TryParse(textperchent.Text, out d);

            //float.TryParse(label17.Text, out f);

            //c = f * d / 100;


            //textparchent.Text = c.ToString("0.00");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            btnadd.Text = "F_Update";
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

        private void texttaxvalue_TextChanged(object sender, EventArgs e)
        {
            calculatetotal();
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    row.Cells["qtypice"].Value = Convert.ToDouble(row.Cells["quantity"].Value) * Convert.ToDouble(row.Cells["uom"].Value);
            //    row.Cells["total3"].Value = Convert.ToDouble(row.Cells["qtypice"].Value) * Convert.ToDouble(row.Cells["price3"].Value);
            //    row.Cells["total4"].Value = Convert.ToDouble(row.Cells["qtypice"].Value) * Convert.ToDouble(row.Cells["price4"].Value);
            //}
        }

        private void labelqtypice_TextChanged(object sender, EventArgs e)
        {
            calculatetotal1();
        }

        private void labelprice3_TextChanged(object sender, EventArgs e)
        {
            calculatetotal1();
        }

        private void labelprice4_TextAlignChanged(object sender, EventArgs e)
        {
            calculatetotal1();
        }
    }
}
