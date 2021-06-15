namespace Template.DataAccess
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Template.Entities;
    using Template.IDataAccess;

    public class PersonDataAccess : BaseDataAccess<PersonEntity>, IPersonDataAccess
    {
        public PersonDataAccess(TemplateContext context) : base(context)
        {
        }

        public async Task<PersonEntity> GetFromEmailAndPassword(string email, string password)
        {
            return await this.Context.Persons.Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
        }
    }
}