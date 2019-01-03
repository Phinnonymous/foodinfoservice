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
    public class CommentController : ControllerBase
    {
        [HttpPost]
        [Route("AddComment")]
        public IActionResult AddComment(CommentDTO commentDTO)
        {
            var apiJsonResponse = new ApiJsonResponse();

            try
            {
                if (commentDTO != null)
                {
                    if (commentDTO.UserComment == string.Empty)
                    {
                        return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.CommentCanNotBeEmpty);

                    }
                    else
                    {
                        using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                        {
                            Comment comment = new Comment();
                            try
                            {
                                comment.ProductContent = context.ProductContents.Where(x => x.ID == commentDTO.ProductContentId)
                                    .Include(m => m.Product)
                                    .Include(m => m.NutritionFact)
                                    .Include(m => m.Language)
                                    .Include(m => m.NutritionFact)
                                    .FirstOrDefault();

                            }
                            catch
                            {
                                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ProductContentIdRequired);
                            }

                            comment.UserComment = commentDTO.UserComment;
                            if (commentDTO.CreatedUserId == null)
                            {
                                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ProvideACreatedUserId);
                            }
                            comment.CreatedUserId = commentDTO.CreatedUserId;

                            context.Add(comment);
                            context.SaveChanges();
                            return apiJsonResponse.ApiOkContentResult(commentDTO);

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
        [Route("DeleteCommentById")]
        public IActionResult DeleteCommentById(DeleteCommentDTO deleteCommentDTO)
        {

            var apiJsonResponse = new ApiJsonResponse();

            try
            {
                if (deleteCommentDTO != null)
                {
                    if (deleteCommentDTO.ModifiedUserId != null)
                    {
                        using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                        {
                            try
                            {
                                var comment = context.Comments.Where(x => x.ID == deleteCommentDTO.Id && x.IsDeleted == false).FirstOrDefault();
                                comment.IsDeleted = true;
                                comment.ModifiedDate = DateTime.Now;
                                comment.ModifiedUserId = deleteCommentDTO.ModifiedUserId;
                            }
                            catch
                            {
                                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.CommentNotFound);
                            }
                            context.SaveChanges();
                            return apiJsonResponse.ApiOkContentResult(deleteCommentDTO);

                        }

                    }
                    else
                    {
                        return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.ModifiedUserIdRequired);
                    }


                }
                else
                { return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage); }
            }
            catch
            {
                return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.SysErrorMessage);

            }
        }

    }
}