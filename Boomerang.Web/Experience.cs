using Boomerang.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boomerang.Web
{
    public class Experience
    {

        #region Properties

        public int Id { get; set; }
        public int CandidateId { get; set; }
        public ExperienceType TypeId { get; set; }

        public string Name { get; set; }
        public string Type { get; set; }

        #endregion

        #region Methods

        public void Save()
        {
            if (this.Id == 0)
                this.Id = Experiences.Create(this);
            else
                Experiences.Edit(this);
        }

        #endregion

    }

    public enum ExperienceType
    {
        Unknown,
        Skill,
        Work
    }
}
