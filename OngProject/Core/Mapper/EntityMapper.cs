﻿using System;
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

    }
}
