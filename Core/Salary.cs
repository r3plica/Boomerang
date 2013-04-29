using Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Salary
    {

        #region Fields

        private string _tempRate;
        private string _permRate;

        #endregion

        #region Properties

        public int Id { get; set; }
        public int CandidateId { get; set; }

        public bool FullTime { get; set; }
        public bool PartTime { get; set; }
        public bool Student { get; set; }
        public bool Temp { get; set; }
        public bool NightShifts { get; set; }
        public bool DayShifts { get; set; }
        public bool Other { get; set; }

        public int TempWage { get; set; }
        public int TempRateId { get; set; }
        public string TempRate { get { return _tempRate; } }
        public int PermWage { get; set; }
        public int PermRateId { get; set; }
        public string PermRate { get { return _permRate; } }

        #endregion

        #region Methods

        public void SetTempRate(string value)
        {
            _tempRate = value;
        }

        public void SetPermRate(string value)
        {
            _permRate = value;
        }

        public void Save()
        {
            if (this.Id == 0)
                this.Id = Salaries.Create(this);
            else
                Salaries.Edit(this);
        }

        #endregion

    }
}
