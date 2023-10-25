using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace mekarer
{
    internal class Shelf  
    {
        private int shelfId;
        private int floorNum;
        private List<Item> items = new List<Item>();
        private double shelfSizeOnCM;

        public int ShelfId { get; private set; }
        public int  FloorNum { get;   private set; }
        public void setFloorNum(int numF,Refrigerator r)
        {
            if (numF > 0)
                if (numF == r.getShelvesList().Count) 
                {
                    this.FloorNum = numF;
                }
        }

        public double ShelfSizeOnCM { get { return shelfSizeOnSMR; } set { if (value > 0) shelfSizeOnSMR = value; } }
        public Shelf(double SMR)
        {
            this.shelfId = IdGenrator.giveId(); 
            this.ShelfSizeOnSMR = SMR;
           
        }
        public List<Item> getMyList()
        {
            return items;
        }

        public bool addItem(Item item)
        {
            if (this.placewasLeft() - item.PlaceOnSMR >= 0)
            {
                item.SetFloorNum(this.floorNum, this);
                items.Add(item);
                return true;
            }
            else
            {
                Console.WriteLine("we dont have place for this item");
                return false;
            }
        }

        public override string ToString()
        {
            string sn;
            if (this.FloorNum == 0)
                sn = "no floornum yet";
            else
            {
                sn ="the floornum is "+ this.FloorNum.ToString();
            }
          
            if (items!=null)
                {
                string itemsToSring = @"    the item detelies is  :";
                foreach (Item item in items)
                {
                    itemsToSring += item.ToString() + ",";
                }
               
               
                return @" shelfid is " + this.shelfId  +" "+
                    sn+ 
                    "  the shelfOnSmr is "
                    +this.shelfSizeOnSMR + " " +
                    itemsToSring;
                }
            else
              return
                   "shelfid " +this.shelfId + " " + sn +"shelfOnSmr" +this.shelfSizeOn;
        }
       
        public double placewasLeft()
        {
            if (items==null)
            {
                return ShelfSizeOnCM;
            }
            double places = 0;
            foreach (Item item in items)
            {
                places += item.PlaceOnSMR;
            }
            return this.shelfSizeOnCM - places;
        }

      
        
        public Item takeOutItem(int itemid)
        {    if(items==null)
            { return null; }
            foreach (Item item in items)
            {
                if (item.ItemId == itemid)
                {
                    items.Remove(item);
                    Console.WriteLine("we remove the item from floor:/n {0}", this.FloorNum);
                    return item;
                }

            }

            return null;
        }

        public void itemAreExpired()
        {
            
            int countsElement=items.Count;
                if (items !=null) { 
                    for (int i = 0; i < countsElement; i++)
                {
                    Console.WriteLine(items[i].ToString());
                    //יכולתי פה לשלוח לפונקציה שמוציאה מוצר
                    //אך זה סתם בזבוז כי פהאני כבר יודעת באיזה מוצר ואין לי צורך לחפשו
                    if (items[i].isExpired()) 
                    {
                        Console.WriteLine("we remove this item", items[i].ToString());
                        this.items.Remove(items[i]);
                        countsElement--;
                    }
                }
            }
        }

        public List<Item> findItemsByKashrut(Kashruiot kashrut, Kinds kind)
        {
            List<Item> itemsBySpecificKushrut = new List<Item>();
            if (items == null)
                return itemsBySpecificKushrut;
            else
            {
                //בטוח שהיה אפשר לממש פונקציה שמחפשת אבל היה
                //נראה שצריך דלגייט והתחלתי להסתבך עם זה
                foreach (Item item in items)
                {
                    if (item.Kashrut == kashrut && item.Kind == kind && !(item.isExpired()))
                        itemsBySpecificKushrut.Add(item);
                }
                return itemsBySpecificKushrut;
            }





        }
        public List<Item> sortByDate()
        {
           
            List<Item> SortedList = items.OrderBy(o => o.LastDayUse).ToList();
            
            return SortedList;
        }




    }
}
