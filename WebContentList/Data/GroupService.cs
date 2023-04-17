using Microsoft.EntityFrameworkCore;

namespace WebContentList.Data;

public class GroupService
{
    private readonly ApplicationDbContext _context;
    private int currentDefaultId;

    public GroupService(ApplicationDbContext context)
    {
        _context = context;
        AdjustCommonGroup();
    }

    public void AdjustCommonGroup(string name = "ohne Zuordnung")
    {
        Group curGroup = _context.Group.SingleOrDefault(group => group.isDefault == true);

        if (curGroup == null)
        {
            curGroup = new Group()
            { Description = "ohne Zuordnung", isDefault = true, Name = name };

            _context.Group.Add(curGroup);
        }
        else
        {
            curGroup.Name = name;
        }

        _context.SaveChanges();

        currentDefaultId = curGroup.SubjectId;
    }

    public async Task<List<Group>> GetGroupList()
    {
        return await _context.Group.ToListAsync();
    }

    public async Task<Group> GetGroupById(int groupId)
    {
        return await _context.Group.SingleOrDefaultAsync(group => group.SubjectId == groupId);
    }

    public async Task<bool> UpdateGroup(Group group)
    {
        _context.Group.Update(group);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddGroup(Group group)
    {
        await _context.Group.AddAsync(group);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteSubject(Group group)
    {
        if (group.isDefault)
        {
            return true;
        }
        else
        {
            // default Subject ausfindig machen
            /*Subject defaultSubject = _context.Subject.SingleOrDefault(subject => subject.isDefault == true);
            int defaultSubjectId = defaultSubject.SubjectId;*/

            // Inhalte mit der zur löschenden Gruppe auf die default Gruppe umlenken
            List<Content> _content = new List<Content>();
            _content = await _context.Content.Where(video => video.SubjectId == group.SubjectId)
                .ToListAsync<Content>();

            foreach (var item in _content)
            {
                item.SubjectId = currentDefaultId;
            }

            // Gruppe löschen
            _context.Group.Remove(group);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}