using System;

namespace eVotingApp.DTOs.ResponseDTO
{
    public class PartyRegistrationResponseDTO
    {
        public Guid Id { get; set; }
        public string PartyName { get; set; }
        public string LeaderName { get; set; }
        public string WebsiteName { get; set; }
        public int FoundationYear { get; set; }
    }
}
