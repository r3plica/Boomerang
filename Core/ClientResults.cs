using Core.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Core
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
                    _Collection = Core.Data.Clients.Get();

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

        public Collection<Client> Search()
        {
            Collection<Client> Results = new Collection<Client>(); // Our results collection
            Collection<Client> Temporary = new Collection<Client>(); // Temporary collection to hold potential results

            // If we have a basic search
            if (Basic != null)
            {
                Temporary.Append(Get.Where(
                    c => c.Name.ParseText(Basic.Query)
                        || c.PrimaryContact().Forename.ParseText(Basic.Query)
                        || c.PrimaryContact().Middlenames.ParseText(Basic.Query)
                        || c.PrimaryContact().Surname.ParseText(Basic.Query)
                        || c.PrimaryContact().Email.ParseText(Basic.Query)
                        || c.PrimaryContact().Telephone.ParseText(Basic.Query)
                        || c.PrimaryContact().Mobile.ParseText(Basic.Query)
                        || c.PrimaryContact().Fax.ParseText(Basic.Query)
                        ));

                foreach (Client Client in Get)
                {
                    var sectors = Client.Sectors().Where(
                        c => c.Name.ParseText(Basic.Query));

                    if (sectors.Count() > 0)
                        Temporary.Add(Client);
                }

                // Select only the active clients
                if (Basic.ShowActive)
                    Results.Append(Temporary.Where(c => c.Active));

                // Select only the inactive clients
                if (Basic.ShowInactive)
                    Results.Append(Temporary.Where(c => !c.Active));
            }

            // If we have an advanced search
            if (Advanced != null)
            {
                Temporary.Compare(Get.Where(c => c.Name.ParseText(Advanced.SearchClient))); // Search Client
                
                // Search the primary contact
                if (Advanced.ShowPrimary)
                    Temporary.Compare(Get.Where(
                        c => c.PrimaryContact().Forename.ParseText(Advanced.SearchContact)
                            || c.PrimaryContact().Middlenames.ParseText(Advanced.SearchContact)
                            || c.PrimaryContact().Surname.ParseText(Advanced.SearchContact)
                            || c.PrimaryContact().Telephone.ParseText(Advanced.SearchContact)
                            || c.PrimaryContact().Mobile.ParseText(Advanced.SearchContact)
                            || c.PrimaryContact().Fax.ParseText(Advanced.SearchContact)
                            || c.PrimaryContact().Email.ParseText(Advanced.SearchContact)
                            || c.PrimaryContact().JobTitle.ParseText(Advanced.SearchContact)
                            || c.PrimaryContact().Created.ToString().ParseText(Advanced.SearchContact)
                            || c.PrimaryContact().CallbackDate.ParseText(Advanced.SearchContact)));
                else
                {
                    //// Else, search all contacts
                    //foreach (Client Client in Get)
                    //{
                    //    var contacts = Client.Contacts().Where(
                    //        c => c.Forename.ParseText(Advanced.SearchContact)
                    //            || c.Middlenames.ParseText(Advanced.SearchContact)
                    //            || c.Surname.ParseText(Advanced.SearchContact)
                    //            || c.Telephone.ParseText(Advanced.SearchContact)
                    //            || c.Mobile.ParseText(Advanced.SearchContact)
                    //            || c.Fax.ParseText(Advanced.SearchContact)
                    //            || c.Email.ParseText(Advanced.SearchContact)
                    //            || c.JobTitle.ParseText(Advanced.SearchContact)
                    //            || c.Created.ToString().ParseText(Advanced.SearchContact)
                    //            || c.CallbackDate.ParseText(Advanced.SearchContact));

                    //    if (contacts != null)
                    //        Temporary.Add(Client);
                    //}
                }

                // Select only the active clients
                if (Advanced.ShowActive)
                    Results.Append(Temporary.Where(c => c.Active));

                // Select only the inactive clients
                if (Advanced.ShowInactive)
                    Results.Append(Temporary.Where(c => !c.Active));
            }

            return Results.RemoveDuplicates(); // Remove any duplicates and return our result set
        }

        #endregion

    }
}
