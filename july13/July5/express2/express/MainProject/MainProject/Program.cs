using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using SecureApp;

namespace MainProject
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            String abc = @"Software\Tanmay\Protection";
            string[,] passwordSt = new string[,] // 5X10
            {
                {"ASDF","QWER","MKOG","EDFR","CVBH","DRFW","HNKO","GHER","RERW","SWVU"},
                {"ASDW","HJUM","VGTR","VFDS","PCFT","GEIK","CWTH","GETD","ETDA","EFQS"},
                {"HGFD","POLK","DFRE","NBGH","JYUO","GECS","DFWU","GQAS","VRYE","CAER"},
                {"GFHY", "OPHY","GHSW","JNYH","CFFR","VS5H","CD3T","C67N","F34F","F8J5"},
                {"DRFW", "HNKO","GHER","RERW","SWVU","E4N7","2C8U","3F5N","3CFD","F5UT"}
            };
            Secure sec = new Secure();

            bool logic = sec.Algorithm(passwordSt, abc);
            if(logic ==true)
                Application.Run(new Form1());
        }
    }
}
