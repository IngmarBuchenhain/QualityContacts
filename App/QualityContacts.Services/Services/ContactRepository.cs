using QualityContacts.ServiceInterfaces.Models;
using QualityContacts.ServiceInterfaces.Services;
using QualityContacts.Services.Models;
using System.Collections.ObjectModel;

namespace QualityContacts.Services
{
    public class ContactRepository : IContactRepository
    {

        public ContactRepository() {
            AddToSalutationGenders("Herr", MALE_GENDER, GenderEnum.Male, Language.German);
            AddToSalutationGenders("Herrn", MALE_GENDER, GenderEnum.Male, Language.German);
            AddToSalutationGenders("Mr", MALE_GENDER, GenderEnum.Male, Language.English);
            AddToSalutationGenders("Señor", MALE_GENDER, GenderEnum.Male, Language.Spanish);
            AddToSalutationGenders("M", MALE_GENDER, GenderEnum.Male, Language.French);

            AddToSalutationGenders("Frau", FEMALE_GENDER, GenderEnum.Female, Language.German);
            AddToSalutationGenders("Mrs", FEMALE_GENDER, GenderEnum.Female, Language.English);
            AddToSalutationGenders("Ms", FEMALE_GENDER, GenderEnum.Female, Language.English);
            AddToSalutationGenders("Signora", FEMALE_GENDER, GenderEnum.Female, Language.Italian);
            AddToSalutationGenders("Sig.", MALE_GENDER, GenderEnum.Male, Language.Italian);
            AddToSalutationGenders("Mme", FEMALE_GENDER, GenderEnum.Female, Language.French);
            AddToSalutationGenders("Señora", FEMALE_GENDER, GenderEnum.Female, Language.Spanish);

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

        private void AddToSalutationGenders(string key, string value, GenderEnum gender, Language language)
        {
            if (!REGISTERED_SALUTATION_TO_GENDER.ContainsKey(key))
            {
                REGISTERED_SALUTATION_TO_GENDER.Add(key, value);
            }
            if (!REGISTERED_SALUTATION_TO_GENDEREnum.ContainsKey(value))
            {
                REGISTERED_SALUTATION_TO_GENDEREnum.Add(value, gender);
            }
            if (!REGISTERED_SALUTATION_TO_Language.ContainsKey(key))
            {
                REGISTERED_SALUTATION_TO_Language.Add(key, language);
            }

            if (!REGISTERED_SALUTATION_TO_GENDEREnum.ContainsKey("divers"))
            {
                REGISTERED_SALUTATION_TO_GENDEREnum.Add("divers", GenderEnum.Diverse);
            }
           
        }

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
            if (!string.IsNullOrEmpty(title))
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

        public string GetRegisteredGenderForSalutation(string salutation)
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
        private static readonly Dictionary<string, GenderEnum> REGISTERED_SALUTATION_TO_GENDEREnum = new Dictionary<string, GenderEnum>();
        private static readonly Dictionary<string, Language> REGISTERED_SALUTATION_TO_Language = new Dictionary<string, Language>();

        private const string MALE_GENDER = "männlich";
        private const string FEMALE_GENDER = "weiblich";
        private const string DIVERSE_GENDER = "divers";
        private const string EMPTY_GENDER = "ohne";

        internal string GenerateLetterSalutation(string genderString, string salutation)
        {
            Language language = Language.German;
            GenderEnum gender = GenderEnum.None;
            if (REGISTERED_SALUTATION_TO_Language.ContainsKey(salutation))
            {
                language = REGISTERED_SALUTATION_TO_Language[salutation];
            }
            if(REGISTERED_SALUTATION_TO_GENDEREnum.ContainsKey(genderString))
            {
                gender = REGISTERED_SALUTATION_TO_GENDEREnum[genderString];
            }


            switch (gender)
            {
                case GenderEnum.Male:
                    switch (language)
                    {
                        case Language.German:
                            return "Sehr geehrter Herr";
                        case Language.English:
                            return "Dear Mr";
                        case Language.Italian:
                            return "Egregio Signor";
                        case Language.French:
                            return "Monsieur";
                        case Language.Spanish:
                            return "Estimado Señor";
                        default:
                            return "Sehr geehrter Herr";
                    }
                case GenderEnum.Female:
                    switch (language)
                    {
                        case Language.German:
                            return "Sehr geehrte Frau";
                        case Language.English:
                            return "Dear Ms";
                        case Language.Italian:
                            return "Gentile Signora";
                        case Language.French:
                            return "Madame";
                        case Language.Spanish:
                            return "Estimada Señora";
                        default:
                            return "Sehr geehrter Frau";
                    }
                case GenderEnum.Diverse:
                    switch (language)
                    {
                        case Language.German:
                            return "Hallo";
                        case Language.English:
                            return "Dear";
                        case Language.Italian:
                            return "Ciao";
                        case Language.French:
                            return "Bonjour";
                        case Language.Spanish:
                            return "Hola";
                        default:
                            return "Hallo";
                    }
                case GenderEnum.None:
                    switch (language)
                    {
                        case Language.German:
                            return "Sehr geehrte Damen und Herren";
                        case Language.English:
                            return "Dear Sirs";
                        case Language.Italian:
                           return "Egregi Signori";
                        case Language.French:
                            return "Messieursdames";
                        case Language.Spanish:
                            return "Estimados Señores y Señoras";
                        default:
                            return "Sehr geehrte Damen und Herren";
                    }
                default:
                    return "Hallo";
            }
        }



        public enum Language
        {
            German,
            English,
            Italian,
            French,
            Spanish
        }

       public enum GenderEnum
        {
            Male,
            Female,
            Diverse,
            None
        }
        private static readonly HashSet<string> REGISTERED_GENDERS = new HashSet<string> { "männlich", "weiblich", "divers", "ohne" };

        private static readonly HashSet<string> REGISTERED_SALUTATIONS = new HashSet<string> { "Herr", "Frau", "Mrs", "Mr", "Ms", "Signora", "Sig.", "Mme", "M", "Señora", "Señor" };

        private static readonly HashSet<string> REGISTERED_NOBLE_TITLES = new HashSet<string> { "Prinz", "Prinzessin", "Sir", "Dame", "Freiherrin", "Freiherr", "Baron", "Baronesse", "Ritter", "Graf", "Gräfin", "Fürst", "Fürstin", "Markgraf", "Pfalzgraf", "Landgraf", "Herzog", "Herzogin", "Kurfürst", "Großherzog", "Erzherzog", "König", "Königin" };

        private static readonly HashSet<string> REGISTERED_NOBLE_PRÄ_SUFFIXES = new HashSet<string> { "von", "vom", "van", "de", "zu", "zur" };

        private static readonly HashSet<string> REGISTERED_ACADEMIC_TITLES = new HashSet<string> { };
    }
}
