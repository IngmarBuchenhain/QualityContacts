using QualityContacts.ServiceInterfaces.Models;
using System.ComponentModel;

namespace QualityContacts.Services.Models
{
    /// <summary>
    /// <inheritdoc cref="IContact"/>
    /// </summary>
    public class Contact : IContact
    {
        #region Private members for public properties

        private string _firstAndMiddleName = String.Empty;

        private string _lastName = String.Empty;

        private string _gender = String.Empty;

        private string _salutation = String.Empty;

        private string _titles = String.Empty;

        private string _letterSalutation = String.Empty;

        /// <summary>
        /// Implementation of <see cref="INotifyPropertyChanged"/>.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion Private members for public properties

        #region Public Properties

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string FirstAndMiddleName
        {
            get => _firstAndMiddleName;
            set
            {
                _firstAndMiddleName = value;
                OnPropertyChanged(nameof(FirstAndMiddleName));
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Salutation
        {
            get => _salutation;
            set
            {
                _salutation = value;
                OnPropertyChanged(nameof(Salutation));
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Titles
        {
            get => _titles;
            set
            {
                _titles = value;
                OnPropertyChanged(nameof(Titles));
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string LetterSalutation
        {
            get => _letterSalutation;
            set
            {
                _letterSalutation = value;
                NotifyPropertyChanged(nameof(LetterSalutation));
            }
        }

        #endregion Public Properties

        #region Private/Protected Methods

        /// <summary>
        /// Helper for the MVVM-pattern to notifiy the view when properties changed.
        /// </summary>
        /// <param name="changedPropertyName">Name of the property which changed.</param>
        protected void NotifyPropertyChanged(string changedPropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(changedPropertyName));
        }

        /// <summary>
        /// Helper to always refresh the letter salutation when a property changes.
        /// </summary>
        /// <param name="changedPropertyName"></param>
        private void OnPropertyChanged(string changedPropertyName)
        {
            // Refresh the changed property
            NotifyPropertyChanged(changedPropertyName);

            // Also refresh the letter salutation
            NotifyPropertyChanged(nameof(LetterSalutation));
        }

        #endregion Private/Protected Methods
    }
}
