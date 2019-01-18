using AutoMapper;
using FoodInfo.Service.DTOs;
using FoodInfo.Service.Helper;
using FoodInfo.Service.Models;
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
                        var languageList = context.ProductContents.Where(x => x.Product.BarcodeId == barcodeDTO.BarcodeId && x.IsDeleted == false)
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


        [HttpPost]
        [Route("GetLanguageListOfProductByBarcodeId2")]
        public IActionResult GetLanguageListOfProductByBarcodeId2(BarcodeDTO barcodeDTO)
        {
            var apiJsonResponse = new ApiJsonResponse();
            try
            {
                if (barcodeDTO != null)
                {
                    using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                    {

                        var groupID = context.Products.Where(x => x.BarcodeId == barcodeDTO.BarcodeId && x.IsDeleted == false).FirstOrDefault().ProductGroupId;
                        if (groupID != 0 && groupID != null)
                        {
                            var productList = context.Products.Where(x => x.ProductGroupId == groupID && x.IsDeleted == false).ToList();
                            List<ProductContent> languageList = new List<ProductContent>();
                            foreach (var item in productList)
                            {
                                ProductContent language= new ProductContent();
                                
                                language = context.ProductContents.Where(x => x.Product.BarcodeId == item.BarcodeId && x.IsDeleted == false).
                                    Include(x => x.Language).FirstOrDefault();
                                languageList.Add(language);
                            }
                            List<ProductAndLanguageListDTO> languageDTO = new List<ProductAndLanguageListDTO>();
                            if (languageList != null)
                            {
                                foreach (var item in languageList)
                                {

                                    ProductAndLanguageListDTO language = new ProductAndLanguageListDTO();
                                    language.BarcodeId = item.Product.BarcodeId;
                                    language.ID = item.Language.ID;
                                    language.LanguageCode = item.Language.LanguageCode;
                                    language.LanguageName = item.Language.LanguageName;
                                    language.NativeLanguageName = item.Language.NativeLanguageName;
                                    if (!languageDTO.Any(x => x.LanguageCode == language.LanguageCode))
                                    {
                                        languageDTO.Add(language);
                                    }
                                   // languageDTO.Add(Mapper.Map<LanguageDTO>(item.Language));
                                    
                                    var l = languageDTO;

                                }
                                return apiJsonResponse.ApiOkContentResult(languageDTO);
                            }
                            else
                            {
                                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);
                            }
                        }
                        else
                        {


                            var languageList = context.ProductContents.Where(x => x.Product.BarcodeId == barcodeDTO.BarcodeId && x.IsDeleted == false)
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
