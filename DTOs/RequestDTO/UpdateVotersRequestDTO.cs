using System;

namespace eVotingApp.DTOs.RequestDTO
{
    public class UpdateVotersRequestDTO
    {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string HouseAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
