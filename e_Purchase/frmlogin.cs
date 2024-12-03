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
    public partial class frmlogin : Form
    {
        SqlConnection con = new SqlConnection(Properties.Settings.Default.e_purchaseConnectionString);

        Deshboard f1;
        public frmlogin(Deshboard frm)
        {
            InitializeComponent();
            this.f1 = frm;
            panel2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username, password;
            username = textuser.Text;
            password = textpass.Text;

            try
            {
                String Quary= "SELECT [username],[password]FROM[dbo].[Login_new] WHERE username='"+textuser.Text+"' and password='"+textpass.Text+"'";
                SqlDataAdapter sda = new SqlDataAdapter(Quary, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    username = textuser.Text;
                    password = textpass.Text;


                    f1.btnhome.Enabled = true;
                    f1.buttonorder.Enabled = true;
                    f1.buttonpurchase.Enabled = true;
                    f1.btnaccount.Enabled = true;
                    f1.btnreport.Enabled = true;
                    f1.button4.Enabled = true;
                    f1.button6.Enabled = true;

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password", "Wrong Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                con.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
            panel2.Visible = true;
            panel1.Visible = false;
            textusername.Enabled = false;
            textpassw.Enabled = false;
            textcellno.Enabled = false;
            
            linkLabel3.Enabled = true;
            textunlock.Enabled = true;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel2.Visible = false;
            panel1.Visible = true;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string username, password;
            //username = textuser.Text;
            password = textpass.Text;

            try
            {
                String Quary = "SELECT [password]FROM[dbo].[Login_new] WHERE password='"+ textunlock.Text +"'";
                SqlDataAdapter sda = new SqlDataAdapter(Quary, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    //username = textuser.Text;
                    password = textpass.Text;

                    textusername.Enabled = true;
                    textpassw.Enabled = true;
                    textcellno.Enabled = true;
                    
                   
                    linkLabel3.Enabled = false;
                    textunlock.Enabled = false;
                    textunlock.Clear();
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password", "Wrong Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                con.Close();
            }
        }

        private void frmlogin_Load(object sender, EventArgs e)
        {
            
        }
    }
}
