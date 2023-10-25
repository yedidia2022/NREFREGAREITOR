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
    {
        private int refrigeratorId;
        private string refrigeratorModel;
        private Colors refrigeratorColor;
        private int amountOfShelves;
        private List<Shelf> shelves;
        private static List<Refrigerator> refregitorList=new List<Refrigerator>();
       
        public int RefrigeratorId { get; private set; }
        public string RefrigeratorModel { get; set; }
        public Colors RefrigeratorColor { get; set; }
         public int AmountOfShelves { get { return amountOfShelves; } set { if (value > 0) amountOfShelves = value; } }
        
        public List<Shelf> getShelvesList()
        {
            return shelves;
        }
        
        //public static List<Refrigerator> seeRefregitorList()
        //{
        //    return refregitorList;
        //}
        //public static void addToRefList(Refrigerator r)
        //{  
        //      refregitorList.Add(r);
        //    Console.WriteLine(r);
          
         
        //}
        //public static Refrigerator createRefrigertor(string model, Colors color, int amount)
        //{ Refrigerator r=new Refrigerator(model, color, amount);
        //    addToRefList(r);
        //   return r;
        //}
        public Refrigerator(string model,Colors color, int amount)
        {  this.RefrigeratorId = IdGenrator.giveId();
            this.refrigeratorModel = model;
            this.refrigeratorColor = color;
            this.AmountOfShelves = amount;
            shelves = new List<Shelf>();
            refregitorList.Add(this);
           
        }


        public override string ToString()

        {   string shelvesToString = @"   this is the ShelvesDetails   :";
            if(shelves !=null)
            foreach (Shelf shelf in shelves)
                { 
                shelvesToString += shelf.ToString()+", ";
                }

            return @"  
                   refregireitorId is"+ this.RefrigeratorId +
                "  model is"+ this.refrigeratorModel + " "+
                "  anountofSheleves it can has" + this.amountOfShelves + " "
                +"  color"+ this.refrigeratorColor + ">> " + 
                    shelvesToString;
        }

        public void addShelf(Shelf shelf)
        {
            int count = shelves.Count + 1;
            if (count-1 < amountOfShelves)
            {
                shelves.Add(shelf);
                shelf.setFloorNum(count, this);

            }
            else
            { throw new Exception("no place");
            }
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



      
        public void enterItemToRefrigerator(Item item)
        {     if (this.placeWasLeft() < item.PlaceOnSMR)
                {
                Console.WriteLine("there is no place in that refrigerator");
               
                 }

                  foreach (Shelf shelf in shelves)
                  {
                    if (shelf.placewasLeft() >= item.PlaceOnCM) 
                    {//הוספה ישירות כי אין צורך לשלוח לפונקציה שבודקת
                    item.SetFloorNum(shelf.FloorNum,shelf);
                        shelf.getMyList().Add(item);
                    Console.WriteLine("this item added sucssesfuly to the shelf");
                    }

                  }
                
        }

        public Item takeOutItem(int itemid)
        {    if(shelves!=null)
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

            

        }


        public void cleanTheRefrigeratorFromExpiredItems()
        {   if (shelves!=null)
            foreach(Shelf shelf in shelves)
            {
                shelf.itemAreExpired();
            }
        }


        public List<Item> findItemsByKashrut(Kashruiot kashrut,Kinds kind)
        {
           
            List<Item> itemsBySpecificKushrut = new List<Item>();
            List<Item> SpecificKushrutList = new List<Item>();

            foreach (Shelf shelf in shelves)
            {
                SpecificKushrutList = shelf.findItemsByKashrut(kashrut, kind);
                if(SpecificKushrutList != null) {
                itemsBySpecificKushrut.AddRange(SpecificKushrutList); }

            }
            return itemsBySpecificKushrut;
        }


        public List<Item> sortItemByExpiredDate()
        {
            List<Item> allItemsonlist = new List<Item>(); 
            foreach(Shelf shelf in shelves)
            { 
                allItemsonlist.AddRange(shelf.getMyList());
            }
            List<Item> SortedList = allItemsonlist.OrderBy(o => o.LastDayUse).ToList();
            return SortedList;

        }


        public void sortByFreePlace()
        {
            shelves.Sort((x, y) => x.placewasLeft().CompareTo(y.placewasLeft()));
            shelves.Reverse();
            if(shelves!=null)
            foreach (Shelf shelf in shelves)
            {
                    Console.Write(shelf.placewasLeft());
                Console.WriteLine(shelf);
            }
        }



        public static void sortRefrgitors()
        {
            if (refregitorList != null)
            {
            List<Refrigerator> newLIstRef = refregitorList.OrderBy(o => o.placeWasLeft()).ToList();
            newLIstRef.Reverse();
            foreach(Refrigerator refrigerator1 in newLIstRef)
            {
                Console.WriteLine(refrigerator1);
            }
            }
            else
            { Console.WriteLine("there is just one refrgeritor");
                Console.WriteLine();
            }
        }



        public double CalcSMRbyCriterion(Kashruiot kashrut,DateTime dateExpired,Boolean flagRemove,double howmuchSMR)
        {
            double sumSMR = 0;
            
            foreach (Shelf shelf in shelves) { 
                List <Item> items = new List<Item>();
                int countItems= items.Count;
                for (int i = 0;i< countItems; i++) { 
                    if (items[i].LastDayUse< dateExpired && items[i].Kashrut == kashrut && flagRemove == false) { 
                        sumSMR += items[i].PlaceOnCM;
                        howmuchSMR -= sumSMR;
                        if (howmuchSMR == 0)
                            return sumSMR;


                    }
                    else
                    {
                        if (items[i].LastDayUse <dateExpired && items[i].Kashrut == kashrut && flagRemove == true) { 
                            sumSMR += items[i].PlaceOnCM;
                            items.Remove(items[i]);
                            Console.Write(items[i]);
                            countItems--;
                            howmuchSMR -= sumSMR;
                            if (howmuchSMR == 0)
                            return sumSMR; 
                        }

                    }
                     }   

                   }
              
            return sumSMR;
        }

        
        public void getReadyShopping()
        {

            double sumIfRemoving = 0;
            double freePlace = this.placeWasLeft();
            if (freePlace >= 29)
            {
                Console.Write("There is enough place in the refrigerator.");
                return;
            }
            Console.Write("There is  not enough place in the refrigerator.lets remove things");
            Console.Write("remove expired food");

            this.cleanTheRefrigeratorFromExpiredItems();
            freePlace = this.placeWasLeft();
            if (freePlace >= 29)
            {
                Console.Write("There is enough place in the refrigerator after we remove the expired items");
                return;
            }
            else
            {
                sumIfRemoving = CalcSMRbyCriterion(Kashruiot.MILKY, DateTime.Now.AddDays(3), false, 29 - freePlace);
                if (freePlace + sumIfRemoving >= 29)
                {
                    sumIfRemoving = CalcSMRbyCriterion(Kashruiot.MILKY, DateTime.Now.AddDays(3), true, 29 - freePlace);
                    Console.Write("There is enough place in the refrigerator after we remove the milky items");
                    return;
                }
                else
                {

                    freePlace += sumIfRemoving;
                    sumIfRemoving = CalcSMRbyCriterion(Kashruiot.FLASHY, DateTime.Now.AddDays(7), false, 29 - freePlace);
                    if (freePlace + sumIfRemoving >= 29)
                    {
                        sumIfRemoving = CalcSMRbyCriterion(Kashruiot.FLASHY, DateTime.Now.AddDays(7), true, 29 - freePlace);
                        Console.Write("There is enough place in the refrigerator after we remove the flashy items");
                        return;
                    }
                    else
                    {

                        freePlace += sumIfRemoving;
                        sumIfRemoving = CalcSMRbyCriterion(Kashruiot.PARVE, DateTime.Now.AddDays(1), false, 29 - freePlace);
                        //after all the checking/ find its help to remove lets remove from
                        //all the reasons together.
                        if (freePlace + sumIfRemoving >= 29)
                        {
                            sumIfRemoving = CalcSMRbyCriterion(Kashruiot.MILKY, DateTime.Now.AddDays(3), true, 100);
                            sumIfRemoving = CalcSMRbyCriterion(Kashruiot.FLASHY, DateTime.Now.AddDays(7), true, 100);
                            sumIfRemoving = CalcSMRbyCriterion(Kashruiot.PARVE, DateTime.Now.AddDays(1), true, 29 - freePlace);
                            Console.Write("There is enough place in the refrigerator after we remove the Parve items");
                            return;
                        }
                        else
                            Console.Write("There is not  enough place in the refrigerator. it is not the time for shopping");




                    }
                }










            }


        }
    }
}
