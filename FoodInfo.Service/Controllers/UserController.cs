using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using FoodInfo.Service.Models;
using System.Net;
using FoodInfo.Service.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using FoodInfo.Service.DTOs;
using AutoMapper;
using FoodInfo.Service.Helper;

namespace FoodInfo.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        //[ProducesResponseType(201, Type = typeof(Product))]
        //[ProducesResponseType(400)]
        [HttpPost]
        [Route("CreateUser")]
        public IActionResult CreateUser(UserDTO userDTO)
        {

            try
            {
                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {


                    if (context.User.Any(m => m.Email == userDTO.Email || m.Username == userDTO.Username))
                    {
                        return BadRequest(new ApiBadRequestWithMessage(PublicConstants.UserNameOrEmailAlreadyExistError));

                    }

                    var hashedPassword = HelperFunctions.ComputeSha256Hash(userDTO.Password);
                    userDTO.Password = hashedPassword;
                    var x = context.User.Add(Mapper.Map<User>(userDTO));

                    //if (!ModelState.IsValid)
                    //{
                    //    return BadRequest(new ApiBadRequestResponse(ModelState));
                    //}
                    context.SaveChanges();
                    UserDTO userCredentialsOnSuccess = new UserDTO();
                    userCredentialsOnSuccess.Name = userDTO.Name;
                    userCredentialsOnSuccess.Surname = userDTO.Surname;
                    userCredentialsOnSuccess.Username = userDTO.Username;
                    userCredentialsOnSuccess.Email = userDTO.Email;
                    return Ok(new ApiOkResponse(userCredentialsOnSuccess));
                    //var isExistUsername = foodInfoServiceContext.User.FirstOrDefault(x => x.Username == "Fatihs");


                    //return Ok(new ApiOkResponse(foodInfoServiceContext.User.FirstOrDefault(x => x.Name == "Fatih")));



                }
            }
            catch (Exception ex) { return BadRequest(new ApiBadRequestWithMessage(PublicConstants.SysErrorMessage)); }


            //    try
            //    {
            //        if (!ModelState.IsValid)
            //        {
            //            return BadRequest(new ApiBadRequestResponse(ModelState));
            //        }

            //        using (FoodInfoServiceContext foodInfoServiceContext = new FoodInfoServiceContext())
            //        {
            //            var isExistUsername = foodInfoServiceContext.User.FirstOrDefault(x => x.Username == "Fatihs");


            //            return Ok(new ApiOkResponse(foodInfoServiceContext.User.FirstOrDefault(x => x.Name == "Fatih")));

            //        }
            //    }
            //    catch
            //    {
            //        return BadRequest();

            //    }
            //    return BadRequest();
            //}
            //catch
            //{
            //    return Ok();

            //}
            //return BadRequest();

        }

        [HttpPost]
        [Route("GetUserDetailByUsername")]
        public IActionResult GetUserDetailByUsername(UserDTO userDTO)
        {
            using (FoodInfoServiceContext context = new FoodInfoServiceContext())
            {
                var user = context.User.FirstOrDefault(x => x.Username == userDTO.Username && x.IsDeleted == false);


                return Ok(new ApiOkResponse(Mapper.Map<UserDTO>(user)));
            }

        }
        [HttpPost]
        [Route("SetModeratorByUsername")]
        public IActionResult SetModeratorByUsername(UserDTO userDTO)
        {
            try
            {

                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {
                    var user = context.User.FirstOrDefault(x => x.Username == userDTO.Username && x.IsDeleted == false);

                    if (user == null)
                    {
                        return BadRequest(new ApiBadRequestWithMessage(PublicConstants.UserNotFoundError));

                    }
                    else
                    {
                        user.IsModerator = true;
                        user.IsAdmin = false;

                        if(userDTO.ModifiedUserId != null )
                        { user.ModifiedUserId = userDTO.ModifiedUserId;  }
                        user.ModifiedDate = DateTime.Now;
                        context.SaveChanges();
                        return Ok(new ApiOkResponse(Mapper.Map<ModeratorDTO>(user)));
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiBadRequestWithMessage(PublicConstants.SysErrorMessage));
            }

        }

        [HttpPost]
        [Route("SetAdminByUsername")]
        public IActionResult SetAdminByUsername(UserDTO userDTO)
        {
            try
            {
                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {
                    var user = context.User.FirstOrDefault(x => x.Username == userDTO.Username && x.IsDeleted == false);

                    if (user == null)
                    {
                        return BadRequest(new ApiBadRequestWithMessage(PublicConstants.UserNotFoundError));

                    }
                    else
                    {
                        user.IsModerator = false;
                        user.IsAdmin = true;
                        user.ModifiedDate = DateTime.Now;
                        if (userDTO.ModifiedUserId != null)
                        {
                            user.ModifiedUserId = userDTO.ModifiedUserId;
                        }
                        context.SaveChanges();
                        return Ok(new ApiOkResponse(Mapper.Map<AdminDTO>(user)));
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiBadRequestWithMessage(PublicConstants.SysErrorMessage));
            }
        }

        [HttpGet]
        [Route("GetFirstUser")]
        public IActionResult GetFirstUser()
        {
            //return Ok(new ApiResponse((int)HttpStatusCode.BadRequest, "Sistem Hatası"));
            //return BadRequest(new ApiBadRequestResponse(ModelState));
            using (FoodInfoServiceContext foodInfoServiceContext = new FoodInfoServiceContext())
            {
                var user = foodInfoServiceContext.User.FirstOrDefault(x => x.Name == "Fatih1");
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiBadRequestResponse(ModelState));
                }
                if (user == null)
                {


                    return BadRequest(new ApiBadRequestWithMessage("User can not be found"));

                }
                return Ok(new ApiOkResponse(user));
            }

        }




    }

   
}