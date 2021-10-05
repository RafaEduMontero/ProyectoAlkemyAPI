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
        public ContactsDTO FromContactsToContactsDto(Contacts contact)
        {
            var contactDTO = new ContactsDTO()
            {
                Name = contact.Name,
                Phone = contact.Phone,
                Email = contact.Email,
                Message = contact.Message
            };
           return contactDTO;
        }
        public SlidesDTO FromSlideDetalleToSlideDto(Slides slide)
        {
            var slideDTO = new SlidesDTO()
            {
                ImageUrl = slide.ImageUrl,
                Order = slide.Order,
                Text = slide.Text,
                OrganizationId = slide.OrganizationId

            };
            return slideDTO;
        }

        public ContactsDTO FromContactsToContactsDto(Contacts contact)
        {
            var contactDTO = new ContactsDTO()
            {
                Name = contact.Name,
                Phone = contact.Phone,
                Email = contact.Email,
                Message = contact.Message
            };
            return contactDTO;
        }
    }
}
