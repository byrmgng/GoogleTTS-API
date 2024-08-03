using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeechAPI.Persistence.Contexts;

namespace TextToSpeechAPI.Persistence
{
    public class DesingTimeDbContextFactory : IDesignTimeDbContextFactory<TextToSpeechAPIDbContext>
    {
        public TextToSpeechAPIDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<TextToSpeechAPIDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseSqlServer(Configurations.ConnectionString);
            return new TextToSpeechAPIDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
