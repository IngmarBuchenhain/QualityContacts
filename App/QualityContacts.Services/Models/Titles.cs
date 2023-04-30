using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityContacts.Services.Models
{
    internal class Titles
    {
        public Titles()
        {

        }

        public List<string> DefaultTitles { get; set; }

        private void InitializeTitles()
        {
            if (DefaultTitles == null)
            {
                DefaultTitles = new List<string>();
            }
            DefaultTitles.Add("Dr.");
            DefaultTitles.Add("Prof.");
            DefaultTitles.Add("Dipl.-Ing.");
        }
    }
}
