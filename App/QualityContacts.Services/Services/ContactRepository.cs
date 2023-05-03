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

        public IEnumerable<string> GetRegisteredGenders()
        {
            return REGISTERED_GENDERS;
        }

        public IEnumerable<string> GetRegisteredSalutations()
        {
            return REGISTERED_SALUTATIONS;
        }

        public IEnumerable<string> GetRegisteredNobleTitles()
        {
            return REGISTERED_NOBLE_TITLES;
        }

        public IEnumerable<string> GetRegisteredNoblePreSuffixes()
        {
            return REGISTERED_NOBLE_PRÄ_SUFFIXES;
        }

        private HashSet<string> _titles = new HashSet<string>();

        private ObservableCollection<IContact> _contacts = new ObservableCollection<IContact>();

        private static readonly HashSet<string> REGISTERED_GENDERS = new HashSet<string> { "männlich", "weiblich", "divers", "ohne" };

        private static readonly HashSet<string> REGISTERED_SALUTATIONS = new HashSet<string> { "Herr", "Frau", "Mrs", "Mr", "Ms", "Signora", "Sig.", "Mme", "M", "Señora", "Señor" };

        private static readonly HashSet<string> REGISTERED_NOBLE_TITLES = new HashSet<string> { "Prinz", "Prinzessin", "Sir", "Dame", "Freiherrin", "Freiherr", "Baron", "Baronesse", "Ritter", "Graf", "Gräfin", "Fürst", "Fürstin", "Markgraf", "Pfalzgraf", "Landgraf", "Herzog", "Herzogin", "Kurfürst", "Großherzog", "Erzherzog", "König", "Königin" };

        private static readonly HashSet<string> REGISTERED_NOBLE_PRÄ_SUFFIXES = new HashSet<string> { "von", "vom", "van", "de", "zu", "zur" };
    }
}
