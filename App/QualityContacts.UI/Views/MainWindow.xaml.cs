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

            _viewModel = new MainWindowViewModel();

            this.DataContext = _viewModel;

            storedContactsItemsControl.ItemsSource = _viewModel.StoredContacts;
        }

        private readonly MainWindowViewModel _viewModel;

        private void SplitContactButtonClicked(object sender, RoutedEventArgs e)
        {
            _viewModel.SplitContactInput();
        }

        private void SplitContactEnterPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                _viewModel.SplitContactInput();
            }
        }

        private void InputTextChanged(object sender, TextChangedEventArgs e)
        {

            _viewModel.ValidateContactInput();
        }

        private void ContactTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.ValidateNewContact();
        }

        private void SaveTitleEnterPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                _viewModel.SaveNewTitle();
            }
        }

        private void SaveTitleButtonClicked(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveNewTitle();
        }

        private void SaveContactButtonClicked(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveNewContact();
        }

    }

}
