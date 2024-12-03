using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace e_Purchase
{
    public partial class Deshboard : Form
    {
        public Deshboard()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Loadfrm(new frmorder());
        }
        public void Loadfrm(object form)
        {
            if (this.mainpenel.Controls.Count > 0)
                this.mainpenel.Controls.RemoveAt(0);
            Form d = form as Form;
            d.TopLevel = false;
            d.Dock = DockStyle.Fill;
            this.mainpenel.Enabled = true;
            this.mainpenel.Controls.Add(d);
            this.mainpenel.Tag = d;
            d.Show();
        }

        private void btnhome_Click(object sender, EventArgs e)
        {
            Loadfrm(new frmhome());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Loadfrm(new frmpurchase(this));
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            Loadfrm(new frmlogin(this));
            btnhome.Enabled = false;
            buttonorder.Enabled = false;
            buttonpurchase.Enabled = false;
            btnaccount.Enabled = false;
            btnreport.Enabled = false;
            button4.Enabled = false;
            button6.Enabled = false;
        }

        private void Deshboard_Load(object sender, EventArgs e)
        {
            Loadfrm(new frmlogin(this));
            btnhome.Enabled = false;
            buttonorder.Enabled = false;
            buttonpurchase.Enabled = false;
            btnaccount.Enabled = false;
            btnreport.Enabled = false;
            button4.Enabled = false;
            button6.Enabled = false;


        }

        private void button6_Click(object sender, EventArgs e)
        {
            Loadfrm(new frmtrade());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Loadfrm(new frmstock());
        }

        private void btnaccount_Click(object sender, EventArgs e)
        {
            Loadfrm(new frmbilling());
        }

        private void btnreport_Click(object sender, EventArgs e)
        {
            Loadfrm(new frmsrdsr());
        }
    }
}
