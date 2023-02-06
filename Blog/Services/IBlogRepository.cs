using Blog.Entities;

namespace Blog.Services;

public interface IBlogRepository
{
    Task<IEnumerable<BlogEntry>> GetBlogEntriesAsync();
    Task<BlogEntry> GetBlogEntryAsync(int blogentryId, bool includeComments);
    Task<IEnumerable<Comment>> GetCommentsForBlogEntry(int blogEntryId);
    Task<Comment> GetComment(int blogentryId, int commentId);
    Task<bool> CommentExists(int blogentryId);
    Task<bool> EntryExists(int entryId);
    Task AddCommentAsync(int blogentryId, Comment comment);
    Task<bool> SaveChangesAsync();
    void DeleteComment(Comment comment);
    Task<(IEnumerable<BlogEntry>, PaginationMetadata)> GetEntriesAsync(string? name, string? queryString, int pageNumber, int pageSize);
    Task AddBlogEntry(BlogEntry entry);
}
