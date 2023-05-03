using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityContacts.Services.Models
{
    internal class Titles
    {
        public Titles()
        {
            InitializeTitles();
        }

        private IEnumerable<string> _registeredNobleTitles;

        private IEnumerable<string> _registeredNoblePreSuffixes;

        private IEnumerable<string> _registeredAcademicTitles;

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
