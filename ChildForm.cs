//Dylan Miles
//11/30/22
//Product inventory application that can show, edit, open, and save
//product databases. 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prog8
{
    public partial class ChildForm : Form
    {
        //constructor includes a list of records
        public ChildForm(List<Record> recordList)
        {
            InitializeComponent();
            this.populateListView(recordList);
        }

        //uses the list of records to populate the listView
        private void populateListView(List<Record> recordList)
        {
            //loop through all records in the record list
            foreach(Record c in recordList)
            {
                //construct a listViewItem based on the record
                ListViewItem item = new ListViewItem(
                    new string[] { c.ID.ToString(), c.Name,
                        c.Price.ToString(), c.Quantity.ToString()});
                //add item to the listview
                this.listView.Items.Add(item);
            }
            //display the amount of items in the store
            this.itemCountLabel.Text = "There are " + this.listView.Items.Count + " items in the store";
        }

        //inserts a record 'r' into the listView
        public void insertRecord(Record r)
        {
            //construct listview item from Record
            this.listView.Items.Add(new ListViewItem(new string[] 
            { r.ID.ToString(), r.Name, r.Price.ToString(), r.Quantity.ToString() }));
            //updates the item counter
            this.itemCountLabel.Text = "There are " + this.listView.Items.Count + " items in the store";
        }

        //update the contents of a record
        public void updateRecord(Record r)
        {
            //grab the listView Item that is selected
            //and change it to a new listviewitem based on the records contents
            this.listView.Items[this.listView.FocusedItem.Index]
                = (new ListViewItem(new string[] 
                { r.ID.ToString(), r.Name, r.Price.ToString(), r.Quantity.ToString() }));
        }

        //Called to save all the records to file
        public void saveRecords()
        {
            //create save file dialog
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Save";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                //if the result was OK
                //grab the file name
                string file = saveDialog.FileName;
                //create a binary formatter
                BinaryFormatter reader = new BinaryFormatter();
                // open file with write access
                FileStream output = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write);
                //construct a list of records from the contents of the ListView
                List<Record> records = new List<Record>();
                foreach(ListViewItem l in this.listView.Items)
                {
                    Record r = new Record(
                        int.Parse(l.SubItems[0].Text), l.SubItems[1].Text, 
                        this.Text, double.Parse(l.SubItems[2].Text), int.Parse(l.SubItems[3].Text));
                    records.Add(r);
                }
                //seralize the list of records
                reader.Serialize(output, records);
                //close output file
                output.Close();
            }
        }

        //Deletes the focued item from the listview
        public void deleteSelectedRecord()
        {
            if(this.listView.FocusedItem != null)
                 this.listView.FocusedItem.Remove();
            //update the item counter
            this.itemCountLabel.Text = "There are " + this.listView.Items.Count + " items in the store";
        }
    }
}
