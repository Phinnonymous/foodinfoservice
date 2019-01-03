using AutoMapper;
using FoodInfo.Service.DTOs;
using FoodInfo.Service.Helper;
using FoodInfo.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


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
                    if (languages != null && languages.Count != 0)
                    {
                        List<LanguageDTO> languageDTOs = new List<LanguageDTO>();

                        foreach (var item in languages)
                        {

                            languageDTOs.Add(Mapper.Map<LanguageDTO>(item));
                        }
                        if (languageDTOs != null)
                        {
                            return apiJsonResponse.ApiOkContentResult(languageDTOs);
                        }
                        else
                        {
                            return BadRequest(new ApiBadRequestWithMessage(PublicConstants.NoLanguageFound));
                        }
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
        [HttpPost]
        [Route("GetLanguageListOfProductByBarcodeId")]
        public IActionResult GetLanguageListOfProductByBarcodeId(BarcodeDTO barcodeDTO)
        {
            var apiJsonResponse = new ApiJsonResponse();
            try
            {
                if (barcodeDTO != null)
                {
                    using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                    {
                        var languageList = context.ProductContents.Where(x => x.Product.BarcodeId == barcodeDTO.BarcodeId)
                            .Include(x => x.Language).ToList();
                        List<LanguageDTO> languageDTO = new List<LanguageDTO>();
                        if (languageList != null)
                        {
                            foreach (var item in languageList)
                            {
                                languageDTO.Add(Mapper.Map<LanguageDTO>(item.Language));
                                var l = languageDTO;

                            }
                            return apiJsonResponse.ApiOkContentResult(languageDTO);
                        }
                        else
                        {
                            return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.NoLanguageFound);
                        }
                   
                    }

                }
                else
                {
                    return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.BarcodIdRequired);
                }
            }
            catch
            {
                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);
            }
        }


    }
}
