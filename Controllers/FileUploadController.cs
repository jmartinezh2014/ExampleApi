using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using ExampleApi.Models;



namespace ExampleApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
      

        [HttpGet]
      
        public List<FileDate> ListFiles(){
             
             string ruta = @"public";
             DirectoryInfo di = new DirectoryInfo(ruta);
        
             List <FileDate> files = new List<FileDate>();
             foreach (var fi in di.GetFiles())
             { 
                    //   new FileDate() {name = fi.Name};
                    files.Add(new FileDate{name = fi.Name});
                 
             }
                 return files;  
             
        }


        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "public", file.FileName);
            try
            {
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                //return StatusCode(StatusCodes.Status201Created);
                return Ok(new { mensaje = "OK" });
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }
    };
};