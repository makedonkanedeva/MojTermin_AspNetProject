using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MojTermin.Domain;
using MojTermin.Domain.DomainModels;
using MojTermin.Domain.DTO;
using MojTermin.Domain.Identity;
using MojTermin.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MojTermin.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferralController : ControllerBase
    {
        private readonly IReferralService _referralService;
        private readonly UserManager<MojTerminUser> _userManager;
        private readonly IRoleService _roleService;

        public ReferralController(IReferralService referralService, UserManager<MojTerminUser> userManager, IRoleService roleService)
        {
            _referralService = referralService;
            _userManager = userManager;
            _roleService = roleService;
        }

        [HttpGet("[action]")]
        public IEnumerable<Referral> GetAllReferrals() 
        {
            return this._referralService.GetAllReferrals();
        }

        [HttpPost("[action]")]
        public Referral GetDetailsForReferral(BaseEntity model)
        {
             return this._referralService.GetReferalDetails(model);
        }

        [HttpPost("[action]")]
        public bool ImportAllUsers (List<UserRegistrationDto> model)
        {
            bool status = true; 

            foreach(var item in model)
            {
                var userCheck = _userManager.FindByNameAsync(item.Email).Result;
                if (userCheck == null)
                {
                    var role = _roleService.Get(item.Role);

                    var user = new MojTerminUser
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        UserName = item.Email,
                        NormalizedUserName = item.Email,
                        Email = item.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        Address = item.Address,
                        Role = role
                    };
                    var result = _userManager.CreateAsync(user, item.Password).Result;

                    status = status && result.Succeeded;
                }
                else
                {
                    continue;
                }
            }

            return status;
        }
    }
}
