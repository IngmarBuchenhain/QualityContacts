using QualityContacts.ServiceInterfaces;
using QualityContacts.ServiceInterfaces.Models;
using QualityContacts.ServiceInterfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QualityContacts.Services
{
    public class Parser : IParser
    {
        public IContact Parse(string input)
        {
           var result = new Contact();
           var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();


           //var phoneNumber = phoneNumberUtil.Parse(input, "DE");

           // result.CountryCode = phoneNumber.CountryCode.ToString();

           // result.ThePhoneNumber = phoneNumber.NationalNumber.ToString();

            

           // var length = input.Length;
           //// result.CountryCode = input.Substring(0, 1);
           // result.RegionCode = "";
           // //result.DirectDial = input.Substring(length - 1);
           // result.DirectDial = "";

            return result;
        }
    }
}
