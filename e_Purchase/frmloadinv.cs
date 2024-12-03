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
    public partial class frmloadinv : Form
    {
        SqlConnection cn = new SqlConnection(Properties.Settings.Default.e_purchaseConnectionString);

 
        public frmloadinv()
        {
            InitializeComponent();
           
        }
        public void loadData()
        {
            dataGridView1.Rows.Clear();
            
            int i = 0;
            cn.Open();
            SqlCommand cmd = new SqlCommand("Select * FROM [dbo].[BuyInvoice_Header]  Where invoice_date Between'" + Convert.ToDateTime(dateTimePicker1.Value.ToString()).ToString("yyyy-MM-dd 00:00:00") + "' and '" + Convert.ToDateTime(dateTimePicker2.Value.ToString()).ToString("yyyy-MM-dd 23:59:59") + "'", cn);
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                i += 1;
                dataGridView1.Rows.Add(i, dr["invoice_no"].ToString(), Convert.ToDateTime(dr["invoice_date"].ToString()).ToString("dd/MM/yyyy"), dr["company"].ToString(), Convert.ToDouble(dr["Qty_caton"].ToString()).ToString("0"), Convert.ToDouble(dr["total_dp"].ToString()).ToString("0.00"), Convert.ToDouble(dr["discount"].ToString()).ToString("0.00"), Convert.ToDouble(dr["grand_total"].ToString()).ToString("0.00"), Convert.ToDouble(dr["Free_Total"].ToString()).ToString("0.00"), Convert.ToDouble(dr["invoce_value"].ToString()).ToString("0.00"), dr["user_name"].ToString(), dr["Invoice_Status"].ToString());
            }

            cn.Close();
        }
        private void frmloadinv_Load(object sender, EventArgs e)
        {
            loadData();
            loadcombo();
            combocompany.Text = "--Select--";
        }
       
        
        
        private void btnload_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            int i = 0;
            cn.Open();
            SqlCommand cmd = new SqlCommand("Select * FROM [dbo].[BuyInvoice_Header] Where invoice_date Between'" + Convert.ToDateTime(dateTimePicker1.Value.ToString()).ToString("yyyy-MM-dd 00:00:00") + "' and '" + Convert.ToDateTime(dateTimePicker2.Value.ToString()).ToString("yyyy-MM-dd 23:59:59") + "'", cn); ;
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                i +=1;
                dataGridView1.Rows.Add(i, dr["invoice_no"].ToString(), Convert.ToDateTime(dr["invoice_date"].ToString()).ToString("dd/MM/yyyy"), dr["company"].ToString(), Convert.ToDouble(dr["Qty_caton"].ToString()).ToString("0"), Convert.ToDouble(dr["total_dp"].ToString()).ToString("0.00"), Convert.ToDouble(dr["discount"].ToString()).ToString("0.00"), Convert.ToDouble(dr["grand_total"].ToString()).ToString("0.00"), Convert.ToDouble(dr["Free_Total"].ToString()).ToString("0.00"), Convert.ToDouble(dr["invoce_value"].ToString()).ToString("0.00"), dr["user_name"].ToString(), dr["Invoice_Status"].ToString());
            }

            cn.Close();
            double totalp = 0;
            double totaltaka = 0;
            double totalpqt = 0;
            double totalpice = 0;
            double totalptk = 0;
            double totalpt = 0;

            for (int q = 0; q < dataGridView1.Rows.Count; q++)
            {
                totalp += Convert.ToDouble(dataGridView1.Rows[q].Cells["Quantity"].Value);
                totaltaka += Convert.ToDouble(dataGridView1.Rows[q].Cells["Gross"].Value);
                totalpqt += Convert.ToDouble(dataGridView1.Rows[q].Cells["discount"].Value);
                totalpice += Convert.ToDouble(dataGridView1.Rows[q].Cells["Nett"].Value);
                totalptk += Convert.ToDouble(dataGridView1.Rows[q].Cells["Free"].Value);
                totalpt += Convert.ToDouble(dataGridView1.Rows[q].Cells["Invoice"].Value);
            }
            label3.Text = totalp.ToString("0") + "/" + "Ks";
            label4.Text = totaltaka.ToString("0.00") + "/" + "Tk";
            label5.Text = totalpqt.ToString("0.00") + "/" + "Tk";
            label6.Text =totalpice.ToString("0.00") + "/" + "Tk";
            label7.Text =totalptk.ToString("0.00") + "/" + "Tk";
            label8.Text = totalpt.ToString("0.00") + "/" + "Tk";

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
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            SqlCommand cmd = new SqlCommand("Select * FROM [dbo].[BuyInvoice_Header] Where invoice_date Between'" + Convert.ToDateTime(dateTimePicker1.Value.ToString()).ToString("yyyy-MM-dd 00:00:00") + "' and '" + Convert.ToDateTime(dateTimePicker2.Value.ToString()).ToString("yyyy-MM-dd 23:59:59") + "' and company='" + combocompany.Text + "'", cn); ;
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                i +=1;
                dataGridView1.Rows.Add(i, dr["invoice_no"].ToString(), Convert.ToDateTime(dr["invoice_date"].ToString()).ToString("dd/MM/yyyy"), dr["company"].ToString(), Convert.ToDouble(dr["Qty_caton"].ToString()).ToString("0"), Convert.ToDouble(dr["total_dp"].ToString()).ToString("0.00"), Convert.ToDouble(dr["discount"].ToString()).ToString("0.00"), Convert.ToDouble(dr["grand_total"].ToString()).ToString("0.00"), Convert.ToDouble(dr["Free_Total"].ToString()).ToString("0.00"), Convert.ToDouble(dr["invoce_value"].ToString()).ToString("0.00"), dr["user_name"].ToString(), dr["Invoice_Status"].ToString());
            }

            cn.Close();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["chalnstatus"].Value.ToString() == "Invoice Not Receive")
                {
                    dataGridView1.Rows[i].Cells["chalnstatus"].Style.BackColor = Color.Red;
                    dataGridView1.Rows[i].Cells["chalnstatus"].Style.ForeColor = Color.White;
                   
                }
                else if (dataGridView1.Rows[i].Cells["chalnstatus"].Value.ToString() == "Invoice Has been Received")
                {
                    dataGridView1.Rows[i].Cells["chalnstatus"].Style.BackColor = Color.DarkSeaGreen;
                    dataGridView1.Rows[i].Cells["chalnstatus"].Style.ForeColor = Color.White;
                }

            }


            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    if(row.Cells["chalnstatus"].Value.ToString()== "Invoice Has been Received")
            //    {
            //        row.DefaultCellStyle.BackColor = Color.Green;
            //    }
            //    else if (row.Cells["chalnstatus"].Value.ToString() == "Invoice Not Receive")
            //    {
            //        row.DefaultCellStyle.BackColor = Color.MediumVioletRed;
            //    }
            //}



        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            //{
            //    dataGridView1.CurrentRow.Selected = true;
            //    frmstockchalan f = new frmstockchalan(this);
            //    f.textinvoice.Text = dataGridView1["invno", e.RowIndex].Value.ToString();
            //    f.textdate.Text = dataGridView1["invdate", e.RowIndex].Value.ToString();
            //    f.textcompany.Text = dataGridView1["company", e.RowIndex].Value.ToString();
            //    f.textuser.Text = dataGridView1["user", e.RowIndex].Value.ToString();
            //    //f.textuser.Text = dataGridView1["invno", e.RowIndex].Value.ToString();
            //    f.Show();

            //}
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string colname = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colname == "invno")
            {
                frmstockchalan f = new frmstockchalan(this);
                f.textinvoice.Text = dataGridView1["invno", e.RowIndex].Value.ToString();
                f.textdate.Text = dataGridView1["invdate", e.RowIndex].Value.ToString();
                f.textcompany.Text = dataGridView1["company", e.RowIndex].Value.ToString();
                f.textuser.Text = dataGridView1["user", e.RowIndex].Value.ToString();
               
                f.ShowDialog();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    } 
}
