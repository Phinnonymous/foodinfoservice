using FoodInfo.Service.DTOs;
using FoodInfo.Service.Helper;
using FoodInfo.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace FoodInfo.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {



        [HttpPost]
        [Route("AddVote")]
        public IActionResult AddVote(VoteDTO voteDTO)
        {
            var apiJsonResponse = new ApiJsonResponse();
            try
            {
                if (voteDTO != null)

                {
                    if (voteDTO.UserVote != null && voteDTO.CreatedUserId != null && voteDTO.BarcodeID != string.Empty)
                    {
                        using (FoodInfoServiceContext context = new FoodInfoServiceContext())
                        {
                            if (context.Products.Any(x => x.BarcodeId == voteDTO.BarcodeID && x.IsDeleted== false))
                            {

                                {
                                    var vote = new Vote();
                                    vote.Product = context.Products.Where(x => x.BarcodeId == voteDTO.BarcodeID && x.IsDeleted == false).FirstOrDefault();

                                    if(context.Votes.Any(x =>x.CreatedUserId== voteDTO.CreatedUserId && x.Product.ID == vote.Product.ID && vote.IsDeleted == false))
                                    {

                                        return apiJsonResponse.ApiBadRequestWithMessage(PublicConstants.UserAlreadyVoteThisProduct);
                                    }


                                    vote.UserVote = voteDTO.UserVote;
                                    vote.CreatedUserId = voteDTO.CreatedUserId;

                                    context.Add(vote);
                                    context.SaveChanges();
                                    return apiJsonResponse.ApiOkContentResult(voteDTO);

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
