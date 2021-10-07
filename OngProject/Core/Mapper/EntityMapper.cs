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

        public OrganizationsDTO FromOrganizationToOrganizationDto(Organizations organization)
        {
            return new OrganizationsDTO
            {
                Name = organization.Name,
                Image = organization.Image,
                Phone = organization.Phone,
                Address = organization.Address,
                FacebookUrl = organization.FacebookUrl,
                InstagramUrl = organization.InstagramUrl,
                LinkedinUrl = organization.LinkedinUrl
            };
        }

        public CategoryDTO FromCategoryToCategoryDto(Category category)
        {
            var categoryDTO = new CategoryDTO()
            {
                Name = category.Name,
                Description = category.Description,
                Image = category.Image
            };
            return categoryDTO;
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
        public UserDTO FromsUserToUserDto(User user)
        {
            var userDTO = new UserDTO()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Photo = user.Photo,
                RoleId = user.RoleId
            };
            return userDTO;
        }
        public CategoryNameDTO FromCategoryToCategoryNameDto(Category category)
        {
            var categoryNameDTO = new CategoryNameDTO()
            {
                Name = category.Name
            };
            return categoryNameDTO;
        }


        public CommentsDTO FromCommentsToCommentsDto(Comments comment)
        {
            var CommentsDTO = new CommentsDTO()
            {
                Body = comment.Body
            };
            return CommentsDTO;
        }

         public MembersDTO FromMembersToMembersDto(Member member)
        {
            var membersDTO = new MembersDTO()
            {
                Name = member.Name,
                FacebookUrl = member.FacebookUrl,
                InstagramUrl=member.InstagramUrl,
                LinkedinUrl = member.LinkedinUrl,
                Image = member.Image,
                Description= member.Description

            };
            return membersDTO;
        }
        public User FromUserDtoToUser(UserDTO userDTO)
        {
            var user = new User()
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                Password = userDTO.Password,
                Photo = userDTO.Photo,
                RoleId = userDTO.RoleId
            };
            return user;
        }

    }
}
