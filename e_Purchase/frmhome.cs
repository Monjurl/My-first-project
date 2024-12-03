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
    public partial class frmhome : Form
    {
        public frmhome()
        {
            InitializeComponent();
        }
        public void Loadfrm(object form)
        {
            if (this.panelhome.Controls.Count > 0)
                this.panelhome.Controls.RemoveAt(0);
            Form f = form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.panelhome.Controls.Add(f);
            this.panelhome.Tag = f;
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Loadfrm(new frmcompany());
        }
    }
}
