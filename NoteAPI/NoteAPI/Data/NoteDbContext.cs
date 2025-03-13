using Microsoft.EntityFrameworkCore;
using NoteAPI.Models.Entities;

namespace NoteAPI.Data
{
    public class NoteDbContext : DbContext
    {
        public NoteDbContext(DbContextOptions<NoteDbContext> options) : base(options) 
        {

        }

        public DbSet<Note> Notes { get; set; }
    }
}
