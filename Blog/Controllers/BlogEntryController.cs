using System.Net.Mime;
using System.Text.Json;
using AutoMapper;
using Blog.Models;
using Blog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[Route("api/entries")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public class BlogEntryContoller : ControllerBase
{
    private readonly IBlogRepository repo;
    private readonly IMapper mapper;

    public BlogEntryContoller(IBlogRepository repo, IMapper mapper)
    {
        this.repo = repo;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BlogEntryDto>>> 
        GetEntries(string? title, string? queryString, int pageNumber = 1, int pageSize = 10)
    {
        var (entries, paginationMetadata) = await repo.GetEntriesAsync(title, queryString, pageNumber, pageSize);

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

        return Ok(mapper.Map<IEnumerable<BlogEntryDto>>(entries));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBlogEntry(int id)
    {

        var entry = await repo.GetBlogEntryAsync(id, false);

        if (entry == null) return NotFound();


        return Ok(mapper.Map<BlogEntryWithoutCommentsDto>(entry));
    }

    [Authorize(Policy = "IsAuthenticated")]
    [HttpGet("wc/{id}", Name = "GetBlogEntryWithComments")]
    public async Task<IActionResult> GetBlogEntryWithComments(int id)
    {

        var entry = await repo.GetBlogEntryAsync(id, true);

        if (entry == null) return NotFound();

        return Ok(mapper.Map<BlogEntryDto>(entry));
    }

    [Authorize(Policy = "IsAuthenticated")]
    [HttpPost]
    public async Task<ActionResult<BlogEntryDto>> createBlogEntry(BlogEntryForCreationDto newentry)
    {
        newentry.PublishingTime = DateTime.UtcNow;
        var entry = mapper.Map<Entities.BlogEntry>(newentry);
        
        await repo.AddBlogEntry(entry);
        await repo.SaveChangesAsync();

        var entrydto = mapper.Map<Models.BlogEntryDto>(entry);

        return CreatedAtRoute("GetBlogEntryWithComments",
            new{
                id = entry.Id
            }, 
            entrydto
            );
    }


    [Authorize(Policy = "IsAuthenticated")]
    [HttpPut]
    public async Task<ActionResult<BlogEntryDto>> updateBlogEntry(int entryid, BlogEntryForUpdateDto updatedEntry)
    {
        //somehow check if user == author...
        var entryentity = await repo.GetBlogEntryAsync(entryid, true);
        if(entryentity == null) 
            return NotFound();

        mapper.Map(updatedEntry, entryentity);
        await repo.SaveChangesAsync();

        return NoContent();
    }

}
