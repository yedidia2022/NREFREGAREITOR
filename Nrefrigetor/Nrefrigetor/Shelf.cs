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
    internal class Shelf  /*IEquatable<Item>, IComparable<Item>*/
    {
        private int shelfId;
        private int floorNum;
        private List<Item> items;
        private double shelfSizeOnSMR;

        public int ShelfId { get; private set; }
        public int  FloorNum { get; set; }

        public double ShelfSizeOnSMR { get; set; }
        public Shelf(double SMR)
        {
            this.shelfId = IdGenrator.giveIdHash();
            this.shelfSizeOnSMR = SMR;
            List<Item> items=new List<Item>();
        }
        public List<Item> getMyList()
        {
            return items;
        }
        
        //האם להקצאות בבנאי את רשימת המוצרים
        //או רק בפונקציה ואז לראות אם אפשר ואם כן אולי כדאי לעשות משתנה נוסף שיסכם את כמות הסמר
        //התפוסים בשביל לא להצטרך לספור בכל פעם מחדש
        public bool addItem(Item item)
        {// הלבטתי האם לשנות ישר בסמר בכל הוספת או הסרת מוצר ואז בפונקציה שבודקת כמות המקום הפנוי פשוט תקח ממשתנה גודל המדף או שזה לא תואם את האפיון שנתנו לי
            if (this.placewasLeft() + item.PlaceOnSMR <= this.shelfSizeOnSMR) {
                item.SetFloorNum(this.floorNum);
                return true;
            }
            else
            {
                Console.WriteLine("we dont have place for this item");
                return false;
            }
            //להוריד פה מהמקום ולוהסיף למערך. ואלי להקצות מערך אם י=עדין לא קיים

        }

        public override string ToString()
        {
            if (items.Count != 0)
            {
                string itemsToSring = "the item detelies is";
                foreach (Item item in items)
                {
                    itemsToSring += item.ToString() + ",";
                }


                return this.shelfId + " " + this.floorNum + " " + this.shelfSizeOnSMR + " " + itemsToSring;
            }
            else { return this.shelfId + " " + this.floorNum + " " + this.shelfSizeOnSMR; }
        }
        //אולי כדאי לעשות מחלקת חישוב ששם יהיה בצורה ויטואלית ופה ידרסו אך מצד שני בכל מקום זה כ"כ שונה כי אני נגשת למוצר אחר.
        public double placewasLeft()
        {
            if (items.Count == 0)
            {
                return ShelfSizeOnSMR;
            }
            double places = 0;
            foreach (Item item in items)
            {
                places += item.PlaceOnSMR;
            }
            return this.shelfSizeOnSMR - places;
        }

        //public Boolean isThisItemInThisShelf(int idnum)
        //{
        //    if (items.Count == 0)
        //        return false;
        //    //return items.Contains(new Item (){ itemId = idnum,Name="" });

        //    foreach (Item item in items)
        //    {
        //        if (item.ItemId == idnum)
        //            return true;
        //    }
        //    return false;
        //}
        public Item takeOutItem(int itemid)
        {    if(items.Count == 0)
            { return null; }
            foreach (Item item in items)
            {
                if (item.ItemId == itemid)
                {
                    items.Remove(item);
                    Console.WriteLine("we remove the item from floor: {0}", this.FloorNum);
                    return item;
                }

            }

            return null;
        }

        public void itemAreExpired()
        {
            
            int countsElement=items.Count;
                if (items.Count != 0) { 
                    for (int i = 0; i < countsElement; i++)
                {
                   
                    //יכולתי פה לשלוח לפונקציה שמוציאה מוצר
                    //אך זה סתם בזבוז כי פהאני כבר יודעת באיזה מוצר ואין לי צורך לחפשו
                    if (items[i].isExpired()) 
                    {
                        this.items.Remove(items[i]);
                        countsElement--;
                    }
                }//יכול להיות שצריך פה רשימת עזר שתאסוף את כל הדפוקים ואז נעשה removeAll או פונקציה אחרת שמורידה רשימה מרשימה
            }
        }
        //public double itemAreExpired()
        //{ double smr = 0;
        //    if (items.Count!= 0)
        //    {
        //        foreach (Item item in items)
        //        {
        //            //יכולתי פה לשלוח לפונקציה שמוציאה מוצר
        //            //אך זה סתם בזבוז כי פהאני כבר יודעת באיזה מוצר ואין לי צורך לחפשו
        //            if (!item.isExpired())
        //            { 
        //                this.items.Remove(item);
        //               smr+= item.PlaceOnSMR;
        //            }

        //        }
        //        return smr;
        //    } return smr;
        //} 
        public List<Item> findItemsByKashrut(Kashruiot kashrut, Kinds kind)
        {
            List<Item> itemsBySpecificKushrut = new List<Item>();
            if (items.Count == 0)
                return itemsBySpecificKushrut;
            else
            {
                //בטוח שהיה אפשר לממש פונקציה שמחפשת אבל היה
                //נראה שצריך דלגייט והתחלתי להסתבך עם זה
                foreach (Item item in items)
                {
                    if (item.Kashrut == kashrut && item.Kind == kind && item.isExpired())
                        itemsBySpecificKushrut.Add(item);
                }
                return itemsBySpecificKushrut;
            }





        }
        //פונקציות מיון 
        //public int SortByNameAscending(DateTime name1, DateTime name2)
        //{

        //    return name1.CompareTo(name2);
        //}
        // לא מכיר את this
        //public int CompareTo(Item item)
        //{
        //    // A null value means that this object is greater.
        //    if (item == null)
        //        return 1;

        //    else
        //        return this.LastDayUse.CompareTo(item.LastDayUse);
        //}
        public List<Item> sortByDate()
        {
            // items.Sort();
            List<Item> SortedList = items.OrderBy(o => o.LastDayUse).ToList();
            foreach (Item item in SortedList)
            {
                Console.WriteLine(item);
            }
            return SortedList;
        }




    }
}
