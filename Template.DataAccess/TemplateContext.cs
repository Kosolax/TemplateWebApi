namespace Template.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Template.DataAccess.Configuration;
    using Template.DataAccess.Seed;
    using Template.Entities;

    public class TemplateContext : DbContext
    {
        public TemplateContext(DbContextOptions<TemplateContext> options)
            : base(options)
        {
        }

        public DbSet<PersonEntity> Persons { get; set; }

        public async Task EnsureSeedData(bool isProduction)
        {
            ContextInitializer initializer = new ContextInitializer();
            await initializer.Seed(this, isProduction);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Action> listConfiguration = new List<Action>
            {
                new PersonConfiguration(modelBuilder).Execute,
            };

            foreach (Action action in listConfiguration)
            {
                action.Invoke();
            }
        }
    }
}