//Dylan Miles
//11/30/22
//Product inventory application that can show, edit, open, and save
//product databases. 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog8
{
    //Taken from the assignment

    [Serializable]
    public class Record
    {
        //modified to initialize all variables,
        //removed empty constructor as it was not used
        public Record(int id, string name, string store, double price, int quantity)
        {
            this.ID = id;
            this.Name = name;
            this.Store = store;
            this.Price = price;
            this.Quantity = quantity;
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Store { get; set; }
    }
}
