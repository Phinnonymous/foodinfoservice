using AutoMapper;
using FoodInfo.Service.DTOs;
using FoodInfo.Service.Helper;
using FoodInfo.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
                                if (!context.ProductCategories.Any(x => x.ID == productDTO.ProductCategory.ID && x.IsDeleted == false))
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

                            var barcodeIdAndNameDTO = new BarcodeIdAndNameDTO();
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
        [HttpPost]
        [Route("SearchProductByName")]
        public IActionResult SearchProductByName(string productName)
        {
            var apiJsonResponse = new ApiJsonResponse();
            try
            {

                if (productName != string.Empty)
                {
                    using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                    {
                        if (productName.Length >= 3)
                        {
                            List<SearchByNameDTO> searchByNameDTOs = new List<SearchByNameDTO>();
                            try
                            {
                                List<Product> products = context.Products.Where(x => x.ProductName.Contains(productName) && x.IsDeleted == false).ToList();

                                if (products.Count == 0)
                                {
                                    return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ProductNotFound);
                                }
                                foreach (var item in products)
                                {
                                    searchByNameDTOs.Add(Mapper.Map<SearchByNameDTO>(item));

                                }

                                return apiJsonResponse.ApiOkContentResult(searchByNameDTOs);
                            }
                            catch
                            {

                                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ProductNotFound);
                            }







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
            catch (Exception)
            {
                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);
            }
        }



        [HttpPost]
        [Route("DeleteProduct")]
        public IActionResult DeleteProduct(string BarcodeId)
        {
            var apiJsonResponse = new ApiJsonResponse();
            try
            {
                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {
                    if (BarcodeId != string.Empty)
                    {
                        var product = context.Products.Where(x => x.BarcodeId == BarcodeId && x.IsDeleted == false).FirstOrDefault();
                        var contents = context.ProductContents.Where(x => x.Product.BarcodeId == BarcodeId && x.IsDeleted == false).ToList();
                        if (contents != null )
                        {
                            foreach (var item in contents)
                            {
                                item.IsDeleted = true;
                            }
                        }
                        if (product != null)
                        {
                            product.IsDeleted = true;
                        }

                        context.SaveChanges();
                        return apiJsonResponse.ApiOkContentResult(BarcodeId);




                    }
                    else
                    {
                        return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.BarcodIdRequired);
                    }


                }
            }
            catch (Exception ex)
            {
                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);

            }

        }

        [HttpPost]
        [Route("AllProducts")]
        public IActionResult AllProduct()
        {
            var apiJsonResponse = new ApiJsonResponse();
            try
            {
                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {
                    var products = context.Products.Where(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedDate).ToList();
                    var productsDTO = new List<ProductDTO>();
                    foreach(var item in products)
                    {
                        item.FirstImage = null;
                        item.SecondImage = null;
                        item.ThirdImage = null; 

                        productsDTO.Add(Mapper.Map<ProductDTO>(item));
                    }
                    return apiJsonResponse.ApiOkContentResult(productsDTO);
                }
            }
            catch
            {
                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);
            }
        }


        [HttpPost]
        [Route("GroupProducts")]
        public IActionResult GroupProducts(GroupProdutsDTO groupProdutsDTO)
        {
            var apiJsonResponse = new ApiJsonResponse(); 
            try
            {
                if(groupProdutsDTO != null )
                {

                    using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                    {
                       var product =  context.Products.OrderByDescending(x => x.ProductGroupId).FirstOrDefault();
                        if(groupProdutsDTO.ModifiedUserId == null)
                        {
                            return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ModifiedUserIdRequired);
                        }


                        if(groupProdutsDTO.BarcodeId.Count < 2)
                        {
                            return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.PleaseSelectMoreThanOneProduct);
                        }
                        foreach (var item in groupProdutsDTO.BarcodeId)
                        {
                            var groupedProduct = context.Products.Where(x => x.BarcodeId == item).FirstOrDefault();
                            if(groupedProduct  == null )
                            {
                                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ProductNotFound);
                            }
                            if(groupedProduct.ProductGroupId != 0 )
                            {
                                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ProductHasAlreadyGroup);
                            }
                            groupedProduct.ProductGroupId = product.ProductGroupId + 1;
                            groupedProduct.ModifiedUserId = groupProdutsDTO.ModifiedUserId;
                            groupedProduct.ModifiedDate = DateTime.Now;
                            groupProdutsDTO.ProductGroupId = groupedProduct.ProductGroupId;
                            
                        }

                        context.SaveChanges();
                        return apiJsonResponse.ApiOkContentResult(groupProdutsDTO);
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