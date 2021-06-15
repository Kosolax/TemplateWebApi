namespace Template.DataAccess
{
    using Template.Entities;
    using Template.IDataAccess;

    public class PersonDataAccess : BaseDataAccess<PersonEntity>, IPersonDataAccess
    {
        public PersonDataAccess(TemplateContext context) : base(context)
        {
        }
    }
}