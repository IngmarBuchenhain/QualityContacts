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

            // Create an empty contact for the UI.
            _newContact = _repository.GetNewContact();

            // Generate hints for user for missing/wrong contact parts.
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
        /// String containing all validation warnings for the <see cref="NewContact"/>.
        /// </summary>
        public string ContactValidationWarnings
        {
            get => _contactValidationWarnings;
            private set
            {
                _contactValidationWarnings = value;
                NotifyPropertyChanged(nameof(ContactValidationWarnings));
                NotifyPropertyChanged(nameof(ShowContactValidationWarnings));
            }
        }

        /// <summary>
        /// Indicator whether contact validation warnings are present and should be shown by the UI.
        /// </summary>
        /// <value><see langword="true"/> if <see cref="ContactValidationWarnings"/> is not <see langword="null"/> or empty, otherwise <see langword="false"/>.</value>
        public bool ShowContactValidationWarnings { get => !string.IsNullOrEmpty(ContactValidationWarnings); }

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
        /// Splits the input in <see cref="ContactInput"/> into a <see cref="IContact"/> at best guess.<br/>
        /// Presents the new contact in the editing area and resets the input field.
        /// </summary>
        internal void SplitContactInput()
        {
            var newContact = _parser.ParseContactFreeInput(ContactInput);

            NewContact = newContact;            

            ContactInput = String.Empty;

            // Generate hints for user for missing/wrong contact parts.
            ValidateContactInput();
            ValidateNewContact();
        }

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

                // Generate hints for user for missing/wrong contact parts.
                ValidateContactInput();
                ValidateNewContact();
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

        /// <summary>
        /// Validates the <see cref="NewContact"/> in the editing area and presents errors/warnings if present.
        /// </summary>
        internal void ValidateNewContact()
        {
            // Apply validation
            IValidationResult contactValidation = _validator.Validate(NewContact);

            // Reset error/warning indicators
            ResetContactValidationErrorsAndWarnings();

            if (contactValidation.IsValid)
            {
                EnableContactSaving = true;

                foreach (var warning in contactValidation.ValidationWarnings)
                {
                    if (ContactValidationWarnings.Length == 0)
                    {
                        ContactValidationWarnings += MatchValidationWarningsToMessage(warning);
                    }
                    else
                        ContactValidationWarnings += Environment.NewLine + MatchValidationWarningsToMessage(warning);
                }
                if (contactValidation.PossibleNewTitle.Length > 0)
                {
                    NewTitle = contactValidation.PossibleNewTitle;
                }
            }
            else
            {
                EnableContactSaving = false;

                // Errors
                foreach (var error in contactValidation.ValidationErrors)
                {
                    // Set error indications
                    switch (error)
                    {
                        case ValidationError.FirstNameMissing:
                            FirstNameError = true;
                            break;
                        case ValidationError.LastNameMissing:
                            LastNameError = true;
                            break;
                        case ValidationError.GenderNotRegistered:
                            GenderError = true;
                            break;
                        case ValidationError.SalutationNotRegistered:
                            SalutationError = true;
                            break;
                        default:
                            break;
                    }

                    // Set error messages
                    if (ContactValidationErrors.Length == 0)
                    {
                        ContactValidationErrors += MatchValidationErrorsToMessage(error);
                    }
                    else
                        ContactValidationErrors += Environment.NewLine + MatchValidationErrorsToMessage(error);
                }

                // Warnings
                foreach (var warning in contactValidation.ValidationWarnings)
                {
                    if (ContactValidationWarnings.Length == 0)
                    {
                        ContactValidationWarnings += MatchValidationWarningsToMessage(warning);
                    }
                    else
                        ContactValidationWarnings += Environment.NewLine + MatchValidationWarningsToMessage(warning);
                }

                if (contactValidation.PossibleNewTitle.Length > 0)
                {
                    NewTitle = contactValidation.PossibleNewTitle;
                }
            }
        }

        /// <summary>
        /// Validates the <see cref="ContactInput"/> in the editing area and presents warnings if present.
        /// </summary>
        internal void ValidateContactInput()
        {
            // Apply validation
            IValidationResult contactValidation = _validator.Validate(ContactInput);

            // Reset warning indicators
            ResetContactInputValidationWarnings();

            if (contactValidation.IsValid)
            {
                // Present the warnings.
                foreach (var warning in contactValidation.ValidationWarnings)
                {
                    if (InputValidationWarnings.Length == 0)
                    {
                        InputValidationWarnings += MatchValidationWarningsToMessage(warning);
                    }
                    else
                    {
                        InputValidationWarnings += Environment.NewLine + MatchValidationWarningsToMessage(warning);
                    }
                }
            }
            else
            {
                // Only warnings currently, so exit the app.
                throw new NotImplementedException("No contact input string errors are supported right now!");
            }
        }

        #endregion Public/Internal Methods

        #region Private/Protected Methods

        /// <summary>
        /// Matches <see cref="ValidationWarning"/>s to an appropriate message for the UI.
        /// </summary>
        /// <param name="warning">The warning to match an UI-text to.</param>
        /// <returns>An user presentable text message.</returns>
        private static string MatchValidationWarningsToMessage(ValidationWarning warning)
        {
            return warning switch
            {
                ValidationWarning.UnusualCharacters => "Eingabe enthält ungewöhnliche Zeichen.",
                ValidationWarning.Incomplete => "Eingabe enthält zu wenig Wörter, um gültig aufgetrennt zu werden.",
                ValidationWarning.Ambiguous => "Eingabe/Kontakt konnte nicht eindeutig getrennt werden.",
                ValidationWarning.GenderMissing => "Es wurde kein Geschlecht angegeben.",
                ValidationWarning.SalutationMissing => "Es wurde keine Anrede angegeben.",
                ValidationWarning.TitleUnknown => "Es wurde ein unbekannter Titel verwendet. Dieser kann in die Datenbank aufgenommen werden.",
                _ => warning.ToString(),
            };
        }

        /// <summary>
        /// Matches <see cref="ValidationError"/>s to an appropriate message for the UI.
        /// </summary>
        /// <param name="error">The error to match an UI-text to.</param>
        /// <returns>An user presentable text message.</returns>
        private static string MatchValidationErrorsToMessage(ValidationError error)
        {
            return error switch
            {
                ValidationError.FirstNameMissing => "Es muss ein Vorname angegeben werden!",
                ValidationError.LastNameMissing => "Es muss ein Nachname angegeben werden!",
                ValidationError.GenderNotRegistered => "Es wurde ein ungültiges Geschlecht angegeben!",
                ValidationError.SalutationNotRegistered => "Es wurde eine ungültige Anrede angegeben!",
                _ => error.ToString(),
            };
        }

        /// <summary>
        /// Reset the warning indicators for the free contact input string.
        /// </summary>
        private void ResetContactInputValidationWarnings()
        {
            InputValidationWarnings = String.Empty;
        }

        /// <summary>
        /// Reset the error/warning indicators for the new contact.
        /// </summary>
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
