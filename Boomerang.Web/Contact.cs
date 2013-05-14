using System;
using Boomerang.Web.Data;

namespace Boomerang.Web
{
    public class Contact
    {

        #region Properties

        public int Id { get; set; }
        public int ClientId { get; set; }

        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Office { get; set; }
        public string Telephone { get; set; }
        public string TelephoneAlt { get; set; }
        public string Email { get; set; }
        public string EmailAlt { get; set; }
        public string Fax { get; set; }
        public string JobTitle { get; set; }
        public string Bio { get; set; }
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
            this.Surname = c.Surname;
            this.Office = c.Office;
            this.Telephone = c.Telephone;
            this.TelephoneAlt = c.TelephoneAlt;
            this.Email = c.Email;
            this.EmailAlt = c.EmailAlt;
            this.Fax = c.Fax;
            this.JobTitle = c.JobTitle;
            this.Created = c.Created;
            this.CallbackDate = c.CallbackDate;
            this.Bio = c.Bio;
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

        public void Delete(int id)
        {
            Contacts.Delete(id);
        }

        #endregion
    }
}
