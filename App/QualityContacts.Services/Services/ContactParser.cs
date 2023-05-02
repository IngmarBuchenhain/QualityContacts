using System.Text.RegularExpressions;
using QualityContacts.ServiceInterfaces.Models;
using QualityContacts.ServiceInterfaces.Services;
using QualityContacts.Services.Models;

namespace QualityContacts.Services
{
    /// <summary>
    /// <inheritdoc cref="IContactParser"/><br/>
    /// Implementation of <see cref="IContactParser"/>.
    /// </summary>
    public class ContactParser : IContactParser
    {

        #region Public Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="input"></param>
        /// <returns><inheritdoc/></returns>
        public IContact ParseContactInput(string input)
        {


            if (string.IsNullOrEmpty(input)) return new Contact();

            var contactParts = SplitStringToPossibleContactParts(input, ' ');

            var numberContactParts = contactParts.Count();
            var result = new Contact();
            switch (numberContactParts) {
                case 1:
                    
                    result.LastName = contactParts[0];
                    return result;
                case 2:
                    result.FirstName = contactParts[0];
                    result.LastName = contactParts[1];
                    return result;
                case 3:
                    result.Salutation = contactParts[0];
                    result.FirstName = contactParts[1];
                    result.LastName = contactParts[2];

                    return result;
                case 4:
                    break;
                default:
                    break;
            }


            return new Contact();
        }

        #endregion Public Methods

        #region Private Methods

        

        private string[] SplitStringToPossibleContactParts(string input, char separator)
        {
            var parts = input.Split(separator);

            

            return parts;
        }



        #endregion Private Methods



       
    }
}
