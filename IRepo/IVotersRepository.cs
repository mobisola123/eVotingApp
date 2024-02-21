using eVotingApp.DTOs.RequestDTO;
using eVotingApp.DTOs.ResponseDTO;
using eVotingApp.Generic;
using System;
using System.Threading.Tasks;

namespace eVotingApp.IRepo
{
    public interface IVotersRepository
    {
        public Task<Response<VotersRegistrationResponseDTO>> AddAsync(VotersRegistrationRequestDTO model);
        public Task<ResponseList<VotersRegistrationResponseDTO>> GetAllAsync();
        public Task<Response<VotersRegistrationResponseDTO>> GetByIdAsync(Guid id);
        public Task<Response<bool>> DeleteAsync(Guid id);
        public Task<Response<bool>> UpdateAsync( Guid id, UpdateVotersRequestDTO model);
    }
}
