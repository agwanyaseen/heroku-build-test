using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private string DirName = "Directory";
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var directory = Path.Combine(Directory.GetCurrentDirectory(),DirName);
            var infor = Directory.GetFiles(directory);
            return Ok(infor);
        }

        
        [HttpPost]
        public IActionResult Post(IFormFile file)
        {
            IActionResult result;

            try
            {
                var directory = Path.Combine(Directory.GetCurrentDirectory(), DirName);
                var dir = Directory.Exists(directory);
                if (!dir)
                {
                    Directory.CreateDirectory(DirName);
                }
                var path = Path.Combine(directory, file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

               result = Ok("Copied");
            }catch(Exception e)
            {
                result = BadRequest(e.Message); 
            }

            return result;
        }
    }
}
