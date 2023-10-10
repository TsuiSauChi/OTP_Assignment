using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OTPApp.Data;
using OTPApp.Models;

namespace OTPApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves Enrollment By Id
        /// </summary>
        [HttpGet("form/{id}")]
        public async Task<ActionResult<User>> GetAllUs(int id)
        {
            var query = from user in _context.Users
                where user.Id == id
                select new User{
                    Id = user.Id,
                    StatusId = user.StatusId,
                    Name = user.Name,
                    Email = user.Email,
                };

            return await query.FirstAsync(); 
        }
    }
}