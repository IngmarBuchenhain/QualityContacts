using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityContacts.ServiceInterfaces.Models
{
    public interface IContact
    {
        






        string CountryCode { get; }
        string RegionCode { get; }
        string ThePhoneNumber { get; }
        string DirectDial { get; }
        string FormattedPhoneNumber { get; }
    }
}
