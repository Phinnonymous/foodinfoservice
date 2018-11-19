using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using FoodInfo.Service.Models;
using FoodInfo.Service.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;

namespace FoodInfo.Service.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpPost]

        public IActionResult Get(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiBadRequestResponse(ModelState));


                }

            }
            catch { }

            //    if (request == null)
            //    {
            //        return NotFound(new ApiResponse(404, $"Product not found with id {request.value}"));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    try
            //    {

            //        return BadRequest(new ApiBadRequestResponse(ModelState));

            //    }
            //    catch (Exception ep){ }
            //    }

            using (FoodInfoServiceContext foodInfoServiceContext = new FoodInfoServiceContext())
            {

                return Ok(new ApiOkResponse(foodInfoServiceContext.User.FirstOrDefault(x => x.Name == "Fatih")));

            }


            //mymodel a = new mymodel();
            //a.name = "Fatih";
            //a.surname = new string[] { "Cankurtaran", "Cankurtaran2" };
            //a.age = "21";
            //mysecondmodel mysecondmodel = new mysecondmodel();
            //mysecondmodel.notlari = "1";
            //List<mysecondmodel> mysecondmodels = new List<mysecondmodel>();
            //mysecondmodels.Add(mysecondmodel);
            //a.mysecondmodels = mysecondmodels;
            //mysecondmodel.notlari = "2";
            //mysecondmodels.Add(mysecondmodel);
            //string injson = JsonConvert.SerializeObject(a);
            //mymodel notjson = JsonConvert.DeserializeObject<mymodel>(injson);

            //return JsonConvert.SerializeObject(a);


        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpGet]
        
        public string Post(model item)
        {
            mymodel a = new mymodel();
           

                User user = new User();
            user.Name = item.value;
            user.Surname = item.value + "1";
            a.name = "Fatih";
            a.surname = new  string [] { "Cankurtaran", "Cankurtaran2" };
            a.age = "21";
            using (FoodInfoServiceContext foodInfoServiceContext = new FoodInfoServiceContext())
            {
                foodInfoServiceContext.User.Add(user);
                foodInfoServiceContext.SaveChanges();
            }
            mysecondmodel mysecondmodel = new mysecondmodel();
            mysecondmodel.notlari = "1";
            List<mysecondmodel> mysecondmodels = new List<mysecondmodel>();
            mysecondmodels.Add(mysecondmodel);
            a.mysecondmodels = mysecondmodels; 
            mysecondmodel.notlari = "2";
            mysecondmodels.Add(mysecondmodel);

            var myitem = item;

            return JsonConvert.SerializeObject(myitem);
        }
        public class mymodel
        {

            public string name;
            public string []  surname;
            public string age;

            public List<mysecondmodel> mysecondmodels;
            
        };
        public class model
        {
            public string value;
        }
        public class mysecondmodel
        {
          public string notlari ;  
        };

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
