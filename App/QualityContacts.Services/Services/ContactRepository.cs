using QualityContacts.ServiceInterfaces.Models;
using QualityContacts.ServiceInterfaces.Services;
using QualityContacts.Services.Models;
using System.Collections.ObjectModel;

namespace QualityContacts.Services
{
    public class ContactRepository : IContactRepository
    {
        public IContact GetNewContact()
        {
            return new Contact();
        }

        public HashSet<string> GetTitles()
        {
            return _titles;
        }

        public void SaveNewTitle(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                _titles.Add(title);
            }

        }

        public ObservableCollection<IContact> GetContacts()
        {
            return _contacts;
        }
        public void SaveNewContact(IContact contact)
        {
            if (contact == null) throw new ArgumentNullException(nameof(contact));
            _contacts.Insert(0, contact);
        }

        private HashSet<string> _titles = new HashSet<string>();

        private ObservableCollection<IContact> _contacts = new ObservableCollection<IContact>();
    }
}
