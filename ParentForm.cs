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

    public partial class ParentForm : Form 
    {
        public ParentForm()
        {
            InitializeComponent();
        }

        //called when clicking 'open'
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //shows the openfiledialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult result = openFileDialog.ShowDialog();

            //creates a null list of records
            List<Record> list;
            //if the dialog result is ok
            if (result == DialogResult.OK)
            {
                //open a filestream with read permissions
                //with the filename from the dialog
                FileStream input = new FileStream(openFileDialog.FileName,
                    FileMode.OpenOrCreate, FileAccess.Read);
                //create a binary formatter to deseralize the file
                BinaryFormatter reader = new BinaryFormatter();
                list = (List<Record>)reader.Deserialize(input);
                //close the input for reading
                input.Close();
                //if the dialog result isn't ok, we don't want to open 
                //a child or do anything else, so we return.
            } else
                return;

            if (list != null)
            {
                //create a childform
                ChildForm newMDIChild = new ChildForm(list);
                // Set the Parent Form of the Child window.
                newMDIChild.MdiParent = this;
                newMDIChild.Text = openFileDialog.SafeFileName;
                // Display the new form.
                newMDIChild.Show();
            }
        }

        //called when clicking 'insert'
        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if you have an active mdi child
            if (this.ActiveMdiChild != null)
            {
                //create and show an InsertForm, passing the childform
                InsertForm form = new InsertForm((ChildForm)this.ActiveMdiChild, false);
                form.Show();
            }
        }

        //called when click 'delete
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ask the user if they want to delete the item
            DialogResult dialogResult = MessageBox.Show("Do you want to delete this item?", "Delete Item", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //if they say yes, call the delete selected record method from the childform object
                ((ChildForm)this.ActiveMdiChild).deleteSelectedRecord();
            }
        }

        //called when clicking 'update'
        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //create and show an 'insertform' form, passing the childform object
            InsertForm form = new InsertForm((ChildForm)this.ActiveMdiChild, true);
            form.Show();
        }

        //called when clicking 'exit'
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //exits
            this.Close();
        }

        //called when clicking 'save'
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //calls the saveRecords method from the ChildForm object
            ((ChildForm)this.ActiveMdiChild).saveRecords();
        }

        //called when clicking 'about'
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //opens the AboutForm
            AboutForm form4 = new AboutForm();
            form4.Show();
        }

        //called when clicking 'new'
        //this isn't required by the assignment, but not having it bothers me
        //maybe ill get me a few extra points ;) ;)
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //creates an example list with 1 placeholder record
            List<Record> list = new List<Record>();
            list.Add(new Record(1, "Example", "Example", 99, 99));
         
            //creates a childform with that list
            ChildForm newMDIChild = new ChildForm(list);
            // Set the Parent Form of the Child window.
            newMDIChild.MdiParent = this;
            //set its title
            newMDIChild.Text = list[0].Store;
            //show the window
            newMDIChild.Show();
        }
    }
}
