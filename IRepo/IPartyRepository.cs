using eVotingApp.DTOs.RequestDTO;
using eVotingApp.DTOs.ResponseDTO;
using eVotingApp.Generic;
using System;
using System.Threading.Tasks;

namespace eVotingApp.IRepo
{
    public interface IPartyRepository
    {
        public Task<Response<PartyRegistrationResponseDTO>> AddAsync(PartyRegistrationRequestDTO model);
        public Task<ResponseList<PartyRegistrationResponseDTO>> GetAllAsync();
        public Task<Response<PartyRegistrationResponseDTO>> GetByIdAsync(Guid id);
        public Task<Response<bool>> UpdateAsync(Guid id, UpdatePartyRequestDTO model);
        public Task<Response<bool>> DeleteByIdAsync(Guid id);
    }
}
