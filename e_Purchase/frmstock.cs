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
    public partial class frmstock : Form
    {
        SqlConnection cn = new SqlConnection(Properties.Settings.Default.e_purchaseConnectionString);
        public frmstock()
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
        private void frmstock_Load(object sender, EventArgs e)
        {
            loadcombo();
            combocompany.Text = "--Select--";
        }

        private void btngo_Click(object sender, EventArgs e)
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            SqlCommand cmd = new SqlCommand("Select * from product_table where company='" + combocompany.Text + "' and status='Active' Order by p_name ASC", cn);
            SqlDataReader dr = cmd.ExecuteReader();
           
            while (dr.Read())
            {
                i += 1;
                dataGridView1.Rows.Add(i, dr["p_code"].ToString(), dr["p_name"].ToString(), dr["weight"].ToString(), dr["upc"].ToString(), Convert.ToDouble(dr["stock_qty"].ToString()).ToString("0"), Convert.ToDouble(dr["dp_unitprice"].ToString()).ToString("0.00"), 0.00, Convert.ToDouble(dr["tp_unitprice"].ToString()).ToString("0.00"),0.00, 0.0, Convert.ToDouble(dr["S_Quantity"].ToString()).ToString("0"), Convert.ToDouble(dr["DP_Price"].ToString()).ToString("0.00"), Convert.ToDouble(dr["Sale_Price"].ToString()).ToString("0.00"), 0.00,0.00);

            }


            cn.Close();
            double totalp = 0;
            double totaltaka = 0;
            double totalpqt = 0;
            double totalpice = 0;
            double totalptk = 0;

            for(int q=0; q<dataGridView1.Rows.Count; q++)
            {
                totalp += Convert.ToDouble(dataGridView1.Rows[q].Cells["quantity"].Value);
                totaltaka += Convert.ToDouble(dataGridView1.Rows[q].Cells["total"].Value);
                totalpqt += Convert.ToDouble(dataGridView1.Rows[q].Cells["qtypice"].Value);
                totalpice += Convert.ToDouble(dataGridView1.Rows[q].Cells["Stockps"].Value);
                totalptk += Convert.ToDouble(dataGridView1.Rows[q].Cells["total3"].Value);
            }
            label2.Text = "Pending :" + totalp.ToString("0")+ "/" + "Ks";
            label3.Text = totaltaka.ToString("0.00")+"/"+ "Tk";
            label4.Text = "Stock :" + totalpqt.ToString("0.00")+ "/" + "Ks";
            label5.Text ="Stock :"+ totalpice.ToString("0")+ "/" + "Ps";
            label6.Text = totalptk.ToString("0.00")+ "/" + "Tk";

        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //double totalp = 0;
            //double qtycase = 0;
            //double totaldprice = 0;
            //double totaltprice = 0;



            foreach (DataGridViewRow Row in dataGridView1.Rows)
            {
                Row.Cells["total"].Value = Convert.ToDouble(Row.Cells["quantity"].Value) * Convert.ToDouble(Row.Cells["price"].Value);
                Row.Cells["total2"].Value = Convert.ToDouble(Row.Cells["quantity"].Value) * Convert.ToDouble(Row.Cells["price2"].Value);
                Row.Cells["qtypice"].Value = Convert.ToDouble(Row.Cells["Stockps"].Value) / Convert.ToDouble(Row.Cells["uom"].Value);
                Row.Cells["total3"].Value = Convert.ToDouble(Row.Cells["Stockps"].Value) * Convert.ToDouble(Row.Cells["price3"].Value);
                Row.Cells["total4"].Value = Convert.ToDouble(Row.Cells["Stockps"].Value) * Convert.ToDouble(Row.Cells["price4"].Value);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
