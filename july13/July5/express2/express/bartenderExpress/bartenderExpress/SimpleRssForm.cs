using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using RavSoft;

namespace SimpleRssDemo
{
  public partial class SimpleRssForm : Form
  {
    public SimpleRssForm()
    {
      InitializeComponent ();
    }

    private void OnLoad(object sender, EventArgs e)
    {
      // Load combo box with a few channels
      comboChannels.Items.Add
        (new RSSChannel
          ("ABC News - International",
           "http://my.abcnews.go.com/rsspublic/world_rss20.xml"));
      comboChannels.Items.Add
        (new RSSChannel
          ("CBS News - US",
           "http://www.cbsnews.com/feeds/rss/national.rss"));
      comboChannels.Items.Add
        (new RSSChannel
          ("Google News - World",
           "http://news.google.com/news?ned=us&topic=w&output=rss"));
      comboChannels.Items.Add
        (new RSSChannel
          ("Reuters - Business News",
           "http://feeds.feedburner.com/reuters/businessNews"));
      comboChannels.Items.Add
        (new RSSChannel
          ("Yahoo Sports",
           "http://rss.news.yahoo.com/rss/sports"));
      comboChannels.Items.Add
        (new RSSChannel
          ("New York Times - National News",
           "http://www.nytimes.com/services/xml/rss/nyt/National.xml"));
      comboChannels.Items.Add
        (new RSSChannel
          ("ZiffDavis Technology News",
           "http://rssnewsapps.ziffdavis.com/tech.xml"));

      // And select the first one
      comboChannels.SelectedIndex = 0;
    }

    private void OnBtnUpdate_Clicked(object sender, EventArgs e)
    {
      // Fetch the items in the selected channel and reload the list control
      RSSChannel channel = comboChannels.SelectedItem as RSSChannel;
      textDescription.Text = "";
      listItems.Items.Clear();
      Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
      channel.fetchItems();
      Cursor.Current = System.Windows.Forms.Cursors.Arrow;

      // Refresh the list control
      listItems.BeginUpdate();
      foreach (RSSItem rssItem in channel.Items) {

        string strPublished = "";
        if (rssItem.Published == DateTime.MinValue) {
           strPublished = rssItem.PublishedAsString;
           if (strPublished.Equals (String.Empty))
              strPublished = "[Not specified]";
        } else {
           strPublished = rssItem.Published.ToShortDateString() + " " + rssItem.Published.ToShortTimeString();
        }

        ListViewItem lvItem = new ListViewItem (strPublished);
        lvItem.Tag = rssItem;
        lvItem.SubItems.Add (rssItem.Title);
        listItems.Items.Add (lvItem);
      }
      listItems.EndUpdate();

      // Update the count and update timestamp
      lblItems.Text = channel.Items.Count.ToString() + " item(s)";
      lblUpdated.Text = "Updated: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
      lblUpdated.Visible = true;

      // Select the first item
      if (listItems.Items.Count > 0)
          listItems.Items[0].Selected = true;

    }

    private void OnListItems_SelChanged(object sender, EventArgs e)
    {
      // Display the selected item's description
      string strDescription = "";
      RSSItem rssItem = getSelectedItem();
      if (rssItem != null)
          strDescription = rssItem.Description;
      textDescription.Text = strDescription;
    }


    private void OnList_DblClicked(object sender, EventArgs e)
    {
      displaySelectedItem();
    }


    private void OnBtnView_Clicked(object sender, EventArgs e)
    {
      displaySelectedItem();
    }

    // Helper methods

    /// <summary>
    /// Returns the selected RSS item or null if none.
    /// </summary>
    RSSItem getSelectedItem() {
      ListView.SelectedListViewItemCollection selectedItems = listItems.SelectedItems;
      if (selectedItems.Count > 0) {
          ListViewItem lvItem = selectedItems [0];
          RSSItem rssItem = lvItem.Tag as RSSItem;
          return rssItem;
      }
      return null;
    }

    /// <summary>
    /// Displays the selected item (if any) in a web browser
    /// </summary>
    void displaySelectedItem() {
      RSSItem rssItem = getSelectedItem();
      if (rssItem != null)
          System.Diagnostics.Process.Start ("IExplore.exe", rssItem.Link);
    }
  }
}