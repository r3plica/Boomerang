using System.ComponentModel.DataAnnotations;
namespace Core
{
    public enum QueryType
    {
        Client
        , Candidate
    }

    public class BasicSearchModel
    {
        public string Query { get; set; }
        public QueryType Type { get; set; }
        [Display(Name = "Show active")] public bool ShowActive { get; set; }
        [Display(Name = "Show inactive")] public bool ShowInactive { get; set; }
    }

    public class AdvancedClientSearchModel
    {
        [Display(Name = "Search clients")] public string SearchClient { get; set; }
        [Display(Name = "Search addresses")] public string SearchAddress { get; set; }
        [Display(Name = "Search contacts")] public string SearchContact { get; set; }
        [Display(Name = "Search history")] public string SearchHistory { get; set; }
        [Display(Name = "Search assigned")] public string SearchAssigned { get; set; }

        [Display(Name = "Search active")] public bool ShowActive { get; set; }
        [Display(Name = "Search inactive")] public bool ShowInactive { get; set; }
        [Display(Name = "Search delivery")] public bool ShowDelivery { get; set; }
        [Display(Name = "Search invoice")] public bool ShowInvoice { get; set; }
        [Display(Name = "Search only primary contact")] public bool ShowPrimary { get; set; }
    }

    public class AdvancedCandidateSearchModel
    {
        [Display(Name = "Search candidates")] public string SearchCandidate { get; set; }
        [Display(Name = "Search addresses")] public string SearchAddress { get; set; }
        [Display(Name = "Search work/skills")] public string SearchExperience { get; set; }
        [Display(Name = "Search history/notes")] public string SearchNotes { get; set; }
        [Display(Name = "Search assigned")] public string SearchAssigned { get; set; }

        [Display(Name = "Search active")] public bool ShowActive { get; set; }
        [Display(Name = "Search inactive")] public bool ShowInactive { get; set; }
    }
}