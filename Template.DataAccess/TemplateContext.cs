namespace Template.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Template.DataAccess.Seed;

    public class TemplateContext : DbContext
    {
        public TemplateContext(DbContextOptions<TemplateContext> options)
            : base(options)
        {
        }

        public async Task EnsureSeedData(bool isProduction)
        {
            ContextInitializer initializer = new ContextInitializer();
            await initializer.Seed(this, isProduction);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Action> listConfiguration = new List<Action>
            {
            };

            foreach (Action action in listConfiguration)
            {
                action.Invoke();
            }
        }
    }
}