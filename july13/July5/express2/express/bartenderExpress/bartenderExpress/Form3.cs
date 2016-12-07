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
    public partial class Form3 : Form
    {
        public Form3(Form1 parent)
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'custom_recipesDataSet.myRecipes' table. You can move, or remove it, as needed.
            this.myRecipesTableAdapter.Fill(this.custom_recipesDataSet.myRecipes);

        }

        string exception = "Exception Occured";
        private void ClearTable(DataTable tableAdapterManager)
        {
            try
            {
                tableAdapterManager.Clear();
            }
            catch (DataException )
            {
                // Process exception and return
                MessageBox.Show(exception);
            }
        }
        public void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            
            //this.myRecipesBindingSource.AddNew();
            //this.myRecipesBindingSource.Clear();
        }

        private void myRecipesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.myRecipesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.custom_recipesDataSet);
            string message = "Custom recipes saved";
            MessageBox.Show(message);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form3_Load_1(object sender, EventArgs e)
        {

        }
    }
}
