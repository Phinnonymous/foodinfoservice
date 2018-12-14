using AutoMapper;
using FoodInfo.Service.DTOs;
using FoodInfo.Service.Helper;
using FoodInfo.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;


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
                    if (userDTO.Username == string.Empty)
                    { return apiJsonResponse.ApiBadRequestWithMessage("Username is required"); }
                    if (userDTO.Email == string.Empty)
                    { return apiJsonResponse.ApiBadRequestWithMessage("Email is required"); }
                    if (userDTO.Password == string.Empty)
                    { return apiJsonResponse.ApiBadRequestWithMessage("Password is required"); }


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
        [HttpPost]
        [Route("UpdateEmail")]
        public IActionResult UpdateEmail(UpdateEmailDTO updateEmailDTO)
        {
            var apiJsonResponse = new ApiJsonResponse();
            try
            {
                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {
                    if (context.User.Any(x => x.IsDeleted == false && x.Email == updateEmailDTO.oldEmail))
                    {
                        if (context.User.Any(x => x.IsDeleted == false && x.Email == updateEmailDTO.newEmail))
                        {
                            return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.UserNameOrEmailAlreadyExistError);

                        }
                        else
                        {
                            var User = context.User.FirstOrDefault(x => x.IsDeleted == false && x.Email == updateEmailDTO.oldEmail);
                            User.Email = updateEmailDTO.newEmail;
                            User.ModifiedDate = DateTime.Now;
                            
                            context.SaveChanges();
                            return apiJsonResponse.ApiOkContentResult(updateEmailDTO);
                        }
                    }
                    else
                    {
                        return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.UserNotFoundError);
                    }

                }
            }
            catch
            { return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage); }

        }
        [HttpPost]
        [Route("UpdatePassword")]
        public IActionResult UpdatePassword (UpdatePasswordDTO updatePasswordDTO)
        {
            var apiJsonResponse = new ApiJsonResponse();

            try
            {
                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {
                    if(context.User.Any(x => x.IsDeleted == false && x.Username == updatePasswordDTO.Username))
                    {
                        var User = context.User.FirstOrDefault(x => x.IsDeleted == false && x.Username == updatePasswordDTO.Username);
                        if (updatePasswordDTO.NewPassword != string.Empty)
                        {
                            User.Password = HelperFunctions.ComputeSha256Hash(updatePasswordDTO.NewPassword);
                            User.ModifiedDate = DateTime.Now;
                            context.SaveChanges();
                            return apiJsonResponse.ApiOkContentResult(updatePasswordDTO);
                        }
                        else
                        { return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.PasswordRequired); }
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
        [Route("UpdateNameAndSurname")]
        public IActionResult UpdateNameAndSurname (UpdateNameAndSurnameDTO updateNameAndSurnameDTO)
        {
            var apiJsonResult = new ApiJsonResponse();
            try
            {
                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {
                    if(context.User.Any(x => x.Username == updateNameAndSurnameDTO.Username && x.IsDeleted == false))
                    {
                        var User = context.User.FirstOrDefault(x => x.Username == updateNameAndSurnameDTO.Username && x.IsDeleted == false);

                        if (updateNameAndSurnameDTO.NewName != string.Empty)
                        {
                            User.Name = updateNameAndSurnameDTO.NewName;
                            User.ModifiedDate = DateTime.Now;
                        }
                        if (updateNameAndSurnameDTO.NewSurname != string.Empty)
                        {
                            User.Surname = updateNameAndSurnameDTO.NewSurname;
                            User.ModifiedDate = DateTime.Now;
                        }
                        context.SaveChanges();
                        return apiJsonResult.ApiOkContentResult(updateNameAndSurnameDTO);
                    }
                    else
                    { return apiJsonResult.ApiBadRequestWithMessage(PublicConstants.UserNotFoundError); }
                }
            }
            catch
            {
                return apiJsonResult.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);
            }

        }





    }

}