using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace mekarer
{
    internal class Program
    {

        static void Main(string[] args)
        {


            try
            {
                Refrigerator r = fullTheRefrigetor();
                menuProgram(r);

            }

            catch (Exception ex)
            { Console.WriteLine(ex.Message); }

        }


        public static Refrigerator fullTheRefrigetor()
        {
            Refrigerator r = new Refrigerator("bosch", Colors.RED, 4);
            Refrigerator v = new Refrigerator("sharp", Colors.BLACK, 5);
            Shelf p = new Shelf(25);
            Shelf p1 = new Shelf(40);
            Shelf p2 = new Shelf(55);
            v.addShelf(p);
            v.addShelf(p1);
            v.addShelf(p2);


            Shelf s = new Shelf(18);
            Shelf s1 = new Shelf(20);
            Shelf s2 = new Shelf(20);

            r.addShelf(s);
            r.addShelf(s1);
            r.addShelf(s2);


            Item i = new Item("pasrama", new DateTime(2022, 01, 10), Kinds.FOOD, Kashruiot.FLASHY, 13);
            Item i1 = new Item("Choclate", new DateTime(2021, 10, 10), Kinds.FOOD, Kashruiot.PARVE, 9);
            Item i2 = new Item("meat", new DateTime(2025, 11, 01), Kinds.FOOD, Kashruiot.FLASHY, 21);
            Item i4 = new Item("water", new DateTime(2023, 11, 22), Kinds.DRINK, Kashruiot.PARVE, 4);
            Item i3 = new Item("milk", new DateTime(2023, 11, 10), Kinds.DRINK, Kashruiot.MILKY, 7);
            Item i5 = new Item("Apple", new DateTime(2023, 11, 18), Kinds.FOOD, Kashruiot.PARVE, 3);
            Item i6 = new Item("Chicken", new DateTime(2023, 11, 10), Kinds.FOOD, Kashruiot.FLASHY, 20);
            Item i7 = new Item("Cola", new DateTime(2024, 01, 10), Kinds.DRINK, Kashruiot.PARVE, 7);
            Item i8 = new Item("Riba", new DateTime(2023, 10, 29), Kinds.FOOD, Kashruiot.PARVE, 11);

            s.addItem(i);
            s.addItem(i1);
            s1.addItem(i6);
            s.addItem(i3);
            s2.addItem(i4);
            s2.addItem(i8);

            return r;

        }

        public static void menuProgram(Refrigerator MyRefrigeritor)
        {
            int currentChoice = 0;
            string inputStr;
            while (currentChoice != 100)
            {
                Console.WriteLine(@"
                              enter 1:  print all the refrigerator's detailes and  his capcity detailes             
                              enter 2:  it prints the refrigerator's free place                                  
                              enter 3: enter item to its by enter the item's detailes                             
                              enter 4:  takeOut item from its by enter the item's id                            
                              enter 5:  clean its and print you the detailes of the item were checked           
                              enter 6:what you want to eat and which kashrut you can now ?                      
                              enter 7:t prints all the item sort by expired day                                 
                              enter 8: print the shelves detailes sort by their free place                      
                              enter 9: print all thr refrigerators sort by their free place                    
                              enter 10: prepare its for shopping                    
                              enter 100:close the program");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out currentChoice))
                {
                    Console.WriteLine("Invalid input!");
                    return;
                }
                else
                {
                    currentChoice = int.Parse(input);

                    switch (currentChoice)
                    {
                        case 1:
                            {
                                Console.WriteLine("print all the cacity of mt refregitor :/n {0} ", MyRefrigeritor);
                                break;
                            }
                        case 2:
                            {
                                Console.WriteLine("the place was left in my Refrigertor :/n {0}", MyRefrigeritor.placeWasLeft());
                                break;
                            }
                        case 3:
                            {

                                double place;
                                string name;
                                DateTime date;
                                Kinds kind;
                                Kashruiot kashrut;
                                double SMR;
                                Console.WriteLine("enter the item name");
                                name = Console.ReadLine();
                                Console.WriteLine("enter the item expired date");
                                inputStr = Console.ReadLine();
                                if (!DateTime.TryParse(inputStr, out date))
                                    if (!DateTime.TryParse(inputStr, out date))
                                        throw new Exception("not valid input for date");
                                    else
                                        date = DateTime.Parse(inputStr);
                                Console.WriteLine("enter the item kind");
                                try { 
                                kind = (Kinds)Enum.Parse(typeof(Kinds), Console.ReadLine());}
                                catch(Exception e)
                                {
                                    throw new Exception(e.Message);
                                }
                                Console.WriteLine("enter the item Kashrut");
                                try
                                {
                                    kashrut = (Kashruiot)Enum.Parse(typeof(Kashruiot), Console.ReadLine());
                                }
                                catch(Exception e)
                                {
                                    throw new Exception(e.Message);
                                }
                                Console.WriteLine("enter the item size of the item");
                                inputStr = Console.ReadLine();
                                if (!double.TryParse(inputStr, out SMR))
                                {
                                    throw new Exception("invalid input for SMR");
                                }
                                else
                                {
                                    SMR = double.Parse(inputStr);
                                }
                                Item newItemForEnter = new Item(name, date, kind, kashrut, SMR);
                                MyRefrigeritor.enterItemToRefrigerator(newItemForEnter);
                                break;
                            }

                        case 4:
                            {
                                int codeItem;
                                Console.WriteLine("enter the item code");
                                inputStr = Console.ReadLine();
                                if (!int.TryParse(inputStr, out codeItem))
                                    throw new Exception("invalid input for itemCode");
                                else
                                {
                                    codeItem = int.Parse(inputStr);
                                    try
                                    {
                                        MyRefrigeritor.takeOutItem(codeItem);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("we dont have item with this code");
                                    }
                                }
                                break;
                            }

                        case 5:
                            {
                                MyRefrigeritor.cleanTheRefrigeratorFromExpiredItems();
                                break;
                            }
                        case 6:
                            {
                                Kinds kind;
                                Kashruiot Kashrut;
                                Console.WriteLine("what do you want to eat ?");
                                inputStr = Console.ReadLine();
                                if (!Kinds.TryParse(inputStr, out kind))
                                    throw new Exception("invalid input for kind");
                                else

                                    kind = (Kinds)Enum.Parse(typeof(Kinds), inputStr);

                                Console.WriteLine("which kashrut do you want ?");
                                inputStr = Console.ReadLine();
                                if (!Kashruiot.TryParse(inputStr, out Kashrut))
                                    throw new Exception("invalid input for kashrut");
                                else

                                    Kashrut = (Kashruiot)Enum.Parse(typeof(Kashruiot), inputStr);
                                List<Item> itemsList = MyRefrigeritor.findItemsByKashrut(Kashrut, kind);
                                if (itemsList!=null)
                                    foreach (Item item in itemsList)
                                    {
                                        Console.WriteLine(item);
                                    }
                                else
                                    Console.WriteLine("we dont have any thing for you!");
                                    break;
                               
                            }
                        case 7:
                            {
                                List<Item> itemsList = MyRefrigeritor.sortItemByExpiredDate();
                                if (itemsList != null)
                                    foreach (Item item in itemsList)
                                    {
                                        Console.WriteLine(item);
                                    }
                                break;
                            }
                        case 8:
                            {
                                MyRefrigeritor.sortByFreePlace();
                                break;
                            }
                        case 9:
                            {
                                Refrigerator.sortRefrgitors();
                                break;
                            }
                        case 10:
                            {
                                MyRefrigeritor.getReadyShopping();
                                break;
                            }
                        case 100: {
                                Console.WriteLine("we hipe you eenjoy!!");
                                break;
                            }

                    }
            }
            }

        }


    }
}
