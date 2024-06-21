using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Books39_40.Classes;

namespace Books39_40.DBModel
{
    public partial class Book
    {
        //internal string Description;

        public SolidColorBrush DiscountColor
        {
            get
            {
                if (ConnectionClass.conn.MaxDiscount.Where(c => c.Id_MaxDiscount == Id_MaxDiscount).First().Value >= 15)
                    return Brushes.Chartreuse;
                else if (ConnectionClass.conn.MaxDiscount.Where(c => c.Id_MaxDiscount == Id_MaxDiscount).First().Value >= 10)
                    return Brushes.Yellow;
                else if (ConnectionClass.conn.MaxDiscount.Where(c => c.Id_MaxDiscount == Id_MaxDiscount).First().Value >= 5)
                    return Brushes.LightGreen;
                else
                    return Brushes.Transparent;
            }
        }

        public string NewPrice
        { 
            get
            {
                if (Id_Ative_Discount != null)
                {
                    string a = Convert.ToString(Price - (Price / 100 * ConnectionClass.conn.ActiveDiscount.Where(c => c.Id_Ative_Discount == Id_Ative_Discount).First().Value));
                    return "\t" + a;
                }
                else return null;

            }

        }
    }
}
