namespace Template.IBusiness
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Template.BusinessObject;

    public interface IPersonBusiness : IBaseBusiness<Person>
    {
        Task<string> Connection(string authorizationHeader);

        Task<KeyValuePair<bool, Person>> CreateOrUpdate(Person personToCreateOrUpdate);

        Task<Person> Delete(int id);

        Task<Person> Get(int id);

        Task<List<Person>> List();
    }
}