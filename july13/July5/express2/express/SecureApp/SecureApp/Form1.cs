using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.Win32;

namespace SecureApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string[,] getpassword;
        string regPath;
        public Form1(String[,] passwordString, String path)
        {
            InitializeComponent();
            getpassword = passwordString;
            regPath = path;
        }
        private String globalCode = "";
        private bool testPassword(String pass) // get password array & test
        {
            String test = pass;
            String key = "";
            Boolean flag = false;
            char[] splitter = { '-' };
            String[] ar = test.Split(splitter);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (getpassword[i, j] == ar[i])
                    {
                        key = key + Convert.ToString(j);
                        flag = true;
                        break;
                    }
                    else
                        flag = false;
                }
                if (flag == false)
                    break;
            }
            if (flag == false)
                return false; // don't save password
            else
            {
                globalCode = key;
                return true; // save password                
            }
        }

        public void saveEntry(String Nmae, String Data)
        {
            RegistryKey regkey = Registry.CurrentUser;
            regkey = regkey.CreateSubKey(regPath); //path

            if (regkey != null)
            {
                regkey.SetValue(Nmae, Data); //Value Name,Value Data
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if password true then send true	
            string password = textBox1.Text + "-" + textBox2.Text + "-" + textBox3.Text + "-" + textBox4.Text + "-" + textBox5.Text;
            //****** Rewrite password
            //------------------ String Rewrite
            String newultimate = "";
            for (int i = 0; i < password.Length; i++)
            {
                char a = Convert.ToChar(password[i]);
                int j = Convert.ToInt32(a);
                newultimate = newultimate + Convert.ToString(j);
            }
            //--------- End Rewrite

            bool value = testPassword(password); //passwordEntry(getpassword,textBox1.Text);
            if (value ==true) // save code & paassword
            {
                saveEntry("Code", globalCode); // save code
                saveEntry("Password", newultimate); // save Password in rewrite form
                MessageBox.Show("Thank you for activating your copy of Bartender Express","Activated",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Hide();
                this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("Activation Key is not valid! Please Enter a valid Activation Key!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			//----------------------------------------------		
		
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://lolliesoft.com/bartenderexpress/purchase");
            }
            catch { } 
        }
    }
}
