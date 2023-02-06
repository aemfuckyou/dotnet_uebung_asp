using System.Net.Mime;
using System.Text.Json;
using AutoMapper;
using Blog.Models;
using Blog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[Route("api/comments")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public class CommentController : ControllerBase
{
    private readonly IBlogRepository repo;
    private readonly IMapper mapper;

    public CommentController(IBlogRepository repo, IMapper mapper)
    {
        this.repo = repo;
        this.mapper = mapper;
    }

    [Authorize(Policy = "IsAuthenticated")]
    [HttpGet("{id}", Name ="GetComment")]
    public async Task<IActionResult> GetComments(int id)
    {
        var entry = await repo.GetBlogEntryAsync(id, true);

        if (entry == null) return NotFound();

        return Ok(mapper.Map<ICollection<CommentDto>>(entry.Comments));
    }

    [Authorize(Policy = "IsAuthenticated")]
    [HttpPost("{id}")]
    public async Task<IActionResult> CreateComment(int id, CommentForCreationDto creationDto)
    {
        creationDto.PublishingTime = DateTime.UtcNow;
        var comment = mapper.Map<Entities.Comment>(creationDto);
        
        if (!await repo.EntryExists(id))
        {
            return NotFound();
        }

        await repo.AddCommentAsync(id, comment);
        await repo.SaveChangesAsync();

        var commentDto = mapper.Map<Models.CommentDto>(comment);

        return CreatedAtRoute("GetComment",
            new{
                id = comment.Id
            }, 
            commentDto
            );
    }

    [Authorize(Policy = "IsAuthenticated")]
    [HttpPut]
    public async Task<ActionResult<CommentDto>> updateComment(int entryid, int commentid, CommentForUpdateDto updatedEntry)
    {
        //somehow check if user == author...
        if(!await repo.EntryExists(entryid)){
            return NotFound();
        }

        if(!await repo.CommentExists(commentid)){
            return NotFound();
        }

        var commententity = await repo.GetComment(entryid, commentid);
        if(commententity == null) 
            return NotFound();

        mapper.Map(updatedEntry, commententity);
        await repo.SaveChangesAsync();

        return NoContent();
    }



}
