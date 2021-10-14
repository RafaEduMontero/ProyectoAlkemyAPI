using OngProject.Core.DTOs;
using OngProject.Core.Interfaces.IServices;
using OngProject.Core.Mapper;
using OngProject.Infrastructure.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Services
{
    public class ActivitiesServices : IActivitiesServices
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper _mapper;
        public ActivitiesServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new EntityMapper();
        }
        public async Task<IEnumerable<ActivitiesDTO>> GetAll()
        {
            var activitiesList = await _unitOfWork.ActivitiesRepository.GetAll();
            var activitiesDTO = activitiesList.Select(x => _mapper.FromActivitiesToActivitiesDTO(x)).ToList();

            return activitiesDTO;
        }

        public async Task<ActivitiesDTO> GetById(int id)
        {
            var activities = await _unitOfWork.ActivitiesRepository.GetById(id);
            var activitiesDTO = _mapper.FromActivitiesToActivitiesDTO(activities);

            return activitiesDTO;
        }

        public async Task<ActivitiesDTO> Insert(ActivitiesDTO activitiesDTO)
        {
            var newActivity = _mapper.FromActivitiesDTOToActivities(activitiesDTO);
            await _unitOfWork.ActivitiesRepository.Insert(newActivity);
            await _unitOfWork.SaveChangesAsync();

            return activitiesDTO;
        }

        public bool EntityExists(int id)
        {
            return _unitOfWork.ActivitiesRepository.EntityExists(id);
        }
    }
}
