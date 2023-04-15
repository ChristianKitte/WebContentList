using Microsoft.EntityFrameworkCore;

namespace WebContentList.Data;

public class SubjectsService
{
    private readonly ApplicationDbContext _context;
    private int currentDefaultId;

    public SubjectsService(ApplicationDbContext context)
    {
        _context = context;
        AdjustCommonGroup();
    }

    public void AdjustCommonGroup(string name = "ohne Zuordnung")
    {
        Subject curSubject = _context.Subject.SingleOrDefault(subject => subject.isDefault == true);

        if (curSubject == null)
        {
            curSubject = new Subject()
                { Description = "ohne Zuordnung", isDefault = true, Name = name };

            _context.Subject.Add(curSubject);
        }
        else
        {
            curSubject.Name = name;
        }

        _context.SaveChanges();

        currentDefaultId = curSubject.SubjectId;
    }

    public async Task<List<Subject>> GetSubjectList()
    {
        return await _context.Subject.ToListAsync();
    }

    public async Task<Subject> GetSubjectById(int SubjectId)
    {
        return await _context.Subject.SingleOrDefaultAsync(subject => subject.SubjectId == SubjectId);
    }

    public async Task<bool> UpdateSubject(Subject subject)
    {
        _context.Subject.Update(subject);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddSubject(Subject subject)
    {
        await _context.Subject.AddAsync(subject);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteSubject(Subject subject)
    {
        if (subject.isDefault)
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
            _content = await _context.Content.Where(video => video.SubjectId == subject.SubjectId)
                .ToListAsync<Content>();

            foreach (var item in _content)
            {
                item.SubjectId = currentDefaultId;
            }

            // Gruppe löschen
            _context.Subject.Remove(subject);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}