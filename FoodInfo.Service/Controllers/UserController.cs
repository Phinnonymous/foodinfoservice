using AutoMapper;
using FoodInfo.Service.DTOs;
using FoodInfo.Service.Helper;
using FoodInfo.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;


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
            var apiJsonResponse = new ApiJsonResponse();
            try
            {
                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {


                    if (context.User.Any(m => m.Email == userDTO.Email || m.Username == userDTO.Username))
                    {
                        return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.UserNameOrEmailAlreadyExistError);

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
                    return apiJsonResponse.ApiOkContentResult(userCredentialsOnSuccess);
                    //var isExistUsername = foodInfoServiceContext.User.FirstOrDefault(x => x.Username == "Fatihs");


                    //return Ok(new ApiOkResponse(foodInfoServiceContext.User.FirstOrDefault(x => x.Name == "Fatih")));



                }
            }
            catch (Exception ex)
            {
                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);
            }


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
            var apiJsonResponse = new ApiJsonResponse();
            try
            {
                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {
                    var user = context.User.FirstOrDefault(x => x.Username == userDTO.Username && x.IsDeleted == false);

                    if (user != null)
                    {
                        user.Password = null;
                        return apiJsonResponse.ApiOkContentResult(Mapper.Map<UserDTO>(user));
                    }
                    else
                    {
                        return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.UserNotFoundError);
                    }
                }
            }
            catch (Exception ex)
            {
                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);

            }

        }
        [HttpPost]
        [Route("SetModeratorByUsername")]
        public IActionResult SetModeratorByUsername(UserDTO userDTO)
        {
            var apiJsonResponse = new ApiJsonResponse();

            try
            {

                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {
                    var user = context.User.FirstOrDefault(x => x.Username == userDTO.Username && x.IsDeleted == false);

                    if (user == null)
                    {
                        return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.UserNotFoundError);

                    }
                    else
                    {
                        user.IsModerator = true;
                        user.IsAdmin = false;

                        if (userDTO.ModifiedUserId != null)
                        { user.ModifiedUserId = userDTO.ModifiedUserId; }
                        else
                        {
                            return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ModifiedUserIdRequired);
                        }
                        user.ModifiedDate = DateTime.Now;

                        context.SaveChanges();

                        return apiJsonResponse.ApiOkContentResult(Mapper.Map<ModeratorDTO>(user));
                    }
                }
            }
            catch (Exception ex)
            {
                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);
            }

        }

        [HttpPost]
        [Route("SetAdminByUsername")]
        public IActionResult SetAdminByUsername(UserDTO userDTO)
        {

            var apiJsonResponse = new ApiJsonResponse();

            try
            {
                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {
                    var user = context.User.FirstOrDefault(x => x.Username == userDTO.Username && x.IsDeleted == false);

                    if (user == null)
                    {
                        return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.UserNotFoundError);

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
                        else
                        {
                            return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ModifiedUserIdRequired);
                        }
                        context.SaveChanges();
                        return apiJsonResponse.ApiOkContentResult(Mapper.Map<AdminDTO>(user));
                    }
                }

            }
            catch (Exception ex)
            {
                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);
            }
        }

        [HttpPost]
        [Route("SetNormalUserByUsername")]
        public IActionResult SetNormalUserByUsername(UserDTO userDTO)
        {

            var apiJsonResponse = new ApiJsonResponse();

            try
            {
                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {
                    var user = context.User.FirstOrDefault(x => x.Username == userDTO.Username && x.IsDeleted == false);

                    if (user == null)
                    {
                        return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.UserNotFoundError);

                    }
                    else
                    {
                        user.IsModerator = false;
                        user.IsAdmin = false;
                        user.ModifiedDate = DateTime.Now;
                        if (userDTO.ModifiedUserId != null)
                        {
                            user.ModifiedUserId = userDTO.ModifiedUserId;
                        }
                        else
                        {
                            return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ModifiedUserIdRequired);
                        }
                        context.SaveChanges();
                        return apiJsonResponse.ApiOkContentResult(Mapper.Map<AdminDTO>(user));
                    }
                }

            }
            catch (Exception ex)
            {
                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);
            }
        }

        [HttpPost]
        [Route("DeleteUserByUsername")]
        public IActionResult DeleteUserByUsername(UserDTO userDTO)
        {

            var apiJsonResponse = new ApiJsonResponse();

            try
            {
                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {
                    var user = context.User.FirstOrDefault(x => x.Username == userDTO.Username && x.IsDeleted == false);

                    if (user == null)
                    {
                        return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.UserNotFoundError);

                    }
                    else
                    {
                        user.IsDeleted = true;
                        user.ModifiedDate = DateTime.Now;
                        if (userDTO.ModifiedUserId != null)
                        {
                            user.ModifiedUserId = userDTO.ModifiedUserId;
                        }
                        else
                        {
                            return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ModifiedUserIdRequired);
                        }
                        context.SaveChanges();
                        return apiJsonResponse.ApiOkContentResult(Mapper.Map<AdminDTO>(user));
                    }
                }

            }
            catch (Exception ex)
            {
                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);
            }
        }


        [HttpPost]
        [Route("GetAllUsersOrderByCreatedDate")]
        public IActionResult GetAllUsersOrderByCreatedDate()
        {
            var apiJsonResponse = new ApiJsonResponse();
            try
            {
                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {
                  var users =   context.User.Where(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedDate).ToList();

                    List<UserDTO> userList = new List<UserDTO>();
                    if (users.Count > 0)
                    {
                        foreach (var item in users)
                        {
                            item.Password = null;
                            userList.Add(Mapper.Map<UserDTO>(item));
                            
                        }

                        return apiJsonResponse.ApiOkContentResult(userList);
                    }
                    else
                    {
                        return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.UserNotFoundError);
                    }
                }
                
            }
            catch
            {
                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);

            }
        }


        [HttpPost]
        [Route("GetAllUsersOrderByName")]
        public IActionResult GetAllUsersOrderByName()
        {
            var apiJsonResponse = new ApiJsonResponse();
            try
            {
                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {
                    var users = context.User.Where(x => x.IsDeleted == false).OrderBy(x => x.Username).ToList();

                    List<UserDTO> userList = new List<UserDTO>();
                    if (users.Count > 0)
                    {
                        foreach (var item in users)
                        {
                            item.Password = null;
                            userList.Add(Mapper.Map<UserDTO>(item));

                        }

                        return apiJsonResponse.ApiOkContentResult(userList);
                    }
                    else
                    {
                        return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.UserNotFoundError);
                    }
                }

            }
            catch
            {
                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);

            }
        }




        [HttpGet]
        [Route("GetFirstUser")]
        public IActionResult GetFirstUser()
        {
            var apiJsonResponse = new ApiJsonResponse();

            //return Ok(new ApiResponse((int)HttpStatusCode.BadRequest, "Sistem Hatası"));
            //return BadRequest(new ApiBadRequestResponse(ModelState));
            using (FoodInfoServiceContext foodInfoServiceContext = new FoodInfoServiceContext())
            {
                var user = foodInfoServiceContext.User.FirstOrDefault();

                if (user == null)
                {


                    return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.UserNotFoundError);

                }
                return apiJsonResponse.ApiOkContentResult(Mapper.Map<UserDTO>(user));
            }

        }




    }

}