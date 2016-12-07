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
using System.Data.SQLite;
using System.Threading;
using SecureApp;

namespace bartenderExpress
{
    public partial class Form1 : Form
    {
        private System.IO.Stream streamToPrint;
        string streamType;

        public Form1()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "Lolliesoft"));

            Thread t = new Thread(new ThreadStart(SplashScreen));
            t.Start();
            Thread.Sleep(3500);

            InitializeComponent();
            t.Abort();

            SetBackGroundColorOfMDIForm();

            //this.KeyPress += new KeyPressEventHandler(CheckKeys);

            // this.KeyPress += new KeyPressEventHandler(CheckKeys2);

        }

        public void SplashScreen()
        {
            Application.Run(new splashScreen());
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
            int width = image.Width;
            int height = image.Height;
            if ((width / e.MarginBounds.Width) > (height / e.MarginBounds.Height))
            {
                width = e.MarginBounds.Width;
                height = image.Height * e.MarginBounds.Width / image.Width;
            }
            else
            {
                height = e.MarginBounds.Height;
                width = image.Width * e.MarginBounds.Height / image.Height;
            }

            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(x, y, width, height);
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            e.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel);
        }

        public void StartPrint(Stream streamToPrint, string streamType)
        {
            this.printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);
            this.streamToPrint = streamToPrint;
            this.streamType = streamType;
            System.Windows.Forms.PrintDialog PrintDialog1 = new PrintDialog();
            PrintDialog1.AllowSomePages = true;
            PrintDialog1.ShowHelp = true;
            PrintDialog1.Document = printDoc;

            DialogResult result = PrintDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                printDoc.Print();
                //docToPrint.Print();
            }

        }

        private void SetBackGroundColorOfMDIForm()
        {
            foreach (Control ctl in this.Controls)
            {
                if ((ctl) is MdiClient)

                // If the control is the correct type,
                // change the color.
                {
                    ctl.BackColor = System.Drawing.Color.White;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'custom_recipesDataSet.myRecipes' table. You can move, or remove it, as needed.
            this.myRecipesTableAdapter1.Fill(this.custom_recipesDataSet.myRecipes);
            // TODO: This line of code loads data into the 'xpressShotsDataSet.myRecipes' table. You can move, or remove it, as needed.
            this.myRecipesTableAdapter.Fill(this.xpressShotsDataSet.myRecipes);
            // TODO: This line of code loads data into the 'xpressShotsDataSet.cocktails' table. You can move, or remove it, as needed.
            this.cocktailsTableAdapter.Fill(this.xpressShotsDataSet.cocktails);
            // TODO: This line of code loads data into the 'xpressShotsDataSet.nonalcoholic' table. You can move, or remove it, as needed.
            this.nonalcoholicTableAdapter.Fill(this.xpressShotsDataSet.nonalcoholic);
            // TODO: This line of code loads data into the 'xpressShotsDataSet.coffeetea' table. You can move, or remove it, as needed.
            this.coffeeteaTableAdapter.Fill(this.xpressShotsDataSet.coffeetea);
            // TODO: This line of code loads data into the 'xpressShotsDataSet.punches' table. You can move, or remove it, as needed.
            this.punchesTableAdapter.Fill(this.xpressShotsDataSet.punches);
            // TODO: This line of code loads data into the 'xpressShotsDataSet.beers' table. You can move, or remove it, as needed.
            this.beersTableAdapter.Fill(this.xpressShotsDataSet.beers);
            // TODO: This line of code loads data into the 'xpressShotsDataSet.liqueurs' table. You can move, or remove it, as needed.
            this.liqueursTableAdapter.Fill(this.xpressShotsDataSet.liqueurs);
            // TODO: This line of code loads data into the 'xpressShotsDataSet.shots' table. You can move, or remove it, as needed.
            this.shotsTableAdapter.Fill(this.xpressShotsDataSet.shots);
            // TODO: This line of code loads data into the 'xpressShotsDataSet.recipes' table. You can move, or remove it, as needed.
            this.recipesTableAdapter.Fill(this.xpressShotsDataSet.recipes);
            // TODO: This line of code loads data into the 'bartenderexpressDataSet.recipes' table. You can move, or remove it, as needed.
            CueProvider.SetCue(textBox1, "Search Drinks");
            CueProvider.SetCue(textBox2, "Search Shots");
            CueProvider.SetCue(textBox3, "Search Liqueurs");
            CueProvider.SetCue(textBox4, "Search Beer and Ales");
            CueProvider.SetCue(textBox5, "Search Punches");
            CueProvider.SetCue(textBox6, "Search Coffee and Teas");
            CueProvider.SetCue(textBox7, "Search Non-Alcoholic Drinks");
            CueProvider.SetCue(textBox8, "Search Cocktails");
            CueProvider.SetCue(textBox9, "Search Your Recipes");
        }

        private void nameListCount(object sender, EventArgs e)
        {
            if (nameListBox.SelectedItem != null)
            {
                string statusbarrecipe = nameListBox.SelectedValue.ToString();


                using (SQLiteConnection conn = new SQLiteConnection("Data Source = |DataDirectory|\\XpressShots.db"))
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand("SELECT drink_num FROM recipes WHERE name='" + (statusbarrecipe.Trim().Replace("'", "''")) + "'", conn);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    toolStripStatusLabel2.Text = "Drink " + reader["drink_num"].ToString() + " of " + (this.nameListBox.Items.Count.ToString());
                }
            }
        }

        private void nameListCount2(object sender, EventArgs e)
        {
            if (nameListBox2.SelectedItem != null)
            {
                string statusbarrecipe = nameListBox2.SelectedValue.ToString();


                using (SQLiteConnection conn2 = new SQLiteConnection("Data Source = |DataDirectory|\\XpressShots.db"))
                {
                    conn2.Open();
                    SQLiteCommand cmd = new SQLiteCommand("SELECT shots_key FROM shots WHERE name='" + (statusbarrecipe.Trim().Replace("'", "''")) + "'", conn2);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    toolStripStatusLabel2.Text = "Drink " + reader["shots_key"].ToString() + " of " + (this.nameListBox2.Items.Count.ToString());
                }
            }
        }

        private void nameListCount3(object sender, EventArgs e)
        {   
            if (nameListBox3.SelectedItem != null)
            {
                string statusbarrecipe = nameListBox3.SelectedValue.ToString();


                using (SQLiteConnection conn3 = new SQLiteConnection("Data Source = |DataDirectory|\\XpressShots.db"))
                {
                    conn3.Open();
                    SQLiteCommand cmd = new SQLiteCommand("SELECT liqueur_key FROM liqueurs WHERE name ='" + statusbarrecipe + "'", conn3);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    toolStripStatusLabel2.Text = "Drink " + reader["liqueur_key"].ToString() + " of " + (this.nameListBox3.Items.Count.ToString());
                }
            }
        }

        private void nameListCount4(object sender, EventArgs e)
        {
            if (nameListBox4.SelectedItem != null)
            {
                string statusbarrecipe = nameListBox4.SelectedValue.ToString();


                using (SQLiteConnection conn4 = new SQLiteConnection("Data Source = |DataDirectory|\\XpressShots.db"))
                {
                    conn4.Open();
                    SQLiteCommand cmd = new SQLiteCommand("SELECT beer_key FROM beers WHERE name ='" + (statusbarrecipe.Trim().Replace("'", "''")) + "'", conn4);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    toolStripStatusLabel2.Text = "Drink " + reader["beer_key"].ToString() + " of " + (this.nameListBox4.Items.Count.ToString());
                }
            }
        }

        private void nameListCount5(object sender, EventArgs e)
        {
            if (nameListBox5.SelectedItem != null)
            {
                string statusbarrecipe = nameListBox5.SelectedValue.ToString();


                using (SQLiteConnection conn5 = new SQLiteConnection("Data Source = |DataDirectory|\\XpressShots.db"))
                {
                    conn5.Open();
                    SQLiteCommand cmd = new SQLiteCommand("SELECT punch_key FROM punches WHERE name ='" + (statusbarrecipe.Trim().Replace("'", "''")) + "'", conn5);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    toolStripStatusLabel2.Text = "Drink " + reader["punch_key"].ToString() + " of " + (this.nameListBox5.Items.Count.ToString());
                }
            }
        }

        private void nameListCount6(object sender, EventArgs e)
        {
            if (nameListBox6.SelectedItem != null)
            {
                string statusbarrecipe = nameListBox6.SelectedValue.ToString();


                using (SQLiteConnection conn6 = new SQLiteConnection("Data Source = |DataDirectory|\\XpressShots.db"))
                {
                    conn6.Open();
                    SQLiteCommand cmd = new SQLiteCommand("SELECT coffeetea_key FROM coffeetea WHERE name ='" + statusbarrecipe + "'", conn6);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    toolStripStatusLabel2.Text = "Drink " + reader["coffeetea_key"].ToString() + " of " + (this.nameListBox6.Items.Count.ToString());
                }
            }
        }

        private void nameListCount7(object sender, EventArgs e)
        {
            if (nameListBox7.SelectedItem != null)
            {
                string statusbarrecipe = nameListBox7.SelectedValue.ToString();


                using (SQLiteConnection conn7 = new SQLiteConnection("Data Source = |DataDirectory|\\XpressShots.db"))
                {
                    conn7.Open();
                    SQLiteCommand cmd = new SQLiteCommand("SELECT nonalcoholic_key FROM nonalcoholic WHERE name ='" + (statusbarrecipe.Trim().Replace("'", "''")) + "'", conn7);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    toolStripStatusLabel2.Text = "Drink " + reader["nonalcoholic_key"].ToString() + " of " + (this.nameListBox7.Items.Count.ToString());
                }
            }
        }

        private void nameListCount8(object sender, EventArgs e)
        {
            if (nameListBox8.SelectedItem != null)
            {
                string statusbarrecipe = nameListBox8.SelectedValue.ToString();


                using (SQLiteConnection conn8 = new SQLiteConnection("Data Source = |DataDirectory|\\XpressShots.db"))
                {
                    conn8.Open();
                    SQLiteCommand cmd = new SQLiteCommand("SELECT cocktail_key FROM cocktails WHERE name ='" + (statusbarrecipe.Trim().Replace("'", "''")) + "'", conn8);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    toolStripStatusLabel2.Text = "Drink " + reader["cocktail_key"].ToString() + " of " + (this.nameListBox8.Items.Count.ToString());
                }
            }
        }

        private void nameListCount9(object sender, EventArgs e)
        {
            if (nameListBox9.SelectedItem != null)
            {
                int totalDrinks = (this.nameListBox9.Items.Count);
                toolStripStatusLabel2.Text = "Drink Count: " + (totalDrinks.ToString());
            }
        }
        
        private void nameListBox_DoubleClick(object sender, EventArgs e)
        {

            if (nameListBox.SelectedItem != null)
            {
                string statusbarrecipe = nameListBox.SelectedValue.ToString();
                toolStripStatusLabel1.Text = statusbarrecipe;

                //toolStripStatusLabel2.Text = (this.nameListBox.Items.Count.ToString());

                using (SQLiteConnection cs = new SQLiteConnection("Data Source = |DataDirectory|\\bartenderExpress.db"))
                {
                    cs.Open();

                    //Get ID
                    SQLiteCommand cmd = new SQLiteCommand("SELECT id FROM recipes WHERE name='" + (statusbarrecipe.Trim().Replace("'", "''")) + "'", cs);
                    //SQLiteCommand cmd = new SQLiteCommand("SELECT id FROM recipes WHERE name ='" + statusbarrecipe + "'", cs);
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    //MessageBox.Show(reader["id"].ToString());
                    
                    Form2 child = new Form2(this);
                    child.Text = nameListBox.SelectedValue.ToString();
                    child.toolStripStatusLabel1.Text = statusbarrecipe;
                    child.Show();


                    while (reader.Read())
                    {
                        // Get Ingredients
                        SQLiteCommand cmding = new SQLiteCommand("SELECT name FROM ingredients INNER JOIN recipeingredients ON ingredients.id=recipeingredients.ingid WHERE recipeid=" + (reader["id"]) + "", cs);
                        SQLiteDataReader rdring = cmding.ExecuteReader();

                        while (rdring.Read())
                        {
                            ListViewItem ingredients = new ListViewItem();
                            ingredients.SubItems[0].Text = rdring[0].ToString();
                            child.listView2.Items.Add(ingredients);

                        }
                        //Get Amounts
                        SQLiteCommand cmdamt = new SQLiteCommand("SELECT amount FROM recipeingredients INNER JOIN ingredients ON ingredients.id=recipeingredients.ingid WHERE recipeid=" + (reader["id"]) + "", cs);
                        SQLiteDataReader rdramt = cmdamt.ExecuteReader();

                        while (rdramt.Read())
                        {
                            ListViewItem amount = new ListViewItem();
                            amount.SubItems[0].Text = rdramt[0].ToString();
                            child.listView1.Items.Add(amount);
                        }

                        //Get Directions
                        SQLiteCommand cmddir = new SQLiteCommand("SELECT directions FROM recipes WHERE id = " + (reader["id"]) + "", cs);
                        SQLiteDataReader rdr = cmddir.ExecuteReader();

                        while (rdr.Read())
                        {
                            child.richTextBox1.Text = rdr[0].ToString();
                        }


                    }
                }

            }
        }
        private void nameListBox2_DoubleClick(object sender, EventArgs e)
        {
            if (nameListBox2.SelectedItem != null)
            {
                string statusbarrecipe2 = nameListBox2.SelectedValue.ToString();
                toolStripStatusLabel1.Text = statusbarrecipe2;

                //MessageBox.Show(statusbarrecipe2);



                using (SQLiteConnection cs2 = new SQLiteConnection("Data Source = |DataDirectory|\\XpressShots.db"))
                {
                    cs2.Open();

                    //Get ID
                    //string selectedvalue

                    SQLiteCommand cmd = new SQLiteCommand("SELECT shots_key FROM shots WHERE name ='" + (statusbarrecipe2.Trim().Replace("'", "''")) + "'", cs2);
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    //MessageBox.Show(reader["shots_key"].ToString());
                    Form2 child = new Form2(this);
                    child.Text = nameListBox2.SelectedValue.ToString();
                    child.toolStripStatusLabel1.Text = statusbarrecipe2;
                    child.Show();

                    while (reader.Read())
                    {
                        // Get Ingredients
                        SQLiteCommand cmding = new SQLiteCommand("SELECT ingredient1, ingredient2, ingredient3, ingredient4, ingredient5, ingredient6, ingredient7, ingredient8, ingredient9, ingredient10, ingredient11 FROM shots WHERE shots_key=" + (reader["shots_key"]) + "", cs2);
                        SQLiteDataReader rdring = cmding.ExecuteReader();

                        while (rdring.Read())
                        {
                            ListViewItem ingredient1 = new ListViewItem();
                            ingredient1.SubItems[0].Text = rdring[0].ToString();
                            child.listView2.Items.Add(ingredient1);

                            ListViewItem ingredient2 = new ListViewItem();
                            ingredient2.SubItems[0].Text = rdring[1].ToString();
                            child.listView2.Items.Add(ingredient2);

                            ListViewItem ingredient3 = new ListViewItem();
                            ingredient3.SubItems[0].Text = rdring[2].ToString();
                            child.listView2.Items.Add(ingredient3);

                            ListViewItem ingredient4 = new ListViewItem();
                            ingredient4.SubItems[0].Text = rdring[3].ToString();
                            child.listView2.Items.Add(ingredient4);

                            ListViewItem ingredient5 = new ListViewItem();
                            ingredient5.SubItems[0].Text = rdring[4].ToString();
                            child.listView2.Items.Add(ingredient5);

                            ListViewItem ingredient6 = new ListViewItem();
                            ingredient6.SubItems[0].Text = rdring[5].ToString();
                            child.listView2.Items.Add(ingredient6);

                            ListViewItem ingredient7 = new ListViewItem();
                            ingredient7.SubItems[0].Text = rdring[6].ToString();
                            child.listView2.Items.Add(ingredient7);

                            ListViewItem ingredient8 = new ListViewItem();
                            ingredient8.SubItems[0].Text = rdring[7].ToString();
                            child.listView2.Items.Add(ingredient8);

                            ListViewItem ingredient9 = new ListViewItem();
                            ingredient9.SubItems[0].Text = rdring[8].ToString();
                            child.listView2.Items.Add(ingredient9);

                            ListViewItem ingredient10 = new ListViewItem();
                            ingredient10.SubItems[0].Text = rdring[9].ToString();
                            child.listView2.Items.Add(ingredient10);

                            ListViewItem ingredient11 = new ListViewItem();
                            ingredient11.SubItems[0].Text = rdring[10].ToString();
                            child.listView2.Items.Add(ingredient11);
                        }
                        //Get Amounts
                        SQLiteCommand cmdamt = new SQLiteCommand("SELECT amt1, amt2, amt3, amt4, amt5, amt6, amt7, amt8, amt9, amt10, amt11 FROM shots WHERE shots_key=" + (reader["shots_key"]) + "", cs2);
                        SQLiteDataReader rdramt = cmdamt.ExecuteReader();

                        while (rdramt.Read())
                        {
                            ListViewItem amt1 = new ListViewItem();
                            amt1.SubItems[0].Text = rdramt[0].ToString();
                            //amount.SubItems.Add(rdramt[0].ToString());
                            child.listView1.Items.Add(amt1);

                            ListViewItem amt2 = new ListViewItem();
                            amt2.SubItems[0].Text = rdramt[1].ToString();
                            child.listView1.Items.Add(amt2);

                            ListViewItem amt3 = new ListViewItem();
                            amt3.SubItems[0].Text = rdramt[2].ToString();
                            child.listView1.Items.Add(amt3);

                            ListViewItem amt4 = new ListViewItem();
                            amt4.SubItems[0].Text = rdramt[3].ToString();
                            child.listView1.Items.Add(amt4);

                            ListViewItem amt5 = new ListViewItem();
                            amt5.SubItems[0].Text = rdramt[4].ToString();
                            child.listView1.Items.Add(amt5);

                            ListViewItem amt6 = new ListViewItem();
                            amt6.SubItems[0].Text = rdramt[5].ToString();
                            child.listView1.Items.Add(amt6);

                            ListViewItem amt7 = new ListViewItem();
                            amt7.SubItems[0].Text = rdramt[6].ToString();
                            child.listView1.Items.Add(amt7);

                            ListViewItem amt8 = new ListViewItem();
                            amt8.SubItems[0].Text = rdramt[7].ToString();
                            child.listView1.Items.Add(amt8);

                            ListViewItem amt9 = new ListViewItem();
                            amt9.SubItems[0].Text = rdramt[8].ToString();
                            child.listView1.Items.Add(amt9);

                            ListViewItem amt10 = new ListViewItem();
                            amt10.SubItems[0].Text = rdramt[9].ToString();
                            child.listView1.Items.Add(amt10);

                            ListViewItem amt11 = new ListViewItem();
                            amt11.SubItems[0].Text = rdramt[10].ToString();
                            child.listView1.Items.Add(amt11);

                        }

                        //Get Directions
                        SQLiteCommand cmddir = new SQLiteCommand("SELECT directions FROM shots WHERE shots_key = " + (reader["shots_key"]) + "", cs2);
                        SQLiteDataReader rdr = cmddir.ExecuteReader();

                        while (rdr.Read())
                        {
                            child.richTextBox1.Text = rdr[0].ToString();
                        }

                    }
                }

            }
        }
        private void nameListBox3_DoubleClick(object sender, EventArgs e)
        {
            if (nameListBox3.SelectedItem != null)
            {
                string statusbarrecipe2 = nameListBox3.SelectedValue.ToString();
                toolStripStatusLabel1.Text = statusbarrecipe2;

                //MessageBox.Show(statusbarrecipe2);



                using (SQLiteConnection cs3 = new SQLiteConnection("Data Source = |DataDirectory|\\XpressShots.db"))
                {
                    cs3.Open();

                    //Get ID
                    //string selectedvalue
                    //SQLiteCommand cmd = new SQLiteCommand("SELECT shots_key FROM shots WHERE name ='" + (statusbarrecipe2.Trim().Replace("'", "''")) + "'", cs2);
                    SQLiteCommand cmd = new SQLiteCommand("SELECT liqueur_key FROM liqueurs WHERE name ='" + statusbarrecipe2 + "'", cs3);
                    //SQLiteCommand cmd = new SQLiteCommand("SELECT liqueur_key FROM liqueurs WHERE liqueur_key = 5", cs3);
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    //MessageBox.Show(reader["liqueur_key"].ToString());
                    Form2 child = new Form2(this);
                    child.Text = nameListBox3.SelectedValue.ToString();
                    child.toolStripStatusLabel1.Text = statusbarrecipe2;
                    child.Show();

                    while (reader.Read())
                    {
                        // Get Ingredients
                        SQLiteCommand cmding = new SQLiteCommand("SELECT ingredient1, ingredient2, ingredient3, ingredient4, ingredient5, ingredient6, ingredient7, ingredient8, ingredient9, ingredient10, ingredient11, ingredient12 FROM liqueurs WHERE liqueur_key=" + (reader["liqueur_key"]) + "", cs3);
                        SQLiteDataReader rdring = cmding.ExecuteReader();

                        while (rdring.Read())
                        {
                            ListViewItem ingredient1 = new ListViewItem();
                            ingredient1.SubItems[0].Text = rdring[0].ToString();
                            child.listView2.Items.Add(ingredient1);

                            ListViewItem ingredient2 = new ListViewItem();
                            ingredient2.SubItems[0].Text = rdring[1].ToString();
                            child.listView2.Items.Add(ingredient2);

                            ListViewItem ingredient3 = new ListViewItem();
                            ingredient3.SubItems[0].Text = rdring[2].ToString();
                            child.listView2.Items.Add(ingredient3);

                            ListViewItem ingredient4 = new ListViewItem();
                            ingredient4.SubItems[0].Text = rdring[3].ToString();
                            child.listView2.Items.Add(ingredient4);

                            ListViewItem ingredient5 = new ListViewItem();
                            ingredient5.SubItems[0].Text = rdring[4].ToString();
                            child.listView2.Items.Add(ingredient5);

                            ListViewItem ingredient6 = new ListViewItem();
                            ingredient6.SubItems[0].Text = rdring[5].ToString();
                            child.listView2.Items.Add(ingredient6);

                            ListViewItem ingredient7 = new ListViewItem();
                            ingredient7.SubItems[0].Text = rdring[6].ToString();
                            child.listView2.Items.Add(ingredient7);

                            ListViewItem ingredient8 = new ListViewItem();
                            ingredient8.SubItems[0].Text = rdring[7].ToString();
                            child.listView2.Items.Add(ingredient8);

                            ListViewItem ingredient9 = new ListViewItem();
                            ingredient9.SubItems[0].Text = rdring[8].ToString();
                            child.listView2.Items.Add(ingredient9);

                            ListViewItem ingredient10 = new ListViewItem();
                            ingredient10.SubItems[0].Text = rdring[9].ToString();
                            child.listView2.Items.Add(ingredient10);

                            ListViewItem ingredient11 = new ListViewItem();
                            ingredient11.SubItems[0].Text = rdring[10].ToString();
                            child.listView2.Items.Add(ingredient11);

                            ListViewItem ingredient12 = new ListViewItem();
                            ingredient12.SubItems[0].Text = rdring[11].ToString();
                            child.listView2.Items.Add(ingredient12);

                        }
                        //Get Amounts
                        SQLiteCommand cmdamt = new SQLiteCommand("SELECT amt1, amt2, amt3, amt4, amt5, amt6, amt7, amt8, amt9, amt10, amt11, amt12 FROM liqueurs WHERE liqueur_key=" + (reader["liqueur_key"]) + "", cs3);
                        SQLiteDataReader rdramt = cmdamt.ExecuteReader();

                        while (rdramt.Read())
                        {
                            ListViewItem amt1 = new ListViewItem();
                            amt1.SubItems[0].Text = rdramt[0].ToString();
                            //amount.SubItems.Add(rdramt[0].ToString());
                            child.listView1.Items.Add(amt1);

                            ListViewItem amt2 = new ListViewItem();
                            amt2.SubItems[0].Text = rdramt[1].ToString();
                            child.listView1.Items.Add(amt2);

                            ListViewItem amt3 = new ListViewItem();
                            amt3.SubItems[0].Text = rdramt[2].ToString();
                            child.listView1.Items.Add(amt3);

                            ListViewItem amt4 = new ListViewItem();
                            amt4.SubItems[0].Text = rdramt[3].ToString();
                            child.listView1.Items.Add(amt4);

                            ListViewItem amt5 = new ListViewItem();
                            amt5.SubItems[0].Text = rdramt[4].ToString();
                            child.listView1.Items.Add(amt5);

                            ListViewItem amt6 = new ListViewItem();
                            amt6.SubItems[0].Text = rdramt[5].ToString();
                            child.listView1.Items.Add(amt6);

                            ListViewItem amt7 = new ListViewItem();
                            amt7.SubItems[0].Text = rdramt[6].ToString();
                            child.listView1.Items.Add(amt7);

                            ListViewItem amt8 = new ListViewItem();
                            amt8.SubItems[0].Text = rdramt[7].ToString();
                            child.listView1.Items.Add(amt8);

                            ListViewItem amt9 = new ListViewItem();
                            amt9.SubItems[0].Text = rdramt[8].ToString();
                            child.listView1.Items.Add(amt9);

                            ListViewItem amt10 = new ListViewItem();
                            amt10.SubItems[0].Text = rdramt[9].ToString();
                            child.listView1.Items.Add(amt10);

                            ListViewItem amt11 = new ListViewItem();
                            amt11.SubItems[0].Text = rdramt[10].ToString();
                            child.listView1.Items.Add(amt11);

                            ListViewItem amt12 = new ListViewItem();
                            amt12.SubItems[0].Text = rdramt[11].ToString();
                            child.listView1.Items.Add(amt12);
                        }

                        //Get Directions
                        SQLiteCommand cmddir = new SQLiteCommand("SELECT directions FROM liqueurs WHERE liqueur_key= " + (reader["liqueur_key"]) + "", cs3);
                        SQLiteDataReader rdr = cmddir.ExecuteReader();

                        while (rdr.Read())
                        {
                            child.richTextBox1.Text = rdr[0].ToString();
                        }

                    }
                }

            }
        }
        private void nameListBox4_DoubleClick(object sender, EventArgs e)
        {
            if (nameListBox4.SelectedItem != null)
            {
                string statusbarrecipe2 = nameListBox4.SelectedValue.ToString();
                toolStripStatusLabel1.Text = statusbarrecipe2;

                //MessageBox.Show(statusbarrecipe2);



                using (SQLiteConnection cs4 = new SQLiteConnection("Data Source = |DataDirectory|\\XpressShots.db"))
                {
                    cs4.Open();

                    //Get ID
                    //string selectedvalue

                    SQLiteCommand cmd = new SQLiteCommand("SELECT beer_key FROM beers WHERE name ='" + (statusbarrecipe2.Trim().Replace("'", "''")) + "'", cs4);
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    //MessageBox.Show(reader["beer_key"].ToString());
                    Form2 child = new Form2(this);
                    child.Text = nameListBox4.SelectedValue.ToString();
                    child.toolStripStatusLabel1.Text = statusbarrecipe2;
                    child.Show();

                    while (reader.Read())
                    {
                        // Get Ingredients
                        SQLiteCommand cmding = new SQLiteCommand("SELECT ingredient1, ingredient2, ingredient3, ingredient4, ingredient5, ingredient6, ingredient7, ingredient8, ingredient9 FROM beers WHERE beer_key=" + (reader["beer_key"]) + "", cs4);
                        SQLiteDataReader rdring = cmding.ExecuteReader();

                        while (rdring.Read())
                        {
                            ListViewItem ingredient1 = new ListViewItem();
                            ingredient1.SubItems[0].Text = rdring[0].ToString();
                            child.listView2.Items.Add(ingredient1);

                            ListViewItem ingredient2 = new ListViewItem();
                            ingredient2.SubItems[0].Text = rdring[1].ToString();
                            child.listView2.Items.Add(ingredient2);

                            ListViewItem ingredient3 = new ListViewItem();
                            ingredient3.SubItems[0].Text = rdring[2].ToString();
                            child.listView2.Items.Add(ingredient3);

                            ListViewItem ingredient4 = new ListViewItem();
                            ingredient4.SubItems[0].Text = rdring[3].ToString();
                            child.listView2.Items.Add(ingredient4);

                            ListViewItem ingredient5 = new ListViewItem();
                            ingredient5.SubItems[0].Text = rdring[4].ToString();
                            child.listView2.Items.Add(ingredient5);

                            ListViewItem ingredient6 = new ListViewItem();
                            ingredient6.SubItems[0].Text = rdring[5].ToString();
                            child.listView2.Items.Add(ingredient6);

                            ListViewItem ingredient7 = new ListViewItem();
                            ingredient7.SubItems[0].Text = rdring[6].ToString();
                            child.listView2.Items.Add(ingredient7);

                            ListViewItem ingredient8 = new ListViewItem();
                            ingredient8.SubItems[0].Text = rdring[7].ToString();
                            child.listView2.Items.Add(ingredient8);

                            ListViewItem ingredient9 = new ListViewItem();
                            ingredient9.SubItems[0].Text = rdring[8].ToString();
                            child.listView2.Items.Add(ingredient9);
                        }
                        //Get Amounts
                        SQLiteCommand cmdamt = new SQLiteCommand("SELECT amt1, amt2, amt3, amt4, amt5, amt6, amt7, amt8, amt9 FROM beers WHERE beer_key=" + (reader["beer_key"]) + "", cs4);
                        SQLiteDataReader rdramt = cmdamt.ExecuteReader();

                        while (rdramt.Read())
                        {
                            ListViewItem amt1 = new ListViewItem();
                            amt1.SubItems[0].Text = rdramt[0].ToString();
                            //amount.SubItems.Add(rdramt[0].ToString());
                            child.listView1.Items.Add(amt1);

                            ListViewItem amt2 = new ListViewItem();
                            amt2.SubItems[0].Text = rdramt[1].ToString();
                            child.listView1.Items.Add(amt2);

                            ListViewItem amt3 = new ListViewItem();
                            amt3.SubItems[0].Text = rdramt[2].ToString();
                            child.listView1.Items.Add(amt3);

                            ListViewItem amt4 = new ListViewItem();
                            amt4.SubItems[0].Text = rdramt[3].ToString();
                            child.listView1.Items.Add(amt4);

                            ListViewItem amt5 = new ListViewItem();
                            amt5.SubItems[0].Text = rdramt[4].ToString();
                            child.listView1.Items.Add(amt5);

                            ListViewItem amt6 = new ListViewItem();
                            amt6.SubItems[0].Text = rdramt[5].ToString();
                            child.listView1.Items.Add(amt6);

                            ListViewItem amt7 = new ListViewItem();
                            amt7.SubItems[0].Text = rdramt[6].ToString();
                            child.listView1.Items.Add(amt7);

                            ListViewItem amt8 = new ListViewItem();
                            amt8.SubItems[0].Text = rdramt[7].ToString();
                            child.listView1.Items.Add(amt8);

                            ListViewItem amt9 = new ListViewItem();
                            amt9.SubItems[0].Text = rdramt[8].ToString();
                            child.listView1.Items.Add(amt9);
                        }

                        //Get Directions
                        SQLiteCommand cmddir = new SQLiteCommand("SELECT directions FROM beers WHERE beer_key= " + (reader["beer_key"]) + "", cs4);
                        SQLiteDataReader rdr = cmddir.ExecuteReader();

                        while (rdr.Read())
                        {
                            child.richTextBox1.Text = rdr[0].ToString();
                        }

                    }
                }

            }
        }
        private void nameListBox5_DoubleClick(object sender, EventArgs e)
        {
            if (nameListBox5.SelectedItem != null)
            {
                string statusbarrecipe2 = nameListBox5.SelectedValue.ToString();
                toolStripStatusLabel1.Text = statusbarrecipe2;

                //MessageBox.Show(statusbarrecipe2);



                using (SQLiteConnection cs5 = new SQLiteConnection("Data Source = |DataDirectory|\\XpressShots.db"))
                {
                    cs5.Open();

                    //Get ID
                    //string selectedvalue

                    SQLiteCommand cmd = new SQLiteCommand("SELECT punch_key FROM punches WHERE name ='" + (statusbarrecipe2.Trim().Replace("'", "''")) + "'", cs5);
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    //MessageBox.Show(reader["beer_key"].ToString());
                    Form2 child = new Form2(this);
                    child.Text = nameListBox5.SelectedValue.ToString();
                    child.toolStripStatusLabel1.Text = statusbarrecipe2;
                    child.Show();

                    while (reader.Read())
                    {
                        // Get Ingredients
                        SQLiteCommand cmding = new SQLiteCommand("SELECT ingredient1, ingredient2, ingredient3, ingredient4, ingredient5, ingredient6, ingredient7, ingredient8, ingredient9, ingredient10, ingredient11, ingredient12, ingredient13, ingredient14 FROM punches WHERE punch_key=" + (reader["punch_key"]) + "", cs5);
                        SQLiteDataReader rdring = cmding.ExecuteReader();

                        while (rdring.Read())
                        {
                            ListViewItem ingredient1 = new ListViewItem();
                            ingredient1.SubItems[0].Text = rdring[0].ToString();
                            child.listView2.Items.Add(ingredient1);

                            ListViewItem ingredient2 = new ListViewItem();
                            ingredient2.SubItems[0].Text = rdring[1].ToString();
                            child.listView2.Items.Add(ingredient2);

                            ListViewItem ingredient3 = new ListViewItem();
                            ingredient3.SubItems[0].Text = rdring[2].ToString();
                            child.listView2.Items.Add(ingredient3);

                            ListViewItem ingredient4 = new ListViewItem();
                            ingredient4.SubItems[0].Text = rdring[3].ToString();
                            child.listView2.Items.Add(ingredient4);

                            ListViewItem ingredient5 = new ListViewItem();
                            ingredient5.SubItems[0].Text = rdring[4].ToString();
                            child.listView2.Items.Add(ingredient5);

                            ListViewItem ingredient6 = new ListViewItem();
                            ingredient6.SubItems[0].Text = rdring[5].ToString();
                            child.listView2.Items.Add(ingredient6);

                            ListViewItem ingredient7 = new ListViewItem();
                            ingredient7.SubItems[0].Text = rdring[6].ToString();
                            child.listView2.Items.Add(ingredient7);

                            ListViewItem ingredient8 = new ListViewItem();
                            ingredient8.SubItems[0].Text = rdring[7].ToString();
                            child.listView2.Items.Add(ingredient8);

                            ListViewItem ingredient9 = new ListViewItem();
                            ingredient9.SubItems[0].Text = rdring[8].ToString();
                            child.listView2.Items.Add(ingredient9);

                            ListViewItem ingredient10 = new ListViewItem();
                            ingredient10.SubItems[0].Text = rdring[9].ToString();
                            child.listView2.Items.Add(ingredient10);

                            ListViewItem ingredient11 = new ListViewItem();
                            ingredient11.SubItems[0].Text = rdring[10].ToString();
                            child.listView2.Items.Add(ingredient11);

                            ListViewItem ingredient12 = new ListViewItem();
                            ingredient12.SubItems[0].Text = rdring[11].ToString();
                            child.listView2.Items.Add(ingredient12);

                            ListViewItem ingredient13 = new ListViewItem();
                            ingredient13.SubItems[0].Text = rdring[12].ToString();
                            child.listView2.Items.Add(ingredient13);

                            ListViewItem ingredient14 = new ListViewItem();
                            ingredient14.SubItems[0].Text = rdring[13].ToString();
                            child.listView2.Items.Add(ingredient14);
                        }
                        //Get Amounts
                        SQLiteCommand cmdamt = new SQLiteCommand("SELECT amt1, amt2, amt3, amt4, amt5, amt6, amt7, amt8, amt9, amt10, amt11, amt12, amt13, amt14 FROM punches WHERE punch_key=" + (reader["punch_key"]) + "", cs5);
                        SQLiteDataReader rdramt = cmdamt.ExecuteReader();

                        while (rdramt.Read())
                        {
                            ListViewItem amt1 = new ListViewItem();
                            amt1.SubItems[0].Text = rdramt[0].ToString();
                            //amount.SubItems.Add(rdramt[0].ToString());
                            child.listView1.Items.Add(amt1);

                            ListViewItem amt2 = new ListViewItem();
                            amt2.SubItems[0].Text = rdramt[1].ToString();
                            child.listView1.Items.Add(amt2);

                            ListViewItem amt3 = new ListViewItem();
                            amt3.SubItems[0].Text = rdramt[2].ToString();
                            child.listView1.Items.Add(amt3);

                            ListViewItem amt4 = new ListViewItem();
                            amt4.SubItems[0].Text = rdramt[3].ToString();
                            child.listView1.Items.Add(amt4);

                            ListViewItem amt5 = new ListViewItem();
                            amt5.SubItems[0].Text = rdramt[4].ToString();
                            child.listView1.Items.Add(amt5);

                            ListViewItem amt6 = new ListViewItem();
                            amt6.SubItems[0].Text = rdramt[5].ToString();
                            child.listView1.Items.Add(amt6);

                            ListViewItem amt7 = new ListViewItem();
                            amt7.SubItems[0].Text = rdramt[6].ToString();
                            child.listView1.Items.Add(amt7);

                            ListViewItem amt8 = new ListViewItem();
                            amt8.SubItems[0].Text = rdramt[7].ToString();
                            child.listView1.Items.Add(amt8);

                            ListViewItem amt9 = new ListViewItem();
                            amt9.SubItems[0].Text = rdramt[8].ToString();
                            child.listView1.Items.Add(amt9);

                            ListViewItem amt10 = new ListViewItem();
                            amt10.SubItems[0].Text = rdramt[9].ToString();
                            child.listView1.Items.Add(amt10);

                            ListViewItem amt11 = new ListViewItem();
                            amt11.SubItems[0].Text = rdramt[10].ToString();
                            child.listView1.Items.Add(amt11);

                            ListViewItem amt12 = new ListViewItem();
                            amt12.SubItems[0].Text = rdramt[11].ToString();
                            child.listView1.Items.Add(amt12);

                            ListViewItem amt13 = new ListViewItem();
                            amt13.SubItems[0].Text = rdramt[12].ToString();
                            child.listView1.Items.Add(amt13);

                            ListViewItem amt14 = new ListViewItem();
                            amt14.SubItems[0].Text = rdramt[13].ToString();
                            child.listView1.Items.Add(amt14);
                        }

                        //Get Directions
                        SQLiteCommand cmddir = new SQLiteCommand("SELECT directions FROM punches WHERE punch_key= " + (reader["punch_key"]) + "", cs5);
                        SQLiteDataReader rdr = cmddir.ExecuteReader();

                        while (rdr.Read())
                        {
                            child.richTextBox1.Text = rdr[0].ToString();
                        }

                    }
                }

            }
        }
        private void nameListBox6_DoubleClick(object sender, EventArgs e)
        {
            if (nameListBox6.SelectedItem != null)
            {
                string statusbarrecipe2 = nameListBox6.SelectedValue.ToString();
                toolStripStatusLabel1.Text = statusbarrecipe2;

                //MessageBox.Show(statusbarrecipe2);



                using (SQLiteConnection cs6 = new SQLiteConnection("Data Source = |DataDirectory|\\XpressShots.db"))
                {
                    cs6.Open();

                    //Get ID
                    //string selectedvalue

                    SQLiteCommand cmd = new SQLiteCommand("SELECT coffeetea_key FROM coffeetea WHERE name ='" + statusbarrecipe2 + "'", cs6);
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    //MessageBox.Show(reader["coffeetea_key"].ToString());
                    Form2 child = new Form2(this);
                    child.Text = nameListBox6.SelectedValue.ToString();
                    child.toolStripStatusLabel1.Text = statusbarrecipe2;
                    child.Show();

                    while (reader.Read())
                    {
                        // Get Ingredients
                        SQLiteCommand cmding = new SQLiteCommand("SELECT ingredient1, ingredient2, ingredient3, ingredient4, ingredient5, ingredient6, ingredient7, ingredient8, ingredient9 FROM coffeetea WHERE coffeetea_key=" + (reader["coffeetea_key"]) + "", cs6);
                        SQLiteDataReader rdring = cmding.ExecuteReader();

                        while (rdring.Read())
                        {
                            ListViewItem ingredient1 = new ListViewItem();
                            ingredient1.SubItems[0].Text = rdring[0].ToString();
                            child.listView2.Items.Add(ingredient1);

                            ListViewItem ingredient2 = new ListViewItem();
                            ingredient2.SubItems[0].Text = rdring[1].ToString();
                            child.listView2.Items.Add(ingredient2);

                            ListViewItem ingredient3 = new ListViewItem();
                            ingredient3.SubItems[0].Text = rdring[2].ToString();
                            child.listView2.Items.Add(ingredient3);

                            ListViewItem ingredient4 = new ListViewItem();
                            ingredient4.SubItems[0].Text = rdring[3].ToString();
                            child.listView2.Items.Add(ingredient4);

                            ListViewItem ingredient5 = new ListViewItem();
                            ingredient5.SubItems[0].Text = rdring[4].ToString();
                            child.listView2.Items.Add(ingredient5);

                            ListViewItem ingredient6 = new ListViewItem();
                            ingredient6.SubItems[0].Text = rdring[5].ToString();
                            child.listView2.Items.Add(ingredient6);

                            ListViewItem ingredient7 = new ListViewItem();
                            ingredient7.SubItems[0].Text = rdring[6].ToString();
                            child.listView2.Items.Add(ingredient7);

                            ListViewItem ingredient8 = new ListViewItem();
                            ingredient8.SubItems[0].Text = rdring[7].ToString();
                            child.listView2.Items.Add(ingredient8);

                            ListViewItem ingredient9 = new ListViewItem();
                            ingredient9.SubItems[0].Text = rdring[8].ToString();
                            child.listView2.Items.Add(ingredient9);

                        }
                        //Get Amounts
                        SQLiteCommand cmdamt = new SQLiteCommand("SELECT amt1, amt2, amt3, amt4, amt5, amt6, amt7, amt8, amt9 FROM coffeetea WHERE coffeetea_key=" + (reader["coffeetea_key"]) + "", cs6);
                        SQLiteDataReader rdramt = cmdamt.ExecuteReader();

                        while (rdramt.Read())
                        {
                            ListViewItem amt1 = new ListViewItem();
                            amt1.SubItems[0].Text = rdramt[0].ToString();
                            //amount.SubItems.Add(rdramt[0].ToString());
                            child.listView1.Items.Add(amt1);

                            ListViewItem amt2 = new ListViewItem();
                            amt2.SubItems[0].Text = rdramt[1].ToString();
                            child.listView1.Items.Add(amt2);

                            ListViewItem amt3 = new ListViewItem();
                            amt3.SubItems[0].Text = rdramt[2].ToString();
                            child.listView1.Items.Add(amt3);

                            ListViewItem amt4 = new ListViewItem();
                            amt4.SubItems[0].Text = rdramt[3].ToString();
                            child.listView1.Items.Add(amt4);

                            ListViewItem amt5 = new ListViewItem();
                            amt5.SubItems[0].Text = rdramt[4].ToString();
                            child.listView1.Items.Add(amt5);

                            ListViewItem amt6 = new ListViewItem();
                            amt6.SubItems[0].Text = rdramt[5].ToString();
                            child.listView1.Items.Add(amt6);

                            ListViewItem amt7 = new ListViewItem();
                            amt7.SubItems[0].Text = rdramt[6].ToString();
                            child.listView1.Items.Add(amt7);

                            ListViewItem amt8 = new ListViewItem();
                            amt8.SubItems[0].Text = rdramt[7].ToString();
                            child.listView1.Items.Add(amt8);

                            ListViewItem amt9 = new ListViewItem();
                            amt9.SubItems[0].Text = rdramt[8].ToString();
                            child.listView1.Items.Add(amt9);

                        }

                        //Get Directions
                        SQLiteCommand cmddir = new SQLiteCommand("SELECT directions FROM coffeetea WHERE coffeetea_key= " + (reader["coffeetea_key"]) + "", cs6);
                        SQLiteDataReader rdr = cmddir.ExecuteReader();

                        while (rdr.Read())
                        {
                            child.richTextBox1.Text = rdr[0].ToString();
                        }

                    }
                }

            }
        }
        private void nameListBox7_DoubleClick(object sender, EventArgs e)
        {
            if (nameListBox7.SelectedItem != null)
            {
                string statusbarrecipe2 = nameListBox7.SelectedValue.ToString();
                toolStripStatusLabel1.Text = statusbarrecipe2;

                //MessageBox.Show(statusbarrecipe2);



                using (SQLiteConnection cs7 = new SQLiteConnection("Data Source = |DataDirectory|\\XpressShots.db"))
                {
                    cs7.Open();

                    //Get ID
                    //string selectedvalue

                    SQLiteCommand cmd = new SQLiteCommand("SELECT nonalcoholic_key FROM nonalcoholic WHERE name ='" + (statusbarrecipe2.Trim().Replace("'", "''")) + "'", cs7);
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    //MessageBox.Show(reader["nonalcoholic_key"].ToString());
                    Form2 child = new Form2(this);
                    child.Text = nameListBox7.SelectedValue.ToString();
                    child.toolStripStatusLabel1.Text = statusbarrecipe2;
                    child.Show();

                    while (reader.Read())
                    {
                        // Get Ingredients
                        SQLiteCommand cmding = new SQLiteCommand("SELECT ingredient1, ingredient2, ingredient3, ingredient4, ingredient5, ingredient6, ingredient7, ingredient8, ingredient9, ingredient10, ingredient11, ingredient12 FROM nonalcoholic WHERE nonalcoholic_key=" + (reader["nonalcoholic_key"]) + "", cs7);
                        SQLiteDataReader rdring = cmding.ExecuteReader();

                        while (rdring.Read())
                        {
                            ListViewItem ingredient1 = new ListViewItem();
                            ingredient1.SubItems[0].Text = rdring[0].ToString();
                            child.listView2.Items.Add(ingredient1);

                            ListViewItem ingredient2 = new ListViewItem();
                            ingredient2.SubItems[0].Text = rdring[1].ToString();
                            child.listView2.Items.Add(ingredient2);

                            ListViewItem ingredient3 = new ListViewItem();
                            ingredient3.SubItems[0].Text = rdring[2].ToString();
                            child.listView2.Items.Add(ingredient3);

                            ListViewItem ingredient4 = new ListViewItem();
                            ingredient4.SubItems[0].Text = rdring[3].ToString();
                            child.listView2.Items.Add(ingredient4);

                            ListViewItem ingredient5 = new ListViewItem();
                            ingredient5.SubItems[0].Text = rdring[4].ToString();
                            child.listView2.Items.Add(ingredient5);

                            ListViewItem ingredient6 = new ListViewItem();
                            ingredient6.SubItems[0].Text = rdring[5].ToString();
                            child.listView2.Items.Add(ingredient6);

                            ListViewItem ingredient7 = new ListViewItem();
                            ingredient7.SubItems[0].Text = rdring[6].ToString();
                            child.listView2.Items.Add(ingredient7);

                            ListViewItem ingredient8 = new ListViewItem();
                            ingredient8.SubItems[0].Text = rdring[7].ToString();
                            child.listView2.Items.Add(ingredient8);

                            ListViewItem ingredient9 = new ListViewItem();
                            ingredient9.SubItems[0].Text = rdring[8].ToString();
                            child.listView2.Items.Add(ingredient9);

                            ListViewItem ingredient10 = new ListViewItem();
                            ingredient10.SubItems[0].Text = rdring[9].ToString();
                            child.listView2.Items.Add(ingredient10);

                            ListViewItem ingredient11 = new ListViewItem();
                            ingredient11.SubItems[0].Text = rdring[10].ToString();
                            child.listView2.Items.Add(ingredient11);

                            ListViewItem ingredient12 = new ListViewItem();
                            ingredient12.SubItems[0].Text = rdring[11].ToString();
                            child.listView2.Items.Add(ingredient12);

                        }
                        //Get Amounts
                        SQLiteCommand cmdamt = new SQLiteCommand("SELECT amt1, amt2, amt3, amt4, amt5, amt6, amt7, amt8, amt9, amt10, amt11, amt12 FROM nonalcoholic WHERE nonalcoholic_key=" + (reader["nonalcoholic_key"]) + "", cs7);
                        SQLiteDataReader rdramt = cmdamt.ExecuteReader();

                        while (rdramt.Read())
                        {
                            ListViewItem amt1 = new ListViewItem();
                            amt1.SubItems[0].Text = rdramt[0].ToString();
                            //amount.SubItems.Add(rdramt[0].ToString());
                            child.listView1.Items.Add(amt1);

                            ListViewItem amt2 = new ListViewItem();
                            amt2.SubItems[0].Text = rdramt[1].ToString();
                            child.listView1.Items.Add(amt2);

                            ListViewItem amt3 = new ListViewItem();
                            amt3.SubItems[0].Text = rdramt[2].ToString();
                            child.listView1.Items.Add(amt3);

                            ListViewItem amt4 = new ListViewItem();
                            amt4.SubItems[0].Text = rdramt[3].ToString();
                            child.listView1.Items.Add(amt4);

                            ListViewItem amt5 = new ListViewItem();
                            amt5.SubItems[0].Text = rdramt[4].ToString();
                            child.listView1.Items.Add(amt5);

                            ListViewItem amt6 = new ListViewItem();
                            amt6.SubItems[0].Text = rdramt[5].ToString();
                            child.listView1.Items.Add(amt6);

                            ListViewItem amt7 = new ListViewItem();
                            amt7.SubItems[0].Text = rdramt[6].ToString();
                            child.listView1.Items.Add(amt7);

                            ListViewItem amt8 = new ListViewItem();
                            amt8.SubItems[0].Text = rdramt[7].ToString();
                            child.listView1.Items.Add(amt8);

                            ListViewItem amt9 = new ListViewItem();
                            amt9.SubItems[0].Text = rdramt[8].ToString();
                            child.listView1.Items.Add(amt9);

                            ListViewItem amt10 = new ListViewItem();
                            amt10.SubItems[0].Text = rdramt[9].ToString();
                            child.listView1.Items.Add(amt10);

                            ListViewItem amt11 = new ListViewItem();
                            amt11.SubItems[0].Text = rdramt[10].ToString();
                            child.listView1.Items.Add(amt11);

                            ListViewItem amt12 = new ListViewItem();
                            amt12.SubItems[0].Text = rdramt[11].ToString();
                            child.listView1.Items.Add(amt12);

                        }

                        //Get Directions
                        SQLiteCommand cmddir = new SQLiteCommand("SELECT directions FROM nonalcoholic WHERE nonalcoholic_key= " + (reader["nonalcoholic_key"]) + "", cs7);
                        SQLiteDataReader rdr = cmddir.ExecuteReader();

                        while (rdr.Read())
                        {
                            child.richTextBox1.Text = rdr[0].ToString();
                        }

                    }
                }

            }
        }
        private void nameListBox8_DoubleClick(object sender, EventArgs e)
        {
            if (nameListBox8.SelectedItem != null)
            {
                string statusbarrecipe2 = nameListBox8.SelectedValue.ToString();
                toolStripStatusLabel1.Text = statusbarrecipe2;

                //MessageBox.Show(statusbarrecipe2);



                using (SQLiteConnection cs8 = new SQLiteConnection("Data Source = |DataDirectory|\\XpressShots.db"))
                {
                    cs8.Open();

                    //Get ID
                    //string selectedvalue

                    SQLiteCommand cmd = new SQLiteCommand("SELECT cocktail_key FROM cocktails WHERE name ='" + (statusbarrecipe2.Trim().Replace("'", "''")) + "'", cs8);
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    //MessageBox.Show(reader["cocktail_key"].ToString());
                    Form2 child = new Form2(this);
                    child.Text = nameListBox8.SelectedValue.ToString();
                    child.toolStripStatusLabel1.Text = statusbarrecipe2;
                    child.Show();

                    while (reader.Read())
                    {
                        // Get Ingredients
                        SQLiteCommand cmding = new SQLiteCommand("SELECT ingredient1, ingredient2, ingredient3, ingredient4, ingredient5, ingredient6, ingredient7, ingredient8, ingredient9, ingredient10, ingredient11, ingredient12, ingredient13 FROM cocktails WHERE cocktail_key=" + (reader["cocktail_key"]) + "", cs8);
                        SQLiteDataReader rdring = cmding.ExecuteReader();

                        while (rdring.Read())
                        {
                            ListViewItem ingredient1 = new ListViewItem();
                            ingredient1.SubItems[0].Text = rdring[0].ToString();
                            child.listView2.Items.Add(ingredient1);

                            ListViewItem ingredient2 = new ListViewItem();
                            ingredient2.SubItems[0].Text = rdring[1].ToString();
                            child.listView2.Items.Add(ingredient2);

                            ListViewItem ingredient3 = new ListViewItem();
                            ingredient3.SubItems[0].Text = rdring[2].ToString();
                            child.listView2.Items.Add(ingredient3);

                            ListViewItem ingredient4 = new ListViewItem();
                            ingredient4.SubItems[0].Text = rdring[3].ToString();
                            child.listView2.Items.Add(ingredient4);

                            ListViewItem ingredient5 = new ListViewItem();
                            ingredient5.SubItems[0].Text = rdring[4].ToString();
                            child.listView2.Items.Add(ingredient5);

                            ListViewItem ingredient6 = new ListViewItem();
                            ingredient6.SubItems[0].Text = rdring[5].ToString();
                            child.listView2.Items.Add(ingredient6);

                            ListViewItem ingredient7 = new ListViewItem();
                            ingredient7.SubItems[0].Text = rdring[6].ToString();
                            child.listView2.Items.Add(ingredient7);

                            ListViewItem ingredient8 = new ListViewItem();
                            ingredient8.SubItems[0].Text = rdring[7].ToString();
                            child.listView2.Items.Add(ingredient8);

                            ListViewItem ingredient9 = new ListViewItem();
                            ingredient9.SubItems[0].Text = rdring[8].ToString();
                            child.listView2.Items.Add(ingredient9);

                            ListViewItem ingredient10 = new ListViewItem();
                            ingredient10.SubItems[0].Text = rdring[9].ToString();
                            child.listView2.Items.Add(ingredient10);

                            ListViewItem ingredient11 = new ListViewItem();
                            ingredient11.SubItems[0].Text = rdring[10].ToString();
                            child.listView2.Items.Add(ingredient11);

                            ListViewItem ingredient12 = new ListViewItem();
                            ingredient12.SubItems[0].Text = rdring[11].ToString();
                            child.listView2.Items.Add(ingredient12);

                            ListViewItem ingredient13 = new ListViewItem();
                            ingredient13.SubItems[0].Text = rdring[12].ToString();
                            child.listView2.Items.Add(ingredient13);
                        }
                        //Get Amounts
                        SQLiteCommand cmdamt = new SQLiteCommand("SELECT amt1, amt2, amt3, amt4, amt5, amt6, amt7, amt8, amt9, amt10, amt11, amt12, amt13 FROM cocktails WHERE cocktail_key=" + (reader["cocktail_key"]) + "", cs8);
                        SQLiteDataReader rdramt = cmdamt.ExecuteReader();

                        while (rdramt.Read())
                        {
                            ListViewItem amt1 = new ListViewItem();
                            amt1.SubItems[0].Text = rdramt[0].ToString();
                            //amount.SubItems.Add(rdramt[0].ToString());
                            child.listView1.Items.Add(amt1);

                            ListViewItem amt2 = new ListViewItem();
                            amt2.SubItems[0].Text = rdramt[1].ToString();
                            child.listView1.Items.Add(amt2);

                            ListViewItem amt3 = new ListViewItem();
                            amt3.SubItems[0].Text = rdramt[2].ToString();
                            child.listView1.Items.Add(amt3);

                            ListViewItem amt4 = new ListViewItem();
                            amt4.SubItems[0].Text = rdramt[3].ToString();
                            child.listView1.Items.Add(amt4);

                            ListViewItem amt5 = new ListViewItem();
                            amt5.SubItems[0].Text = rdramt[4].ToString();
                            child.listView1.Items.Add(amt5);

                            ListViewItem amt6 = new ListViewItem();
                            amt6.SubItems[0].Text = rdramt[5].ToString();
                            child.listView1.Items.Add(amt6);

                            ListViewItem amt7 = new ListViewItem();
                            amt7.SubItems[0].Text = rdramt[6].ToString();
                            child.listView1.Items.Add(amt7);

                            ListViewItem amt8 = new ListViewItem();
                            amt8.SubItems[0].Text = rdramt[7].ToString();
                            child.listView1.Items.Add(amt8);

                            ListViewItem amt9 = new ListViewItem();
                            amt9.SubItems[0].Text = rdramt[8].ToString();
                            child.listView1.Items.Add(amt9);

                            ListViewItem amt10 = new ListViewItem();
                            amt10.SubItems[0].Text = rdramt[9].ToString();
                            child.listView1.Items.Add(amt10);

                            ListViewItem amt11 = new ListViewItem();
                            amt11.SubItems[0].Text = rdramt[10].ToString();
                            child.listView1.Items.Add(amt11);

                            ListViewItem amt12 = new ListViewItem();
                            amt12.SubItems[0].Text = rdramt[11].ToString();
                            child.listView1.Items.Add(amt12);

                            ListViewItem amt13 = new ListViewItem();
                            amt13.SubItems[0].Text = rdramt[12].ToString();
                            child.listView1.Items.Add(amt13);

                        }

                        //Get Directions
                        SQLiteCommand cmddir = new SQLiteCommand("SELECT directions FROM cocktails WHERE cocktail_key = " + (reader["cocktail_key"]) + "", cs8);
                        SQLiteDataReader rdr = cmddir.ExecuteReader();

                        while (rdr.Read())
                        {
                            child.richTextBox1.Text = rdr[0].ToString();
                        }

                    }
                }

            }
        }
        private void nameListBox9_DoubleClick(object sender, EventArgs e)
        {
            if (nameListBox9.SelectedItem != null)
            {
                string statusbarrecipe2 = nameListBox9.SelectedValue.ToString();
                toolStripStatusLabel1.Text = statusbarrecipe2;

                //MessageBox.Show(statusbarrecipe2);



                using (SqlCeConnection cs9 = new SqlCeConnection("Data Source = |DataDirectory|\\custom_recipes.sdf"))
                {
                    cs9.Open();

                    //Get ID
                    //string selectedvalue

                    SqlCeCommand cmd = new SqlCeCommand("SELECT myrecipes_key FROM myRecipes WHERE name='" + (statusbarrecipe2.Trim().Replace("'", "''")) + "'", cs9);
                    SqlCeDataReader reader = cmd.ExecuteReader();

                    //MessageBox.Show(reader["myrecipes_key"].ToString());
                    Form2 child = new Form2(this);
                    child.Text = nameListBox9.SelectedValue.ToString();
                    child.toolStripStatusLabel1.Text = statusbarrecipe2;
                    child.Show();

                    while (reader.Read())
                    {
                        // Get Ingredients
                        SqlCeCommand cmding = new SqlCeCommand("SELECT ingredient1, ingredient2, ingredient3, ingredient4, ingredient5, ingredient6, ingredient7, ingredient8, ingredient9, ingredient10, ingredient11, ingredient12, ingredient13, ingredient14, ingredient15 FROM myRecipes WHERE myrecipes_key=" + (reader["myrecipes_key"]) + "", cs9);
                        SqlCeDataReader rdring = cmding.ExecuteReader();

                        while (rdring.Read())
                        {

                            ListViewItem ingredient1 = new ListViewItem();
                            ingredient1.SubItems[0].Text = rdring[0].ToString();
                            child.listView2.Items.Add(ingredient1);

                            ListViewItem ingredient2 = new ListViewItem();
                            ingredient2.SubItems[0].Text = rdring[1].ToString();
                            child.listView2.Items.Add(ingredient2);

                            ListViewItem ingredient3 = new ListViewItem();
                            ingredient3.SubItems[0].Text = rdring[2].ToString();
                            child.listView2.Items.Add(ingredient3);

                            ListViewItem ingredient4 = new ListViewItem();
                            ingredient4.SubItems[0].Text = rdring[3].ToString();
                            child.listView2.Items.Add(ingredient4);

                            ListViewItem ingredient5 = new ListViewItem();
                            ingredient5.SubItems[0].Text = rdring[4].ToString();
                            child.listView2.Items.Add(ingredient5);

                            ListViewItem ingredient6 = new ListViewItem();
                            ingredient6.SubItems[0].Text = rdring[5].ToString();
                            child.listView2.Items.Add(ingredient6);

                            ListViewItem ingredient7 = new ListViewItem();
                            ingredient7.SubItems[0].Text = rdring[6].ToString();
                            child.listView2.Items.Add(ingredient7);

                            ListViewItem ingredient8 = new ListViewItem();
                            ingredient8.SubItems[0].Text = rdring[7].ToString();
                            child.listView2.Items.Add(ingredient8);

                            ListViewItem ingredient9 = new ListViewItem();
                            ingredient9.SubItems[0].Text = rdring[8].ToString();
                            child.listView2.Items.Add(ingredient9);

                            ListViewItem ingredient10 = new ListViewItem();
                            ingredient10.SubItems[0].Text = rdring[9].ToString();
                            child.listView2.Items.Add(ingredient10);

                            ListViewItem ingredient11 = new ListViewItem();
                            ingredient11.SubItems[0].Text = rdring[10].ToString();
                            child.listView2.Items.Add(ingredient11);

                            ListViewItem ingredient12 = new ListViewItem();
                            ingredient12.SubItems[0].Text = rdring[11].ToString();
                            child.listView2.Items.Add(ingredient12);

                            ListViewItem ingredient13 = new ListViewItem();
                            ingredient13.SubItems[0].Text = rdring[12].ToString();
                            child.listView2.Items.Add(ingredient13);

                            ListViewItem ingredient14 = new ListViewItem();
                            ingredient14.SubItems[0].Text = rdring[13].ToString();
                            child.listView2.Items.Add(ingredient14);

                            ListViewItem ingredient15 = new ListViewItem();
                            ingredient15.SubItems[0].Text = rdring[14].ToString();
                            child.listView2.Items.Add(ingredient15);
                        }
                        //Get Amounts
                        SqlCeCommand cmdamt = new SqlCeCommand("SELECT amt1, amt2, amt3, amt4, amt5, amt6, amt7, amt8, amt9, amt10, amt11, amt12, amt13, amt14, amt15 FROM myRecipes WHERE myrecipes_key=" + (reader["myrecipes_key"]) + "", cs9);
                        SqlCeDataReader rdramt = cmdamt.ExecuteReader();

                        while (rdramt.Read())
                        {
                            ListViewItem amt1 = new ListViewItem();
                            amt1.SubItems[0].Text = rdramt[0].ToString();
                            //amount.SubItems.Add(rdramt[0].ToString());
                            child.listView1.Items.Add(amt1);

                            ListViewItem amt2 = new ListViewItem();
                            amt2.SubItems[0].Text = rdramt[1].ToString();
                            child.listView1.Items.Add(amt2);

                            ListViewItem amt3 = new ListViewItem();
                            amt3.SubItems[0].Text = rdramt[2].ToString();
                            child.listView1.Items.Add(amt3);

                            ListViewItem amt4 = new ListViewItem();
                            amt4.SubItems[0].Text = rdramt[3].ToString();
                            child.listView1.Items.Add(amt4);

                            ListViewItem amt5 = new ListViewItem();
                            amt5.SubItems[0].Text = rdramt[4].ToString();
                            child.listView1.Items.Add(amt5);

                            ListViewItem amt6 = new ListViewItem();
                            amt6.SubItems[0].Text = rdramt[5].ToString();
                            child.listView1.Items.Add(amt6);

                            ListViewItem amt7 = new ListViewItem();
                            amt7.SubItems[0].Text = rdramt[6].ToString();
                            child.listView1.Items.Add(amt7);

                            ListViewItem amt8 = new ListViewItem();
                            amt8.SubItems[0].Text = rdramt[7].ToString();
                            child.listView1.Items.Add(amt8);

                            ListViewItem amt9 = new ListViewItem();
                            amt9.SubItems[0].Text = rdramt[8].ToString();
                            child.listView1.Items.Add(amt9);

                            ListViewItem amt10 = new ListViewItem();
                            amt10.SubItems[0].Text = rdramt[9].ToString();
                            child.listView1.Items.Add(amt10);

                            ListViewItem amt11 = new ListViewItem();
                            amt11.SubItems[0].Text = rdramt[10].ToString();
                            child.listView1.Items.Add(amt11);

                            ListViewItem amt12 = new ListViewItem();
                            amt12.SubItems[0].Text = rdramt[11].ToString();
                            child.listView1.Items.Add(amt12);

                            ListViewItem amt13 = new ListViewItem();
                            amt13.SubItems[0].Text = rdramt[12].ToString();
                            child.listView1.Items.Add(amt13);

                            ListViewItem amt14 = new ListViewItem();
                            amt14.SubItems[0].Text = rdramt[13].ToString();
                            child.listView1.Items.Add(amt14);

                            ListViewItem amt15 = new ListViewItem();
                            amt15.SubItems[0].Text = rdramt[14].ToString();
                            child.listView1.Items.Add(amt15);
                        }

                        //Get Directions
                        SqlCeCommand cmddir = new SqlCeCommand("SELECT directions FROM myRecipes WHERE myrecipes_key= " + (reader["myrecipes_key"]) + "", cs9);
                        SqlCeDataReader rdr = cmddir.ExecuteReader();

                        while (rdr.Read())
                        {
                            child.richTextBox1.Text = rdr[0].ToString();
                        }

                        //Refresh database
                        nameListBox9.DataSource = null;
                        nameListBox9.Items.Clear();
                        //loads data into the 'custom_RecipesDataSet' table.
                        this.myRecipesTableAdapter1.Fill(this.custom_recipesDataSet.myRecipes);
                        nameListBox9.DataSource = custom_recipesDataSet.Tables["myRecipes"];
                        nameListBox9.DisplayMember = "name";
                        nameListBox9.ValueMember = "name";
                    }
                }

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int index = nameListBox.FindString(textBox1.Text);
            if (0 <= index)
            {
                nameListBox.SelectedIndex = index;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int index = nameListBox2.FindString(textBox2.Text);
            if (0 <= index)
            {
                nameListBox2.SelectedIndex = index;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int index = nameListBox3.FindString(textBox3.Text);
            if (0 <= index)
            {
                nameListBox3.SelectedIndex = index;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            int index = nameListBox4.FindString(textBox4.Text);
            if (0 <= index)
            {
                nameListBox4.SelectedIndex = index;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            int index = nameListBox5.FindString(textBox5.Text);
            if (0 <= index)
            {
                nameListBox5.SelectedIndex = index;
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            int index = nameListBox6.FindString(textBox6.Text);
            if (0 <= index)
            {
                nameListBox6.SelectedIndex = index;
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            int index = nameListBox7.FindString(textBox7.Text);
            if (0 <= index)
            {
                nameListBox7.SelectedIndex = index;
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            int index = nameListBox8.FindString(textBox8.Text);
            if (0 <= index)
            {
                nameListBox8.SelectedIndex = index;
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            int index = nameListBox9.FindString(textBox9.Text);
            if (0 <= index)
            {
                nameListBox9.SelectedIndex = index;
            }
        }

        private void CheckKeys(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.nameListBox_DoubleClick(e.KeyChar, e);
            }
        }

        private void CheckKeys2(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.nameListBox2_DoubleClick(e.KeyChar, e);
            }
        }

        private void CheckKeys3(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.nameListBox3_DoubleClick(e.KeyChar, e);
            }
        }

        private void CheckKeys4(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.nameListBox4_DoubleClick(e.KeyChar, e);
            }
        }

        private void CheckKeys5(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.nameListBox5_DoubleClick(e.KeyChar, e);
            }
        }

        private void CheckKeys6(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.nameListBox6_DoubleClick(e.KeyChar, e);
            }
        }

        private void CheckKeys7(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.nameListBox7_DoubleClick(e.KeyChar, e);
            }
        }

        private void CheckKeys8(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.nameListBox8_DoubleClick(e.KeyChar, e);
            }
        }

        private void CheckKeys9(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.nameListBox9_DoubleClick(e.KeyChar, e);
            }
        }

        private void CascadeMyWindows(object sender, System.EventArgs e)
        {
            // Cascade all MDI child windows.
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox5.Clear();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox6.Clear();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox7.Clear();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBox8.Clear();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox9.Clear();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            nameListBox9.DataSource = null;
            nameListBox9.Items.Clear();
            //loads data into the 'custom_RecipesDataSet' table.
            this.myRecipesTableAdapter1.Fill(this.custom_recipesDataSet.myRecipes);
            nameListBox9.DataSource = custom_recipesDataSet.Tables["myRecipes"];
            nameListBox9.DisplayMember = "name";
            nameListBox9.ValueMember = "name";
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (nameListBox9.SelectedItem != null)
            {
                string statusbarrecipe3 = nameListBox9.SelectedValue.ToString();
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to Delete " + statusbarrecipe3 + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    using (SqlCeConnection cs10 = new SqlCeConnection("Data Source = |DataDirectory|\\custom_recipes.sdf"))
                    {
                        cs10.Open();

                        //Get ID
                        //string selectedvalue
                        SqlCeCommand cmd = new SqlCeCommand("SELECT myrecipes_key FROM myRecipes WHERE name ='" + (statusbarrecipe3.Trim().Replace("'", "''")) + "'", cs10);
                        SqlCeDataReader readthis = cmd.ExecuteReader();
                        while (readthis.Read())
                        {


                            object key = (readthis["myrecipes_key"]);
                            // MessageBox.Show("Are you sure you want to delete?");
                            SqlCeCommand cmdit = new SqlCeCommand("DELETE FROM myRecipes WHERE  name ='" + (statusbarrecipe3.Trim().Replace("'", "''")) + "'", cs10);
                            cmdit.ExecuteNonQuery();
                        }
                        nameListBox9.DataSource = null;
                        nameListBox9.Items.Clear();
                        //loads data into the 'custom_RecipesDataSet' table.
                        this.myRecipesTableAdapter1.Fill(this.custom_recipesDataSet.myRecipes);
                        nameListBox9.DataSource = custom_recipesDataSet.Tables["myRecipes"];
                        nameListBox9.DisplayMember = "name";
                        nameListBox9.ValueMember = "name";
                    }
                }
            }

        }

        private void button21_Click(object sender, EventArgs e)
        {
            nameListBox8.DataSource = null;
            nameListBox8.Items.Clear();
            //loads data into the 'xpressShotsDataSet.cocktails' table.
            this.cocktailsTableAdapter.Fill(this.xpressShotsDataSet.cocktails);
            nameListBox8.DataSource = xpressShotsDataSet.Tables["cocktails"];
            nameListBox8.DisplayMember = "name";
            nameListBox8.ValueMember = "name";
        }

        private void button22_Click(object sender, EventArgs e)
        {
            nameListBox7.DataSource = null;
            nameListBox7.Items.Clear();
            //loads data into the 'xpressShotsDataSet.nonalcoholic' table.
            this.nonalcoholicTableAdapter.Fill(this.xpressShotsDataSet.nonalcoholic);
            nameListBox7.DataSource = xpressShotsDataSet.Tables["nonalcoholic"];
            nameListBox7.DisplayMember = "name";
            nameListBox7.ValueMember = "name";
        }

        private void button23_Click(object sender, EventArgs e)
        {
            nameListBox6.DataSource = null;
            nameListBox6.Items.Clear();
            //loads data into the 'xpressShotsDataSet.coffeetea' table.
            this.coffeeteaTableAdapter.Fill(this.xpressShotsDataSet.coffeetea);
            nameListBox6.DataSource = xpressShotsDataSet.Tables["coffeetea"];
            nameListBox6.DisplayMember = "name";
            nameListBox6.ValueMember = "name";
        }


        private void button24_Click(object sender, EventArgs e)
        {
            nameListBox5.DataSource = null;
            nameListBox5.Items.Clear();
            //loads data into the 'xpressShotsDataSet.punches' table.
            this.punchesTableAdapter.Fill(this.xpressShotsDataSet.punches);
            nameListBox5.DataSource = xpressShotsDataSet.Tables["punches"];
            nameListBox5.DisplayMember = "name";
            nameListBox5.ValueMember = "name";
        }

        private void button25_Click(object sender, EventArgs e)
        {
            nameListBox4.DataSource = null;
            nameListBox4.Items.Clear();
            //loads data into the 'xpressShotsDataSet.beers' table.
            this.beersTableAdapter.Fill(this.xpressShotsDataSet.beers);
            nameListBox4.DataSource = xpressShotsDataSet.Tables["beers"];
            nameListBox4.DisplayMember = "name";
            nameListBox4.ValueMember = "name";
        }

        private void button26_Click(object sender, EventArgs e)
        {
            nameListBox3.DataSource = null;
            nameListBox3.Items.Clear();
            //loads data into the 'xpressShotsDataSet.liqueurs' table.
            this.liqueursTableAdapter.Fill(this.xpressShotsDataSet.liqueurs);
            nameListBox3.DataSource = xpressShotsDataSet.Tables["liqueurs"];
            nameListBox3.DisplayMember = "name";
            nameListBox3.ValueMember = "name";
        }

        private void button27_Click(object sender, EventArgs e)
        {
            nameListBox2.DataSource = null;
            nameListBox2.Items.Clear();
            //loads data into the 'xpressShotsDataSet.shots' table.
            this.shotsTableAdapter.Fill(this.xpressShotsDataSet.shots);
            nameListBox2.DataSource = xpressShotsDataSet.Tables["shots"];
            nameListBox2.DisplayMember = "name";
            nameListBox2.ValueMember = "name";
        }

        private void button28_Click(object sender, EventArgs e)
        {
            nameListBox.DataSource = null;
            nameListBox.Items.Clear();
            //loads data into the 'xpressShotsDataSet.recipes' table.
            this.recipesTableAdapter.Fill(this.xpressShotsDataSet.recipes);
            nameListBox.DataSource = xpressShotsDataSet.Tables["recipes"];
            nameListBox.DisplayMember = "name";
            nameListBox.ValueMember = "name";

        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDialog PrintDialog1 = new PrintDialog();
            PrintDialog1.ShowDialog();

        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
            printPreviewDialog1.ShowDialog();
        }

        //public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);



        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bartender Express Version 3.0\nCopyright © 1996-2012 LollieSoft Inc.\nwww.lolliesoft.com", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, Application.StartupPath + "\\bartenderexpress.chm");
        }

        private void indexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelpIndex(this, Application.StartupPath + "\\bartenderexpress.chm");
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, Application.StartupPath + "\\bartenderexpress.chm");
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
        }

        private void tileHorizontalyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);
        }

        private void tileVerticallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);
        }

        private void cascadetoolStripButton_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
        }

        private void tileHorizontaltoolStripButton_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);
        }

        private void tileVerticaltoolStripButton_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);
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
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            // This event is called once for each tab button in your tab control

            // First paint the background with a color based on the current tab

            // e.Index is the index of the tab in the TabPages collection.
            switch (e.Index)
            {
                case 0:
                    e.Graphics.FillRectangle(new SolidBrush(Color.Red), e.Bounds);
                    break;
                case 1:
                    e.Graphics.FillRectangle(new SolidBrush(Color.CadetBlue), e.Bounds);
                    break;
                case 2:
                    e.Graphics.FillRectangle(new SolidBrush(Color.Crimson), e.Bounds);
                    break;
                case 3:
                    e.Graphics.FillRectangle(new SolidBrush(Color.DarkViolet), e.Bounds);
                    break;
                case 4:
                    e.Graphics.FillRectangle(new SolidBrush(Color.DeepPink), e.Bounds);
                    break;
                case 5:
                    e.Graphics.FillRectangle(new SolidBrush(Color.DimGray), e.Bounds);
                    break;
                case 6:
                    e.Graphics.FillRectangle(new SolidBrush(Color.DarkSeaGreen), e.Bounds);
                    break;
                case 7:
                    e.Graphics.FillRectangle(new SolidBrush(Color.BurlyWood), e.Bounds);
                    break;
                case 8:
                    e.Graphics.FillRectangle(new SolidBrush(Color.DarkOrange), e.Bounds);
                    break;
                default:
                    break;
            }

            // Then draw the current tab button text 
            Rectangle paddedBounds = e.Bounds;
            paddedBounds.Inflate(0, -2);
            e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, this.Font, SystemBrushes.HighlightText, paddedBounds);

        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            Form3 child = new Form3(this);
            child.myRecipesBindingSource.AddNew();
            child.Show();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 child = new Form3(this);
            child.myRecipesBindingSource.AddNew();
            child.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void maleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 child = new Form4(this);
            child.Show();
        }

        private void femaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 child = new Form5(this);
            child.Show();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BAC_Calculator child = new BAC_Calculator(this);
            child.Show();
        }

        private void bartenderExpressMeasurementCalculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + "\\bartenderExpressCalculator.exe");
        }

        private void bartendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + "\\barbasics.pdf");
        }

        private void facebookToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://www.facebook.com/BartenderExpress");
            }
            catch { }
        }

        private void twitterToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://twitter.com/lolliesoft");
            }
            catch { } 
        }

        private void pinterestStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://pinterest.com/lolliesoft/bartender-express-software/");
            }
            catch { } 

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            String abc = @"Software\LollieSoft\Bartender Express";
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
           //if (logic == true);       
        }
      }
    }



   

