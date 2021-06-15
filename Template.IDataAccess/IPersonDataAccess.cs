namespace Template.IDataAccess
{
    using System.Threading.Tasks;

    using Template.Entities;

    public interface IPersonDataAccess : IBaseDataAccess<PersonEntity>
    {
        Task<PersonEntity> GetFromEmailAndPassword(string email, string password);
    }
}