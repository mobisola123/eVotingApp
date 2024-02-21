using eVotingApp.DTOs.RequestDTO;
using eVotingApp.IRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace eVotingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotersController : ControllerBase
    {
        private readonly IVotersRepository _votersRepository;
        public VotersController(IVotersRepository votersRepository)
        {
            _votersRepository = votersRepository;
        }

        [HttpPost]
        [Route("registerVoter")]
        public async Task<ActionResult> AddAsync(VotersRegistrationRequestDTO model)
        {
            var response = await _votersRepository.AddAsync(model);

            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("getAllVoters")]
        public async Task<ActionResult> GetAllAsync()
        {
            var response = await _votersRepository.GetAllAsync();
            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("getById")]
        public async Task<ActionResult> GetAsync(Guid id)
        {
            var response = await _votersRepository.GetByIdAsync(id);
            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("updateVoter")]
        public async Task<ActionResult> UpdateAsync(Guid id,UpdateVotersRequestDTO model)
        {
            var response = await _votersRepository.UpdateAsync(id, model);
            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete]
        [Route("deleteVoter")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var response = await _votersRepository.DeleteAsync(id);

            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }



    }
}
