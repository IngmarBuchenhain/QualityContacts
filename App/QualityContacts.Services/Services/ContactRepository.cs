using QualityContacts.ServiceInterfaces.Models;
using QualityContacts.ServiceInterfaces.Services;
using QualityContacts.Services.Models;
using System.Collections.ObjectModel;

namespace QualityContacts.Services
{
    /// <summary>
    /// <inheritdoc cref="IContactRepository"/>
    /// </summary>
    /// <remarks>
    /// NOTE: During prototyping phase no saving to persistance store will be performed.<br/>
    /// After prototyping phase this class would bind to a data access interface handling the concrete data storage. In this prototype all data storage values are stored directly into fields.
    /// </remarks>
    public class ContactRepository : IContactRepository
    {
        /// <summary>
        /// Creates an instance of <see cref="ContactRepository"/> to retrieve and store data.
        /// </summary>
        public ContactRepository()
        {
        }

        #region IContactRepository implementation

        public IContact GetNewContact()
        {
            return new Contact();
        }

        public HashSet<string> GetTitles()
        {
            return REGISTERED_ACADEMIC_TITLES;
        }

        public void SaveNewTitle(string title)
        {
            if (!string.IsNullOrEmpty(title.Trim()))
            {
                REGISTERED_ACADEMIC_TITLES.Add(title);
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

        public string GetDefaultGender()
        {
            return EMPTY_GENDER;
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
            return REGISTERED_NOBLE_PRE_SUFFIXES;
        }

        public string GetRegisteredGenderForSalutation(string salutation)
        {
            if (REGISTERED_SALUTATION_TO_GENDER.TryGetValue(salutation, out var value))
            {
                return value;
            }
            return String.Empty;
        }

        public Language GetLanguageForSalutation(string salutation)
        {
            if (REGISTERED_SALUTATION_TO_LANGUAGE.TryGetValue(salutation, out var value))
            {
                return value;
            }
            return Language.German;
        }

        public Gender GetTypedGenderForGender(string gender)
        {
            if (REGISTERED_GENDER_TO_TYPED_GENDER.TryGetValue(gender, out var value))
            {
                return value;
            }
            return Gender.None;
        }

        public string GetMaleLetterSalutation(Language language)
        {
            return language switch
            {
                Language.German => "Sehr geehrter Herr",
                Language.English => "Dear Mr",
                Language.Italian => "Egregio Signor",
                Language.French => "Monsieur",
                Language.Spanish => "Estimado Señor",
                _ => "Sehr geehrter Herr",
            };
        }

        public string GetFemaleLetterSalutation(Language language)
        {
            return language switch
            {
                Language.German => "Sehr geehrte Frau",
                Language.English => "Dear Ms",
                Language.Italian => "Gentile Signora",
                Language.French => "Madame",
                Language.Spanish => "Estimada Señora",
                _ => "Sehr geehrter Frau",
            };
        }

        public string GetDiverseLetterSalutation(Language language)
        {
            return language switch
            {
                Language.German => "Hallo",
                Language.English => "Dear",
                Language.Italian => "Ciao",
                Language.French => "Bonjour",
                Language.Spanish => "Hola",
                _ => "Hallo",
            };
        }

        public string GetDefaultLetterSalutation(Language language)
        {
            return language switch
            {
                Language.German => "Sehr geehrte Damen und Herren",
                Language.English => "Dear Sirs",
                Language.Italian => "Egregi Signori",
                Language.French => "Messieursdames",
                Language.Spanish => "Estimados Señores y Señoras",
                _ => "Sehr geehrte Damen und Herren",
            };
        }

        #endregion IContactRepository implementation

        #region Dummy Data (from DB after prototyping phase)

        private const string MALE_GENDER = "männlich";
        private const string FEMALE_GENDER = "weiblich";
        private const string DIVERSE_GENDER = "divers";
        private const string EMPTY_GENDER = "ohne";

        // "Stored" contacts.
        private readonly ObservableCollection<IContact> _contacts = new ObservableCollection<IContact>();

        private static readonly Dictionary<string, string> REGISTERED_SALUTATION_TO_GENDER = new Dictionary<string, string>()
        {
            {"Herr", MALE_GENDER},
            {"Herrn", MALE_GENDER},
            {"Mr", MALE_GENDER},
            {"Sig.", MALE_GENDER},
            {"Signor", MALE_GENDER},
            {"Señor", MALE_GENDER},
            {"M", MALE_GENDER},
            {"Frau", FEMALE_GENDER},
            {"Mrs", FEMALE_GENDER},
            {"Ms", FEMALE_GENDER},
            {"Signora", FEMALE_GENDER},
            {"Mme", FEMALE_GENDER},
            {"Señora", FEMALE_GENDER}
        };

        private static readonly Dictionary<string, Language> REGISTERED_SALUTATION_TO_LANGUAGE = new Dictionary<string, Language>()
        {
            {"Herr", Language.German},
            {"Herrn", Language.German},
            {"Mr", Language.English},
            {"Sig.", Language.Italian},
            {"Signor", Language.Italian},
            {"Señor", Language.Spanish},
            {"M", Language.French},
            {"Frau", Language.German},
            {"Mrs", Language.English},
            {"Ms", Language.English},
            {"Signora", Language.Italian},
            {"Mme", Language.French},
            {"Señora", Language.Spanish}
        };

        private static readonly Dictionary<string, Gender> REGISTERED_GENDER_TO_TYPED_GENDER = new Dictionary<string, Gender>()
        {
            {MALE_GENDER, Gender.Male},
            {FEMALE_GENDER, Gender.Female},
            {DIVERSE_GENDER, Gender.Diverse},
            {EMPTY_GENDER, Gender.None}
        };

        private static readonly HashSet<string> REGISTERED_GENDERS = new HashSet<string> { "männlich", "weiblich", "divers", "ohne" };

        private static readonly HashSet<string> REGISTERED_SALUTATIONS = new HashSet<string> { "Herr", "Frau", "Mrs", "Mr", "Ms", "Signora", "Signor", "Sig.", "Mme", "M", "Señora", "Señor", "" };

        private static readonly HashSet<string> REGISTERED_NOBLE_TITLES = new HashSet<string> { "Prinz", "Prinzessin", "Sir", "Dame", "Freiherrin", "Freiherr", "Baron", "Baronesse", "Ritter", "Graf", "Gräfin", "Fürst", "Fürstin", "Markgraf", "Pfalzgraf", "Landgraf", "Herzog", "Herzogin", "Kurfürst", "Großherzog", "Erzherzog", "König", "Königin" };

        private static readonly HashSet<string> REGISTERED_NOBLE_PRE_SUFFIXES = new HashSet<string> { "von", "vom", "van", "de", "zu", "zur" };

        private static readonly HashSet<string> REGISTERED_ACADEMIC_TITLES = new HashSet<string> { "Professorin", "Professor", "Prof.", "Dr.", "Dr.-Ing.", "rer.", "nat.", "mult.", "h.c.", "Dipl.-Ing.", "Dipl.", "Ing.", "B.S.", "M.S.", "B.A.", "M.A." };

        #endregion Dummy Data (from DB after prototyping phase)
    }
}
