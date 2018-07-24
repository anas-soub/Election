using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ElectionDb
{
    public class PersonDetails
    {
        private Person person;

        public BitmapSource Image
        {
            get
            {
                return (person == null || person.GetImage() == null) ?
null : ToBitmapSource(new Bitmap(person.GetImage()));
            }
        }
        public string Name { get { return person == null ? null : person.Name; } }
        public string Age { get { return person == null ? null : calculateAge(); } }

        public PersonDetails(Person person)
        {
            this.person = person;
        }
        private string calculateAge()
        {

            if (!person.GetBirthDate().HasValue) return "";

            DateTime now = DateTime.Now;
            DateTime dob = person.GetBirthDate().Value;
            TimeSpan ageTs = dob.Subtract(now);
            int y = now.Year - dob.Year;
            int m = now.Month - dob.Month;
            if (m > 11)
            {
                m = m - 12;
                y++;
            }
            else if (m < 0)
            {
                m = 12 - m;
                y--;
            }
            return string.Format("{0}:{1}", y, m);

        }
        public static BitmapSource ToBitmapSource(System.Drawing.Bitmap bmp)
        {

            BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
    bmp.GetHbitmap(),
    IntPtr.Zero,
    System.Windows.Int32Rect.Empty,
    BitmapSizeOptions.FromWidthAndHeight(bmp.Width, bmp.Height));
            return bs;
        }
    }
}
