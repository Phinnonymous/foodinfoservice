using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FoodInfo.Service.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace FoodInfo.Service.Controllers
{
    public class ApiJsonResponse : ControllerBase


    {

        public ApiJsonResponse()
        {


        }

        public ContentResult ApiOkContentResult(object objectResult)

        {

            var okResponseObject = new OkResponseDTO();
            okResponseObject.Result = objectResult;
            okResponseObject.statusCode = (int)HttpStatusCode.OK;
            return Content(JsonConvert.SerializeObject(okResponseObject),contentType:"application/json");


            

        }
        public ObjectResult ApiBadRequestWithMessage (string Message)
        {
            var badReqestObject = new BadRequestDTO();
            badReqestObject.Message = Message;
            badReqestObject.statusCode = (int)HttpStatusCode.BadRequest;
            return BadRequest(badReqestObject);
        }
        public ObjectResult ApiBadRequestWithObject (object objectResult)
        {
            var badRequestObject = new BadRequestDTO();
            badRequestObject.statusCode = (int)HttpStatusCode.BadRequest;
            badRequestObject.Result = objectResult;
            return BadRequest(badRequestObject);
        }
        public ObjectResult ApiBadRequestWithMessageAndObject (object objectResult , string Message)
        {
            var badRequestObject = new BadRequestDTO();
            badRequestObject.Message = Message;
            badRequestObject.Result = objectResult;
            badRequestObject.statusCode = (int)HttpStatusCode.BadRequest;
            return BadRequest(badRequestObject);
            
            
        }

    }
}
