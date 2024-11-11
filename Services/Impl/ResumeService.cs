using AutoMapper;
using BusinessObjects.DTO;
using BusinessObjects.Entity;
using DAL.Repositories;
using DAL.Repositories.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
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
        Task<ResponseResumeDTO> CreateResumeAsync(string PathFile, long userId, ResumeDTO resumeDTO);
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
            List<ResponseResumeDTO> listnew = new List<ResponseResumeDTO>();

            foreach (var resume in resumes) 
            {
                // Chuyển đổi JSON thành danh sách đối tượng WorkExperienceDTO
                var workExperiences = !string.IsNullOrEmpty(resume.ExperiencesJson)
                ? JsonConvert.DeserializeObject<List<WorkExperienceDTO>>(resume.ExperiencesJson)
                : new List<WorkExperienceDTO>();

                // Chuyển đổi JSON thành danh sách đối tượng ProjectDTO
                var projects = !string.IsNullOrEmpty(resume.ProjectsJson)
                ? JsonConvert.DeserializeObject<List<ProjectDTO>>(resume.ProjectsJson)
                : new List<ProjectDTO>();

                // Chuyển đổi JSON thành danh sách đối tượng EducationDTO
                var educations = !string.IsNullOrEmpty(resume.EducationJson) ? JsonConvert.DeserializeObject<List<EducationDTO>>(resume.EducationJson) : new List<EducationDTO>();

                // Chuyển đổi JSON thành danh sách đối tượng SkillsDTO
                var skills = !string.IsNullOrEmpty(resume.SkillsJson) ? JsonConvert.DeserializeObject<List<SkillsDTO>>(resume.SkillsJson) : new List<SkillsDTO>();

                // Chuyển đổi JSON thành danh sách đối tượng AwardAndCertificateDTO
                var awardsAndCertificates = !string.IsNullOrEmpty(resume.CertificationsJson) ? JsonConvert.DeserializeObject<List<AwardAndCertificateDTO>>(resume.CertificationsJson) : new List<AwardAndCertificateDTO>();
                var contactInfo = !string.IsNullOrEmpty(resume.ContactInfoJson)
                ? JsonConvert.DeserializeObject<ContactInfoDTO>(resume.ContactInfoJson)
                : new ContactInfoDTO();

                ResponseResumeDTO responseResumeDTO = new ResponseResumeDTO();
                responseResumeDTO.Id = resume.Id;
                responseResumeDTO.UserId = resume.UserId;
                responseResumeDTO.FilePath = resume.FilePath;
                responseResumeDTO.ContactInfo = contactInfo;
                responseResumeDTO.WorkExperience = workExperiences;
                responseResumeDTO.Project = projects;
                responseResumeDTO.Education = educations;
                responseResumeDTO.Skills = skills;
                responseResumeDTO.AwardAndCertificate = awardsAndCertificates;

                listnew.Add(responseResumeDTO);
            }
             
            return listnew;
        }

        public async Task<IEnumerable<ResponseResumeDTO>> GetAllResumesByUserAsync(long idUser)
        {
            var resumes = await _resumeRepository.GetAllResumesByUserAsync(idUser);


            List<ResponseResumeDTO> listnew = new List<ResponseResumeDTO>();

            foreach (var resume in resumes)
            {
                // Chuyển đổi JSON thành danh sách đối tượng WorkExperienceDTO
                var workExperiences = !string.IsNullOrEmpty(resume.ExperiencesJson)
                ? JsonConvert.DeserializeObject<List<WorkExperienceDTO>>(resume.ExperiencesJson)
                : new List<WorkExperienceDTO>();

                // Chuyển đổi JSON thành danh sách đối tượng ProjectDTO
                var projects = !string.IsNullOrEmpty(resume.ProjectsJson)
                ? JsonConvert.DeserializeObject<List<ProjectDTO>>(resume.ProjectsJson)
                : new List<ProjectDTO>();

                // Chuyển đổi JSON thành danh sách đối tượng EducationDTO
                var educations = !string.IsNullOrEmpty(resume.EducationJson) ? JsonConvert.DeserializeObject<List<EducationDTO>>(resume.EducationJson) : new List<EducationDTO>();

                // Chuyển đổi JSON thành danh sách đối tượng SkillsDTO
                var skills = !string.IsNullOrEmpty(resume.SkillsJson) ? JsonConvert.DeserializeObject<List<SkillsDTO>>(resume.SkillsJson) : new List<SkillsDTO>();

                // Chuyển đổi JSON thành danh sách đối tượng AwardAndCertificateDTO
                var awardsAndCertificates = !string.IsNullOrEmpty(resume.CertificationsJson) ? JsonConvert.DeserializeObject<List<AwardAndCertificateDTO>>(resume.CertificationsJson) : new List<AwardAndCertificateDTO>();
                var contactInfo = !string.IsNullOrEmpty(resume.ContactInfoJson)
                ? JsonConvert.DeserializeObject<ContactInfoDTO>(resume.ContactInfoJson)
                : new ContactInfoDTO();

                ResponseResumeDTO responseResumeDTO = new ResponseResumeDTO();
                responseResumeDTO.Id = resume.Id;
                responseResumeDTO.UserId = resume.UserId;
                responseResumeDTO.FilePath = resume.FilePath;
                responseResumeDTO.ContactInfo = contactInfo;
                responseResumeDTO.WorkExperience = workExperiences;
                responseResumeDTO.Project = projects;
                responseResumeDTO.Education = educations;
                responseResumeDTO.Skills = skills;
                responseResumeDTO.AwardAndCertificate = awardsAndCertificates;

                listnew.Add(responseResumeDTO);
            }

            return listnew;
        }

        public async Task<ResponseResumeDTO> GetResumeByIdAsync(long id)
        {
            var resumes = await _resumeRepository.GetResumeByIdAsync(id);
            if(resumes == null)
            {
                throw new Exception("resume not found");
            }
            // Chuyển đổi JSON thành danh sách đối tượng WorkExperienceDTO
            var workExperiences = !string.IsNullOrEmpty(resumes.ExperiencesJson)
            ? JsonConvert.DeserializeObject<List<WorkExperienceDTO>>(resumes.ExperiencesJson)
            : new List<WorkExperienceDTO>();

            // Chuyển đổi JSON thành danh sách đối tượng ProjectDTO
            var projects = !string.IsNullOrEmpty(resumes.ProjectsJson)
            ? JsonConvert.DeserializeObject<List<ProjectDTO>>(resumes.ProjectsJson)
            : new List<ProjectDTO>();

            // Chuyển đổi JSON thành danh sách đối tượng EducationDTO
            var educations = !string.IsNullOrEmpty(resumes.EducationJson) ? JsonConvert.DeserializeObject<List<EducationDTO>>(resumes.EducationJson) : new List<EducationDTO>();

            // Chuyển đổi JSON thành danh sách đối tượng SkillsDTO
            var skills = !string.IsNullOrEmpty(resumes.SkillsJson) ? JsonConvert.DeserializeObject<List<SkillsDTO>>(resumes.SkillsJson) : new List<SkillsDTO>();

            // Chuyển đổi JSON thành danh sách đối tượng AwardAndCertificateDTO
            var awardsAndCertificates = !string.IsNullOrEmpty(resumes.CertificationsJson) ? JsonConvert.DeserializeObject<List<AwardAndCertificateDTO>>(resumes.CertificationsJson) : new List<AwardAndCertificateDTO>();
            var contactInfo = !string.IsNullOrEmpty(resumes.ContactInfoJson)
            ? JsonConvert.DeserializeObject<ContactInfoDTO>(resumes.ContactInfoJson)
            : new ContactInfoDTO();

            ResponseResumeDTO responseResumeDTO = new ResponseResumeDTO();
            responseResumeDTO.Id = resumes.Id;
            responseResumeDTO.UserId = resumes.UserId;
            responseResumeDTO.FilePath = resumes.FilePath;
            responseResumeDTO.ContactInfo = contactInfo;
            responseResumeDTO.WorkExperience = workExperiences;
            responseResumeDTO.Project = projects;
            responseResumeDTO.Education = educations;
            responseResumeDTO.Skills = skills;
            responseResumeDTO.AwardAndCertificate = awardsAndCertificates;
            return responseResumeDTO;
        }

        public async Task<ResponseResumeDTO> CreateResumeAsync(string PathFile, long userId, ResumeDTO resumeDTO)
        {
            
            Resume resume = new Resume()
            {
                UserId = userId,
                FilePath = PathFile,
                    // Chuyển các đối tượng thành chuỗi JSON
               ContactInfoJson = JsonConvert.SerializeObject(new
               {
                Name = resumeDTO.Name.ToString(),
                Email = resumeDTO.Email.ToString(),
                Phone = resumeDTO.Phone.ToString(),
                Address = resumeDTO.Address.ToString(),
                CareerGoal = resumeDTO.CareerGoal.ToString(),
                MoreInfomation = resumeDTO.MoreInfomation.ToString()
               }),
               ExperiencesJson = JsonConvert.SerializeObject(resumeDTO.WorkExperience),
               ProjectsJson = JsonConvert.SerializeObject(resumeDTO.Project),
               EducationJson = JsonConvert.SerializeObject(resumeDTO.Education),
               SkillsJson = JsonConvert.SerializeObject(resumeDTO.Skills),
               CertificationsJson = JsonConvert.SerializeObject(resumeDTO.AwardAndCertificate)

            };
            var createResume = await _resumeRepository.CreateResumeAsync(resume);
            if (createResume == null)
            {
                throw new IOException("resume creation failed.");
            }
            // Chuyển đổi JSON thành danh sách đối tượng WorkExperienceDTO
            var workExperiences = JsonConvert.DeserializeObject<List<WorkExperienceDTO>>(createResume.ExperiencesJson);

            // Chuyển đổi JSON thành danh sách đối tượng ProjectDTO
            var projects = JsonConvert.DeserializeObject<List<ProjectDTO>>(createResume.ProjectsJson);

            // Chuyển đổi JSON thành danh sách đối tượng EducationDTO
            var educations = JsonConvert.DeserializeObject<List<EducationDTO>>(createResume.EducationJson);

            // Chuyển đổi JSON thành danh sách đối tượng SkillsDTO
            var skills = JsonConvert.DeserializeObject<List<SkillsDTO>>(createResume.SkillsJson);

            // Chuyển đổi JSON thành danh sách đối tượng AwardAndCertificateDTO
            var awardsAndCertificates = JsonConvert.DeserializeObject<List<AwardAndCertificateDTO>>(createResume.CertificationsJson);
            var contactInfo = !string.IsNullOrEmpty(createResume.ContactInfoJson)
            ? JsonConvert.DeserializeObject<ContactInfoDTO>(createResume.ContactInfoJson)
            : new ContactInfoDTO();

            ResponseResumeDTO responseResumeDTO= new ResponseResumeDTO();
            responseResumeDTO.Id = createResume.Id;
            responseResumeDTO.UserId = createResume.UserId;
            responseResumeDTO.FilePath = createResume.FilePath;
            responseResumeDTO.ContactInfo = contactInfo;
            responseResumeDTO.WorkExperience = workExperiences;
            responseResumeDTO.Project = projects;
            responseResumeDTO.Education= educations;
            responseResumeDTO.Skills = skills; 
            responseResumeDTO.AwardAndCertificate = awardsAndCertificates;


            return responseResumeDTO;
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
