using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Win32;
using System.Windows.Forms;

namespace SecureApp
{
    public class Secure
    {
        private string globalPath;

        private void firstTime()
        {
            RegistryKey regkey = Registry.CurrentUser;
            regkey = regkey.CreateSubKey(globalPath); //path

            DateTime dt = DateTime.Now;
            string onlyDate = dt.ToShortDateString(); // get only date not time

            regkey.SetValue("Install", onlyDate); //Value Name,Value Data
            regkey.SetValue("Use", onlyDate); //Value Name,Value Data
        }

        private String checkfirstDate()
        {
            RegistryKey regkey = Registry.CurrentUser;
            regkey = regkey.CreateSubKey(globalPath); //path
            string Br = (string)regkey.GetValue("Install");
            if (regkey.GetValue("Install") == null)
                return "First";
            else
                return Br;
        }

        private bool checkPassword(String[,] pass)
        {
            // get from code, genarate password string, check it & return 
            RegistryKey regkey = Registry.CurrentUser;
            regkey = regkey.CreateSubKey(globalPath); //path
            string code = (string)regkey.GetValue("Code");
            // check exist or not
            if (regkey.GetValue("Code") != null)
            {
                string ultimate = "";
                for (int i = 0; i < code.Length; i++)
                {
                    char a = Convert.ToChar(code[i]);
                    string b = Convert.ToString(a);
                    int j = Convert.ToInt32(b);

                    if (i == 0)
                        ultimate = pass[i, j];
                    else
                        ultimate = ultimate + "-" + pass[i, j];
                }
                // ultimate hold coresponding passwordString
                // Now we rewrite it 
                //------------------ String Rewrite
                String newultimate = "";
                for (int i = 0; i < ultimate.Length; i++)
                {
                    char a = Convert.ToChar(ultimate[i]);
                    int j = Convert.ToInt32(a);
                    newultimate = newultimate + Convert.ToString(j);
                }
                //----------
                string Br = (string)regkey.GetValue("Password"); // both are same Form1 & it.
                if (Br == newultimate)
                    return true; //good
                else
                    return false;//bad
            }
            else
                return false;

        }

        private String dayDifPutPresent()
        {
            // get present date from system
            DateTime dt = DateTime.Now;
            string today = dt.ToShortDateString();
            DateTime presentDate = Convert.ToDateTime(today);

            // get instalation date
            RegistryKey regkey = Registry.CurrentUser;
            regkey = regkey.CreateSubKey(globalPath); //path
            string Br = (string)regkey.GetValue("Install");
            DateTime installationDate = Convert.ToDateTime(Br);

            TimeSpan diff = presentDate.Subtract(installationDate); //first.Subtract(second);
            int totaldays = (int)diff.TotalDays;

            // special check if user chenge date in system
            string usd = (string)regkey.GetValue("Use");
            DateTime lastUse = Convert.ToDateTime(usd);
            TimeSpan diff1 = presentDate.Subtract(lastUse); //first.Subtract(second);
            int useBetween = (int)diff1.TotalDays;

            // put next use day in registry
            regkey.SetValue("Use", today); //Value Name,Value Data

            if (useBetween >= 0)
            {

                if (totaldays < 0)
                    return "Error"; // if user change date in system like date set before installation
                else if (totaldays >= 0 && totaldays <= 30)
                    return Convert.ToString(30 - totaldays); //how many days remaining
                else
                    return "Expired"; //Expired
            }
            else
                return "Error"; // if user change date in system
        }

        private void blackList()
        {
            RegistryKey regkey = Registry.CurrentUser;
            regkey = regkey.CreateSubKey(globalPath); //path

            regkey.SetValue("Black", "True");

        }

        private bool blackListCheck()
        {
            RegistryKey regkey = Registry.CurrentUser;
            regkey = regkey.CreateSubKey(globalPath); //path
            string Br = (string)regkey.GetValue("Black");
            if (regkey.GetValue("Black") == null)
                return false; //No
            else
                return true;//Yes
        }

        public bool Algorithm(String[,] appPassword, String pass)
        {
            globalPath = pass;
            bool chpass = checkPassword(appPassword);
            if (chpass == true) //execute
                return true;
            else
            {
                bool block = blackListCheck();
                if (block == false)
                {
                    string chinstall = checkfirstDate();
                    if (chinstall == "First")
                    {
                        firstTime();// installation date
                        DialogResult ds = MessageBox.Show("Thank you for downloading and installing Bartender Express\n Would you like to purchase and activate this trial version now?", "Bartender Express Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (ds == DialogResult.Yes)
                        {
                            Form1 f1 = new Form1(appPassword, globalPath);
                            DialogResult ds1 = f1.ShowDialog();
                            if (ds1 == DialogResult.OK)
                                return true;
                            else
                                return false;
                        }
                        else
                            return true;
                    }
                    else
                    {
                        string status = dayDifPutPresent();
                        if (status == "Error")
                        {
                            blackList();
                            DialogResult ds = MessageBox.Show("Application Can't be loaded, Unauthorized Date Interrupt Occurred! Without activation it can't run! Would you like to activate it?", "Terminate Error-02", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                            if (ds == DialogResult.Yes)
                            {
                                Form1 f1 = new Form1(appPassword, globalPath);
                                DialogResult ds1 = f1.ShowDialog();
                                if (ds1 == DialogResult.OK)
                                    return true;
                                else
                                    return false;
                            }
                            else
                                return false;
                        }
                        else if (status == "Expired")
                        {
                            DialogResult ds = MessageBox.Show("The Bartender Express trial version is now expired! Would you like to Activate Now?", "Bartender Express Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (ds == DialogResult.Yes)
                            {
                                Form1 f1 = new Form1(appPassword, globalPath);
                                DialogResult ds1 = f1.ShowDialog();
                                if (ds1 == DialogResult.OK)
                                    return true;
                                else
                                    return false;
                            }
                            else
                                return false;
                        }
                        else // execute with how many day remaining
                        {
                            DialogResult ds = MessageBox.Show("You are using the Bartender Express Trial Edition, you have " + status + " days left to Activate! Would you like to Activate now?", "Bartender Express Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (ds == DialogResult.Yes)
                            {
                                Form1 f1 = new Form1(appPassword, globalPath);
                                DialogResult ds1 = f1.ShowDialog();
                                if (ds1 == DialogResult.OK)
                                    return true;
                                else
                                    return false;
                            }
                            else
                                return true;
                        }
                    }
                }
                else
                {
                    DialogResult ds = MessageBox.Show("Application Can't be loaded, Unauthorized Date Interrupt Occurred! Without activation it can't run! Would you like to activate it?", "Terminate Error-01", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (ds == DialogResult.Yes)
                    {
                        Form1 f1 = new Form1(appPassword, globalPath);
                        DialogResult ds1 = f1.ShowDialog();
                        if (ds1 == DialogResult.OK)
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                    //return "BlackList";
                }
            }
        }

    }
}
