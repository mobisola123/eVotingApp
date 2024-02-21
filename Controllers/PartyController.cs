using eVotingApp.DTOs.RequestDTO;
using eVotingApp.DTOs.ResponseDTO;
using eVotingApp.Generic;
using eVotingApp.IRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace eVotingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartyController : ControllerBase
    {
        private readonly IPartyRepository _partyRepository;
        public PartyController(IPartyRepository partyRepository)
        {
            _partyRepository = partyRepository;
        }
        [HttpPost]
        [Route("registerParty")]
        public async Task<ActionResult> AddAsync(PartyRegistrationRequestDTO model)
        {
            if (!ModelState.IsValid) 
            {
                var error = ModelState.Values.Where(x => x.Errors.Count > 0)
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .FirstOrDefault();

                return BadRequest(
                    new Response<PartyRegistrationResponseDTO>()
                    {
                        Error = new ResponseError()
                        {
                            ErrorCode = 55,
                            ErrorMessage = error
                        },
                        ResponseCode = "55",
                        IsSuccessful = false,
                        Description = "Invalid Payload"
                    }); 
            }

            var response = await _partyRepository.AddAsync(model);

            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }


        [HttpGet]
        [Route("getAllAsync")]
        public async Task<ActionResult> GetAllAsync()
        {
            return Ok(await _partyRepository.GetAllAsync());
        }


        [HttpGet]
        [Route("getByIdAsync/{id}")]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            var response = await _partyRepository.GetByIdAsync(id);
            if (!response.IsSuccessful) 
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        [HttpPut]
        [Route("updateAsync/{Id}")]
        public async Task<ActionResult> UpdateAsync(Guid Id, UpdatePartyRequestDTO model)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.Where(x => x.Errors.Count > 0)
                   .SelectMany(x => x.Errors)
                   .Select(x => x.ErrorMessage)
                   .FirstOrDefault();


                return BadRequest(
                    new Response <bool>()
                    {
                        IsSuccessful = false,
                        ResponseCode = "55",
                        Description = "Invalid payload",
                    });
            }
            var response = await _partyRepository.UpdateAsync(Id,model);

            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }
            return Ok(response); 
        }

        [HttpDelete]
        [Route("deleteAsync/{Id}")]
        public async Task<ActionResult> DeleteByIdAsync(Guid Id)
        {
            var response = await _partyRepository.DeleteByIdAsync(Id);
            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
            

    }
}
