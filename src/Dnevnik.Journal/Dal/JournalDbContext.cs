using Dnevnik.Journal.Dal.Models;

using Microsoft.EntityFrameworkCore;

namespace Dnevnik.Journal.Dal;

public class JournalDbContext(DbContextOptions<JournalDbContext> options) : DbContext(options)
{
    public DbSet<UserMarks> UserMarks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserMarks>(entity =>
        {
            entity.ToTable("user_marks");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.UserId);
            entity.Property(e => e.LessonId);
            entity.Property(e => e.Mark);
            entity.Property(e => e.Comment);
            entity.Property(e => e.Subject);
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamptz");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamptz");

            entity.HasIndex(e => new { e.UserId }, "ix_user_marks_user_id");
            entity.HasIndex(e => new { e.LessonId }, "ix_user_marks_lesson_id");
            entity.HasIndex(e => new { e.Subject }, "ix_user_marks_subject");
            entity.HasIndex(e => new { e.CreatedAt }, "ix_user_marks_created_at");
        });
    }
}