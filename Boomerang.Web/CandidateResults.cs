using Boomerang.Web.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Boomerang.Web
{
    public class CandidateResults
    {

        #region Properties

        private BasicSearchModel Basic { get; set; }
        private AdvancedCandidateSearchModel Advanced { get; set; }

        public Collection<Candidate> _Collection;
        public Collection<Candidate> Get
        {
            get
            {
                if (_Collection == null)
                    _Collection = Boomerang.Web.Data.Candidates.Get();

                return _Collection;
            }
        }

        #endregion

        #region Constructors

        public CandidateResults()
        {
        }

        public CandidateResults(BasicSearchModel Search)
        {
            this.Basic = Search;
        }

        public CandidateResults(AdvancedCandidateSearchModel Search)
        {
            this.Advanced = Search;
        }

        #endregion

        #region Public methods

        public Collection<Candidate> Search()
        {
            IEnumerable<Candidate> filteredCandidates = Get.Where(c => c.Id > 0); // Our results collection
            Collection<Candidate> results = new Collection<Candidate>();

            // If we have a basic search
            if (Basic != null)
            {
                filteredCandidates = filteredCandidates.Where(
                    c => c.Forename.ParseText(Basic.Query)
                        || c.Surname.ParseText(Basic.Query)
                        );

                // Select only the active clients
                if (Basic.ShowActive)
                    results.Append(filteredCandidates.Where(c => c.Active));

                // Select only the inactive clients
                if (Basic.ShowInactive)
                    results.Append(filteredCandidates.Where(c => !c.Active));
            }

            if (Advanced != null)
            {
                filteredCandidates = GetCandidatesByName(filteredCandidates);
                filteredCandidates = GetCandidatesByAddress(filteredCandidates);
                filteredCandidates = GetCandidatesByExperience(filteredCandidates, ExperienceType.Skill);
                filteredCandidates = GetCandidatesByExperience(filteredCandidates, ExperienceType.Work);
                filteredCandidates = GetCandidatesBySalary(filteredCandidates);
                filteredCandidates = GetFiltered(filteredCandidates);

                results.Append(filteredCandidates);
            }

            return results.RemoveDuplicates(); // Remove any duplicates and return our result set
        }     

        #endregion

        #region Private methods

        private IEnumerable<Candidate> GetCandidatesByName(IEnumerable<Candidate> candidates)
        {
            if (string.IsNullOrEmpty(Advanced.SearchCandidate))
                return candidates;

            return candidates.Where(
                c => c.Forename.ParseText(Advanced.SearchCandidate)
                    || c.Surname.ParseText(Advanced.SearchCandidate)
                    );
        }

        private IEnumerable<Candidate> GetCandidatesByAddress(IEnumerable<Candidate> candidates)
        {
            if (string.IsNullOrEmpty(Advanced.SearchAddress))
                return candidates;

            var temporaryCollection = (candidates == null) ? Get : candidates;
            return temporaryCollection.Where(
                c => c.Address().HouseNumber.ParseText(Advanced.SearchAddress)
                    || c.Address().Street.ParseText(Advanced.SearchAddress)
                    || c.Address().Area.ParseText(Advanced.SearchAddress)
                    || c.Address().Town.ParseText(Advanced.SearchAddress)
                    || c.Address().County.ParseText(Advanced.SearchAddress)
                    || c.Address().PostCode.ParseText(Advanced.SearchAddress)
                    );
        }

        private IEnumerable<Candidate> GetCandidatesByExperience(IEnumerable<Candidate> candidates, ExperienceType type)
        {
            var searchQuery = (type == ExperienceType.Work) ? Advanced.SearchWork : Advanced.SearchSkills;

            if (string.IsNullOrEmpty(searchQuery))
                return candidates;

            var temporaryCollection = (candidates == null) ? Get : candidates;
            return temporaryCollection.Where(
                c => c.Experience().Any(
                    e => e.TypeId == type
                        && e.Name.ParseText(searchQuery)
                        ));
        }

        private IEnumerable<Candidate> GetCandidatesBySalary(IEnumerable<Candidate> candidates)
        {
            var temporaryCollection = (candidates == null) ? Get : candidates;

            if (Advanced.FullTime)
                temporaryCollection = temporaryCollection.Where(c => c.SalaryDetails().FullTime);
            if (Advanced.PartTime)
                temporaryCollection = temporaryCollection.Where(c => c.SalaryDetails().PartTime);
            if (Advanced.Student)
                temporaryCollection = temporaryCollection.Where(c => c.SalaryDetails().Student);
            if (Advanced.Temp)
                temporaryCollection = temporaryCollection.Where(c => c.SalaryDetails().Temp);
            if (Advanced.NightShifts)
                temporaryCollection = temporaryCollection.Where(c => c.SalaryDetails().NightShifts);
            if (Advanced.DayShifts)
                temporaryCollection = temporaryCollection.Where(c => c.SalaryDetails().DayShifts);
            if (Advanced.Other)
                temporaryCollection = temporaryCollection.Where(c => c.SalaryDetails().Other);

            return temporaryCollection;
        }

        private IEnumerable<Candidate> GetFiltered(IEnumerable<Candidate> candidates)
        {
            if (candidates == null)
                return null;

            if (Advanced.ShowCrb && Advanced.HideCrb || !Advanced.ShowCrb && !Advanced.HideCrb)
            {
                // Do nothing
            }
            else if (Advanced.ShowCrb)
                candidates = candidates.Where(c => c.CRB);
            else if (Advanced.HideCrb)
                candidates = candidates.Where(c => !c.CRB);

            if (Advanced.Interviewed && Advanced.NotInterviewed || !Advanced.Interviewed && !Advanced.NotInterviewed)
            {
                // Do nothing
            }
            else if (Advanced.Interviewed)
                candidates = candidates.Where(c => c.Interviewed);
            else if (Advanced.NotInterviewed)
                candidates = candidates.Where(c => !c.Interviewed);

            if (Advanced.References && Advanced.NoReferences || !Advanced.References && !Advanced.NoReferences)
            {
                // Do nothing
            }
            else if (Advanced.References)
                candidates = candidates.Where(c => c.References);
            else if (Advanced.NoReferences)
                candidates = candidates.Where(c => !c.References);

            if (Advanced.ShowActive && Advanced.ShowInactive || !Advanced.ShowActive && !Advanced.ShowInactive)
            {
                // Do nothing
            }
            else if (Advanced.ShowActive)
                candidates = candidates.Where(c => c.Active);
            else if (Advanced.ShowInactive)
                candidates = candidates.Where(c => !c.Active);

            return candidates;
        }

        #endregion

    }
}
