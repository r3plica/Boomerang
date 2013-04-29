using Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Document
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int CandidateId { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public string FilePath { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }

        public Document()
        {
        }

        public Document(int Id)
        {
            Document d = Documents.Get(Id);
            this.Id = d.Id;
            this.ClientId = d.ClientId;
            this.CandidateId = d.CandidateId;
            this.FileName = d.FileName;
            this.FileSize = d.FileSize;
            this.FilePath = d.FilePath;
            this.UserId = d.UserId;
            this.UserName = d.UserName;
            this.Date = d.Date;
        }

        public void Save()
        {
            if (this.Id == 0)
                this.Id = Documents.Create(this);
            else
                Documents.Edit(this);
        }

        public void Delete()
        {
            Documents.Delete(this.Id);
        }
    }
}
