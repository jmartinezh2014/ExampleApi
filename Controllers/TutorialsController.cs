using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using ExampleApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;


namespace ExampleApi.Controllers
{
[Route("api/[controller]")]
[ApiController]
public class TutorialsController: ControllerBase
{
private readonly TodoContext _context;

public TutorialsController(TodoContext context)
{
 _context = context;
}

[HttpGet]
public async Task<ActionResult<IEnumerable<Tutorials>>> GetTutorials()
 {
   return await _context.Tutorials.ToListAsync();
    
 }

public async Task<ActionResult<Tutorials>> GetTutorials(int id)
{
    var tutorials = await _context.Tutorials.FindAsync(id);
    if(tutorials == null)
    {
         return NotFound();
    }
    return tutorials;
}

[HttpPost]
public async Task<ActionResult> PostTutorials(Tutorials tutorials)
{
 _context.Tutorials.Add(tutorials);
 await _context.SaveChangesAsync();
 return CreatedAtAction("GetTutorials",new {id=tutorials.Id}, tutorials);
}


[HttpPut("id")]
public async Task<IActionResult> PutTutorials(int id, Tutorials tutorials)
{
    if(id!= tutorials.Id)
    {
        return BadRequest();
    }
    _context.Entry(tutorials).State = EntityState.Modified;
    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if(!TutorialsExists(id)){
            return NotFound();
        }
        else
        {
            throw;
        }
         // TODO
    }
    return NoContent();
    
}


    
}
}


