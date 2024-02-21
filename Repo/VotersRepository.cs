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
using System.Net.Mail;
using System.Threading.Tasks;

namespace eVotingApp.Repo
{
    public class VotersRepository : IVotersRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public VotersRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response<VotersRegistrationResponseDTO>> AddAsync(VotersRegistrationRequestDTO model)
        {
            Response<VotersRegistrationResponseDTO> response = new();

            //check if voter alraedy exist
            try
            {
                var existingvoter = await _context.Voters.Where(x => x.EmailAddress == model.EmailAddress).FirstOrDefaultAsync();

                if (existingvoter != null)
                {
                    response.IsSuccessful = false;
                    response.Description = "voter already exist";
                    ResponseError error = new()
                    {
                        ErrorCode = 22,
                        ErrorMessage = "invalid credentials"
                    };
                    return response;
                }
                Voters voter = new()
                {
                    FullName = model.FullName,
                    EmailAddress = model.EmailAddress,
                    PhoneNumber = model.PhoneNumber,
                    HouseAddress = model.HouseAddress,
                    DateOfBirth = model.DateOfBirth,
                };
                var addVoters = await _context.Voters.AddAsync(voter);
                await _context.SaveChangesAsync();


                response.IsSuccessful = true;
                response.ResponseCode = "00";
                response.Data = _mapper.Map<VotersRegistrationResponseDTO>(voter);
                response.Description = "Voter added succesfully";
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                response.IsSuccessful = false;
                ResponseError responseError = new()
                {
                    ErrorCode = 22,
                    ErrorMessage = "internal server ereror"
                };
                return response;
            }
        }

        public async Task<ResponseList<VotersRegistrationResponseDTO>> GetAllAsync()
        {
            ResponseList<VotersRegistrationResponseDTO> response = new();

            var voters = await _context.Voters
                .ProjectTo<VotersRegistrationResponseDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (voters.Count > 0)
            {
                response.Data = voters;
                response.IsSuccessful = true;
                response.ResponseCode = "00";
                response.Description = "successfull";
                return response;
            }
            ResponseError error = new()
            {
                ErrorCode = 22,
                ErrorMessage = "Invalid payload"
            };
            response.IsSuccessful = false;
            response.Description = "No records";
            return response;

        }

        public async Task<Response<VotersRegistrationResponseDTO>> GetByIdAsync(Guid id)
        {
            Response<VotersRegistrationResponseDTO> response = new();

            var voters = await _context.Voters
                .ProjectTo<VotersRegistrationResponseDTO>(_mapper.ConfigurationProvider)
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            if (voters == null)
            {
                response.IsSuccessful = false;
                response.Description = "Wrong Id";
                ResponseError error = new()
                {
                    ErrorCode = 21,
                    ErrorMessage = "Invalid payload"
                };
                return response;
            }
            response.Data = voters;
            response.IsSuccessful = true;
            response.ResponseCode = "00";
            response.Description = "successfull";
            return response;
        }

        public async Task<Response<bool>> UpdateAsync(Guid id, UpdateVotersRequestDTO model)
        {
            Response<bool> response = new();
            var voter = await _context.Voters.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (voter == null)
            {
                response.IsSuccessful = false;
                response.Description = "Wrong Id";
                ResponseError error = new()
                {
                    ErrorCode = 21,
                    ErrorMessage = "Invalid payload"
                };
                return response;
            }

            {
                voter.FullName = model.FullName;
                voter.EmailAddress = model.EmailAddress;
                voter.HouseAddress = model.HouseAddress;
                voter.PhoneNumber = model.PhoneNumber;
                voter.DateOfBirth = model.DateOfBirth;

                await _context.SaveChangesAsync();
                response.Data = true;
                response.IsSuccessful = true;
                response.ResponseCode = "00";
                response.Description = "Updated successfully";
                return response;
            }
        }
            public async Task<Response<bool>> DeleteAsync(Guid id)
            {
                var response = new Response<bool>();

                var voter = await _context.Voters.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (voter == null)
                {
                    response.IsSuccessful = false;
                    ResponseError responseError = new()
                    {
                        ErrorCode = 22,
                        ErrorMessage = "Invalid VotersId"
                    };
                    return response;
                }
                _context.Voters.Remove(voter);
                _context.SaveChanges();
                response.IsSuccessful = true;
                response.Description = "Deleted successfully";
                return response;
            }
        }
    }