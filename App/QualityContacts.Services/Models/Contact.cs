using QualityContacts.ServiceInterfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QualityContacts.Services.ContactRepository;

namespace QualityContacts.Services.Models
{
    public class Contact : IContact
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
      
        public string Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                NotifyPropertyChanged(nameof(LetterSalutation));
                NotifyPropertyChanged(nameof(Gender));
            }
        }
        public string Salutation { get; set; } = string.Empty;
        public string Titles { get; set; } = string.Empty;
        public string LetterSalutation { get => generateLetterSalutation(); set { } }


        private GenderEnum _genderEnum = GenderEnum.None;
        private string _gender = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Helper for the MVVM-pattern to notifiy the view when properties changed.
        /// </summary>
        /// <param name="changedPropertyName">Name of the property which changed.</param>
        protected void NotifyPropertyChanged(string changedPropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(changedPropertyName));
        }

        private string generateLetterSalutation()
        {
            string result = string.Empty;
            var salutation = new ContactRepository().GenerateLetterSalutation(Gender, Salutation);

            switch (Gender)
            {
                case "divers":
                    result = $"{salutation} {Titles} {FirstName} {LastName}";
                    break;
                case "ohne":
                    result = salutation;
                    break;
                case "männlich":
                    result = $"{salutation} {Titles} {LastName}";
                    break;
                case "weiblich":
                    result = $"{salutation} {Titles} {LastName}";
                    break;
                default:
                    result = salutation;
                    break;
            }

            return result;
        }
    }
}
