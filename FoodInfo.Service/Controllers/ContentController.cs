using AutoMapper;
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
    public class ContentController : ControllerBase
    {

        [HttpPost]
        [Route("CreateContentOfProduct")]
        public IActionResult CreateContentOfProduct(ContentDTO contentDTO)
        {
            var apiJsonResponse = new ApiJsonResponse();
            try
            {
                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {
                    if (contentDTO != null)
                    {
                        if (contentDTO.Product.BarcodeId != null)
                        {

                            if (contentDTO.CreatedUserId != null)
                            {
                                if (context.Products.Any(x => x.BarcodeId == contentDTO.Product.BarcodeId && x.IsDeleted == false))
                                {
                                    contentDTO.Product.ID = context.Products.FirstOrDefault(x => x.BarcodeId == contentDTO.Product.BarcodeId && x.IsDeleted== false).ID;
                                }
                               
                                else
                                {
                                    return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.BarcodIdRequired);
                                }
                                if (context.Languages.Any(x => x.LanguageCode == contentDTO.Language.LanguageCode && x.IsDeleted == false))
                                {
                                    contentDTO.Language.ID = context.Languages.FirstOrDefault(x => x.LanguageCode == contentDTO.Language.LanguageCode && x.IsDeleted == false).ID;
                                }
                                else
                                {
                                    return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ProvideLanguageCode);
                                }
                                if (context.ProductContents.Any(x => x.Product.ID == contentDTO.Product.ID && x.IsDeleted == false && x.Language.ID == contentDTO.Language.ID))
                                {
                                    return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ExistingContentForProduct);
                                }
                                contentDTO.NutritionFact.ID = context.NutritionFacts.FirstOrDefault(x => x.ID == contentDTO.NutritionFact.ID && x.IsDeleted == false).ID;
                                
                                var content = context.ProductContents.Add(Mapper.Map<ProductContent>(contentDTO));
                                context.SaveChanges();
                                return apiJsonResponse.ApiOkContentResult(Mapper.Map<ProductContent>(contentDTO));
                            }
                            else
                            {
                                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ProvideACreatedUserId);
                            }
                        }
                        else
                        {
                            return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.BarcodIdRequired);
                        }
                    }
                    else
                    { return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage); }
                }

            }
            catch (Exception ex)
            {
                { return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage); }


            }
        }

    }
}