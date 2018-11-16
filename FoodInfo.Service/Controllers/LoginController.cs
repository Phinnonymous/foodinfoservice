using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodInfo.Service.DTOs;
using FoodInfo.Service.Models;
using FoodInfo.Service.Helper;
namespace FoodInfo.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("CheckUserOnLogin")]
        public async Task<IActionResult> CheckUserOnLogin(LoginDTO loginDTO)
        {
            try
            {
                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {
                    var user = context.User.FirstOrDefault(x => x.Username == loginDTO.UsernameOrEmail || x.Email == loginDTO.UsernameOrEmail);
                    if (user == null )
                    {
                        return BadRequest(new ApiBadRequestWithMessage("The user does not exist."));
                        
                    }
                    if (user.IsDeleted == true)
                    {
                        return BadRequest(new ApiBadRequestWithMessage("The user does not exist."));

                    }
                    if (loginDTO.Password == string.Empty || loginDTO.Password == null)
                    {
                        return BadRequest(new ApiBadRequestWithMessage("Write a password for user."));
                    }
                    if(HelperFunctions.ComputeSha256Hash(loginDTO.Password) == user.Password)
                    {
                        return Ok(new ApiOkResponse(loginDTO));
                    }
                    else
                    {
                         return BadRequest(new ApiBadRequestWithMessage("The username or password is wrong."));
                    }
                    

                }
                

            }
            catch (Exception ex){

                return BadRequest(new ApiBadRequestWithMessage("The username or password is wrong."));

            }
        }

    }
}