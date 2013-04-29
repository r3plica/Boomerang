using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Web;
using Core.Components;
using Core.Data;
using System.Text;
using System.Collections.Generic;

namespace Core
{
    public class Client
    {

        #region Fields

        private Collection<Address> _addresses;
        private Collection<Sector> _sectors;
        private Collection<Contact> _contacts;
        private Collection<Note> _history;
        private Collection<Document> _documents;

        #endregion

        #region Properties

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public bool Active { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }

        #endregion

        #region Constructors

        public Client()
        {
        }

        public Client(int Id)
        {
            Client c = Core.Data.Clients.Get(Id);
            this.Id = c.Id;
            this.Name = c.Name;
            this.UserId = c.UserId;
            this.UserName = c.UserName;
        }

        #endregion

        #region Public methods

        public Collection<Address> Addresses()
        {
            if (_addresses == null)
                if (this.Id > 0)
                    _addresses = Core.Data.Addresses.Get(0, this.Id);

            return _addresses;
        }

        public Collection<Contact> Contacts()
        {
            if (_contacts == null)
                if (this.Id > 0)
                    _contacts = Core.Data.Contacts.GetClient(this.Id);

            return _contacts;
        }

        public Contact PrimaryContact()
        {
            if (_contacts == null)
                if (this.Id > 0)
                    _contacts = Core.Data.Contacts.GetClient(this.Id);

            return _contacts.Where(c => c.Primary == true)
                             .DefaultIfEmpty(new Contact())
                             .Single();
        }

        public Collection<Note> History()
        {
            if (_history == null)
                if (this.Id > 0)
                    _history = Notes.Get(0, this.Id);

            return _history;
        }

        public Collection<Document> Documents()
        {
            if (_documents == null)
                if (this.Id > 0)
                    _documents = Data.Documents.Get(0, this.Id);

            return _documents;
        }

        public Collection<Sector> Sectors()
        {
            if (_sectors == null)
                if (this.Id > 0)
                    _sectors = Data.Sectors.Get(this.Id);

            return _sectors;
        }

        public void Save()
        {
            if (this.Id > 0)
            {
                Core.Data.Clients.Edit(this);
                this.ReplaceInSession();
            }
            else
            {
                this.Id = Core.Data.Clients.Create(this);
                //this.AddToSession();
                this.AddToSession<Client>("Clients");
            }
        }

        public void Delete()
        {
            Core.Data.Clients.Delete(this.Id);
            this.RemoveFromSession();
        }

        #region Html building

        public string HtmlAddresses()
        {
            return AddressManager.DisplayAddresses(this.Addresses());
        }

        #endregion

        #endregion

        #region Private methods

        //private void AddToSession()
        //{
        //    if (HttpContext.Current.Session["Clients"] != null)
        //    {
        //        Collection<Client> SessionClients = (Collection<Client>)HttpContext.Current.Session["Clients"];
        //        SessionClients.Add(this);
        //        HttpContext.Current.Session.Add("Clients", SessionClients);
        //    }
        //}

        private void RemoveFromSession()
        {
            if (HttpContext.Current.Session["Clients"] != null)
            {
                Collection<Client> UpdatedClients = new Collection<Client>();
                Collection<Client> SessionClients = (Collection<Client>)HttpContext.Current.Session["Clients"];
                foreach (Client Client in SessionClients)
                {
                    if (Client.Id != this.Id)
                        UpdatedClients.Add(Client);
                }
                HttpContext.Current.Session.Add("Clients", UpdatedClients);
            }
        }

        private void ReplaceInSession()
        {
            if (HttpContext.Current.Session["Clients"] != null)
            {
                this.RemoveFromSession();
                //this.AddToSession();
                this.AddToSession<Client>("Clients");
            }
        }

        #endregion

    }
}
