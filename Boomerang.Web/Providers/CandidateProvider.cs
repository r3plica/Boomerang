using Boomerang.Web.Data;
using System.Collections.ObjectModel;
using System.Web;

namespace Boomerang.Web.Providers
{
    public class CandidateProvider
    {
        public static Collection<Candidate> CurrentCandidates()
        {
            return Candidates.Get();
        }
    }
}
