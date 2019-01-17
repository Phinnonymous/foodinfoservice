using FoodInfo.Service.DTOs;
using FoodInfo.Service.Helper;
using FoodInfo.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
namespace FoodInfo.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("CheckUserOnLogin")]
        public IActionResult CheckUserOnLogin(LoginDTO loginDTO)
        {
            var apiJsonResponse = new ApiJsonResponse();
            try
            {
                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {
                    var user = context.User.FirstOrDefault(x => x.Username == loginDTO.Username || x.Email == loginDTO.Email);
                    if (user == null)
                    {

                        return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.UserNotFoundError);


                    }
                    if (user.IsDeleted == true)
                    {
                        return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.UserNotFoundError);


                    }
                    if (loginDTO.Password == string.Empty || loginDTO.Password == null)
                    {
                        return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.PasswordRequired);


                    }
                    if (HelperFunctions.ComputeSha256Hash(loginDTO.Password) == user.Password)
                    {

                        loginDTO.IsAdmin = user.IsAdmin;
                        loginDTO.IsModerator = user.IsModerator;
                        loginDTO.Email = user.Email;
                        loginDTO.Username = user.Username;
                        return apiJsonResponse.ApiOkContentResult(loginDTO);
                    }
                    else
                    {
                        return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.UsernameOrPasswordWrong);

                    }


                }


            }
            catch (Exception ex)
            {

                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);

            }
        }

    }
}