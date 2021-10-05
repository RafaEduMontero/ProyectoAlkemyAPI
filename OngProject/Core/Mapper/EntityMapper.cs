using System;
using OngProject.Core.DTOs;
using OngProject.Core.Entities;

namespace OngProject.Core.Mapper
{
    public class EntityMapper
    {
        public SlidesDTO FromSlideToSlideDto(Slides slide)
        {
            var slideDTO = new SlidesDTO()
            {
                ImageUrl = slide.ImageUrl,
                Order = slide.Order
            };
            return slideDTO;
        }

        internal object FromContactsToContactsDto(Contacts x)
        {
            throw new NotImplementedException();
        }
    }
}
