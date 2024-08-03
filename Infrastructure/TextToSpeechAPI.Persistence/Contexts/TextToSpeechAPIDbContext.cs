using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeechAPI.Domain.Entities;
using TextToSpeechAPI.Domain.Entities.Commons;
using TextToSpeechAPI.Domain.Entities.Identities;


namespace TextToSpeechAPI.Persistence.Contexts
{
    public class TextToSpeechAPIDbContext:IdentityDbContext<AppUser>
    {
        public TextToSpeechAPIDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageUsage> PackageUsages { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entriesDatas = ChangeTracker.Entries<BaseEntity>();
            foreach (var entry in entriesDatas)
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedDate = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
