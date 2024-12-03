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
    public partial class frmedit : Form
    {
        SqlConnection cn = new SqlConnection(Properties.Settings.Default.e_purchaseConnectionString);
        frmcompany frm;
        public frmedit(frmcompany fm)
        {
            InitializeComponent();
            frm = fm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                textproduct.Enabled = true;
            }
            else
            {
                textproduct.Enabled = false;
            }           
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                textweight.Enabled = true;
            }
            else
            {
                textweight.Enabled = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
               textupc.Enabled = true;
            }
            else
            {
                textupc.Enabled = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                textdpprice.Enabled = true;
            }
            else
            {
                textdpprice.Enabled = false;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                texttpprice.Enabled = true;
            }
            else
            {
                texttpprice.Enabled = false;
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                comboBstatus.Enabled = true;
            }
            else
            {
                comboBstatus.Enabled = false;
            }
        }

        private void frmedit_Load(object sender, EventArgs e)
        {
            comboBstatus.Enabled = false;
            texttpprice.Enabled = false;
            textdpprice.Enabled = false;
            textdpprice.Enabled = false;
            textweight.Enabled = false;
            textproduct.Enabled = false;
            textupc.Enabled = false;
            textpid.Enabled = false;
            textfixed.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            cn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE [dbo].[product_table] SET [p_name] ='" + textproduct.Text + "' ,[weight] ='" + textweight.Text + "' ,[upc] ='" + textupc.Text + "' ,[dp_unitprice] ='" + textdpprice.Text + "' ,[tp_unitprice] ='" + texttpprice.Text + "',[Status] ='" + comboBstatus.Text + "',[Fixxed_price] ='" + textfixed.Text + "' WHERE  [p_code] = '" + textpid.Text + "'", cn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("This Record Updated.....", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            cn.Close();
            frm.LoadData();
            this.Close();

        }

        private void textdpprice_KeyDown(object sender, KeyEventArgs e)
        {
           
            
        }

        private void textweight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsLetterOrDigit(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
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

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked == true)
            {
                textfixed.Enabled = true;
            }
            else
            {
                textfixed.Enabled = false;
            }
        }
    }
}
