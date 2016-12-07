namespace SimpleRssDemo
{
  partial class SimpleRssForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose ();
      }
      base.Dispose (disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.btnView = new System.Windows.Forms.Button ();
      this.btnUpdate = new System.Windows.Forms.Button ();
      this.lblUpdated = new System.Windows.Forms.Label ();
      this.lblItems = new System.Windows.Forms.Label ();
      this.listItems = new System.Windows.Forms.ListView ();
      this.columnHeaderTime = new System.Windows.Forms.ColumnHeader ();
      this.columnHeaderItem = new System.Windows.Forms.ColumnHeader ();
      this.lblChannel = new System.Windows.Forms.Label ();
      this.textDescription = new System.Windows.Forms.TextBox ();
      this.comboChannels = new System.Windows.Forms.ComboBox ();
      this.SuspendLayout ();
      // 
      // btnView
      // 
      this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.btnView.Font = new System.Drawing.Font ("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnView.Location = new System.Drawing.Point (412, 315);
      this.btnView.Name = "btnView";
      this.btnView.Size = new System.Drawing.Size (63, 23);
      this.btnView.TabIndex = 14;
      this.btnView.Text = "View";
      this.btnView.Click += new System.EventHandler (this.OnBtnView_Clicked);
      // 
      // btnUpdate
      // 
      this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.btnUpdate.Location = new System.Drawing.Point (221, 21);
      this.btnUpdate.Name = "btnUpdate";
      this.btnUpdate.Size = new System.Drawing.Size (62, 23);
      this.btnUpdate.TabIndex = 10;
      this.btnUpdate.Text = "Update";
      this.btnUpdate.Click += new System.EventHandler (this.OnBtnUpdate_Clicked);
      // 
      // lblUpdated
      // 
      this.lblUpdated.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.lblUpdated.Location = new System.Drawing.Point (230, 48);
      this.lblUpdated.Name = "lblUpdated";
      this.lblUpdated.Size = new System.Drawing.Size (244, 16);
      this.lblUpdated.TabIndex = 12;
      this.lblUpdated.Text = "Updated:";
      this.lblUpdated.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.lblUpdated.Visible = false;
      // 
      // lblItems
      // 
      this.lblItems.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.lblItems.Font = new System.Drawing.Font ("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblItems.Location = new System.Drawing.Point (13, 48);
      this.lblItems.Name = "lblItems";
      this.lblItems.Size = new System.Drawing.Size (83, 16);
      this.lblItems.TabIndex = 11;
      this.lblItems.Text = "Items:";
      this.lblItems.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // listItems
      // 
      this.listItems.Columns.AddRange (new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTime,
            this.columnHeaderItem});
      this.listItems.Font = new System.Drawing.Font ("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.listItems.FullRowSelect = true;
      this.listItems.GridLines = true;
      this.listItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.listItems.HideSelection = false;
      this.listItems.Location = new System.Drawing.Point (12, 64);
      this.listItems.MultiSelect = false;
      this.listItems.Name = "listItems";
      this.listItems.Size = new System.Drawing.Size (462, 194);
      this.listItems.TabIndex = 13;
      this.listItems.UseCompatibleStateImageBehavior = false;
      this.listItems.View = System.Windows.Forms.View.Details;
      this.listItems.DoubleClick += new System.EventHandler (this.OnList_DblClicked);
      this.listItems.SelectedIndexChanged += new System.EventHandler (this.OnListItems_SelChanged);
      // 
      // columnHeaderTime
      // 
      this.columnHeaderTime.Text = "Time";
      this.columnHeaderTime.Width = 114;
      // 
      // columnHeaderItem
      // 
      this.columnHeaderItem.Text = "Item";
      this.columnHeaderItem.Width = 340;
      // 
      // lblChannel
      // 
      this.lblChannel.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.lblChannel.Font = new System.Drawing.Font ("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblChannel.Location = new System.Drawing.Point (13, 7);
      this.lblChannel.Name = "lblChannel";
      this.lblChannel.Size = new System.Drawing.Size (83, 16);
      this.lblChannel.TabIndex = 8;
      this.lblChannel.Text = "Channel:";
      this.lblChannel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // textDescription
      // 
      this.textDescription.Font = new System.Drawing.Font ("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textDescription.Location = new System.Drawing.Point (12, 261);
      this.textDescription.Multiline = true;
      this.textDescription.Name = "textDescription";
      this.textDescription.ReadOnly = true;
      this.textDescription.Size = new System.Drawing.Size (462, 47);
      this.textDescription.TabIndex = 15;
      // 
      // comboChannels
      // 
      this.comboChannels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboChannels.Location = new System.Drawing.Point (12, 23);
      this.comboChannels.Name = "comboChannels";
      this.comboChannels.Size = new System.Drawing.Size (204, 21);
      this.comboChannels.Sorted = true;
      this.comboChannels.TabIndex = 9;
      // 
      // SimpleRssForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size (483, 344);
      this.Controls.Add (this.btnView);
      this.Controls.Add (this.btnUpdate);
      this.Controls.Add (this.lblUpdated);
      this.Controls.Add (this.lblItems);
      this.Controls.Add (this.listItems);
      this.Controls.Add (this.lblChannel);
      this.Controls.Add (this.textDescription);
      this.Controls.Add (this.comboChannels);
      this.Font = new System.Drawing.Font ("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Name = "SimpleRssForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Simple RSS";
      this.Load += new System.EventHandler (this.OnLoad);
      this.ResumeLayout (false);
      this.PerformLayout ();

    }

    #endregion

    private System.Windows.Forms.Button btnView;
    private System.Windows.Forms.Button btnUpdate;
    private System.Windows.Forms.Label lblUpdated;
    private System.Windows.Forms.Label lblItems;
    private System.Windows.Forms.ListView listItems;
    private System.Windows.Forms.ColumnHeader columnHeaderTime;
    private System.Windows.Forms.ColumnHeader columnHeaderItem;
    private System.Windows.Forms.Label lblChannel;
    private System.Windows.Forms.TextBox textDescription;
    private System.Windows.Forms.ComboBox comboChannels;
  }
}

