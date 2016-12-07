using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bartenderExpress
{
    public partial class BAC_Calculator : Form
    {
        public BAC_Calculator(Form1 parent)
        {
            InitializeComponent();

            MdiParent = parent;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bartender Express BAC Calculator Version 1.0\n Dedicated to Abby Connors\nCopyright © 2012 LollieSoft Inc.", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void calc(object sender, System.EventArgs e)
        {
            //Widmark Formula %BAC = (A x 5.14/W x r) - .015 x H
            double A = (double)(valDrinks.Value);
            double W = (double)(valWeight.Value);
            double H = (double)(valHours.Value);
            double rFem = 0.66;
            double rmale = 0.73;
            double BAC;

            if (radFemale.Checked)
            {
                BAC = (A * 5.14) / (W * rFem) - .015 * H;
            }
            else
            {
                BAC = (A * 5.14) / (W * rmale) - .015 * H;
            }
            if (BAC < 0)
            {
                BAC = 0.00;
            }
            lblBAC.Text = (BAC.ToString("0.#####"));
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.lolliesoft.com/");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://www.facebook.com/BartenderExpress");
            }
            catch { }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://twitter.com/lolliesoft");
            }
            catch { } 
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://pinterest.com/lolliesoft/bartender-express-software/");
            }
            catch { }
        }
    }
}
