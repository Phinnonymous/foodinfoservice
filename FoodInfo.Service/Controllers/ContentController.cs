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
                                using (var transaction = context.Database.BeginTransaction())
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
                                    // contentDTO.NutritionFact.ID = context.NutritionFacts.FirstOrDefault(x => x.ID == contentDTO.NutritionFact.ID && x.IsDeleted == false).ID;

                                    var nutritionFacts = new NutritionFacts();

                                    nutritionFacts = Mapper.Map<NutritionFacts>(contentDTO.NutritionFact);
                                    nutritionFacts.BarcodeId = contentDTO.Product.BarcodeId;
                                    nutritionFacts.CreatedUserId = contentDTO.CreatedUserId;
                                    nutritionFacts.LanguageCode = contentDTO.Language.LanguageCode;

                                    context.Add(nutritionFacts);
                                    context.SaveChanges();
                                    contentDTO.NutritionFact.ID = context.NutritionFacts.Where(x => x.BarcodeId == contentDTO.Product.BarcodeId && x.LanguageCode == contentDTO.Language.LanguageCode && x.IsDeleted == false).FirstOrDefault().ID;

                                    var content = context.ProductContents.Add(Mapper.Map<ProductContent>(contentDTO));

                                    context.SaveChanges();



                                    transaction.Commit();
                                    return apiJsonResponse.ApiOkContentResult(Mapper.Map<ProductContent>(contentDTO));
                                }
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
                        if (context.Products.Any(x => x.BarcodeId == languageAndProductDTO.BarcodeId))
                        {
                            if (context.ProductContents.Any(x => x.Language.LanguageCode == languageAndProductDTO.LanguageCode && x.IsDeleted == false && x.Product.BarcodeId == languageAndProductDTO.BarcodeId))
                            {
                                var product = context.ProductContents.Where(x => x.Product.BarcodeId == languageAndProductDTO.BarcodeId
                                        && x.Language.LanguageCode == languageAndProductDTO.LanguageCode &&
                                        x.IsDeleted == false)
                                    .Include(m => m.NutritionFact)
                                    .Include(m => m.Product).FirstOrDefault();
                                ContentDTO contentDTO = Mapper.Map<ContentDTO>(product);
                                try
                                {
                                    var comments = context.Comments.Where(x => x.ProductContent.ID == product.ID && x.IsDeleted == false).ToList();

                                    if (comments != null)
                                    {

                                        List<CommentDTO> commentDTOs = new List<CommentDTO>();
                                        // contentDTO.Comments = Mapper.Map<List<CommentDTO>>(comments);
                                        int i = 0;
                                        foreach (var item in comments)
                                        {

                                            var commentDTO = new CommentDTO();
                                            var temp = context.User.Where(x => x.ID == item.CreatedUserId).FirstOrDefault();
                                            commentDTO.CreatedDate = item.CreatedDate;
                                            commentDTO.CreatedUserId = item.CreatedUserId;
                                            commentDTO.Name = temp.Name;
                                            commentDTO.Surname = temp.Surname;
                                            commentDTO.Username = temp.Username;
                                            commentDTO.UserComment = item.UserComment;
                                            commentDTO.ProductContentId = item.ProductContent.ID;
                                            commentDTO.ModifiedDate = item.ModifiedDate;
                                            commentDTO.ModifiedUserId = item.ModifiedUserId;



                                            commentDTOs.Add(commentDTO);



                                        }

                                        contentDTO.Comments = commentDTOs;
                                    }


                                }
                                catch
                                {

                                }


                                int totalVotes = context.Votes.Count(x => x.Product.BarcodeId == languageAndProductDTO.BarcodeId);
                                if (totalVotes == 0)
                                {
                                    contentDTO.AverageVote = 5.0M;
                                }
                                else
                                {
                                    int count = 0;
                                    foreach (var item in context.Votes.Where(x => x.Product.BarcodeId == languageAndProductDTO.BarcodeId))
                                    {
                                        if (item.UserVote != null)
                                        {
                                            count += (int)item.UserVote;
                                        }
                                    }
                                    if (count != 0)
                                    { contentDTO.AverageVote = decimal.Round((count / (decimal)totalVotes), 2); }
                                    else
                                    {
                                        contentDTO.AverageVote = 5.0M;
                                    }
                                }


                                return apiJsonResponse.ApiOkContentResult(contentDTO);
                            }
                            else
                            {
                                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.BarcodeIdOrLanguageCodeDoesNotFound);
                            }
                        }
                        else
                        {
                            return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ProductDoesNotFound);
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


        [HttpPost]
        [Route("DeleteContent")]
        public IActionResult DeleteContent(LanguageAndProductDTO languageAndProductDTO)
        {
            var apiJsonResponse = new ApiJsonResponse();
            try
            {
                using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                {
                    var product = context.ProductContents.Where(x => x.Product.BarcodeId == languageAndProductDTO.BarcodeId && x.Language.LanguageCode == languageAndProductDTO.LanguageCode && x.IsDeleted == false).FirstOrDefault();
                    if (product == null)
                    {
                        return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ProductDoesNotFound);
                    }
                    product.IsDeleted = true;

                    context.SaveChanges();
                    return apiJsonResponse.ApiOkContentResult(languageAndProductDTO);
                }
            }
            catch (Exception ex)
            {


                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);
            }


        }


        [HttpPost]
        [Route("UpdateContent")]
        public IActionResult UpdateContent(ContentDTO contentDTO)

        //Product->BarcodeId && Language-> LanguageCode is necessary
        {
            var apiJsonResponse = new ApiJsonResponse();
            try
            {
                if(contentDTO != null )
                {
                    using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                    {

                        if (context.ProductContents.Any(x => x.Product.BarcodeId == contentDTO.Product.BarcodeId && x.Language.LanguageCode == contentDTO.Language.LanguageCode && x.IsDeleted == false ))
                        {
                            if(contentDTO.NutritionFact != null  )
                            {

                             var content =    context.ProductContents.Where(x => x.Product.BarcodeId == contentDTO.Product.BarcodeId && x.Language.LanguageCode == contentDTO.Language.LanguageCode && x.IsDeleted ==false).FirstOrDefault();




                                contentDTO.NutritionFact.ID = 0;
                                content.NutritionFact = Mapper.Map<NutritionFacts>(contentDTO.NutritionFact);
                                content.CookingTips = contentDTO.CookingTips;
                                content.Details = contentDTO.Details;
                                content.Ingredients = contentDTO.Ingredients;
                                content.Language = Mapper.Map<Language>(contentDTO.Language);
                                content.Recommendations = contentDTO.Recommendations;
                                content.VideoURL = contentDTO.VideoURL;
                                content.Warnings = contentDTO.Warnings;
                                content.ModifiedDate = DateTime.Now;
                                if(contentDTO.ModifiedUserId ==null)
                                {

                                    return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ModifiedUserIdRequired);

                                }
                                content.ModifiedUserId = contentDTO.ModifiedUserId;
                                context.SaveChanges();
                                return apiJsonResponse.ApiOkContentResult(contentDTO);
                                
                            }
                            else
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