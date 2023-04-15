using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace WebContentList.Data;

public class ContentsService
{
    private readonly ApplicationDbContext _context;

    public ContentsService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Content>> GetContentList()
    {
        List<Content> _content = new List<Content>();
        _content = await _context.Content.ToListAsync<Content>();

        return _content;
    }

    public async Task<List<Content>> GetContentListBySubject(int SubjectId)
    {
        List<Content> _content = new List<Content>();
        _content = await _context.Content.Where(video => video.SubjectId == SubjectId).ToListAsync<Content>();

        return _content;
    }

    /*
    public Task<List<YouTubeVideo>> GetVideoList2()
    {
        List<YouTubeVideo> _videos = new List<YouTubeVideo>();

        _videos.Add(
            new YouTubeVideo()
            {
                gr = 1,
                Url = "https://www.youtube.com/embed/videoseries?list=PLimEpCj86HxfiIB3QMHdFr4012-z6Eqkg",
                isPlaylist = true,
                Name = "Der Name",
                Description = "Die Beschreibung",
            }
        );

        _videos.Add(
            new YouTubeVideo()
            {
                Gruppe = 1,
                Url = "https://www.produktundmarkt.de",
                isPlaylist = true,
                Name = "Der Name",
                Description = "Die Beschreibung",
            }
        );

        _videos.Add(
            new YouTubeVideo()
            {
                Gruppe = 1,
                Url = "https://www.youtube.com/embed/videoseries?list=PLimEpCj86HxfiIB3QMHdFr4012-z6Eqkg",
                isPlaylist = true,
                Name = "Der Name",
                Description = "Die Beschreibung",
            }
        );

        _videos.Add(
            new YouTubeVideo()
            {
                Gruppe = 1,
                Url = "https://www.youtube.com/embed/videoseries?list=PLimEpCj86HxfiIB3QMHdFr4012-z6Eqkg",
                isPlaylist = true,
                Name = "Der Name",
                Description = "Die Beschreibung",
            }
        );

        _videos.Add(
            new YouTubeVideo()
            {
                Gruppe = 1,
                Url = "https://www.youtube.com/embed/JeLnR_ObHd8",
                isPlaylist = false,
                Name = "Der Name",
                Description = "Die Beschreibung",
            }
        );
        return Task.FromResult(_videos);
    }
    */

    public async Task<Content> GetContentById(int id)
    {
        return await _context.Content.SingleOrDefaultAsync(content => content.ContentId == id);
    }

    [Authorize]
    public async Task<bool> UpdateContent(Content content)
    {
        _context.Content.Update(content);
        await _context.SaveChangesAsync();

        return true;
    }

    [Authorize]
    public async Task<bool> AddContent(Content content)
    {
        await _context.Content.AddAsync(content);
        await _context.SaveChangesAsync();

        return true;
    }

    [Authorize]
    public async Task<bool> DeleteContent(Content content)
    {
        _context.Content.Remove(content);
        await _context.SaveChangesAsync();

        return true;
    }
}