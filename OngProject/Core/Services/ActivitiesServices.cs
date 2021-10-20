using OngProject.Common;
using OngProject.Core.DTOs;
using OngProject.Core.Entities;
using OngProject.Core.Helper.FomFileData;
using OngProject.Core.Helper.S3;
using OngProject.Core.Interfaces.IServices;
using OngProject.Core.Interfaces.IServices.AWS;
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
        private readonly IImageService _imageServices;
        public ActivitiesServices(IUnitOfWork unitOfWork, IImageService imageServices)
        {
            _unitOfWork = unitOfWork;
            _mapper = new EntityMapper();
            _imageServices = imageServices;
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

        public async Task<Result> Update(ActivitiesDTO activitiesUpdateDTO,int id)
        {
            Result resp = new Result();

            var activitie = await _unitOfWork.ActivitiesRepository.GetById(id);
            if(activitie != null)
            {
                    if(activitiesUpdateDTO.Image != activitie.Image && !string.IsNullOrEmpty(activitiesUpdateDTO.Image))
                    {
                    var response = await _imageServices.Delete(activitie.Image);
                    if (response)
                        {
                            byte[] bytesFile = Convert.FromBase64String(activitiesUpdateDTO.Image);
                            var FileName = ValidateFiles.GetImageExtensionFromFile(bytesFile);
                            string uniqueName = "activities_" + DateTime.Now.ToString().Replace(",", "").Replace("/", "").Replace(" ", "");
                            
                            var formFile = new FormFileData()
                            {
                                FileName = FileName,
                                ContentType = "image/" + FileName.Replace(".",""),
                                Name = activitiesUpdateDTO.Name
                            };
                            var image = ConvertFile.BinaryToFormFile(bytesFile, formFile);

                            var urlImage = await _imageServices.Save(uniqueName + formFile.FileName, image);

                        activitie.Name = activitiesUpdateDTO.Name;
                        activitie.Content = activitiesUpdateDTO.Content;
                        activitie.Image = urlImage;

                           await _unitOfWork.ActivitiesRepository.Update(activitie);
                        await _unitOfWork.SaveChangesAsync();
                        resp = new Result().Success("Activitie modificada con éxito");
                        }
                    }
                    else
                    {
                    activitie.Name = activitiesUpdateDTO.Name;
                    activitie.Content = activitiesUpdateDTO.Content;
                    await _unitOfWork.ActivitiesRepository.Update(activitie);
                    await _unitOfWork.SaveChangesAsync();
                        resp = new Result().Success("Activitie modificada con éxito");
                    }                   
            }
            else
            {
                resp = new Result().Success("Activitie inexistente");
            }

            return resp;
        }
    }
}
