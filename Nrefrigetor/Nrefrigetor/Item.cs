using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mekarer
{
    internal class Item
    {
        private int itemId; 
        private string name;
        private DateTime lastDayUse;
        private Kinds kind;
        private Kashruiot kashrut;
        private double placeOnSMR;
        //אולי לשנות שמכיל מדף 
        private int floorNum;
        public int ItemId { get; set; }
        public string Name { get; set; }
        public DateTime LastDayUse { get {  return lastDayUse; } set { if(value >DateTime.Today) lastDayUse = value; } }
        public Kinds Kind { get { return kind; } set { kind = value; } }
        public Kashruiot Kashrut { get { return kashrut; } set { kashrut = value; } }
        public double PlaceOnSMR { get {  return placeOnSMR; } set {  placeOnSMR = value; } }
        public int FloorNum
        {
            get { return floorNum; }
           private set { floorNum = value; }
        }
        public void SetFloorNum(int numF,Shelf s)
        {
         if(numF>0)
                if (numF == s.FloorNum) {
                    this.FloorNum = numF;
                }
            
        }
        public Item(string name,DateTime date,Kinds kind,Kashruiot kashrut,double SMR)
        {
            this.ItemId = IdGenrator.giveId();
            this.Name = name;
            this.LastDayUse=date;
            this.kind = kind;
            this.Kashrut = kashrut;
            this.PlaceOnSMR = SMR;
        }
        public override string ToString()
        {
            return "@item name : "+this.Name + " " +
                "   kashrut :"+ this.Kashrut + " "+
                " kind :" + this.Kind + " " +
                "  placeonSMR  :"+ this.PlaceOnSMR + " " +
                this.LastDayUse + " " +"itemid :"+ this.ItemId;
        }
        //לכאורה יכולתי לגשת ממחלקת מדף ולבדוק בגט תאריך האם גדוך מהיום אך למען הסדר עשיתי פה
        public Boolean isExpired()
        {

            if (this.LastDayUse > DateTime.Now)
            {
                return true;
            }
            else
            {   
                return false;
            }
            
          
        }
        


    }
}
