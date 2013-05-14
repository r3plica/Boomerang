using Boomerang.Web.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Boomerang.Web
{
    public class ClientResults
    {

        #region Properties

        private BasicSearchModel Basic { get; set; }
        private AdvancedClientSearchModel Advanced { get; set; }
        
        public Collection<Client> _Collection;
        public Collection<Client> Get
        {
            get
            {
                if (_Collection == null)
                    _Collection = Boomerang.Web.Data.Clients.Get();

                return _Collection;
            }
        }

        #endregion

        #region Constructors

        public ClientResults()
        {
        }

        public ClientResults(BasicSearchModel Search)
        {
            this.Basic = Search;
        }

        public ClientResults(AdvancedClientSearchModel Search)
        {
            this.Advanced = Search;
        }

        #endregion

        #region Public methods

        public IEnumerable<Client> Search()
        {
            IEnumerable<Client> filteredClients = Get.Where(c => c.Id > 0); // Our results collection
            Collection<Client> results = new Collection<Client>();

            // If we have a basic search
            if (Basic != null)
            {
                filteredClients = filteredClients.Where(
                    c => c.Name.ParseText(Basic.Query)
                        || c.PrimaryContact().Forename.ParseText(Basic.Query)
                        || c.PrimaryContact().Surname.ParseText(Basic.Query)
                        );

                // Select only the active clients
                if (Basic.ShowActive)
                    results.Append(filteredClients.Where(c => c.Active));

                // Select only the inactive clients
                if (Basic.ShowInactive)
                    results.Append(filteredClients.Where(c => !c.Active));
            }

            // If we have an advanced search
            if (Advanced != null)
            {
                filteredClients = GetClientsByName(filteredClients);
                filteredClients = GetClientsByAddress(filteredClients);
                filteredClients = GetClientsByContact(filteredClients);
                filteredClients = GetClientsBySector(filteredClients);

                results.Append(filteredClients);
            }

            return results.RemoveDuplicates(); // Remove any duplicates and return our result set
        }

        #endregion

        #region Private methods

        private IEnumerable<Client> GetClientsByName(IEnumerable<Client> clients)
        {
            if (string.IsNullOrEmpty(Advanced.SearchClient))
                return clients;

            return clients.Where(
                c => c.Name.ParseText(Advanced.SearchClient)
                    );
        }

        private IEnumerable<Client> GetClientsByAddress(IEnumerable<Client> clients)
        {
            if (string.IsNullOrEmpty(Advanced.SearchAddress))
                return clients;

            var temporaryCollection = (clients == null) ? Get : clients;
            return temporaryCollection.Where(
                c => c.Addresses().Any(
                    a => a.HouseNumber.ParseText(Advanced.SearchAddress)
                    || a.Street.ParseText(Advanced.SearchAddress)
                    || a.Area.ParseText(Advanced.SearchAddress)
                    || a.Town.ParseText(Advanced.SearchAddress)
                    || a.County.ParseText(Advanced.SearchAddress)
                    || a.PostCode.ParseText(Advanced.SearchAddress)
                    ));
        }

        private IEnumerable<Client> GetClientsBySector(IEnumerable<Client> clients)
        {
            if (string.IsNullOrEmpty(Advanced.SearchSector))
                return clients;

            var temporaryCollection = (clients == null) ? Get : clients;
            return temporaryCollection.Where(
                c => c.Sectors().Any(
                    a => a.Name.ParseText(Advanced.SearchSector)
                    ));
        }

        private IEnumerable<Client> GetClientsByContact(IEnumerable<Client> clients)
        {
            if (string.IsNullOrEmpty(Advanced.SearchContact))
                return clients;

            var temporaryCollection = (clients == null) ? Get : clients;
            return temporaryCollection.Where(
                c => c.Contacts().Any(
                    a => a.Forename.ParseText(Advanced.SearchContact)
                    || a.Surname.ParseText(Advanced.SearchContact)
                    ));
        }

        #endregion

    }
}
