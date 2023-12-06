using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieBase.Common;
using MovieBase.Data;
using System.Net.Mime;

namespace MovieBase.Api.Controllers;
[ApiController]
[Route("[controller]")]
[EnableCors("CorsPolicy")]
//[Authorize]
public class MoviesController : ControllerBase
{
    private readonly ILogger<MoviesController> _logger;
    private readonly MovieContext _context;
    private readonly IMapper _mapper;

    public MoviesController(ILogger<MoviesController> logger, MovieContext context, IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("[action]/{pageSize}/{pageNo}", Name = "MovieList")]
    [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
    [AllowAnonymous]
    public async Task<IActionResult> List(int pageSize, int pageNo)
    {
        var count = await _context.Movies.CountAsync();
        var data = (await _context
            .Movies
            .Skip(pageNo * pageSize)
            .Take(pageSize)
            .ToArrayAsync())
            .Select(_mapper.Map<Movie,MovieDTO>)
            .ToList();

        var result = new ResultPage<MovieDTO> { Data = data, Page = pageNo, PageSize = pageSize, TotalCount = count };
        return Ok(result);
    }

    [HttpGet("{id}", Name = "Movie")]
    [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
    public async Task<IActionResult> One(int id)
    {
        await Task.Delay(1500);
        var data = await _context.Movies.FindAsync(id);
        return data == null
            ? NotFound()
            : Ok(data);
    }
    //public async Task<IActionResult> One(string id)
    //{
    //    if (int.TryParse(id, out var realId))
    //    {
    //        var data = await _context.Movies.FindAsync(realId);
    //        return data == null
    //            ? NotFound()
    //            : Ok(data);
    //    }
    //    return BadRequest("An int is expected");
    //}

    [HttpPost(Name = "NewMovie")]
    public async Task<IActionResult> Add([FromBody] Movie newMovie)
    {
        try
        {
            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Movie", new { id = newMovie.Id }, newMovie);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Add failed");
            return BadRequest();
        }
    }

    [HttpDelete("{id}", Name ="Delete Movie")]
    public async Task<IActionResult> Delete(int id)
    {
        if(await _context.Movies.FindAsync(id) is Movie theOne)
        { 
            _context.Movies.Remove(theOne);
            await _context.SaveChangesAsync();
            return Ok();
        }

        return NotFound();
    }

    [HttpPut(Name ="Update Movie")]
    public async Task<IActionResult> Update([FromBody] Movie movie)
    {
        if (await _context.Movies.FindAsync(movie.Id) is Movie theOne)
        {
            _context.Entry(theOne).CurrentValues.SetValues(movie);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        return NotFound();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<Movie> patchDoc)
    {
        try
        {
            var movie = await _context.Movies.FindAsync(id);
            patchDoc.ApplyTo(movie!, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _context.SaveChangesAsync();
            return new ObjectResult(movie);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}

public class Result<T>
{
    public bool Succeeded { get; set; }
    public T? Payload { get; set; }
    public string? Message { get; set; }
}
