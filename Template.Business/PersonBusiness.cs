namespace Template.Business
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    using Template.BusinessObject;
    using Template.Entities;
    using Template.IBusiness;
    using Template.IDataAccess;

    public class PersonBusiness : IPersonBusiness
    {
        private readonly IConfiguration configuration;

        private readonly IPersonDataAccess dataAccess;

        public PersonBusiness(IPersonDataAccess dataAccess, IConfiguration configuration)
        {
            this.dataAccess = dataAccess;
            this.configuration = configuration;
        }

        public async Task<string> Connection(string authorizationHeader)
        {
            KeyValuePair<string, string> emailPassword = this.ReadAuthenticationHeader(authorizationHeader);

            PersonEntity entity = await this.dataAccess.GetFromEmailAndPassword(emailPassword.Key, emailPassword.Value);

            if (entity != null)
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, entity.Role.ToString()));

                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddSeconds(120),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(this.configuration["JWTSecret"])),
                        SecurityAlgorithms.HmacSha256Signature),
                };

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(securityToken);
            }

            return string.Empty;
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

        private KeyValuePair<string, string> ReadAuthenticationHeader(string authenticationHeader)
        {
            if (authenticationHeader != null && authenticationHeader.StartsWith("Basic"))
            {
                // Read and decode the basic header
                string encodedUsernamePassword = authenticationHeader["Basic ".Length..].Trim();
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));
                string[] emailPassword = usernamePassword.Split(':');

                // Check if we have login and password to avoir crash OutOfRangeException
                return new KeyValuePair<string, string>(emailPassword[0], emailPassword[1]);
            }

            return new KeyValuePair<string, string>(string.Empty, string.Empty);
        }
    }
}