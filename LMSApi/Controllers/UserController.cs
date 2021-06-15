using LMSApi.Data;
using LMSDataManager.Library.DataAccess;
using LMSDataManager.Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LMSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserData _userData;

        public UserController(ApplicationDbContext context, UserManager<IdentityUser> userManager,
            IUserData userData)
        {
            _context = context;
            _userManager = userManager;
            _userData = userData;
        }
        [HttpGet]
        public UserModel GetById()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _userData.GetUserById(userId).First();
        }
        [HttpGet]
        [Route("GetAllUsers")]
        public List<ApplicationUserModel> GetAllUsers()
        {
            List<ApplicationUserModel> output = new List<ApplicationUserModel>();


            var users = _context.Users.ToList();
            var userRoles = from ur in _context.UserRoles
                            join r in _context.Roles on ur.RoleId equals r.Id
                            select new { ur.UserId, ur.RoleId, r.Name };

            foreach (var user in users)
            {
                UserModel userInDB = _userData.GetUserById(user.Id).First();
                ApplicationUserModel u = new ApplicationUserModel
                {
                    StaffName = userInDB.StaffName,
                    StaffId = user.Id,
                    Email = user.Email

                };

                u.Roles = userRoles.Where(x => x.UserId == u.StaffId).ToDictionary(key => key.RoleId, val => val.Name);
                output.Add(u);
            }

            return output;
        }

    }
}
