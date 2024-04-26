using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MediaCenter.Models;

public partial class MediaCenterDbContext : DbContext
{
    public virtual DbSet<Movie> Movie { get; set; }
    public MediaCenterDbContext()
    {
    }

    public MediaCenterDbContext(DbContextOptions<MediaCenterDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
         modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("Movie");
                entity.HasKey(a => a.Id);
            });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
