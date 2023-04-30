using QualityContacts.ServiceInterfaces.ErrorHandling;
using QualityContacts.ServiceInterfaces.Models;
using QualityContacts.ServiceInterfaces.Services;
using QualityContacts.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace QualityContacts.UI
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            _validator = new Validator();
            _parser = new Parser();
            _repository = new Repository();
        }

        private string _newTitle = string.Empty;

        public string NewTitle
        {
            get
            {
                return _newTitle;
            }
            set
            {
                _newTitle = value;
                NotifyPropertyChanged(nameof(NewTitle));
            }
        }

        private readonly IContactValidator _validator;

        private readonly IParser _parser;

        private readonly IRepository _repository;

        public string Titles
        {
            get
            {
                string allTitlesList = String.Empty;
                foreach (var title in _repository.GetTitles())
                {
                    allTitlesList += title + Environment.NewLine;
                }
                return allTitlesList;
            }
            set { /* Intentionally left empty, because of WPF-Binding bug */ }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<IContact> StoredContacts
        {
            get
            {
                return _repository.GetContacts();
            }
        }


        private IContact _newContact = new Contact();

        public IContact NewContact
        {
            get
            {
                return _newContact;
            }
            private set
            {
                _newContact = value;
                NotifyPropertyChanged(nameof(NewContact));
            }
        }


        private bool _enableContactSaving = true;

        public bool EnableContactSaving
        {
            get => _enableContactSaving;
            private set
            {
                _enableContactSaving = value;
                NotifyPropertyChanged(nameof(EnableContactSaving));
            }
        }

        private bool _enableInputSplitting = true;

        public bool EnableInputSplitting
        {
            get => _enableInputSplitting;
            private set
            {
                _enableInputSplitting = value;
                NotifyPropertyChanged(nameof(EnableInputSplitting));
            }
        }

        private string _contactInput = string.Empty;

        /// <summary>
        /// Free input of the user.
        /// </summary>
        public string ContactInput
        {
            get => _contactInput;
            set
            {
                _contactInput = value;
                NotifyPropertyChanged(nameof(ContactInput));
            }
        }

        private string _inputValidationErrors = string.Empty;

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


        private string _inputValidationWarnings = string.Empty;

        /// <summary>
        /// String containing all validation errors for the free input.
        /// </summary>
        public string InputValidationWarnings
        {
            get => _inputValidationWarnings;
            private set
            {
                _inputValidationWarnings = value;
                NotifyPropertyChanged(nameof(InputValidationWarnings));
                NotifyPropertyChanged(nameof(ShowInputValidationWarnings));
            }
        }

        public bool ShowInputValidationWarnings { get => !string.IsNullOrEmpty(InputValidationWarnings); }

        /// <summary>
        /// Indicator whether input validation errors are present and should be shown by the UI.
        /// </summary>
        /// <value><see langword="true"/> if <see cref="InputValidationErrors"/> is not <see langword="null"/> or empty, otherwise <see langword="false"/>.</value>
        public bool ShowInputValidationErrors { get => !string.IsNullOrEmpty(InputValidationErrors); }

        private string _contactValidationErrors = string.Empty;

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

        private bool _salutationError = false;

        public bool SalutationError
        {
            get => _salutationError;
            private set
            {
                _salutationError = value;
                NotifyPropertyChanged(nameof(SalutationError));
            }
        }

        private bool _genderError = false;

        public bool GenderError
        {
            get => _genderError;
            private set
            {
                _genderError = value;
                NotifyPropertyChanged(nameof(GenderError));
            }
        }

        private bool _titlesError = false;

        public bool TitlesError
        {
            get => _titlesError;
            set
            {
                _titlesError = value;
                NotifyPropertyChanged(nameof(TitlesError));
            }
        }

        private bool _firstNameError = false;

        public bool FirstNameError
        {
            get => _firstNameError;
            set
            {
                _firstNameError = value;
                NotifyPropertyChanged(nameof(FirstNameError));
            }

        }

        private bool _lastNameError = false;

        public bool LastNameError
        {
            get => _lastNameError; 
            set
            {
                _lastNameError = value;
                NotifyPropertyChanged(nameof(LastNameError));
            }
        }

        /// <summary>
        /// Indicator whether contact validation errors are present and should be shown by the UI.
        /// </summary>
        /// <value><see langword="true"/> if <see cref="ContactValidationErrors"/> is not <see langword="null"/> or empty, otherwise <see langword="false"/>.</value>
        public bool ShowContactValidationErrors { get => !string.IsNullOrEmpty(ContactValidationErrors); }

        /// <summary>
        /// Helper for the MVVM-pattern to notifiy the view when properties changed.
        /// </summary>
        /// <param name="changedPropertyName">Name of the property which changed.</param>
        protected void NotifyPropertyChanged(string changedPropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(changedPropertyName));
        }

        internal void SaveNewContact()
        {

            _repository.SaveNewContact(NewContact);
            NotifyPropertyChanged(nameof(StoredContacts));

            NewContact = new Contact();
        }

        internal void SaveNewTitle()
        {
            _repository.SaveNewTitle(NewTitle);
            NewTitle = String.Empty;
            NotifyPropertyChanged(nameof(Titles));

        }

        internal void ValidateNewContact()
        {
            IValidationResult contactValidation = _validator.Validate(NewContact);

            if (contactValidation.IsValid)
            {
                ContactValidationErrors = String.Empty;

                EnableContactSaving = true;
            }
            else
            {
                EnableContactSaving = false;

                foreach (var error in contactValidation.validationErrors)
                {
                    ContactValidationErrors += error + Environment.NewLine;
                }
            }
        }

        internal void SplitContactInput()
        {
            var newContact = _parser.ParseContactInput(ContactInput);
            NewContact = newContact;
        }

        internal void ValidateContactInput()
        {
            IValidationResult contactValidation = _validator.Validate(ContactInput);

            if (contactValidation.IsValid)
            {
                InputValidationErrors = String.Empty;

                EnableInputSplitting = true;

                if (contactValidation.hasWarnings)
                {
                    foreach (var warning in contactValidation.validationWarnings)
                    {
                        InputValidationWarnings += warning + Environment.NewLine;
                    }
                }
                else
                {
                    InputValidationWarnings = String.Empty;
                }
            }
            else
            {
                EnableInputSplitting = false;

                foreach (var error in contactValidation.validationErrors)
                {
                    InputValidationErrors += error + Environment.NewLine;
                }

                foreach (var warning in contactValidation.validationWarnings)
                {
                    InputValidationWarnings += warning + Environment.NewLine;
                }
            }
        }

       
    }
}
