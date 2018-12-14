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
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        [HttpPost]
        [Route("CreateProduct")]
        public IActionResult CreateProduct(ProductDTO productDTO)
        {
            var apiJsonResponse = new ApiJsonResponse();
            try
            {
                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {
                    if (productDTO != null)
                    {
                        return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);

                        if (productDTO.BarcodeId != null)

                        {
                            return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);

                        }
                    }
                    else
                    {
                        return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);

                throw;
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