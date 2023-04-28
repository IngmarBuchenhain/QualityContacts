using QualityContacts.ServiceInterfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityContacts.Services
{
    public class PhoneNumber : IContact
    {
        public string CountryCode { get; set; } = "";

        public string RegionCode { get; set; } = "";

        public string DirectDial { get; set; } = "";

        public string FormattedPhoneNumber
                {
            get
            {
                return $"{CountryCode}{RegionCode}{ThePhoneNumber}{DirectDial}";
            }
}

public string ThePhoneNumber { get; set; } = "";
    }
}
