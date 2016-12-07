using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.Data.SqlClient;

namespace bartenderExpress
{

    public partial class Form2 : Form
    {
        private System.IO.Stream streamToPrint;
        string streamType;

        public Form2(Form1 parent)
        {
            InitializeComponent();

            MdiParent = parent;

        }

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private static extern bool BitBlt
        (
            IntPtr hdcDest, // handle to destination DC
            int nXDest, // x-coord of destination upper-left corner
            int nYDest, // y-coord of destination upper-left corner
            int nWidth, // width of destination rectangle
            int nHeight, // height of destination rectangle
            IntPtr hdcSrc, // handle to source DC
            int nXSrc, // x-coordinate of source upper-left corner
            int nYSrc, // y-coordinate of source upper-left corner
            System.Int32 dwRop // raster operation code
        );

        private void printDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            System.Drawing.Image image = System.Drawing.Image.FromStream(this.streamToPrint);
            int x = e.MarginBounds.X;
            int y = e.MarginBounds.Y;
            int width = 385;
            int height = 404;

            //if ((width / e.MarginBounds.Width) > (height / e.MarginBounds.Height))
            //{
            //    width = e.MarginBounds.Width;
            //    height = image.Height * e.MarginBounds.Width / image.Width;
            //}
            //else
            //{
            //    height = e.MarginBounds.Height;
            //    width = image.Width * e.MarginBounds.Height / image.Height;
            //}

            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(x, y, width, height);
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            e.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel);
        }


        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.lolliesoft.com/");
        }

        public void StartPrint(Stream streamToPrint, string streamType)
        {
            this.printDocform2.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);
            this.streamToPrint = streamToPrint;
            this.streamType = streamType;
            System.Windows.Forms.PrintDialog PrintDialog1 = new PrintDialog();
            PrintDialog1.AllowSomePages = true;
            PrintDialog1.ShowHelp = true;
            PrintDialog1.Document = printDocform2;

            DialogResult result = PrintDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                printDocform2.Print();
                //docToPrint.Print();
            }
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            Graphics g1 = this.CreateGraphics();
            Image MyImage = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height, g1);
            Graphics g2 = Graphics.FromImage(MyImage);
            IntPtr dc1 = g1.GetHdc();
            IntPtr dc2 = g2.GetHdc();
            BitBlt(dc2, 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height, dc1, 0, 0, 13369376);
            g1.ReleaseHdc(dc1);
            g2.ReleaseHdc(dc2);
            MyImage.Save(Application.StartupPath + "\\PrintPage.jpg", ImageFormat.Jpeg);
            FileStream fileStream = new FileStream(Application.StartupPath + "\\PrintPage.jpg", FileMode.Open, FileAccess.Read);
            StartPrint(fileStream, "Image");
            fileStream.Close();

            if (System.IO.File.Exists(Application.StartupPath + "\\PrintPage.jpg"))
            {
                System.IO.File.Delete(Application.StartupPath + "\\PrintPage.jpg");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bartender Express Version 3.0\nCopyright © 1996-2012 Lolliesoft Ltd.", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDialog PrintDialog1 = new PrintDialog();
            PrintDialog1.ShowDialog();
        }

        private void facebookToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://www.facebook.com/BartenderExpress");
            }
            catch { }
        }

        private void TwitterToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://twitter.com/lolliesoft");
            }
            catch { } 
        }

        private void pinterestToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://pinterest.com/lolliesoft/bartender-express-software/");
            }
            catch { }
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, Application.StartupPath + "\\bartenderexpress.chm");
        }

    }
}
         
