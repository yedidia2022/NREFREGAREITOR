
//האם יש צורך לבדוק שהערך שהגיע הוא מהסוג שרציתי לקבל או שזה בנוי במערכת.
//האם להוסיף משתנה נוסף שישמור מקופ פנוי וכל הזמן נתחזק או שלהשמשיך בצורה של הפונקיה שבודקת
//האם הכיסוי שעשיתי לעדכון קטמה בפריט בצורה שזה פבליק שפונה לפריבט זה מספק?
using System.Dynamic;

namespace mekarer
{
    internal class IdGenrator
    { public static int idNumber { get; private set; }
        public static int giveId()
        {
         return idNumber++;
        }
        public static int giveIdHash()
        {
            return idNumber.GetHashCode();
        }

    }
}
