using Core.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Core
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
                    _Collection = Core.Data.Candidates.Get();

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
            Collection<Candidate> Results = new Collection<Candidate>(); // Our results collection
            Collection<Candidate> Temporary = new Collection<Candidate>(); // Temporary collection to hold potential results

            // If we have a basic search
            if (Basic != null)
            {
                Temporary.Append(Get.Where(
                    c => c.Forename.ParseText(Basic.Query)
                        || c.Surname.ParseText(Basic.Query)
                        || c.Telephone.ParseText(Basic.Query)
                        || c.Mobile.ParseText(Basic.Query)
                        || c.Email.ParseText(Basic.Query)
                        ));

                // Search work/skills
                foreach (Candidate Candidate in Get)
                {
                    var exp = Candidate.Experience().Where(
                        e => e.Name.ParseText(Basic.Query));

                    if (exp != null)
                        Temporary.Add(Candidate);
                }

                // Select only the active clients
                if (Basic.ShowActive)
                    Results.Append(Temporary.Where(c => c.Active));

                // Select only the inactive clients
                if (Basic.ShowInactive)
                    Results.Append(Temporary.Where(c => !c.Active));
            }

            if (Advanced != null)
            {
                // Search the candidate
                Temporary.Append(Get.Where(
                    c => c.Forename.ParseText(Advanced.SearchCandidate)
                        || c.Surname.ParseText(Advanced.SearchCandidate)
                        || c.Telephone.ParseText(Advanced.SearchCandidate)
                        || c.Mobile.ParseText(Advanced.SearchCandidate)
                        || c.Email.ParseText(Advanced.SearchCandidate)));

                // Search work/skills
                foreach (Candidate Candidate in Get)
                {
                    var exp = Candidate.Experience().Where(
                        e => e.Name.ParseText(Advanced.SearchExperience));

                    if (exp != null)
                        Temporary.Add(Candidate);
                }

                // Search history/notes
                foreach (Candidate Candidate in Get)
                {
                    var notes = Candidate.Notes().Where(
                        n => n.Message.ParseText(Advanced.SearchNotes));

                    if (notes != null)
                        Temporary.Add(Candidate);
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
