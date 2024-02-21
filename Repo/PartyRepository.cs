using AutoMapper;
using AutoMapper.QueryableExtensions;
using eVotingApp.DTOs.RequestDTO;
using eVotingApp.DTOs.ResponseDTO;
using eVotingApp.Entities;
using eVotingApp.Generic;
using eVotingApp.IRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace eVotingApp.Repo
{
    public class PartyRepository : IPartyRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PartyRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<PartyRegistrationResponseDTO>> AddAsync(PartyRegistrationRequestDTO model)
        {
            Response<PartyRegistrationResponseDTO> response = new();

            try
            {
                string capitalizedPartyName = model.PartyName.ToUpper();

                var existingParty = await _context
                    .Parties
                    .Where(x => x.CapitalizedPartyName == capitalizedPartyName)
                    .FirstOrDefaultAsync();

                if (existingParty != null)
                {
                    response.IsSuccessful = false;
                    response.Error = new ResponseError()
                    {
                        ErrorCode = 10,
                        ErrorMessage = "Party already exist"
                    };

                    return response;
                }

                Party party = new()
                {
                    PartyName = model.PartyName,
                    CapitalizedPartyName = model.PartyName.ToUpper(),
                    LeaderName = model.LeaderName,
                    WebsiteName = model.WebsiteName,
                    FoundationYear = model.FoundationYear
                };

                var addParty = await _context.Parties.AddAsync(party);
                await _context.SaveChangesAsync();

                response.IsSuccessful = true;
                response.ResponseCode = "00";
                response.Description = "Party added succesfully";
                response.Data = _mapper.Map<PartyRegistrationResponseDTO>(party);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                response.Error = new ResponseError()
                {
                    ErrorCode = 99,
                    ErrorMessage = "Internal Server Error"
                };
                response.IsSuccessful = false;
                response.Description = "Please try again later";
                return response;
            }
        }

        public async Task<ResponseList<PartyRegistrationResponseDTO>> GetAllAsync()
        {
            ResponseList<PartyRegistrationResponseDTO> response = new();

            var parties = await _context.Parties
                .ProjectTo<PartyRegistrationResponseDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (!parties.Any())
            {
                response.IsSuccessful = false;
                response.Error = new ResponseError()
                {
                    ErrorCode = 10,
                    ErrorMessage = " No parties added"
                };
                return response;
            }
            response.Data = parties;
            response.IsSuccessful = true;
            response.ResponseCode = "00";
            response.Description = "successful";
            return response;
        }

        public async Task<Response<PartyRegistrationResponseDTO>> GetByIdAsync(Guid id)
        {
            Response<PartyRegistrationResponseDTO> response = new();

            var party = await _context.Parties
                .Where(x => x.Id == id)
                .ProjectTo<PartyRegistrationResponseDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (party == null)
            {
                response.IsSuccessful = false;
                response.Error = new ResponseError()
                {
                    ErrorCode = 10,
                    ErrorMessage = "Id does not exixt"
                };
                return response;
            }

            response.Data = party;
            response.IsSuccessful = true;
            response.Description = "succesful";
            response.ResponseCode = "00";
            return response;
        }

        public async Task<Response<bool>> UpdateAsync(Guid Partyid, UpdatePartyRequestDTO model)
        {
            Response<bool> response = new();

            var party = await _context
                .Parties
                .Where(x => x.Id == Partyid)
                .FirstOrDefaultAsync();

            if (party == null)
            {
                response.IsSuccessful = false;
                response.Error = new ResponseError()
                {
                    ErrorCode = 11,
                    ErrorMessage = " Invalid PartyId"
                };
                return response;
            }

            party.PartyName = model.PartyName;
            party.LeaderName = model.LeaderName;
            party.WebsiteName = model.WebsiteName;
            party.FoundationYear = model.FoundationYear;

            await _context.SaveChangesAsync();
            response.IsSuccessful = true;
            response.ResponseCode = "00";
            response.Description = "Updated Successfully";
            response.Data = true;


            return response;
        }

        public async Task<Response<bool>> DeleteByIdAsync(Guid id)
        {
            Response<bool> response = new();

            var party = await _context
                .Parties
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (party == null)
            {
                response.IsSuccessful = false;
                response.Error = new ResponseError()
                {
                    ErrorCode = 12,
                    ErrorMessage = "Invalid PartyId"
                };
                return response;
            }

            _context.Parties.Remove(party);
            _context.SaveChanges();

            response.Data = true;
            response.IsSuccessful = true;
            response.ResponseCode = "00";
            response.Description = " Deleted successfully";
            return response;
        }
    }
}