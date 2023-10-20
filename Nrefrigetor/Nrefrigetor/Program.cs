using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace mekarer
{
    internal class Program { 

         static void Main(string[] args)
    {
            Console.WriteLine("njbb");
            Console.ReadLine();
            try
            {
                Refrigerator r = new Refrigerator("bosch", Colors.RED, 4);
                Shelf s = new Shelf(25);
                Shelf s1 = new Shelf(40);
                Shelf s2 = new Shelf(55);
                r.addShelf(s);
                r.addShelf(s1);
                r.addShelf(s2);
                Item i = new Item("Choclate", new DateTime(2023, 11, 10), Kinds.FOOD, Kashruiot.FLASHY, 15);
                Item i1 = new Item("Choclate", new DateTime(2023, 10, 21), Kinds.FOOD, Kashruiot.PARVE, 10);
                Item i2 = new Item("meat", new DateTime(2023, 10, 22), Kinds.FOOD, Kashruiot.FLASHY, 21);
                Item i4 = new Item("water", new DateTime(2023, 12, 22), Kinds.DRINK, Kashruiot.PARVE, 4);
                Item i3 = new Item("milk", new DateTime(2023, 11, 10), Kinds.DRINK, Kashruiot.MILKY, 7);
                Item i5 = new Item("Apple", new DateTime(2023, 11, 18), Kinds.FOOD, Kashruiot.PARVE, 3);
                Item i6 = new Item("Chicken", new DateTime(2023, 11, 10), Kinds.FOOD, Kashruiot.FLASHY, 23);
                Item i7 = new Item("Cola", new DateTime(2024, 01, 10), Kinds.DRINK, Kashruiot.PARVE, 7);
                Item i8 = new Item("Riba", new DateTime(2023, 10, 29), Kinds.FOOD, Kashruiot.PARVE, 11);
                Console.WriteLine(i7);
                Console.Write(s.addItem(i));

                Console.Write(s.addItem(i1));
                Console.Write(s1.addItem(i2));
                Console.Write(s.addItem(i3));
                Console.Write(s2.addItem(i4));
                Console.Write(s1.addItem(i5));
                Console.Write(s2.addItem(i6));
                Console.Write(s2.addItem(i7));
                menuProgram(r);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }


        }

        public static void menuProgram(Refrigerator MyRefrigeritor)
        {
            Console.Write("vvgv");
            string inputStr;

            //Refrigerator MyRefrigeritor = Refrigerator.GetRefrigerator();
            int currentChoice;
            Console.WriteLine("enter 1:  print all the refrigerator's detailes + his capcity detailes" +
                             "  enter 2:  it prints the refrigerator's free place" +
                              "enter 3: enter item to its by enter the item's detailes " +
                              "enter 4:  takeOut item from its by enter the item's id " +
                              "enter 5:  clean its and print you the detailes of the item were checked" +
                              "enter 6:what you want to eat and which kashrut you can now ? " +
                              "enter 7:t prints all the item sort by expired day" +
                              "enter 8: print the shelves detailes sort by their free place" +
                             "enter 9: print all thr refrigerators sort by their free place" +
                             "enter 10: prepare its for shopping" +
                             "enter 100:close the program");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out currentChoice))
            {
                Console.WriteLine("Invalid input!");
                return;
            }
            else

            {
                currentChoice = int.Parse(input);
                string strChoice;
                int numberChioice;
                switch (currentChoice)
                {
                    case 1:
                        {
                            Console.WriteLine("print all the cacity of mt refregitor {0}", MyRefrigeritor);
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("the place was left in my Refrigertor {0}", MyRefrigeritor.placeWasLeft());
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
                            inputStr = Console.ReadLine();

                            if (inputStr.Equals(Kinds.FOOD))
                            {
                                kind = Kinds.FOOD;

                            }
                            else
                                if (inputStr.Equals(Kinds.DRINK))
                            {
                                kind = Kinds.DRINK;
                            }
                            else
                                throw new Exception("it is not valid kind");
                            Console.WriteLine("enter the item Kashrut");
                            inputStr = Console.ReadLine();
                            if (inputStr.Equals(Kashruiot.MILKY))
                            {
                                kashrut = Kashruiot.MILKY;
                            }
                            else
                                if (inputStr.Equals(Kashruiot.PARVE))
                            {
                                kashrut = Kashruiot.PARVE;
                            }
                            else
                                if (inputStr.Equals(Kashruiot.FLASHY))
                            {
                                kashrut = Kashruiot.FLASHY;
                            }
                            else
                                throw new Exception("not valid kashrut");
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
                                kind = inputStr.Equals(Kinds.FOOD) ? Kinds.FOOD : Kinds.DRINK;

                            Console.WriteLine("which kashrut do you want ?");
                            inputStr = Console.ReadLine();
                            if (!Kashruiot.TryParse(inputStr, out Kashrut))
                                throw new Exception("invalid input for kashrut");
                            else
                                Kashrut = inputStr.Equals(Kashruiot.MILKY) ? Kashruiot.MILKY : inputStr.Equals(Kashruiot.FLASHY) ? Kashruiot.FLASHY : Kashruiot.PARVE;
                            List<Item> itemsList = MyRefrigeritor.findItemsByKashrut(Kashrut, kind);
                            if (itemsList.Count > 0)
                                foreach (Item item in itemsList)
                                {
                                    Console.WriteLine(item);
                                }

                            break;
                        }
                    case 7:
                        {
                            List<Item> itemsList = MyRefrigeritor.sortItemByExpiredDate();
                            if (itemsList.Count > 0)
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
                    case 100: { break; }

                }

            }

        }

       
    }
}
