using Core.Data;
using System.Collections.ObjectModel;
using System.Web;

namespace Core.Components
{
    public class CandidateManager
    {
        public static Collection<Candidate> CurrentCandidates()
        {
            return Candidates.Get();
        }
    }
}
