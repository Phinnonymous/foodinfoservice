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

                        if (productDTO.BarcodeId == string.Empty && productDTO.ProductName == string.Empty )

                        {
                            return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);

                        }
                        else
                        {
                            if (!context.Products.Any(x => x.BarcodeId == productDTO.BarcodeId))
                            {
                                var product = new Product();

                                //product.BarcodeId = productDTO.BarcodeId;
                                //product.ProductName = productDTO.ProductName;
                                //product.ProductCategory = productDTO.ProductCategory;
                                //product.CreatedUserId = productDTO.CreatedUserId;
                                //if(productDTO.FirstImage != null)
                                //{
                                //    product.FirstImage = productDTO.FirstImage;

                                //}
                                //if (productDTO.SecondImage != null)
                                //{
                                //    product.SecondImage = productDTO.SecondImage;

                                //}
                                //if (productDTO.ThirdImage != null)
                                //{
                                //    product.ThirdImage = productDTO.ThirdImage;

                                //}
                                
                                product = Mapper.Map<Product> (productDTO);
                                product.ProductCategory = context.ProductCategories.Where(x => x.ID == productDTO.Id).FirstOrDefault();
                                
                                context.Add(product);
                                context.SaveChanges();
                                return apiJsonResponse.ApiOkContentResult(productDTO);
                            }
                            else
                            {
                                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);
                            }

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
    }
}