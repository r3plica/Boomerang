using System;
using Core.Data;

namespace Core
{
    public class Contact
    {

        #region Properties

        public int Id { get; set; }
        public int ClientId { get; set; }

        public string Forename { get; set; }
        public string Middlenames { get; set; }
        public string Surname { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public DateTime Created { get; set; }
        public string CallbackDate { get; set; }
        public bool Primary { get; set; }

        #endregion

        #region Constructors

        public Contact()
        {
        }

        public Contact(int Id)
        {
            Contact c = Contacts.Get(Id);
            this.Id = c.Id;
            this.ClientId = c.ClientId;

            this.Forename = c.Forename;
            this.Middlenames = c.Middlenames;
            this.Surname = c.Surname;
            this.Telephone = c.Telephone;
            this.Mobile = c.Mobile;
            this.Fax = c.Fax;
            this.Email = c.Email;
            this.JobTitle = c.JobTitle;
            this.Created = c.Created;
            this.CallbackDate = c.CallbackDate;
            this.Primary = c.Primary;
        }

        #endregion

        #region Public methods

        public void Save()
        {
            if (this.Id > 0)
                Contacts.Edit(this);
            else
                this.Id = Contacts.Create(this);
        }

        #endregion
    }
}
