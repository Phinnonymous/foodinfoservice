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

    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        [HttpPost]
        [Route("GetProductCategoriesByLanguageCode")]
        public IActionResult GetProductCategoriesByLanguageCode(LanguageCodeDTO languageCodeDTO)
        {
            var apiJsonResponse = new ApiJsonResponse();

            try
            {
                if (languageCodeDTO.LanguageCode != string.Empty)
                {
                    using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                    {
                        
                        var categories = context.ProductCategories.Where(x => x.IsDeleted == false && x.LanguageCode == languageCodeDTO.LanguageCode).ToList();
                        if (categories != null && categories.Count != 0)
                        {
                            List<CategoryNameDTO> categoryNames = new List<CategoryNameDTO>();

                            foreach (var item in categories)
                            {

                                categoryNames.Add(Mapper.Map<CategoryNameDTO>(item));
                            }
                            if (categoryNames != null)
                            {
                                return apiJsonResponse.ApiOkContentResult(categoryNames);
                            }
                            else
                            {
                                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.NoCategoryFound);
                            }
                        }
                        else
                        {
                            return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.NoCategoryFound);

                        }

                    }
                }
                else
                {
                    return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ProvideLanguageCode);
                }
            }
            catch (Exception ex)
            {
                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);
            }
        }
        [HttpPost]
        [Route("AddCategory")]
        public IActionResult AddCategory(CategoryNameDTO category)
        {
            var apiJsonResponse = new ApiJsonResponse();

            try 

            {
                if (category != null)
                {
                    if (category.CategoryName != string.Empty && category.LanguageCode != string.Empty)
                    {
                        using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                        {
                            if (!context.ProductCategories.Any(x => x.CategoryName == category.CategoryName && x.IsDeleted == false && x.LanguageCode == category.LanguageCode))
                            {
                                if (context.Languages.Any(x => x.LanguageCode == category.LanguageCode && x.IsDeleted == false))
                                {
                                    if(category.CreatedUserId == null)
                                    {
                                        return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ProvideACreatedUserId);
                                    }
                                    context.ProductCategories.Add(Mapper.Map<ProductCategory>(category));
                                    context.SaveChanges();

                                    return apiJsonResponse.ApiOkContentResult(category);
                                }
                                else
                                {
                                    return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ProvideLanguageCode);
                                }
                            }
                            else
                            {
                                return apiJsonResponse.ApiBadRequestWithMessageAndObject(category, PublicConstants.AlreadyACategoryDefinedWithThisName);
                            }
                        }
                    }


                    else
                    {
                        return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ProvideCategoryName);
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
        [Route("DeleteCategoryByLanguageCode")]
        public IActionResult DeleteCategoryByLanguageCode (CategoryNameDTO categoryNameDTO)
        {
            var apiJsonResponse = new ApiJsonResponse();

            try
            {
                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {
                    if (categoryNameDTO != null)
                    {
                        var result = context.ProductCategories.FirstOrDefault(x => x.CategoryName == categoryNameDTO.CategoryName && x.LanguageCode == categoryNameDTO.LanguageCode);
                        if (result != null)
                        {
                            if (result.IsDeleted == false)
                            {
                                result.ModifiedDate = DateTime.Now;
                                if (categoryNameDTO.ModifiedUserId == null)
                                { return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ProivdeModifiedUserId); }
                                result.ModifiedUserId = categoryNameDTO.ModifiedUserId;
                                result.IsDeleted = true;
                                context.SaveChanges();
                                return apiJsonResponse.ApiOkContentResult(categoryNameDTO);
                            }
                            else
                            {
                                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.NoCategoryFound);
                            }
                        }
                        else
                        {
                            return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.NoCategoryFound);

                        }
                    }
                    else
                    { return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage); }
                                             
                           
                    
                }
            }
            catch (Exception ex)
            {
                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);
                
            }
        }

        [HttpPost]
        [Route("UpdateCategoryName")]
        public IActionResult UpdateCategoryName(UpdateCategoryNameDTO categoryNameDTO)
        {
            var apiJsonResponse = new ApiJsonResponse();
            try
            {
                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {
                    if (categoryNameDTO != null)
                    {
                        var result = context.ProductCategories.FirstOrDefault(x => x.CategoryName == categoryNameDTO.OldCategoryName);
                        if (result != null)
                        {
                            if (result.IsDeleted == false)
                            {
                                result.ModifiedDate = DateTime.Now;
                                if(categoryNameDTO.ModifiedUserId == null)
                                
                                { return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ProivdeModifiedUserId); }

                                
                                result.ModifiedUserId = categoryNameDTO.ModifiedUserId;
                                result.CategoryName = categoryNameDTO.NewCategoryName;
                                context.SaveChanges();
                                return apiJsonResponse.ApiOkContentResult(categoryNameDTO);
                            }
                            else
                            {
                                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.NoCategoryFound);
                            }
                        }
                        else
                        {
                            return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.NoCategoryFound);

                        }
                    }
                    else
                    { return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage); }



                }
            }
            catch (Exception ex)
            {
                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);

            }
        }

    }
}