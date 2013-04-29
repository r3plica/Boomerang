using System;
using Core.Data;
using System.Web;
using System.Collections.ObjectModel;
using Core.Components;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class Candidate
    {

        #region Fields

        private Collection<Address> _addresses;
        private Salary _salary;
        private Collection<Experience> _experience;
        private Collection<Note> _notes;
        private Collection<Document> _documents;

        #endregion

        #region Properties

        public int Id { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string DOB { get; set; }
        public bool Active { get; set; }
        public string LastContactDate { get; set; }
        public string NiNumber { get; set; }
        public bool CRB { get; set; }
        public bool References { get; set; }
        public bool Interviewed { get; set; }
        public DateTime DateCreated { get; set; }
        public int TransportId { get; set; }
        public string TransportType { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }

        #endregion

        #region Constructors

        public Candidate()
        {
        }

        public Candidate(int Id)
        {
            Candidate c = Candidates.Get(Id);

            this.Id = c.Id;
            this.Forename = c.Forename;
            this.Surname = c.Surname;
            this.Telephone = c.Telephone;
            this.Mobile = c.Mobile;
            this.Email = c.Email;
            this.Active = c.Active;
            this.DOB = c.DOB;
            this.LastContactDate = c.LastContactDate;
            this.NiNumber = c.NiNumber;
            this.CRB = c.CRB;
            this.References = c.References;
            this.Interviewed = c.Interviewed;
            this.DateCreated = c.DateCreated;
            this.TransportId = c.TransportId;
            this.TransportType = c.TransportType;
            this.UserId = c.UserId;
            this.UserName = c.UserName;
        }

        #endregion

        #region Public methods

        public Address Address()
        {
            if (_addresses == null)
                if (this.Id > 0)
                    _addresses = Addresses.Get(this.Id, 0);

            return _addresses.FirstOrDefault();
        }

        public Salary SalaryDetails()
        {
            if (_salary == null)
                if (this.Id > 0)
                    _salary = Salaries.Get(this.Id);

            return _salary;
        }

        public Collection<Experience> Experience()
        {
            if (_experience == null)
                if (this.Id > 0)
                    _experience = Experiences.Get(this.Id);

            return _experience;
        }

        public Collection<Note> Notes()
        {
            if (_notes == null)
                if (this.Id > 0)
                    _notes = Core.Data.Notes.Get(this.Id, 0);

            return _notes;
        }

        public Collection<Document> Documents()
        {
            if (_documents == null)
                if (this.Id > 0)
                    _documents = Data.Documents.Get(this.Id, 0);

            return _documents;
        }

        public void Save()
        {
            if (this.Id > 0)
            {
                Candidates.Edit(this);
                this.ReplaceInSession();
            }
            else
            {
                this.Id = Candidates.Create(this);
                this.AddToSession<Candidate>("Candidates");
            }
        }

        public void Delete()
        {
            Candidates.Delete(this.Id);
            this.RemoveFromSession();
        }

        #endregion

        #region Private methods

        private void RemoveFromSession()
        {
            if (HttpContext.Current.Session["Candidates"] != null)
            {
                Collection<Candidate> UpdatedCandidates = new Collection<Candidate>();
                Collection<Candidate> SessionCandidates = (Collection<Candidate>)HttpContext.Current.Session["Candidates"];
                foreach (Candidate Candidate in SessionCandidates)
                {
                    if (Candidate.Id != this.Id)
                        UpdatedCandidates.Add(Candidate);
                }
                HttpContext.Current.Session.Add("Candidates", UpdatedCandidates);
            }
        }

        private void ReplaceInSession()
        {
            if (HttpContext.Current.Session["Candidates"] != null)
            {
                this.RemoveFromSession();
                this.AddToSession<Candidate>("Candidates");
            }
        }

        #endregion

    }
}
