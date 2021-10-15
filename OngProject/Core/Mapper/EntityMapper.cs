using System;
using OngProject.Core.DTOs;
using OngProject.Core.Entities;
using OngProject.Infrastructure.Repositories;

namespace OngProject.Core.Mapper
{
    public class EntityMapper
    {
        #region Slides Mappers
        public SlidesDTO FromSlideToSlideDto(Slides slide)
        {
            var slideDTO = new SlidesDTO()
            {
                ImageUrl = slide.ImageUrl,
                Order = slide.Order
            };
            return slideDTO;
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

        #endregion

        #region Category Mappers
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

        public CategoryNameDTO FromCategoryToCategoryNameDto(Category category)
        {
            var categoryNameDTO = new CategoryNameDTO()
            {
                Name = category.Name
            };
            return categoryNameDTO;
        }

        public Category FromCategoryCreateDtoToCategory(CategoryDTO categoryDTO)
        {
            var category = new Category()
            {
                Name = categoryDTO.Name,
                Description = categoryDTO.Description,
                Image = categoryDTO.Image
            };
            return category;
        }

        #endregion

        #region News Mappers
        public NewsDTO FromNewsToNewsDTO(News news)
        {
            var newsDTO = new NewsDTO()
            {
                Name = news.Name,
                Content = news.Content,
                Image = news.Image,
                CategoryId = news.CategoryId
            };
            return newsDTO;
        }
        public News FromNewsDTOtoNews(NewsDTO newsDTO)
        {
            var news = new News()
            {
                Name = newsDTO.Name,
                Content = newsDTO.Content,
                Image = newsDTO.Image,
                CategoryId = newsDTO.CategoryId
            };
            return news;
        }

        #endregion

        #region Comments Mapper
        public CommentsDTO FromCommentsToCommentsDto(Comments comment)
        {
            var CommentsDTO = new CommentsDTO()
            {
                Body = comment.Body
            };
            return CommentsDTO;
        }

        public Comments FromNewCommentsDtoToComments(NewCommentsDTO newCommentDTO)
        {
            return new Comments()
            {
                Body = newCommentDTO.Body,
                NewId = newCommentDTO.NewId,
                UserId = newCommentDTO.UserId
            };
        }
        #endregion

        #region Member Mapper
        public MembersDTO FromMembersToMembersDto(Member member)
        {
            var membersDTO = new MembersDTO()
            {
                Name = member.Name,
                FacebookUrl = member.FacebookUrl,
                InstagramUrl = member.InstagramUrl,
                LinkedinUrl = member.LinkedinUrl,
                Image = member.Image,
                Description = member.Description

            };
            return membersDTO;
        }
         public Member FromMembersDTOtoMember(MembersDTO membersDTO)
        {
            return new Member()
            {
                Name = membersDTO.Name,
                FacebookUrl = membersDTO.FacebookUrl,
                InstagramUrl = membersDTO.InstagramUrl,
                LinkedinUrl = membersDTO.LinkedinUrl,
                Image = membersDTO.Image,
                Description = membersDTO.Description
            };
        }
    
        #endregion

        #region Contact Mappers
        public ContactDTO FromContactsToContactsDto(Contacts contact)
        {
            var contactDTO = new ContactDTO()
            {
                Name = contact.Name,
                Phone = contact.Phone,
                Email = contact.Email,
                Message = contact.Message
            };
            return contactDTO;
        }
        public Contacts FromContactsDtoToContacts(ContactDTO contactDTO)
        {
            var contact = new Contacts()
            {
                Name = contactDTO.Name,
                Phone = contactDTO.Phone,
                Email = contactDTO.Email,
                Message = contactDTO.Message
            };
            return contact;
        }

        #endregion

        #region Organization Mappers
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
        #endregion

        #region User Mappers

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

        #endregion

        #region Activities Mappers
        public ActivitiesDTO FromActivitiesToActivitiesDTO (Activities activities)
        {
            var activitiesDTO = new ActivitiesDTO()
            {
                Name = activities.Name,
                Content = activities.Content,
                Image = activities.Image
            };
            return activitiesDTO;
        }
        public Activities FromActivitiesDTOToActivities (ActivitiesDTO activitiesDTO)
        {
            var activities = new Activities()
            {
                Name = activitiesDTO.Name,
                Content = activitiesDTO.Content,
                Image = activitiesDTO.Image
            };
            return activities;
        }

        #endregion
    }
}
