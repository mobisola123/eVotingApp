using AutoMapper;
using eVotingApp.DTOs.ResponseDTO;
using eVotingApp.Entities;

namespace eVotingApp.AutoMapperProfiles
{
    public class Profiles: Profile
    {
        public Profiles()
        {
            CreateMap<Party, PartyRegistrationResponseDTO>().ReverseMap();
            CreateMap<Voters, VotersRegistrationResponseDTO>().ReverseMap();
        }
       
    }
}
