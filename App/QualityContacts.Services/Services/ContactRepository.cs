using QualityContacts.ServiceInterfaces.Models;
using QualityContacts.ServiceInterfaces.Services;
using QualityContacts.Services.Models;
using System.Collections.ObjectModel;

namespace QualityContacts.Services
{
    public class ContactRepository : IContactRepository
    {

        public ContactRepository() {
            AddToSalutationGenders("Herr", MALE_GENDER);
            AddToSalutationGenders("Herrn", MALE_GENDER);
            AddToSalutationGenders("Mr", MALE_GENDER);
            AddToSalutationGenders("Señor", MALE_GENDER);
            AddToSalutationGenders("M", MALE_GENDER);

            AddToSalutationGenders("Frau", FEMALE_GENDER);
            AddToSalutationGenders("Mrs", FEMALE_GENDER);
            AddToSalutationGenders("Ms", FEMALE_GENDER);
            AddToSalutationGenders("Signora", FEMALE_GENDER);
            AddToSalutationGenders("Sig.", FEMALE_GENDER);
            AddToSalutationGenders("Mme", FEMALE_GENDER);
            AddToSalutationGenders("Señora", FEMALE_GENDER);

            // Default academic titles
            REGISTERED_ACADEMIC_TITLES.Add("Dr.");
            REGISTERED_ACADEMIC_TITLES.Add("Professor");
            REGISTERED_ACADEMIC_TITLES.Add("Professorin");
            REGISTERED_ACADEMIC_TITLES.Add("Prof.");
            REGISTERED_ACADEMIC_TITLES.Add("Dr.-Ing.");
            REGISTERED_ACADEMIC_TITLES.Add("h.c.");
            REGISTERED_ACADEMIC_TITLES.Add("mult.");
            REGISTERED_ACADEMIC_TITLES.Add("rer.");
            REGISTERED_ACADEMIC_TITLES.Add("nat.");
            REGISTERED_ACADEMIC_TITLES.Add("Dipl.");
            REGISTERED_ACADEMIC_TITLES.Add("Dipl-Ing.");
            REGISTERED_ACADEMIC_TITLES.Add("Ing.");
            REGISTERED_ACADEMIC_TITLES.Add("B.S.");
            REGISTERED_ACADEMIC_TITLES.Add("M.S.");
        }

        private void AddToSalutationGenders(string key, string value)
        {
            if (REGISTERED_SALUTATION_TO_GENDER.ContainsKey(key)) return;

            REGISTERED_SALUTATION_TO_GENDER.Add(key, value);
        }

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

        internal string GetGender(string salutation)
        {
            if (REGISTERED_SALUTATION_TO_GENDER.ContainsKey(salutation))
            {
                return REGISTERED_SALUTATION_TO_GENDER[salutation];
            }
            return String.Empty;
        }

        internal IEnumerable<string> GetRegisteredAcademicTitles()
        {
            return REGISTERED_ACADEMIC_TITLES;
        }

        private HashSet<string> _titles = new HashSet<string>();

        private ObservableCollection<IContact> _contacts = new ObservableCollection<IContact>();

        private static readonly Dictionary<string, string> REGISTERED_SALUTATION_TO_GENDER = new Dictionary<string, string>();

        private const string MALE_GENDER = "männlich";
        private const string FEMALE_GENDER = "weiblich";
        private const string DIVERSE_GENDER = "divers";
        private const string EMPTY_GENDER = "ohne";

        private static readonly HashSet<string> REGISTERED_GENDERS = new HashSet<string> { "männlich", "weiblich", "divers", "ohne" };

        private static readonly HashSet<string> REGISTERED_SALUTATIONS = new HashSet<string> { "Herr", "Frau", "Mrs", "Mr", "Ms", "Signora", "Sig.", "Mme", "M", "Señora", "Señor" };

        private static readonly HashSet<string> REGISTERED_NOBLE_TITLES = new HashSet<string> { "Prinz", "Prinzessin", "Sir", "Dame", "Freiherrin", "Freiherr", "Baron", "Baronesse", "Ritter", "Graf", "Gräfin", "Fürst", "Fürstin", "Markgraf", "Pfalzgraf", "Landgraf", "Herzog", "Herzogin", "Kurfürst", "Großherzog", "Erzherzog", "König", "Königin" };

        private static readonly HashSet<string> REGISTERED_NOBLE_PRÄ_SUFFIXES = new HashSet<string> { "von", "vom", "van", "de", "zu", "zur" };

        private static readonly HashSet<string> REGISTERED_ACADEMIC_TITLES = new HashSet<string> { };
    }
}
