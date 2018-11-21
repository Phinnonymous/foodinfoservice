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

    public class ErrorController : ControllerBase
    {

        [HttpPost]
        [Route("GetErrorList")]
        public IActionResult GetErrorList()
        {
            var apiJsonResponse = new ApiJsonResponse();
            try
            {

                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {

                    var errors = context.Errors.Where(x => x.IsDeleted == false ).ToList();
                    if (errors != null)
                    {
                        List<ErrorDTO> errorDTOs= new List<ErrorDTO>();
                        foreach(var item in errors)
                        {
                            errorDTOs.Add(Mapper.Map<ErrorDTO>(item));

                        }
                        if (errorDTOs != null)
                        {
                            return apiJsonResponse.ApiOkContentResult(errorDTOs);
                        }
                        else
                        {
                            return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage + " Error list could not be loaded.");
                        }
                    }
                    else {

                        return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage + " Error list could not be loaded.");

                    }
                }

            }

            catch (Exception ex)
            {
                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage + " Error list could not be loaded.");


            }
        }
    }

}
