namespace Template.DataAccess.Seed
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Template.Entities;
    using Template.Entities.Enum;

    public class PersonSeed : IContextSeed
    {
        public PersonSeed(TemplateContext context)
        {
            this.Context = context;
        }

        public TemplateContext Context { get; set; }

        public async Task Execute(bool isProduction)
        {
            if (!this.Context.Persons.Any() && !isProduction)
            {
                List<PersonEntity> persons = new List<PersonEntity>
                {
                    new PersonEntity
                    {
                        Password = "Admin",
                        Email = "Admin",
                        Role = RoleType.Admin,
                    },
                    new PersonEntity
                    {
                        Password = "Basic",
                        Email = "Basic",
                        Role = RoleType.Basic,
                    },
                };

                await this.Context.Persons.AddRangeAsync(persons);
                await this.Context.SaveChangesAsync();
            }
        }
    }
}