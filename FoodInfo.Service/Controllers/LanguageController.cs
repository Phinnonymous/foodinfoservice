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

    public class LanguageController : ControllerBase
    {

        [HttpPost]
        [Route("GetLanguageList")]
        public IActionResult GetLanguageList()
        {
            var apiJsonResponse = new ApiJsonResponse();
            try
            {

                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {

                   var languages = context.Languages.Where(x => x.IsDeleted == false).ToList();
                    if (languages != null)  
                    {
                        List<LanguageDTO> languageDTOs = new List<LanguageDTO>();
                        
                        foreach(var item in languages)
                        {

                            languageDTOs.Add(Mapper.Map<LanguageDTO>(item));
                        }
                        if (languageDTOs != null)
                        {
                            return apiJsonResponse.ApiOkContentResult(languageDTOs);
                        }
                        else return BadRequest(new ApiBadRequestWithMessage(PublicConstants.NoLanguageFound));
                    }
                    else
                    {
                        return BadRequest(new ApiBadRequestWithMessage(PublicConstants.NoLanguageFound));
                    }
                }
                
            }
            catch (Exception ex)
            {

                return BadRequest(new ApiBadRequestWithMessage(PublicConstants.SysErrorMessage));
            }
        }
    }
}
