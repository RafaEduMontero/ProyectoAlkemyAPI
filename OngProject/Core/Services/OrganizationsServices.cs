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
    public class OrganizationsServices : IOrganizationsServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrganizationsServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OrganizationsDTO> Get()
        {
            var mapper = new EntityMapper();
            var organization = await _unitOfWork.OrganizationsRepository.GetAll();
            var organizationDTO = mapper.FromOrganizationToOrganizationDto(organization.First());
            return organizationDTO;
        }
    }
}
