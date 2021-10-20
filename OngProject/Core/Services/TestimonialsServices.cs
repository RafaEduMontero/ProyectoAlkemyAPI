﻿using OngProject.Common;
using OngProject.Core.DTOs;
using OngProject.Core.Entities;
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
        private readonly IImageService _imageServices;

        public TestimonialsServices(IUnitOfWork unitOfWork, IImageService imageServices)
        {
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
    }
}
