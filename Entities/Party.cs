using System;

namespace eVotingApp.Entities
{
    public class Party
    {
        public Guid Id { get; set; }
        public  string  PartyName { get; set; }
        public string CapitalizedPartyName { get; set; }
        public string LeaderName { get; set; }
        public string WebsiteName { get; set; }
        public int FoundationYear { get; set; }

    }
}
