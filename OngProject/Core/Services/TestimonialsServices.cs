using OngProject.Common;
using OngProject.Core.DTOs;
using OngProject.Core.DTOs.SlidesDTOs;
using OngProject.Core.Entities;
using OngProject.Core.Helper.Pagination;
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
    public class TestimonialsServices : ITestimonialsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUriService _uriService;
        private readonly IImageService _imageServices;

        public TestimonialsServices(IUnitOfWork unitOfWork, IImageService imageServices, IUriService uriService)
        {
            _uriService = uriService;
            _unitOfWork = unitOfWork;
            _imageServices = imageServices;
        }
        public async Task<Result> Insert(TestimonialsCreateDTO testimonialsDTO)
        {
            Testimonials testimonial;
            Testimonials response = null;
            if (!string.IsNullOrEmpty(testimonialsDTO.Name) && !string.IsNullOrEmpty(testimonialsDTO.Content))
            {
                if (testimonialsDTO.Imagen != null)
                {
                    string uniqueName = "testimonial_" + DateTime.Now.ToString().Replace(",", "").Replace("/", "").Replace(" ", "");
                    var urlImage = await _imageServices.Save(uniqueName + testimonialsDTO.Imagen.FileName, testimonialsDTO.Imagen);

                    testimonial = new Testimonials()
                    {
                        Name = testimonialsDTO.Name,
                        Image = urlImage,
                        Content = testimonialsDTO.Content
                    };

                    response = await _unitOfWork.TestimonialsRepository.Insert(testimonial);

                    await _unitOfWork.SaveChangesAsync();
                }
                else
                {
                    testimonial = new Testimonials()
                    {
                        Name = testimonialsDTO.Name,
                        Content = testimonialsDTO.Content
                    };

                    response = await _unitOfWork.TestimonialsRepository.Insert(testimonial);

                    await _unitOfWork.SaveChangesAsync();
                }
            }

            if (response != null)

            {
                return new Result().Success("Testimonial ingresado con éxito");
            }

            else
            {
                return new Result().Fail("No se ha podido ingresar el Testimonial");
            }
        }
        public async Task<Result> Delete(int id)
        {
            var testimonial = await _unitOfWork.TestimonialsRepository.GetById(id);
            if(testimonial == null)
            {
                return new Result().NotFound();
            }
            var ulr = testimonial.Image;
            var result = await _unitOfWork.TestimonialsRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            if(result != null)
            {
                await _imageServices.Delete(ulr);
                return new Result().Success("Testimonial eliminado con exito");
            }
            return new Result().Fail("Ocurrio un error al eliminar el testimonial");
        }
        public async Task<Result> Update(int id, TestimonialsCreateDTO testimonialsCreateDTO)
        {
            var testimonial = await _unitOfWork.TestimonialsRepository.GetById(id);
            if (testimonial == null)
            {
                return new Result().NotFound();
            }
            if(!string.IsNullOrEmpty(testimonialsCreateDTO.Name))
            {
                testimonial.Name = testimonialsCreateDTO.Name;
            }
            if(!string.IsNullOrEmpty(testimonialsCreateDTO.Content))
            {
                testimonial.Content = testimonialsCreateDTO.Content;
            }
            if(testimonialsCreateDTO.Imagen != null)
            {
                if(await _imageServices.Delete(testimonial.Image))
                {
                    testimonial.Image = await _imageServices.Save(testimonialsCreateDTO.Imagen.FileName, testimonialsCreateDTO.Imagen);
                }
            }
            await _unitOfWork.TestimonialsRepository.Update(testimonial);
            await _unitOfWork.SaveChangesAsync();

            var testimonialUpdate = await _unitOfWork.TestimonialsRepository.GetById(id);

            if (testimonialUpdate.Name == testimonial.Name 
                && testimonialUpdate.Content == testimonial.Content
                && testimonialUpdate.Image == testimonial.Image)
            {
                
                return new Result().Success($"{testimonialUpdate.Name}  ," + 
                                            $"{testimonialUpdate.Content}  ," + 
                                            $"{testimonialUpdate.Image}" 
                );
                
            }
            return new Result().Fail("El testimonio no se pudo modificar");
        }

        public async Task<PaginationDTO<TestimonialsDTO>> GetByPage(string route, int page, int? sizePage)
        {
            if (page <= 0) page = 1;
            if(sizePage == null) sizePage = 10;
            var n = await _unitOfWork.TestimonialsRepository.GetPageAsync(x => x.Name, (int)sizePage, page);
            var items = n.ToList();
            var mapper = new EntityMapper();
            var itemsList = items.Select(x => mapper.FromTestimonialsToTestimonialsDTO(x)).ToList();
            var totalItems = await _unitOfWork.TestimonialsRepository.CountAsync();
            var totalpages = (int)Math.Ceiling((double)totalItems / (int)sizePage);

            var response = new PaginationDTO<TestimonialsDTO>()
            {
                CurrentPage = page,
                TotalItems = totalItems,
                TotalPages = totalpages,
                PrevPage = page > 1 && page - 1 <= totalpages ? _uriService.GetPage(route, page - 1) : null,
                NextPage = page < totalpages ? _uriService.GetPage(route, page + 1) : null,
                Items = itemsList
            };

            return response;
        }
    }
}
