﻿using QualityContacts.ServiceInterfaces.ErrorHandling;
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
        #region Members, Events and Constructor

        /// <summary>
        /// Initializes the ViewModel with Backend-Services.
        /// </summary>
        public MainWindowViewModel()
        {
            // Only here the actual implementations of the Backend are used, so they can be easily replaced.
            _validator = new ContactValidator();
            _parser = new ContactParser();
            _repository = new ContactRepository();

            _newContact = _repository.GetNewContact();

            ValidateContactInput();
            ValidateNewContact();
        }

        /// <summary>
        /// Instance of <see cref="IContactValidator"/> to get validation services.
        /// </summary>
        private readonly IContactValidator _validator;

        /// <summary>
        /// Instance of <see cref="IContactParser"/> to get parsing services.
        /// </summary>
        private readonly IContactParser _parser;

        /// <summary>
        /// Instance of <see cref="IContactRepository"/> to store and retrieve data.
        /// </summary>
        private readonly IContactRepository _repository;

        /// <summary>
        /// Helper for the MVVM pattern.
        /// <inheritdoc/>
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Do not use directly, if UI should be notified. See <see cref="NewContact"/>-property.
        /// </summary>
        private IContact _newContact;

        /// <summary>
        /// Do not use directly, if UI should be notified. See <see cref="NewTitle"/>-property.
        /// </summary>
        private string _newTitle = String.Empty;

        /// <summary>
        /// Do not use directly, if UI should be notified. See <see cref="ContactInput"/>-property.
        /// </summary>
        private string _contactInput = String.Empty;

        /// <summary>
        /// Do not use directly, if UI should be notified. See <see cref="EnableInputSplitting"/>-property.
        /// </summary>
        private bool _enableInputSplitting = true;

        /// <summary>
        /// Do not use directly, if UI should be notified. See <see cref="EnableContactSaving"/>-property.
        /// </summary>
        private bool _enableContactSaving = true;

        /// <summary>
        /// Do not use directly, if UI should be notified. See <see cref="ContactValidationWarnings"/>-property.
        /// </summary>
        private string _contactValidationWarnings = String.Empty;

        /// <summary>
        /// Do not use directly, if UI should be notified. See <see cref="InputValidationWarnings"/>-property.
        /// </summary>
        private string _inputValidationWarnings = String.Empty;

        /// <summary>
        /// Do not use directly, if UI should be notified. See <see cref="ContactValidationErrors"/>-property.
        /// </summary>
        private string _contactValidationErrors = String.Empty;

        /// <summary>
        /// Do not use directly, if UI should be notified. See <see cref="GenderError"/>-property.
        /// </summary>
        private bool _genderError = false;

        /// <summary>
        /// Do not use directly, if UI should be notified. See <see cref="SalutationError"/>-property.
        /// </summary>
        private bool _salutationError = false;

        /// <summary>
        /// Do not use directly, if UI should be notified. See <see cref="TitlesError"/>-property.
        /// </summary>
        private bool _titlesError = false;

        /// <summary>
        /// Do not use directly, if UI should be notified. See <see cref="FirstNameError"/>-property.
        /// </summary>
        private bool _firstNameError = false;

        /// <summary>
        /// Do not use directly, if UI should be notified. See <see cref="LastNameError"/>-property.
        /// </summary>
        private bool _lastNameError = false;

        #endregion Members, Events and Constructor

        #region Public Properties

        /// <summary>
        /// The contact to be edited directly or after splitting the <see cref="ContactInput"/>.
        /// </summary>
        public IContact NewContact
        {
            get => _newContact;
            private set
            {
                _newContact = value;
                NotifyPropertyChanged(nameof(NewContact));
            }
        }

        /// <summary>
        /// All contacts which has been saved.
        /// </summary>
        /// <remarks>
        /// NOTE: Does not store contacts after exiting the app (during prototyping phase).
        /// </remarks>
        public ObservableCollection<IContact> StoredContacts
        {
            get => _repository.GetContacts();
        }

        /// <summary>
        /// The new title which should be added to the known titles.
        /// </summary>
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

        /// <summary>
        /// All default and saved titles.
        /// </summary>
        /// <remarks>
        /// NOTE: Does not store user titles after exiting the app (during prototyping phase).
        /// </remarks>
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

        /// <summary>
        /// Free user input which can be splitted to a contact.
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

        /// <summary>
        /// Indicator whether splitting of <see cref="ContactInput"/> is enabled or not (due to validation errors).
        /// </summary>
        public bool EnableInputSplitting
        {
            get => _enableInputSplitting;
            private set
            {
                _enableInputSplitting = value;
                NotifyPropertyChanged(nameof(EnableInputSplitting));
            }
        }

        /// <summary>
        /// Indicator whether saving of <see cref="NewContact"/> is enabled or not (due to validation errors).
        /// </summary>
        public bool EnableContactSaving
        {
            get => _enableContactSaving;
            private set
            {
                _enableContactSaving = value;
                NotifyPropertyChanged(nameof(EnableContactSaving));
            }
        }

        /// <summary>
        /// String containing all validation errors for the <see cref="ContactInput"/>.
        /// </summary>
        public string ContactValidationWarnings
        {
            get => _contactValidationWarnings;
            private set
            {
                _contactValidationWarnings = value;
                NotifyPropertyChanged(nameof(ContactValidationWarnings));
                NotifyPropertyChanged(nameof(ShowInputValidationErrors));
            }
        }

        /// <summary>
        /// Indicator whether input validation errors are present and should be shown by the UI.
        /// </summary>
        /// <value><see langword="true"/> if <see cref="ContactValidationWarnings"/> is not <see langword="null"/> or empty, otherwise <see langword="false"/>.</value>
        public bool ShowInputValidationErrors { get => !string.IsNullOrEmpty(ContactValidationWarnings); }

        /// <summary>
        /// String containing all validation warnings for the <see cref="ContactInput"/>.
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

        /// <summary>
        /// Indicator whether input validation warnings are present and should be shown by the UI.
        /// </summary>
        /// <value><see langword="true"/> if <see cref="InputValidationWarnings"/> is not <see langword="null"/> or empty, otherwise <see langword="false"/>.</value>
        public bool ShowInputValidationWarnings { get => !string.IsNullOrEmpty(InputValidationWarnings); }

        /// <summary>
        /// String containing all validation errors for the current <see cref="NewContact"/>.
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
        public bool ShowContactValidationErrors { get => !string.IsNullOrEmpty(ContactValidationErrors); }

        /// <summary>
        /// Indicator whether a gender error is present for <see cref="NewContact"/>.
        /// </summary>
        public bool GenderError
        {
            get => _genderError;
            private set
            {
                _genderError = value;
                NotifyPropertyChanged(nameof(GenderError));
            }
        }

        /// <summary>
        /// Indicator whether a salutation error is present for <see cref="NewContact"/>.
        /// </summary>
        public bool SalutationError
        {
            get => _salutationError;
            private set
            {
                _salutationError = value;
                NotifyPropertyChanged(nameof(SalutationError));
            }
        }

        /// <summary>
        /// Indicator whether a titles error is present for <see cref="NewContact"/>.
        /// </summary>
        public bool TitlesError
        {
            get => _titlesError;
            private set
            {
                _titlesError = value;
                NotifyPropertyChanged(nameof(TitlesError));
            }
        }

        /// <summary>
        /// Indicator whether a firstname error is present for <see cref="NewContact"/>.
        /// </summary>
        public bool FirstNameError
        {
            get => _firstNameError;
            private set
            {
                _firstNameError = value;
                NotifyPropertyChanged(nameof(FirstNameError));
            }

        }

        /// <summary>
        /// Indicator whether a lastname error is present for <see cref="NewContact"/>.
        /// </summary>
        public bool LastNameError
        {
            get => _lastNameError;
            private set
            {
                _lastNameError = value;
                NotifyPropertyChanged(nameof(LastNameError));
            }
        }

        #endregion Public Properties

        #region Public/Internal Methods

        /// <summary>
        /// Save the current edited <see cref="NewContact"/> to the repository if no validation errors are present.<br/>
        /// Resets the editing area.
        /// </summary>
        /// <remarks>
        /// NOTE: During prototyping phase no saving to persistance store will be performed.
        /// </remarks>
        internal void SaveNewContact()
        {
            if (EnableContactSaving)
            {
                _repository.SaveNewContact(NewContact);

                NotifyPropertyChanged(nameof(StoredContacts));

                // Reset the editing area.
                NewContact = _repository.GetNewContact();
                ContactInput = String.Empty;
            }
        }

        /// <summary>
        /// Validates the <see cref="NewContact"/> in the editing area and presents errors if present.
        /// </summary>
        internal void ValidateNewContact()
        {
            IValidationResult contactValidation = _validator.Validate(NewContact);
            ResetContactValidationErrorsAndWarnings();

            if (contactValidation.IsValid)
            {
     
     

                EnableContactSaving = true;
                if (contactValidation.HasWarnings)
                {
                    
                    foreach (var warning in contactValidation.ValidationWarnings)
                    {
                        // TODO: After prototyping phase localization of errors should happen.
                        if (ContactValidationWarnings.Length == 0)
                            ContactValidationWarnings += MatchValidationWarningsToMessage(warning);
                        else
                            ContactValidationWarnings += Environment.NewLine + MatchValidationWarningsToMessage(warning);
                    }
                    if(contactValidation.PossibleNewTitle.Length > 0)
                    {
                  
                            NewTitle = contactValidation.PossibleNewTitle;
                      
                    }
                }
            }
            else
            {
                EnableContactSaving = false;

                foreach (var error in contactValidation.ValidationErrors)
                {
                    // TODO: After prototyping phase localization of errors should happen.
                    if (ContactValidationErrors.Length == 0)
                        ContactValidationErrors += MatchValidationErrorsToMessage(error);
                    else
                        ContactValidationErrors += Environment.NewLine + MatchValidationErrorsToMessage(error);
                }

                foreach (var warning in contactValidation.ValidationWarnings)
                {
                    // TODO: After prototyping phase localization of errors should happen.
                    if (ContactValidationWarnings.Length == 0)
                        ContactValidationWarnings += MatchValidationWarningsToMessage(warning);
                    else
                        ContactValidationWarnings += Environment.NewLine + MatchValidationWarningsToMessage(warning);
                }
                if (contactValidation.PossibleNewTitle.Length > 0)
                {
    
                        NewTitle = contactValidation.PossibleNewTitle;
              
                }
            }
        }

        private void PrepareNewContactFieldsForValidation()
        {
            // For each of the contacts fields some convenient preparations are made to support the user using valid values.

            NewContact.Gender.Trim().ToLower();

            if (!String.IsNullOrEmpty(NewContact.Salutation) && !String.IsNullOrEmpty(NewContact.Salutation.Trim()))
            {
                string cleanedGender = NewContact.Salutation.Trim();

                if (cleanedGender.Length > 1)
                {
                    cleanedGender = String.Concat(cleanedGender[0].ToString().ToUpper(), cleanedGender.AsSpan(1));
                    NewContact.Salutation = cleanedGender;
                }
                else
                {
                    NewContact.Salutation = cleanedGender.ToUpper();
                }

                


            }

            NewContact.Titles = NewContact.Titles.Trim();

            NewContact.FirstName = NewContact.FirstName.Trim();

            NewContact.LastName = NewContact.LastName.Trim();


    }

        /// <summary>
        /// Splits the input in <see cref="ContactInput"/> into a <see cref="IContact"/> if no validation errors are present.<br/>
        /// Presents the new contact in the editing area and resets the input field.
        /// </summary>
        internal void SplitContactInput()
        {
            if (EnableInputSplitting)
            {
                var newContact = _parser.ParseContactFreeInput(ContactInput);

                NewContact = newContact;

                ContactInput = String.Empty;
            }
        }

        /// <summary>
        /// Validates the <see cref="ContactInput"/> in the editing area and presents errors if present.
        /// </summary>
        internal void ValidateContactInput()
        {
            IValidationResult contactValidation = _validator.Validate(ContactInput);
            ContactValidationWarnings = String.Empty;
            InputValidationWarnings = String.Empty;
            if (contactValidation.IsValid)
            {
               

                EnableInputSplitting = true;

                if (contactValidation.HasWarnings)
                {
                    foreach (var warning in contactValidation.ValidationWarnings)
                    {
                        // TODO: After prototyping phase localization of errors should happen.
                        InputValidationWarnings += MatchValidationWarningsToMessage(warning) + Environment.NewLine;
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

                foreach (var error in contactValidation.ValidationErrors)
                {
                    // TODO: After prototyping phase localization of errors should happen.
                    ContactValidationWarnings += error + Environment.NewLine;
                }

                foreach (var warning in contactValidation.ValidationWarnings)
                {
                    // TODO: After prototyping phase localization of errors should happen.

                    InputValidationWarnings += MatchValidationWarningsToMessage(warning) + Environment.NewLine;
                }
            }
        }

        /// <summary>
        /// Save the current edited <see cref="NewTitle"/> to the repository if not already present.
        /// </summary>
        /// <remarks>
        /// NOTE: During prototyping phase no saving to persistance store will be performed.
        /// </remarks>
        internal void SaveNewTitle()
        {
            _repository.SaveNewTitle(NewTitle);

            NotifyPropertyChanged(nameof(Titles));

            // Reset the new title area.
            NewTitle = String.Empty;
        }

        #endregion Public/Internal Methods

        #region Private/Protected Methods

        private string MatchValidationWarningsToMessage(ValidationWarning warning)
        {
            switch (warning)
            {
                case ValidationWarning.UnusualCharacters:
                    return "Eingabe enthält ungewöhnliche Zeichen.";
                case ValidationWarning.Incomplete:
                    return "Eingabe enthält zu wenig Wörter, um gültig aufgetrennt zu werden.";
                default:
                    return warning.ToString();
            }
        }

        private string MatchValidationErrorsToMessage(ValidationError error)
        {
            switch (error)
            {
                case ValidationError.FirstNameMissing:
                    FirstNameError = true;
                    return "Es muss ein Vorname angegeben werden!";
                case ValidationError.LastNameMissing:
                    LastNameError = true;
                    return "Es muss ein Nachname angegeben werden!";
                case ValidationError.GenderNotRegistered:
                    GenderError = true;
                    return "Es muss ein Geschlecht angegeben werden!";
                default:
                    return error.ToString();       
            }
        }

        private void ResetContactValidationErrorsAndWarnings()
        {
            ContactValidationErrors = String.Empty;
            ContactValidationWarnings = String.Empty;

            GenderError = false;
            SalutationError = false;
            FirstNameError = false;
            LastNameError = false;
            TitlesError = false;
        }

        /// <summary>
        /// Helper for the MVVM-pattern to notifiy the view when properties changed.
        /// </summary>
        /// <param name="changedPropertyName">Name of the property which changed.</param>
        protected void NotifyPropertyChanged(string changedPropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(changedPropertyName));
        }

        #endregion Private/Protected Methods
    }
}
