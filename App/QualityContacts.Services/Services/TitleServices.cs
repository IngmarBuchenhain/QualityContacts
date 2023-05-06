using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityContacts.Services
{
    internal class TitleServices
    {
        public TitleServices()
        {
            InitializeTitles();
            _registeredAcademicTitles = new ContactRepository().GetRegisteredAcademicTitles();
        }

        private IEnumerable<string> _registeredNobleTitles;

        private IEnumerable<string> _registeredNoblePreSuffixes;

        private IEnumerable<string> _registeredAcademicTitles;

        public string ConformsToRegisteredAcademicTitles(string titleCandidates)
        {
            var words = titleCandidates.Split(' ').Where(word => !string.IsNullOrEmpty(word)).ToArray();

            string possibleNewTitles = string.Empty;
            foreach (var word in words)
            {
                if (!ConformsToRegisteredTitles(word))
                {
                    if (possibleNewTitles.Length > 0)
                    {
                        possibleNewTitles += " " + word;
                    }
                    else
                    {
                        possibleNewTitles += word;
                    }

                }
            }

            return possibleNewTitles;
        }

        private bool ConformsToRegisteredTitles(string titleCandidate)
        {
            foreach (var title in _registeredAcademicTitles)
            {
                if (title.Equals(titleCandidate))
                {
                    return true;
                }
            }
            return false;
        }

        internal IEnumerable<string> ExtractNobleTitles(string[] contactPartsToSearch)
        {
            List<string> extractedTitles = new List<string>();
            foreach (string contactPart in contactPartsToSearch)
            {

                var lowerContactPart = contactPart.ToLower();
                foreach (string registeredNobleTitle in _registeredNobleTitles)
                {
                    if (lowerContactPart.Equals(registeredNobleTitle.ToLower()))
                    {
                        extractedTitles.Add(contactPart);


                    }
                }

            }

            return extractedTitles;
        }

        internal IEnumerable<string> ExtractNoblePreSuffixes(string[] contactPartsToSearch)
        {
            List<string> extractedTitles = new List<string>();
            foreach (string contactPart in contactPartsToSearch)
            {

                var lowerContactPart = contactPart.ToLower();
                foreach (string registeredNobleTitle in _registeredNoblePreSuffixes)
                {
                    if (lowerContactPart.Equals(registeredNobleTitle.ToLower()))
                    {
                        extractedTitles.Add(contactPart);
                        // Add everything afterwards.


                    }
                }

            }

            return extractedTitles;
        }

        internal IEnumerable<string> ExtractAcademicTitles(string[] contactPartsToSearch)
        {
            List<string> extractedTitles = new List<string>();
            foreach (string contactPart in contactPartsToSearch)
            {

                var lowerContactPart = contactPart.ToLower();
                foreach (string registeredNobleTitle in _registeredAcademicTitles)
                {
                    if (lowerContactPart.Equals(registeredNobleTitle.ToLower()))
                    {
                        extractedTitles.Add(contactPart);


                    }
                }

            }

            return extractedTitles;
        }

        public HashSet<string> DefaultAcademicTitles { get; set; } = new HashSet<string>();

        public HashSet<string> DefaultNobleTitles { get; set; } = new HashSet<string>();

        public Dictionary<string, string> SalutationToGender { get; set; } = new Dictionary<string, string>();

        private void InitializeTitles()
        {
            // Default academic titles
            DefaultAcademicTitles.Add("Dr.");
            DefaultAcademicTitles.Add("Professor");
            DefaultAcademicTitles.Add("Professorin");
            DefaultAcademicTitles.Add("Prof.");
            DefaultAcademicTitles.Add("Dr.-Ing.");
            DefaultAcademicTitles.Add("h.c.");
            DefaultAcademicTitles.Add("mult.");
            DefaultAcademicTitles.Add("rer.");
            DefaultAcademicTitles.Add("nat.");
            DefaultAcademicTitles.Add("Dipl.");
            DefaultAcademicTitles.Add("Ing.");
            DefaultAcademicTitles.Add("B.S.");
            DefaultAcademicTitles.Add("M.S.");


            // Default noble titles
            DefaultNobleTitles.Add("");


        }
    }
}
