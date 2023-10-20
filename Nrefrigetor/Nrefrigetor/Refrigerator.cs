using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace mekarer
{
    internal class Refrigerator
    {//אולי כדאי פונקצית הוספת מדף  ?
        //?אם לא רושמים סט אז הסט פרטי?
        private int refrigeratorId;
        private string refrigeratorModel;
        private Colors refrigeratorColor;
        private int amountOfShelves;
        private List<Shelf> shelves;
        private static List<Refrigerator> refregitorList;
       
        public int RefrigeratorId { get; private set; }
        public string RefrigeratorModel { get; set; }

        public Colors RefrigeratorColor { get; set; }

        public int AmountOfShelves { get; set; }

       
        public List<Shelf> getShelvesList()
        {
            return shelves;
        }
        
        public static List<Refrigerator> seeRefregitorList()
        {
            return refregitorList;
        }
        
        public Refrigerator(string model,Colors color, int amount)
        {  this.RefrigeratorId = IdGenrator.giveId();
           this.RefrigeratorId = IdGenrator.giveIdHash();
            this.refrigeratorModel = model;
            this.refrigeratorColor = color;
            this.amountOfShelves = amount;
            shelves = new List<Shelf>();
            //מהו this
            //refregitorList.Add(this);
        }

        public override string ToString()
        { string shelvesToString = "this is the ShelvesDetails";
            foreach (Shelf shelf in shelves) { 
                shelvesToString += shelf.ToString();}

            return this.refrigeratorId + " " + this.refrigeratorModel + " " + this.amountOfShelves + " "
                + this.refrigeratorColor + " " + shelvesToString;
        }
        public void addShelf(Shelf shelf)
        { int count = shelves.Count;
            if (count < this.AmountOfShelves) { 
                shelves.Add(shelf);
                shelf.FloorNum = count;
               
            }
            throw new Exception("no place");
        }


        public double placeWasLeft()
        {  
            double placeWasLeft = 0;
            foreach (Shelf shelf in shelves)
            {
                placeWasLeft += shelf.placewasLeft();
            }
            return  placeWasLeft;

        }



        //האם כבר יהיה למדף קומה או שעכשיו נצטרך לבחור או שעכשיו נקצה לפי איפה שיש מקום 
        public void enterItemToRefrigerator(Item item)
        {     if (this.placeWasLeft() < item.PlaceOnSMR)
                {
                Console.WriteLine("there is no place in that refrigerator");
               
                 }

                  foreach (Shelf shelf in shelves)
                  {
                    if (shelf.placewasLeft() >= item.PlaceOnSMR) 
                    {//הוספה ישירות כי אין צורך לשלוח לפונקציה שבודקת
                    item.SetFloorNum(shelf.FloorNum);
                        shelf.getMyList().Add(item);
                    }

                   }
                
        }

        public Item takeOutItem(int itemid)
        {    if(shelves.Count > 0)
            { 
              foreach (Shelf shelf in shelves)
              {
                Item BackItem = shelf.takeOutItem(itemid);
                
                if (BackItem !=null)
                {
                   return BackItem;
                     
                }
              } throw new Exception("there isn't this iteemId on our refrigerator.");
            }
            throw new Exception("there is no items in this refregitor.");

            //Console.WriteLine("there isn't this iteemId on our refrigerator");

        }
        public void cleanTheRefrigeratorFromExpiredItems()
        {   if (shelves.Count != 0)
            foreach(Shelf shelf in shelves)
            {
                shelf.itemAreExpired();
            }
        }
        public List<Item> findItemsByKashrut(Kashruiot kashrut,Kinds kind)
        {
            List<Item> itemsBySpecificKushrut = new List<Item>();
            foreach (Shelf shelf in shelves)
            {
                itemsBySpecificKushrut.AddRange(shelf.findItemsByKashrut(kashrut, kind));

            }
            return itemsBySpecificKushrut;
        }
        public List<Item> sortItemByExpiredDate()
        {
            List<Item>sortListByExpiredDay= new List<Item>();
            foreach(Shelf shelf in shelves)
            {
                sortListByExpiredDay.AddRange(shelf.sortByDate());
            }
            return sortListByExpiredDay;

        }
        public void sortByFreePlace()
        {
            shelves.Sort((x, y) => x.placewasLeft().CompareTo(y.placewasLeft()));
          //  List<Item> SortedList = shelves.OrderBy(o=>o.placewasLeft).ToList();
            //foreach (Item item in SortedList)
            //{
            //    Console.WriteLine(item);
            //}
            shelves.Reverse();
            if(shelves.Count > 0)
            foreach (Shelf shelf in shelves)
            {
                Console.WriteLine(shelf);
            }
        }
        
        public static void sortRefrgitors()
        {
            refregitorList.Sort((x,y)=>x.placeWasLeft().CompareTo(y.placeWasLeft()));
            refregitorList.Reverse();
            if(refregitorList.Count > 0)
            foreach(Refrigerator refrigerator in refregitorList)
            {
                Console.WriteLine(refrigerator);
            }
        }



        //כעת בפונקציות של הקניות לא יודעת אם לעבור דף או לא?
        //public void getReadyShopping()
        //{
        //    List<Item> newList = new List<Item>();
        //    List<Item> longList = new List<Item>();
        //    double placefree = this.placeWasLeft();
        //    if (placefree < 20)
        //    {
        //        while (placefree < 20)
        //        {
        //            foreach (Shelf shelf in shelves)
        //            {
        //                List<Item> sortedShelvsListByDate = shelf.sortByDate();
        //                sortedShelvsListByDate.Reverse();
        //                לא השתמשתי בפונקציה שקיימת במדף כי רציתי לקבל את גודל המוצר שהוסר
        //                foreach (Item item in sortedShelvsListByDate)
        //                {
        //                    if (!item.isExpired())
        //                    {
        //                        sortedShelvsListByDate.Remove(item);
        //                        placefree += item.PlaceOnSMR;

        //                    }
        //                }
        //            }





        //            foreach (Shelf shelf in shelves)
        //            {
        //                List<Item> sortedShelvsListByDate = shelf.sortByDate();
        //                sortedShelvsListByDate.Reverse();

        //                foreach (Item item in sortedShelvsListByDate)
        //                {
        //                    if ((item.LastDayUse - DateTime.Today).Days < 3 && item.Kashrut == Kashruiot.MILKY)
        //                    {
        //                        newList.Add(item);
        //                        placefree += item.PlaceOnSMR;

        //                    }
        //                    if (placefree >= 20)

        //                    }
        //            }

        //            foreach (Shelf shelf in shelves)
        //            {
        //                List<Item> sortedShelvsListByDate = shelf.sortByDate();
        //                sortedShelvsListByDate.Reverse();

        //                foreach (Item item in sortedShelvsListByDate)
        //                {
        //                    if ((item.LastDayUse - DateTime.Today).Days < 7 && item.Kashrut == Kashruiot.FLASHY)
        //                    {
        //                        newList.Add(item);
        //                        placefree += item.PlaceOnSMR;

        //                    }
        //                }
        //            }

        //            foreach (Shelf shelf in shelves)
        //            {
        //                List<Item> sortedShelvsListByDate = shelf.sortByDate();
        //                sortedShelvsListByDate.Reverse();

        //                foreach (Item item in sortedShelvsListByDate)
        //                {
        //                    if ((item.LastDayUse - DateTime.Today).Days < 2 && item.Kashrut == Kashruiot.PARVE)
        //                    {
        //                        newList.Add(item);
        //                        placefree += item.PlaceOnSMR;

        //                    }
        //                }
        //            }





        //        }
        //        if (placefree < 20)
        //        {
        //            Console.WriteLine("its not the time for shopping");
        //        }
        //        else
        //        {
        //            foreach (Shelf shelf in shelves)
        //            {
        //                longList.AddRange(shelf.getMyList());
        //            }
        //            foreach (Item item in longList)
        //            {
        //                foreach (Item item1 in newList)
        //                {
        //                    if (item.Equals(item1))
        //                    {
        //                        יש לי דרך להמשיך לזרוק אותו ולחפש באיזה מדף נמצא ואז ללכת להסיר מהפריטים שבאותו מדף
        //                    }
        //                }

        //            }


        //        }



        //    }
        //    else
        //        Console.WriteLine("you are free to go shopping now");

        //}




    }
}
