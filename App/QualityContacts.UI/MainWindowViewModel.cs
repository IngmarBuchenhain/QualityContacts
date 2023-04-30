using QualityContacts.ServiceInterfaces.Services;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityContacts.UI
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel(IValidator validator, IParser parser)
        {
            this.validator = validator;
            this.parser = parser;

            for(int i = 0; i < 10; i++)
            {
                _titles += "Dr.\r\n";
            }
        }

        private string newTitle = String.Empty;

        private bool showHelp = false;
        public bool ShowHelp
        {
            get
            {
return showHelp;
            }
            set
            {
                showHelp = value;
                NotifyPropertyChanged(nameof(ShowHelp));
            }
        }

        public string NewTitle
        {
            get
            {
                return this.newTitle;
            }
            set
            {
                Console.WriteLine("NewTitle");
                this.newTitle = value;
                NotifyPropertyChanged(nameof(NewTitle));
            }
        }

        private readonly Titles titles = new Titles();

        private readonly IValidator validator;

        private readonly IParser parser;

        private string _titles;

        public string Titles
        {
            get
            {
                return _titles;
            }
            set
            {

            }
        }

        public bool HasErrors
        {
            get; private set;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string DirectDial { get => directDial; private set => directDial = value; }

        private bool _letParse = false;

        public bool LetParse
        {
            get
            {
                return _letParse;
            }
            private set
            {
                _letParse = value;
                NotifyPropertyChanged(nameof(LetParse));
            }
        }

        private string input = "";
        public string Input
        {
            get
            {
return input;
            }
            set
            {
                input = value;
                NotifyPropertyChanged(nameof(Input));
            }
        }

        private string _formattedPhoneNumber = " - ";
        private string directDial = " - ";

        public String FormattedPhoneNumber
        {
            get
            {
                return _formattedPhoneNumber;
            }
            private set
            {
                _formattedPhoneNumber = value;
                NotifyPropertyChanged(nameof(FormattedPhoneNumber));
            }
        }

        private string vorwahl0 = " - ";

        public String Landesvorwahl
        {
            get
            {
                return vorwahl0;
            }
            private set
            {
                vorwahl0 = value;
                NotifyPropertyChanged(nameof(Landesvorwahl));
            }
        }

        private string vorwahl1 = " - ";

        public String Ortsvorwahl
        {
            get
            {
                return vorwahl1;
            }
            private set
            {
                vorwahl1 = value;
                NotifyPropertyChanged(nameof(Ortsvorwahl));
            }
        }

        private string number = " - ";

        public String Nummer
        {
            get
            {
                return number;
            }
            private set
            {
                number = value;
                NotifyPropertyChanged(nameof(Nummer));
            }
        }

        private string direct = " - ";

        public String Direct
        {
            get
            {
                return direct;
            }
            private set
            {
                direct = value;
                NotifyPropertyChanged(nameof(Direct));
            }
        }

        public void SaveNumber()
        {
            var phoneNumber = parser.Parse(Input);
            FormattedPhoneNumber = phoneNumber.FormattedPhoneNumber;
            Landesvorwahl = phoneNumber.CountryCode;
            Ortsvorwahl = phoneNumber.RegionCode;
            Nummer = phoneNumber.ThePhoneNumber;
            Direct = phoneNumber.DirectDial;
        }

        public void Validate()
        {
            Console.WriteLine("Test");
            if (validator.Validate(Input).IsValid)
            {
                InputValidationErrors = "";
                LetParse = true;
                HasErrors = false;
            }
            else
            {
                InputValidationErrors = "Not a valid number";
                LetParse = false;
                HasErrors = true;
            }
        }

        private string _inputValidationErrors = String.Empty;

        /// <summary>
        /// String containing all validation errors for the free input.
        /// </summary>
        public string InputValidationErrors
        {
            get => _inputValidationErrors;
            private set
            {
                _inputValidationErrors = value;
                NotifyPropertyChanged(nameof(InputValidationErrors));
                NotifyPropertyChanged(nameof(ShowInputValidationErrors));
            }
        }

        /// <summary>
        /// Indicator whether input validation errors are present and should be shown by the UI.
        /// </summary>
        /// <value><see langword="true"/> if <see cref="InputValidationErrors"/> is not <see langword="null"/> or empty, otherwise <see langword="false"/>.</value>
        public bool ShowInputValidationErrors { get => !String.IsNullOrEmpty(InputValidationErrors); }

        private string _contactValidationErrors = String.Empty;

        /// <summary>
        /// String containing all validation errors for the current contact.
        /// </summary>
        public string ContactValidationErrors
        {
            get => _contactValidationErrors;
            private set
            {
                _contactValidationErrors = value;
                NotifyPropertyChanged(nameof(ContactValidationErrors));
                NotifyPropertyChanged(nameof(ShowContactValidationErrors));
            }
        }

        /// <summary>
        /// Indicator whether contact validation errors are present and should be shown by the UI.
        /// </summary>
        /// <value><see langword="true"/> if <see cref="ContactValidationErrors"/> is not <see langword="null"/> or empty, otherwise <see langword="false"/>.</value>
        public bool ShowContactValidationErrors { get => !String.IsNullOrEmpty(ContactValidationErrors); }

        /// <summary>
        /// Helper for the MVVM-pattern to notifiy the view when properties changed.
        /// </summary>
        /// <param name="changedPropertyName">Name of the property which changed.</param>
        protected void NotifyPropertyChanged(String changedPropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(changedPropertyName));
        }
    }
}
