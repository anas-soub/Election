using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionDb
{
    public class Person
    {
        internal byte[] image;
        internal DateTime? dob;
        public string Name { get; set; }
        public string NationalNumber { get; set; }
        public string dateOfBirth { get { return dob.HasValue ? dob.Value.ToShortDateString() : ""; } }
        public string MunicipalName { get; set; }

        public PersonDetails PersonDetails { get; set; }
        public Person()
        {
            PersonDetails = new PersonDetails(this);
        }
        public Image GetImage()
        {
            if (image == null) return null;
            using (var ms = new MemoryStream(image))
            {
                return Image.FromStream(ms);
            }
        }
        public DateTime? GetBirthDate()
        {
            return dob;
        }



    }
}
