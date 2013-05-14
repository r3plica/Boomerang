using System.ComponentModel.DataAnnotations;
namespace Boomerang.Web
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
        [Display(Name = "Active")] public bool ShowActive { get; set; }
        [Display(Name = "Inactive")] public bool ShowInactive { get; set; }
    }

    public class AdvancedClientSearchModel
    {
        [Display(Name = "Search clients")] public string SearchClient { get; set; }
        [Display(Name = "Search addresses")] public string SearchAddress { get; set; }
        [Display(Name = "Search sectors")] public string SearchSector { get; set; }
        [Display(Name = "Search contacts")] public string SearchContact { get; set; }

        [Display(Name = "Show active")] public bool ShowActive { get; set; }
        [Display(Name = "Show inactive")] public bool ShowInactive { get; set; }
    }

    public class AdvancedCandidateSearchModel
    {
        [Display(Name = "Search candidates")] public string SearchCandidate { get; set; }
        [Display(Name = "Search addresses")] public string SearchAddress { get; set; }
        [Display(Name = "Search skills")] public string SearchSkills { get; set; }
        [Display(Name = "Search work")] public string SearchWork { get; set; }
        
        [Display(Name = "Active")] public bool ShowActive { get; set; }
        [Display(Name = "Inactive")] public bool ShowInactive { get; set; }
        [Display(Name = "CRB checks")] public bool ShowCrb { get; set; }
        [Display(Name = "No CRB checks")] public bool HideCrb { get; set; }
        [Display(Name = "Interviewed")] public bool Interviewed { get; set; }
        [Display(Name = "Not interviewed")] public bool NotInterviewed { get; set; }
        [Display(Name = "References")] public bool References { get; set; }
        [Display(Name = "No references")] public bool NoReferences { get; set; }

        [Display(Name = "Full time")] public bool FullTime { get; set; }
        [Display(Name = "Part time")] public bool PartTime { get; set; }
        public bool Student { get; set; }
        public bool Temp { get; set; }
        [Display(Name = "Night shifts")] public bool NightShifts { get; set; }
        [Display(Name = "Day shifts")] public bool DayShifts { get; set; }
        public bool Other { get; set; }
    }
}