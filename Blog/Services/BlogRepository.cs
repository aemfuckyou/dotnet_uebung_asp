using Blog.Entities;
using Blog.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services;

public class BlogRepository : IBlogRepository
{
    private BlogContext context;

    public BlogRepository(BlogContext context)
    {
        this.context = context;
    }
    public async Task AddCommentAsync(int blogentryId, Comment comment)
    {   
        var blogEntry = await GetBlogEntryAsync(blogentryId, false);
        if(blogEntry != null)
        {
            blogEntry.Comments.Add(comment);
        }
    }

    public async Task<bool> CommentExists(int commentId)
    {
        return await context.Comments.AnyAsync(c => c.Id == commentId);
    }

    public void DeleteComment(Comment comment)
    {
        context.Comments.Remove(comment);
    }

    public async Task<IEnumerable<BlogEntry>> GetBlogEntriesAsync()
    {
        return await context.Entries.Include(e => e.Author).OrderBy(e => e.PublishingTime).ToListAsync();
    }

    public async Task<BlogEntry> GetBlogEntryAsync(int blogentryId, bool includeComments)
    {

        if (includeComments)
            return await context.Entries.Include(e => e.Author).Include(e => e.Comments).ThenInclude(c => c.Author).Where(e => e.Id == blogentryId).SingleOrDefaultAsync();

        return await context.Entries.Include(e => e.Author).Where(e => e.Id == blogentryId).FirstOrDefaultAsync();
    }

    public async Task<Comment> GetComment(int blogentryId, int commentId)
    {
        return await context.Comments.Include(c => c.Author).Where(c => c.BlogEntryId == blogentryId && c.Id == commentId).FirstOrDefaultAsync();
    }

    public async Task<(IEnumerable<BlogEntry>, PaginationMetadata)> 
        GetEntriesAsync(string? title, string? queryString, int pageNumber, int pageSize)
    {
        var collection = context.Entries.Include(e => e.Author) as IQueryable<BlogEntry>;

        if (!string.IsNullOrWhiteSpace(title))
        {
            collection = collection.Where(e => e.Title == title);
        }

        if (!string.IsNullOrWhiteSpace(queryString))
        {
            collection = collection.Where(e => e.Title.Contains(queryString));
        }

        var pageMetadata = new PaginationMetadata(
            await collection.CountAsync(),
            pageSize,
            pageNumber);

        return (await collection.OrderBy(e => e.PublishingTime)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(),
            pageMetadata);
    }
    public async Task<IEnumerable<Comment>> GetCommentsForBlogEntry(int blogEntryId)
    {
        return await context.Comments.Include(e => e.Author).Where(c => c.BlogEntryId == blogEntryId).ToListAsync();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await context.SaveChangesAsync()) >= 0;
    }

    public async Task AddBlogEntry(BlogEntry entry)
    {
        context.Add(entry);
    }

    public async Task<bool> EntryExists(int blogentryId)
    {
        return await context.Entries.AnyAsync(e => e.Id == blogentryId);
    }
}

