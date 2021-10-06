using Microsoft.AspNetCore.Mvc;
using OngProject.Core.DTOs;
using OngProject.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsServices _commentsServices;
        public CommentsController(ICommentsServices commentsServices)
        {
            _commentsServices = commentsServices;
        }
        [HttpGet]
        public async Task<IEnumerable<CommentsDTO>> Get()
        {
            return await _commentsServices.GetAll(); 
        }
    }
}
