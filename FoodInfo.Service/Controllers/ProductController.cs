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

                        if (productDTO.BarcodeId == string.Empty && productDTO.ProductName == string.Empty)

                        {
                            return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);

                        }
                        else
                        {
                            if (!context.Products.Any(x => x.BarcodeId == productDTO.BarcodeId && x.IsDeleted == false))
                            {
                                var product = new Product();
                                
                                //product.BarcodeId = productDTO.BarcodeId;
                                //product.ProductName = productDTO.ProductName;
                                //product.ProductCategory = productDTO.ProductCategory;
                                //product.CreatedUserId = productDTO.CreatedUserId;
                                
                               
                                

                                product = Mapper.Map<Product>(productDTO);
                                if (productDTO.FirstImage != null)
                                {
                                    product.FirstImage = productDTO.FirstImage;

                                }
                                else
                                {
                                    product.FirstImage = null;
                                }
                                if (productDTO.SecondImage != null)
                                {
                                    product.SecondImage = productDTO.SecondImage;

                                }
                                else
                                {
                                    product.SecondImage = null;
                                }
                                if (productDTO.ThirdImage != null)
                                {
                                    product.ThirdImage = productDTO.ThirdImage;

                                }
                                else
                                {
                                    product.ThirdImage = null;
                                }
                                if(!context.ProductCategories.Any(x => x.ID == productDTO.ProductCategory.ID && x.IsDeleted == false ))
                                {
                                    return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.NoCategoryFound);
                                }
                                product.ProductCategory = context.ProductCategories.Where(x => x.ID == productDTO.ProductCategory.ID && x.IsDeleted == false).FirstOrDefault();

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
        [HttpPost]
        [Route("GetProductNameByBarcodeId")]
        public IActionResult GetProductNameByBarcodeId(BarcodeDTO barcodeDTO)
        {
            var apiJsonResponse = new ApiJsonResponse();
            try
            {
                if (barcodeDTO.BarcodeId != string.Empty)
                {
                    using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                    {
                        if (context.Products.Any(x => x.BarcodeId == barcodeDTO.BarcodeId && x.IsDeleted == false))

                        {

                            var barcodeIdAndNameDTO= new BarcodeIdAndNameDTO();
                            barcodeIdAndNameDTO.BarcodeId = barcodeDTO.BarcodeId;
                            barcodeIdAndNameDTO.ProductName = context.Products.FirstOrDefault(x => x.BarcodeId == barcodeIdAndNameDTO.BarcodeId && x.IsDeleted == false).ProductName;

                            return apiJsonResponse.ApiOkContentResult(barcodeIdAndNameDTO);

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
            catch
            {
                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);

            }
        }
    }
}