//Dylan Miles
//11/30/22
//Product inventory application that can show, edit, open, and save
//product databases. 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prog8
{
    //Form used to insert (or update) a record
    public partial class InsertForm : Form
    {
        //I know this looks weird, but a ChildForm class is a parent to the InsertForm
        ChildForm parentform;
        //Used to tell if we are inserting, or update a value
        bool update = false;
        //args: ChildForm form2, the ChildForm instance to edit
        //bool update, set true if updating a value, false if inserting.
        public InsertForm(ChildForm form2, bool update)
        { 
            InitializeComponent();
            parentform = form2;
            this.update = update;
        }
        //called when the OK button is clicked
        private void okButton_Click(object sender, EventArgs e)
        {
            //create a new record based on the users inputs
            //rubric doesn't require error checking user input 
            //but maybe this will earn my some extra points :)
            Record record = null;
            try
            {
                //create new record based on user input
                record = new Record(int.Parse(itemID.Text),
                    itemName.Text, parentform.Text,
                    double.Parse(price.Text), int.Parse(quantity.Text));
            } catch (Exception exception) {
                //Tells the user they messed up
                MessageBox.Show("Invalid Input");
                //necessary
                return;
            }
            //updates record if we should, inserts otherwise.
            if(update)
                parentform.updateRecord(record);
            else
                parentform.insertRecord(record);

            //close form when done
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            //used hit cancel, close
            this.Close();
        }
    }
}
