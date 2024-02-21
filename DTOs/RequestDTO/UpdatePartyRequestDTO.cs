using System;
using System.ComponentModel.DataAnnotations;

namespace eVotingApp.DTOs.RequestDTO
{
    public class UpdatePartyRequestDTO
    {
        [Required]
        public string PartyName { get; set; }
        [Required]
        public string LeaderName { get; set; }
        [Required]
        public string WebsiteName { get; set; }
        [Required]
        public int FoundationYear { get; set; }
    }
}
