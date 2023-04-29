using QualityContacts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            viewModel = new MainWindowViewModel(new Validator(), new Parser());
            this.DataContext = viewModel;

            List<Contact> items = new List<Contact>();
            items.Add(new Contact() { FirstName = "Ingmar", LastName = "Bauckhage", Gender = "M", Titles = "Dr.", Salutation = "Herr" });
            items.Add(new Contact() { FirstName = "Vera", LastName = "Hötzel", Gender = "W", Titles = "Prof.", Salutation = "Frau" });
            items.Add(new Contact() { FirstName = "Yannick", LastName = "Bauckhage", Gender = "D", Titles = "Prof. Dr.", Salutation = "" });
            items.Add(new Contact() { FirstName = "Ingmar", LastName = "Bauckhage", Gender = "M", Titles = "Dr.", Salutation = "Herr" });
            items.Add(new Contact() { FirstName = "Vera", LastName = "Hötzel", Gender = "W", Titles = "Prof.", Salutation = "Frau" });
            items.Add(new Contact() { FirstName = "Yannick", LastName = "Bauckhage", Gender = "D", Titles = "Prof. Dr.", Salutation = "" });
            items.Add(new Contact() { FirstName = "Ingmar", LastName = "Bauckhage", Gender = "M", Titles = "Dr.", Salutation = "Herr" });
            items.Add(new Contact() { FirstName = "Vera", LastName = "Hötzel", Gender = "W", Titles = "Prof.", Salutation = "Frau" });
            items.Add(new Contact() { FirstName = "Yannick", LastName = "Bauckhage", Gender = "D", Titles = "Prof. Dr.", Salutation = "" });

            parsedContactsItemsControl.ItemsSource = items;
        }

        private MainWindowViewModel viewModel;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.ShowHelp)
            {
                viewModel.ShowHelp = false;
            }
            else
            {
                viewModel.ShowHelp = true;
            }

            //viewModel.SaveNumber();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                viewModel.Validate();
                if(!viewModel.HasErrors)
                viewModel.SaveNumber();
                //textBlock1.Text = "You Entered: " + textBox1.Text;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.Validate();
        }

        private void EnterSaveNewTitle(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
               
                //textBlock1.Text = "You Entered: " + textBox1.Text;
            }
        }

        private void ButtonClickSaveNewTitle(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Button clicked");
            viewModel.NewTitle += "b";
        }
    }

    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Salutation { get; set; }
        public string Titles { get; set; }
        public string LetterSalutation { get => $"Hallo {Salutation} {Titles} {FirstName} {LastName}"; set { } }
    }
}
