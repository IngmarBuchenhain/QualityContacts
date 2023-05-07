using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QualityContacts.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Set ViewModel for reuse.
            _viewModel = new MainWindowViewModel();

            // Set DateContext for Binding.
            this.DataContext = _viewModel;

            // Set Binding for stored contacts.
            _storedContactsItemsControl.ItemsSource = _viewModel.StoredContacts;
        }

        private readonly MainWindowViewModel _viewModel;

        /// <summary>
        /// Splits the free contact input to a contact on a button click.
        /// </summary>
        private void SplitContactButtonClicked(object sender, RoutedEventArgs e)
        {
            _viewModel.SplitContactInput();
        }

        /// <summary>
        /// Splits the free contact input to a contact, if the return key is pressed.
        /// </summary>
        private void SplitContactEnterPressed(object sender, KeyEventArgs e)
        {
            // Validate whether the 'enter' key was pressed and ONLY then proceed.
            if (e.Key == Key.Return)
            {
                _viewModel.SplitContactInput();
            }
        }

        /// <summary>
        /// Validates the contact free input field on text change.
        /// </summary>
        private void InputTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.ValidateContactInput();
        }

        /// <summary>
        /// Validates the new contact on text change.
        /// </summary>
        private void ContactTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.ValidateNewContact();
        }

        /// <summary>
        /// Saves a new title, if the return key is pressed.
        /// </summary>
        private void SaveTitleEnterPressed(object sender, KeyEventArgs e)
        {
            // Validate whether the 'enter' key was pressed and ONLY then proceed.
            if (e.Key == Key.Return)
            {
                _viewModel.SaveNewTitle();
            }
        }

        /// <summary>
        /// Saves a new title on a button click.
        /// </summary>
        private void SaveTitleButtonClicked(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveNewTitle();
        }

        /// <summary>
        /// Saves a new contact to the data store on a button click.
        /// </summary>
        private void SaveContactButtonClicked(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveNewContact();
        }
    }
}
