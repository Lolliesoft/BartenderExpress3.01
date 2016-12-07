using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KeyGen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string[,] num = new string[,] // 5X10
            {
                {"ASDF","QWER","MKOG","EDFR","CVBH","DRFW","HNKO","GHER","RERW","SWVU"},
                {"ASDW","HJUM","VGTR","VFDS","PCFT","GEIK","CWTH","GETD","ETDA","EFQS"},
                {"HGFD","POLK","DFRE","NBGH","JYUO","GECS","DFWU","GQAS","VRYE","CAER"},
                {"GFHY", "OPHY","GHSW","JNYH","CFFR","VS5H","CD3T","C67N","F34F","F8J5"},
                {"DRFW", "HNKO","GHER","RERW","SWVU","E4N7","2C8U","3F5N","3CFD","F5UT"}
            };
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.TextLength < 5)
                    MessageBox.Show("Enter 5 Digit Integer!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                {
                    int getnum = Convert.ToInt32(textBox1.Text);

                    string ultimate = "";
                    for (int i = 0; i < textBox1.TextLength; i++)
                    {
                        char a = Convert.ToChar(textBox1.Text[i]);
                        string b = Convert.ToString(a);
                        int j = Convert.ToInt32(b);

                        if (i == 0)
                            ultimate = num[i, j];
                        else
                            ultimate = ultimate + "-" + num[i, j];
                    }
                    textBox2.Text = ultimate;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
