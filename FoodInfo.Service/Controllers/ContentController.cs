﻿using AutoMapper;
using FoodInfo.Service.DTOs;
using FoodInfo.Service.Helper;
using FoodInfo.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                                    contentDTO.Product.ID = context.Products.FirstOrDefault(x => x.BarcodeId == contentDTO.Product.BarcodeId && x.IsDeleted == false).ID;
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

        [HttpPost]
        [Route("GetProductContentByLanguageCode")]
        public IActionResult GetProductContentByLanguageCode(LanguageAndProductDTO languageAndProductDTO)
        {
            var apiJsonResponse = new ApiJsonResponse();
            try
            {
                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {
                    if (languageAndProductDTO.LanguageCode != null)
                    {
                        if (context.ProductContents.Any(x => x.Language.LanguageCode == languageAndProductDTO.LanguageCode && x.IsDeleted == false && x.Product.BarcodeId == languageAndProductDTO.BarcodeId))
                        {
                            var product = context.ProductContents.Where(x =>
                                    x.Language.LanguageCode == languageAndProductDTO.LanguageCode &&
                                    x.IsDeleted == false && x.Product.BarcodeId == languageAndProductDTO.BarcodeId)
                                .Include(m => m.NutritionFact)
                                .Include(m => m.Product).FirstOrDefault();
                            return apiJsonResponse.ApiOkContentResult(Mapper.Map<ContentDTO>(product));
                        }
                        else
                        {
                            return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.BarcodeIdOrLanguageCodeDoesNotFound);
                        }
                    }
                    else
                    {
                        return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ProvideLanguageCode);
                    }
                }
            }
            catch (Exception ex)
            {
                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);
            }
        }

        [HttpPost]
        [Route("SetInitialImagesByBarcodeId")]
        public IActionResult SetInitialImagesByBarcodeId(ImageDTO imageDto)
        {
            var apiJsonResponse = new ApiJsonResponse();
            try
            {
                if (imageDto != null)
                {
                    using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                    {
                        if (context.Products.Any(x => x.BarcodeId == imageDto.BarcodeId && x.IsDeleted == false))
                        {
                            var product = context.Products.FirstOrDefault(x =>
                                x.BarcodeId == imageDto.BarcodeId && x.IsDeleted == false);
                            if (imageDto.FirstImage == null && imageDto.SecondImage == null && imageDto.ThirdImage == null)

                            {
                                return
                                    apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ProvideAtLeastOneImage);
                            }

                            if (imageDto.FirstImage != null)
                            {
                                product.FirstImage = imageDto.FirstImage;
                            }

                            if (imageDto.SecondImage != null)
                            {
                                product.SecondImage = imageDto.SecondImage;
                            }

                            if (imageDto.ThirdImage != null)
                            {
                                product.ThirdImage = imageDto.ThirdImage;
                            }

                            context.SaveChanges();

                            return apiJsonResponse.ApiOkContentResult();


                        }
                        else
                        {
                            return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ProductNotFound);
                        }

                    }
                }
                else
                {
                    return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);
                }

            }
            catch (Exception ex)
            {
                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);
            }
        }
    }
}