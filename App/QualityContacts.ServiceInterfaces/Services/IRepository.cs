using QualityContacts.ServiceInterfaces.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityContacts.ServiceInterfaces.Services
{
    public interface IRepository
    {
        IContact GetNewContact();
        HashSet<string> GetTitles();

        void SaveNewTitle(string title);

        ObservableCollection<IContact> GetContacts();
        void SaveNewContact(IContact contact);
    }
}
