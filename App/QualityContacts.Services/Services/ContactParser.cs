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


            var contactParts = SplitStringToPossibleContactParts(input, ' ');


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
