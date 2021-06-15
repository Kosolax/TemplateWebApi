namespace Template.DataAccess.Configuration
{
    using Microsoft.EntityFrameworkCore;

    using Template.Entities;

    public class PersonConfiguration : ConfigurationManagement<PersonEntity>
    {
        public PersonConfiguration(ModelBuilder modelBuilder)
           : base(modelBuilder)
        {
        }

        protected override void ProcessConstraint()
        {
            this.EntityConfiguration.HasKey(x => x.Id);
            this.EntityConfiguration.Property(x => x.Password).IsRequired(true).HasColumnType("varchar(100)");
            this.EntityConfiguration.Property(x => x.Email).IsRequired(true).HasColumnType("varchar(100)");
            this.EntityConfiguration.Property(x => x.Role).IsRequired(true).HasColumnType("int");
        }

        protected override void ProcessForeignKey()
        {
        }

        protected override void ProcessIndex()
        {
        }

        protected override void ProcessTable()
        {
            this.EntityConfiguration.ToTable("Persons");
        }
    }
}