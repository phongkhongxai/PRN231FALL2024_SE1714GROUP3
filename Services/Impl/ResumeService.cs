using AutoMapper;
using BusinessObjects.DTO;
using BusinessObjects.Entity;
using DAL.Repositories;
using DAL.Repositories.Impl;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Impl
{
    public interface IResumeService
    {
        Task<IEnumerable<ResponseResumeDTO>> GetAllResumesAsync();
        Task<IEnumerable<ResponseResumeDTO>> GetAllResumesByUserAsync(long idUser);
        Task<ResponseResumeDTO> GetResumeByIdAsync(long id);
        Task<ResponseResumeDTO> CreateResumeAsync(string PathFile, long userId);
        Task<bool> DeleteResumeAsync(long id);
    }
    public class ResumeService : IResumeService
    {
        private readonly IResumeRepository _resumeRepository;
        private readonly IMapper _mapper;

        public ResumeService(IResumeRepository resumeRepository, IMapper mapper)
        {
            _resumeRepository = resumeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ResponseResumeDTO>> GetAllResumesAsync()
        {
            var resumes = await _resumeRepository.GetAllResumesAsync();
            return _mapper.Map<IEnumerable<ResponseResumeDTO>>(resumes);
        }

        public async Task<IEnumerable<ResponseResumeDTO>> GetAllResumesByUserAsync(long idUser)
        {
            var resumes = await _resumeRepository.GetAllResumesByUserAsync(idUser);
            return _mapper.Map<IEnumerable<ResponseResumeDTO>>(resumes);
        }

        public async Task<ResponseResumeDTO> GetResumeByIdAsync(long id)
        {
            var resumes = await _resumeRepository.GetResumeByIdAsync(id);
            return _mapper.Map<ResponseResumeDTO>(resumes);
        }

        public async Task<ResponseResumeDTO> CreateResumeAsync(string PathFile, long userId)
        {
            
            Resume resume = new Resume()
            {
                UserId = userId,
                FilePath = PathFile
            };
            var createResume = await _resumeRepository.CreateResumeAsync(resume);
            if (createResume == null)
            {
                throw new IOException("resume creation failed.");
            }
            return _mapper.Map<ResponseResumeDTO>(createResume);
        }

        public async Task<bool> DeleteResumeAsync(long id)
        {
            var resume = await _resumeRepository.GetResumeByIdAsync(id);
            if (resume == null)
            {
                return false;
            }
            return await _resumeRepository.DeleteResumeAsync(id);
        }
    }
}
