using Microsoft.EntityFrameworkCore;

public class NotesContext : DbContext
{
    public DbSet<Note>? Notes { get; set; }

    public string DbPath { get; }

    public NotesContext()
    {
        var path = "./Data";
        DbPath = Path.Join(path, "db.sqlite");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}"); 
}

public class Note
{
    public int NoteId { get; set; }
    public string? Text { get; set; }
}