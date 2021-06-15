namespace Template.Business
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Template.BusinessObject;
    using Template.Entities;
    using Template.IBusiness;
    using Template.IDataAccess;

    public class PersonBusiness : IPersonBusiness
    {
        private readonly IPersonDataAccess dataAccess;

        public PersonBusiness(IPersonDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public Task<string> Connection(string authorizationHeader)
        {
            throw new NotImplementedException();
        }

        public async Task<KeyValuePair<bool, Person>> CreateOrUpdate(Person personToCreateOrUpdate)
        {
            if (personToCreateOrUpdate != null)
            {
                PersonEntity entity = personToCreateOrUpdate.CreateEntity();
                if (personToCreateOrUpdate.ValidationService.Validate(entity))
                {
                    if (entity.Id == default)
                    {
                        entity = await this.dataAccess.Create(entity);
                        personToCreateOrUpdate = new Person(entity);

                        return new KeyValuePair<bool, Person>(true, personToCreateOrUpdate);
                    }
                    else
                    {
                        entity = await this.dataAccess.Update(entity, entity.Id);
                        personToCreateOrUpdate = new Person(entity);

                        return new KeyValuePair<bool, Person>(true, personToCreateOrUpdate);
                    }
                }
            }

            return new KeyValuePair<bool, Person>(false, personToCreateOrUpdate);
        }

        public async Task<Person> Delete(int id)
        {
            if (id != default)
            {
                PersonEntity entity = await this.dataAccess.Find(id);

                if (entity != null)
                {
                    await this.dataAccess.Delete(id);
                    return new Person(entity);
                }
            }

            return null;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<Person> Get(int id)
        {
            if (id != default)
            {
                PersonEntity entity = await this.dataAccess.Find(id);
                if (entity != null)
                {
                    return new Person(entity);
                }
            }

            return null;
        }

        public async Task<List<Person>> List()
        {
            List<Person> persons = new List<Person>();
            List<PersonEntity> personEntities = await this.dataAccess.List();
            foreach (PersonEntity personEntity in personEntities)
            {
                persons.Add(new Person(personEntity));
            }

            return persons;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.dataAccess?.Dispose();
            }
        }
    }
}