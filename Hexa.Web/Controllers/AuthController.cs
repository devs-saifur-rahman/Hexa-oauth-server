using Hexa.Data.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hexa.Web.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // GET: api/<AuthController>/Authoize
        [HttpPost("Authorize")]
        public IActionResult Authorize([FromBody] AuthRequest req)
        {
            return RedirectToAction(actionName:"Authorize", controllerName:"Account");
        }


        [HttpPost("api/v1/auth/token/access")]
        public void AccessToken([FromBody] string value)
        {

        }

        [HttpPost("api/v1/auth/token/refresh")]
        public void RefreshToken([FromBody] string value)
        {

        }


        //Scaffolds
        /*
        // GET: api/<AuthController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        // GET api/<AuthController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AuthController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
