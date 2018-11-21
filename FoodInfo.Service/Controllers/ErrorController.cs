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
            try
            {

                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {

                    var errors = context.Errors.Where(x => x.IsDeleted == false ).ToList();
                    if (errors != null)
                    {
                       
                        return Ok(new ApiOkResponse(Mapper.Map<ErrorDTO>(errors)));

                    }
                    else { 

                        return BadRequest(new ApiBadRequestWithMessage(PublicConstants.SysErrorMessage + " Error list could not be loaded."));

                    }
                }

            }

            catch (Exception ex)
            {
                return BadRequest(new ApiBadRequestWithMessage(PublicConstants.SysErrorMessage + " Error list could not be loaded."));

            }
        }
    }

}
