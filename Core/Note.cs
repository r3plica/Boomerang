using Core.Components;
using Core.Data;
using System;

namespace Core
{
    public class Note
    {

        #region Properties
        
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int CandidateId { get; set; }
        public NoteType TypeId { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }

        #endregion

        #region Constructors

        public Note()
        {
        }

        public Note(int id)
        {
            Note note = Notes.Get(id);
            Id = note.Id;
            ClientId = note.ClientId;
            CandidateId = note.CandidateId;

        }    

        #endregion

        #region Methods

        public void Save()
        {
            if (this.Id == 0)
                this.Id = Notes.Create(this);
            else
                Notes.Edit(this);
        }

        public void Delete()
        {
            if (this.Id > 0)
                Notes.Delete(this.Id);
        }

        public string DisplayHtmlMessage()
        {
            return HtmlExtensions.decodeEscapedText(Message).ToString();
        }

        #endregion

    }

    public enum NoteType
    {
        Unknown,
        Interview,
        History
    }
}
